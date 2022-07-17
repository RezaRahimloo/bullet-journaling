using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly WorkoutProvider _workoutProvider;
        public WorkoutController(WorkoutProvider workoutProvider)
        {
            _workoutProvider = workoutProvider;
        }
        public IActionResult Index()
        {
            return View(_workoutProvider.GetWorkouts());
        }
        [HttpPost]
        public async Task<IActionResult> AddToday(WorkoutModel workout)
        {
            Console.WriteLine(workout.DurationMintues);
            Console.WriteLine(workout.Type);
            if(workout.DurationMintues > 0 && !string.IsNullOrEmpty(workout.Type))
            {
                workout.didWorkout = true;
            }
            Console.WriteLine(workout.didWorkout);
            if(ModelState.IsValid)
            {
                if(_workoutProvider.AddToday(workout))
                {
                    Console.WriteLine("Added");
                }
                
                return RedirectToAction("Index", "Workout");
            }
            Console.WriteLine("Problem");
            return PartialView("_AddTodayPartial", workout);
        }
        [HttpDelete]
        public async Task<IActionResult> ClearToday()
        {
            if(_workoutProvider.ClearToday())
            {
                return RedirectToAction("Index", "Workout");
            }
            else return BadRequest("today has no Data");
        }
    }
}