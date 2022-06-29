using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class MbaController : Controller
    {
        private readonly List<MbaModel> _lessons = new();
        public MbaController()
        {
            PopulateLessons();
        }
        public IActionResult Index()
        {
            return View(_lessons);
        }
        private void PopulateLessons()
        {
            DateTime sixteenWeeksAgo = DateTime.Now.AddDays(-111);
            for(int i =0; i < 112; i++)
            {
                if(i % 4 == 0)
                {
                    _lessons?.Add(new MbaModel 
                    { 
                        DidDo = false
                    });
                }
                else
                {
                    _lessons?.Add(new MbaModel
                    {
                        DidDo = true,
                        Type = "Business model",
                        Part = new Random().Next(1, 12),
                        ImportantLessons = new List<string>()
                        {
                            "murder", "murderer", "Cringe"
                        }
                    });
                }
            
            }
       
        }
    }
}