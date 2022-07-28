using BulletJournaling.AppMVC.Data;
using BulletJournaling.AppMVC.Data.DatabaseModels;
using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class MbaController : Controller
    {
        private readonly MbaProvider _mbaProvider;
        private readonly AppDb _db;
        private readonly UserManager<AppUser> _userManager; 
        private readonly SignInManager<AppUser> _signinManager;
        public MbaController(MbaProvider mbaProvider,
                             AppDb db,
                             UserManager<AppUser> userManager,
                             SignInManager<AppUser> signinManager)
        {
            _mbaProvider = mbaProvider;
            _db = db;
            _userManager = userManager;
            _signinManager = signinManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if(_signinManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                DateOnly threeMonthsAgo = today.AddMonths(-3);
                threeMonthsAgo = new(threeMonthsAgo.Year, threeMonthsAgo.Month, 1);

                List<Mba> mbas = await _db.Mbas
                    .Where(mba => mba.UserId == user.Id)
                    .Where(mba => mba.DidDo)
                    .Where(mba => mba.Date >= threeMonthsAgo)
                    .ToListAsync();

                if(mbas is null)
                {
                    return View(new List<Mba>());
                }

                return View(mbas);
            }

            return View(new List<Mba>());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMba(Mba mba)
        {
            mba.DidDo = false;

            if(!ModelState.IsValid)
            {
                return PartialView("_AddTodayMbaPartial", mba);
            }
            var user = await _userManager.GetUserAsync(User);
            
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            mba.DidDo = true;

            Mba todayMba = await _db.Mbas
                .Where(m => m.UserId == user.Id)
                .FirstOrDefaultAsync(m => m.Date == today);

            if(todayMba is null)
            {
                await _db.AddAsync(new Mba
                {
                    UserId = user.Id,
                    DidDo = true,
                    Type = mba.Type,
                    Part = mba.Part,
                    Date = today
                });
                await _db.SaveChangesAsync();
            }
            else
            {
                todayMba.DidDo = true;
                todayMba.Part = todayMba.Part;
                todayMba.Type = todayMba.Type;
                _db.Update(todayMba);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Mba");
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson(LessonModel lessonModel)
        {
            //lesson.Id = -1;
            Console.WriteLine(lessonModel.Id);
            Console.WriteLine(lessonModel.Lesson);
            if(!ModelState.IsValid)
            {
                return PartialView("_AddTodayLessonPartial");
            }
            if(!_mbaProvider.AddLesson(lessonModel))
            {
                return BadRequest("Lesson cannot be added!");
            }
            return RedirectToAction("Index", "Mba");
        }

        [HttpDelete]
        public async Task<IActionResult> ClearToday()
        {
            if(!_mbaProvider.ClearToday())
            {
                return BadRequest("Today has no MBA Class record!");
            }
            return RedirectToAction("Index", "Mba");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            if(!_mbaProvider.DeleteLessonToday(id))
            {
                return BadRequest("this item does not exist!");
            }
            return RedirectToAction("Index", "Mba");
        }
    }
}