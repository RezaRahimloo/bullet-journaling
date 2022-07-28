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
        private readonly AppDb _db;
        private readonly UserManager<AppUser> _userManager; 
        private readonly SignInManager<AppUser> _signinManager;
        public MbaController(AppDb db,
                             UserManager<AppUser> userManager,
                             SignInManager<AppUser> signinManager)
        {
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
                    .Include(mba => mba.ImportantLessons)
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLesson(Lesson lesson)
        {
            
            if(!ModelState.IsValid)
            {
                Console.WriteLine("Invalid model");
                return PartialView("_AddTodayLessonPartial", lesson);
            }

            var user = await _userManager.GetUserAsync(User);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            Mba todayMba = await _db.Mbas
                .Where(m => m.UserId == user.Id)
                .Where(m => m.Date == today)
                .Include(m => m.ImportantLessons)
                .FirstOrDefaultAsync();

            if(todayMba is null)
            {
                return BadRequest("you need to have taken an MBA class in order to add a lesson of it!");
            }
            todayMba.ImportantLessons.Add(lesson);

            _db.Update(todayMba);
            await _db.SaveChangesAsync();
            Console.WriteLine("added model");
            return RedirectToAction("Index", "Mba");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearToday()
        {
            var user = await _userManager.GetUserAsync(User);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            var deletingMba = await _db.Mbas
                .Where(m => m.UserId == user.Id)
                .FirstOrDefaultAsync(m => m.DidDo && m.Date == today);
            
            if(deletingMba is null)
            {
                return BadRequest("Item does not exist!");
            }

            _db.Mbas.Remove(deletingMba);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Mba");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLesson(Guid id)
        {
            AppUser user = await _userManager.GetUserAsync(User);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            Mba todayMba = await _db.Mbas
                .Where(m => m.UserId == user.Id)
                .Where(m => m.Date == today)
                .Include(m => m.ImportantLessons)
                .FirstOrDefaultAsync();

            if(todayMba is null)
            {
                return BadRequest("You have not taken a class today!");
            }

            var deletingLesson = todayMba.ImportantLessons.FirstOrDefault(lesson => lesson.Id == id);
            
            if(deletingLesson is null)
            {
                return NotFound("Lesson not found in todays class!");
            }

            _db.Remove(deletingLesson);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Mba");
        }
    }
}