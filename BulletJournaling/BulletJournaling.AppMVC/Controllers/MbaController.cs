using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class MbaController : Controller
    {
        private readonly MbaProvider _mbaProvider;
        public MbaController(MbaProvider mbaProvider)
        {
            _mbaProvider = mbaProvider;
        }
        public IActionResult Index()
        {
            return View(_mbaProvider.GetMbaModels());
        }
        
    }
}