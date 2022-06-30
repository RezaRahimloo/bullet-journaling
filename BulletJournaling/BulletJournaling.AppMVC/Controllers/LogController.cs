using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly List<LogModel> _logs = new();
        public LogController()
        {
            PopulateLogs();
        }
        public IActionResult Index()
        {
            return View(_logs);
        }
        private void PopulateLogs()
        {
            DateOnly sixteenWeeksAgo = DateOnly.FromDateTime(DateTime.Now).AddDays(-111);
            for(int i =0; i < 112; i++)
            {
                if(i % 4 == 0)
                {
                    _logs?.Add(new LogModel 
                    { 
                        HasLog = false
                    });
                }
                else
                {
                    _logs?.Add(new LogModel
                    {
                        HasLog = true,
                        Title = "Learend DI",
                        Description = "Did this and that",
                        DurationMinutes = new Random().Next(1, 12),
                        day = sixteenWeeksAgo.AddDays(-i)
                    });
                }
            
            }
       
        }
    }
}