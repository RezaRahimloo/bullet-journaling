using BulletJournaling.AppMVC.Data;
using BulletJournaling.AppMVC.Data.DatabaseModels;
using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly WorkoutProvider _workoutProvider;
        private readonly AppDb _db;
        private readonly UserManager<AppUser> _userManager; 
        private readonly SignInManager<AppUser> _signinManager;
        public WorkoutController(WorkoutProvider workoutProvider,
                                 AppDb db,
                                 UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signinManager)
        {
            _workoutProvider = workoutProvider;
            _db = db;
            _userManager = userManager;
            _signinManager = signinManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if(_signinManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                DateOnly threeMonthsAgo = today.AddMonths(-3);
                threeMonthsAgo = new(threeMonthsAgo.Year, threeMonthsAgo.Month, 1);
                
                var workouts = await _db.Workouts
                    .Where(w => w.UserId == user.Id)
                    .Where(w => w.didWorkout && w.Date <= today)
                    .OrderBy(w => w.Date)
                    .ToListAsync();

                if(workouts is null)
                {
                    return View(new List<Workout>());
                }
                return View(workouts);
            }
            return View(new List<Workout>());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToday(Workout workout)
        {
            
            if(workout.DurationMintues > 0 && !string.IsNullOrEmpty(workout.Type))
            {
                workout.didWorkout = true;
            }
            else
            {
                return NoContent();
            }
            var user = await _userManager.GetUserAsync(User);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            Workout todayWorkout = await _db.Workouts
                .Where(w => w.UserId == user.Id)
                .FirstOrDefaultAsync(w => w.didWorkout && w.Date == today);

            if(todayWorkout is null)
            {
                await _db.Workouts.AddAsync(new Workout
                {
                    UserId = user.Id,
                    DurationMintues = workout.DurationMintues,
                    Type = workout.Type,
                    Date = today,
                    didWorkout = true
                });
                await _db.SaveChangesAsync();
            }
            else
            {
                todayWorkout.didWorkout = true;
                todayWorkout.DurationMintues = workout.DurationMintues;
                todayWorkout.Type = workout.Type;
                
                _db.Update(todayWorkout);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Workout");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearToday()
        {
            var user = await _userManager.GetUserAsync(User);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            Workout todayWorkout = await _db.Workouts
                .Where(w => w.UserId == user.Id)
                .FirstOrDefaultAsync(w => w.didWorkout && w.Date == today);

            if(todayWorkout is null)
            {
                return NotFound("Workout not found");
            }

            _db.Remove(todayWorkout);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Workout");
        }
    }
}