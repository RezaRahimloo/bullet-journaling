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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user is not null)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                DateOnly fourMonthsAgo = today.AddMonths(-3);
                var dayLogs = await _db.DayLogs
                    .Where(dayLogs => dayLogs.UserId == user.Id)
                    .Where(daylog => daylog.day >= fourMonthsAgo && daylog.day <= today)
                    .Include(dayLogs => dayLogs.Logs)
                    .OrderBy(log => log.day)
                    .ToListAsync();
                if(dayLogs is null)
                {
                    return View(new List<DayLog>());
                }
                return View(dayLogs);
            }
            return View(new List<DayLog>());
            
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

                

                var user = await _userManager.GetUserAsync(User);

                DateOnly today = DateOnly.FromDateTime(DateTime.Now);

                var todayDayLog = await _db.DayLogs
                    .Where(dayLog => dayLog.UserId == user.Id)
                    .Include(dayLog => dayLog.Logs)
                    .FirstOrDefaultAsync(dayLog => dayLog.day == today);
                
                if(todayDayLog == null)
                {
                    var logs = new List<Log>();
                    logs.Add(log);
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
                else
                {
                    todayDayLog.HasLog = true;
                    todayDayLog.Logs.Add(log);
                    _db.Update(todayDayLog);
                }

                await _db.SaveChangesAsync();
            }
            return PartialView("_AddTodayPartial", model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLog(Guid logId)
        {
            
            var user = await _userManager.GetUserAsync(User);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly fourMonthsAgo = today.AddMonths(-3);
            fourMonthsAgo = new(fourMonthsAgo.Year, fourMonthsAgo.Month, 1);

            DayLog todayDayLog = await _db.DayLogs
                .Where(daylog => daylog.UserId == user.Id)
                .Where(daylog => daylog.day == today)
                .Include(dayLog => dayLog.Logs)
                .FirstOrDefaultAsync();

            if(todayDayLog is null)
            {
                return BadRequest("Bad Request: the log doesn't exist!");
            }

            Log deletingLog = todayDayLog.Logs.FirstOrDefault(log => log.Id == logId);
            if(deletingLog is null)
            {
                return BadRequest("Bad Request: the log doesn't exist!");
            }
            else
            {
                todayDayLog.Logs.Remove(deletingLog);
                if(todayDayLog.Logs.Count == 0)
                {
                    todayDayLog.HasLog = false;
                }
                await _db.SaveChangesAsync();
                
                return RedirectToAction("Index", "Log");
                
            }
            
        }
    }
}