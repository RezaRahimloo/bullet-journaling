using BulletJournaling.AppMVC.Data;
using BulletJournaling.AppMVC.Models;
using BulletJournaling.AppMVC.Data.DatabaseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly LogProvider _logProvider;
        private readonly AppDb _db;
        public LogController(LogProvider logProvider, AppDb db)
        {
            _logProvider = logProvider;
            _db = db;
        }
        // public IActionResult Index()
        // {
        //     return View(_logProvider.GetDayLogs());
        // }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            var dayLogs = await _db.DayLogs
                .Include(dayLogs => dayLogs.Logs)
                .ToListAsync();
            return View(dayLogs);
        }
        //[AllowAnonymous]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToday(LogModel model)
        {
            if(ModelState.IsValid)
            {
                _logProvider.AddToday(model);
                return RedirectToAction("Index", "Log");
            }
            return PartialView("_AddTodayPartial", model);
        }
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