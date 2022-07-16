using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class WorkoutController : Controller
    {
        private List<WorkoutModel> _workouts;
        public WorkoutController(WorkoutProvider workoutProvider)
        {
            _workouts = workoutProvider.GetWorkouts();
        }
        public IActionResult Index()
        {
            return View(_workouts);
        }
        
    }
}