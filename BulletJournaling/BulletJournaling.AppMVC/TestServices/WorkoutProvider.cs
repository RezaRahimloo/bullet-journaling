using BulletJournaling.AppMVC.Models;

namespace TestServices;
public class WorkoutProvider
{
    private List<WorkoutModel>? _workouts = new List<WorkoutModel>();

    public List<WorkoutModel> GetWorkouts()
    {
        if(_workouts?.Count > 0)
        {
            Console.WriteLine(_workouts.Count);
            return _workouts;
        }
        DateTime fiftySixDaysAgo = DateTime.Now.AddDays(-111);
        for(int i =0; i < 112; i++)
        {
            if(i % 2 == 0)
            {
                _workouts?.Add(new WorkoutModel 
                { 
                    didWorkout = true, 
                    StartTime = fiftySixDaysAgo.AddDays(i),
                    Type = "Murder",
                    DurationMintues = 666
                });
                Console.WriteLine("added_true");
            }
            else
            {
                _workouts?.Add(new WorkoutModel
                {
                    didWorkout = false
                });
                Console.WriteLine("added_false");
            }
        }
        Console.WriteLine(_workouts.Count);
        return _workouts;
    }
}