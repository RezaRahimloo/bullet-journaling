using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class SmokingController : Controller
    {
        private readonly List<SmokingModel> _smokings;
        public SmokingController(SmokesProvider smokesProvider)
        {
            _smokings = smokesProvider.GetSmokings();
        }
        public IActionResult Index()
        {
            return View(_smokings);
        }

       
    }
}