using BulletJournaling.AppMVC.Models;

namespace TestServices;
public class WorkoutProvider
{
    private List<WorkoutModel> _workouts = new List<WorkoutModel>();
    public bool AddToday(WorkoutModel workout)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        WorkoutModel todayWorkout = _workouts.FirstOrDefault(day => day.Date == today);
        if(workout is null)
        {
            return false;
        }
        if(todayWorkout is null)
        {
            Console.WriteLine("END");
            _workouts.Add(workout);
        }
        else
        {
            
            todayWorkout.didWorkout = workout.didWorkout;
            todayWorkout.DurationMintues = workout.DurationMintues;
            todayWorkout.Type = workout.Type;
            Console.WriteLine("Modified");
            Console.WriteLine(todayWorkout.didWorkout);
        }
        return true;
    }

    public List<WorkoutModel> GetWorkouts()
    {
        if(_workouts.Count > 0)
        {
            return _workouts;
        }
        for (int i = -3; i < 1; i++)
        {
            DateOnly fourMonthsAgo = DateOnly.FromDateTime(DateTime.Now).AddMonths(i);
            DateOnly lastFourMonths = new(fourMonthsAgo.Year, fourMonthsAgo.Month, 1);
            for (int j = 0; j < DateTime.DaysInMonth(lastFourMonths.Year, lastFourMonths.Month); j++)
            {
                if(j%4 == 4)
                {
                    _workouts?.Add(new WorkoutModel 
                    { 
                        didWorkout = false
                    });
                }
                else
                {
                    _workouts?.Add(new WorkoutModel
                    {
                        didWorkout = true,
                        Type = "Murder",
                        DurationMintues = 666,
                        Date = lastFourMonths.AddDays(j)
                    });
                }
            }
        }
        return _workouts;
    }
}