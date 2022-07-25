using BulletJournaling.AppMVC.Data;
using BulletJournaling.AppMVC.Models;
using BulletJournaling.AppMVC.Data.DatabaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestServices;
using Microsoft.AspNetCore.Identity;

namespace BulletJournaling.AppMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly LogProvider _logProvider;
        private readonly AppDb _db;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly UserManager<AppUser> _userManager;
        public LogController(LogProvider logProvider, 
                             AppDb db, 
                             SignInManager<AppUser> signinManager, 
                             UserManager<AppUser> userManager)
        {
            _logProvider = logProvider;
            _db = db;
            _signinManager = signinManager;
            _userManager = userManager;
        }
        // public IActionResult Index()
        // {
        //     return View(_logProvider.GetDayLogs());
        // }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            
            var dayLogs = await _db.DayLogs
                .Include(dayLogs => dayLogs.Logs)
                .OrderBy(log => log.day)
                .ToListAsync();
            return View(dayLogs);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToday(LogModel model)
        {
            if(ModelState.IsValid)
            {
                var log = new Log
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    DurationMinutes = model.DurationMinutes
                };

                var logs = new List<Log>();
                logs.Add(log);

                var user = await _userManager.GetUserAsync(User);

                await _db.DayLogs.AddAsync(new DayLog
                {
                    Id= Guid.NewGuid(),
                    UserId = user.Id,
                    HasLog = true,
                    day = DateOnly.FromDateTime(DateTime.Now),
                    Logs = logs
                });
                await _db.SaveChangesAsync();
            }
            return PartialView("_AddTodayPartial", model);
        }
        //[AllowAnonymous]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        // public async Task<IActionResult> AddToday(LogModel model)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         _logProvider.AddToday(model);
        //         return RedirectToAction("Index", "Log");
        //     }
        //     return PartialView("_AddTodayPartial", model);
        // }
        [HttpPost]
        public async Task<IActionResult> DeleteLog(int logId)
        {
            if(_logProvider.DeleteLog(logId))
            {
                return RedirectToAction("Index", "Log");
            }
            return BadRequest("Bad Request: the log doesn't exist!");
        }
    }
}