using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class SmokingController : Controller
    {
        private SmokesProvider _smokeProvider;
        public SmokingController(SmokesProvider smokesProvider)
        {
            _smokeProvider = smokesProvider;
            
        }
        public IActionResult Index()
        {
            return View(_smokeProvider.GetSmokings());
        }
        [HttpPost]
        public async Task<IActionResult> AddToday(SmokingModel smoking)
        {
            if(ModelState.IsValid)
            {
                _smokeProvider.AddToday(smoking);
                return RedirectToAction("Index", "Smoking");
            }
            return PartialView("_AddTodayPartial", smoking);
        }
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