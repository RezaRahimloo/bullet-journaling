using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly LogProvider _logProvider;
        public LogController(LogProvider logProvider)
        {
            _logProvider = logProvider;
        }
        public IActionResult Index()
        {
            return View(_logProvider.GetDayLogs());
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