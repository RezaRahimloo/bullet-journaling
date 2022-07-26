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
    public class SmokingController : Controller
    {
        private SmokesProvider _smokeProvider;
        private readonly AppDb _db;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly UserManager<AppUser> _userManager;
        public SmokingController(SmokesProvider smokesProvider,
                                 AppDb db,
                                 SignInManager<AppUser> signInManager,
                                 UserManager<AppUser> userManager)
        {
            _smokeProvider = smokesProvider;
            _db = db;
            _signinManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // get the user so we can find its Logs
            var user = await _userManager.GetUserAsync(User);
            if(user is not null)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                DateOnly fourMonthsAgo = today.AddMonths(-3);
                fourMonthsAgo = new(fourMonthsAgo.Year, fourMonthsAgo.Month, 1);

                var smokingList = await _db.Smokings
                    .Where(smoking => smoking.UserId == user.Id)
                    .Where(smoking => smoking.Date <= today && smoking.Date >= fourMonthsAgo)
                    .Where(smoking => smoking.DidSmoke)
                    .OrderBy(smoking => smoking.Date)
                    .ToListAsync();

                if(smokingList != null)
                {
                    return View(smokingList);
                }
            }
            return View(new List<Smoking>());
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToday(Smoking smoking)
        {
            //get user 
            var user = await _userManager.GetUserAsync(User);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            
            if(ModelState.IsValid)
            {
                if(smoking.Number > 0)
                {
                    smoking.DidSmoke = true;
                }
                else
                {
                    smoking.DidSmoke = false;
                }

                Smoking todaySmoking = await _db.Smokings
                    .Where(s => s.UserId == user.Id)
                    .FirstOrDefaultAsync(s => s.Date == today);
                
                if(todaySmoking is not null)
                {
                    todaySmoking.Number += smoking.Number;
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Smoking");
                }
                else if(smoking.Number > 0)
                {
                    await _db.AddAsync(new Smoking
                        {
                            UserId = user.Id,
                            Number = smoking.Number,
                            Date = today,
                            DidSmoke = smoking.DidSmoke
                        });
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index", "Smoking");
                }

            }
            return PartialView("_AddTodayPartial", smoking);
        }
        // [HttpPost]
        // public async Task<IActionResult> AddToday(SmokingModel smoking)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         _smokeProvider.AddToday(smoking);
        //         return RedirectToAction("Index", "Smoking");
        //     }
        //     return PartialView("_AddTodayPartial", smoking);
        // }
        [HttpPut]
        public async Task<IActionResult> AddACigar()
        {
            _smokeProvider.AddOne();
            return RedirectToAction("Index", "Smoking");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteToday()
        {
            _smokeProvider.DeleteToday();
            return RedirectToAction("Index", "Smoking");
        }
       
    }
}