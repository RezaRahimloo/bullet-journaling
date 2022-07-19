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
        [HttpPost]
        public async Task<IActionResult> AddMba(MbaModel mba)
        {
            mba.DidDo = false;
            if(!ModelState.IsValid)
            {
                return PartialView("_AddTodayMbaPartial");
            }
            mba.DidDo = true;
            if(!_mbaProvider.AddMba(mba))
            {
                return BadRequest("MBA Session could not be added");
            }
            return RedirectToAction("Index", "Mba");
        }
        [HttpPost]
        public async Task<IActionResult> AddLesson(LessonModel lesson)
        {
            lesson.Id = -1;
            if(!ModelState.IsValid)
            {
                return PartialView("_AddTodayLessonPartial");
            }
            if(!_mbaProvider.AddLesson(lesson))
            {
                return BadRequest("Lesson cannot be added!");
            }
            _mbaProvider.AddLesson(lesson);
            return RedirectToAction("Index", "Mba");
        }
    }
}