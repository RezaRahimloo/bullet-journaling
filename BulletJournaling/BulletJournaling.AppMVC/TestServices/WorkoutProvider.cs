using BulletJournaling.AppMVC.Models;

namespace TestServices;
public class WorkoutProvider
{
    private List<WorkoutModel>? _workouts;

    public List<WorkoutModel> GetWorkouts()
    {
        if(_workouts is not null)
        {
            return _workouts;
        }
        DateTime fiftySixDaysAgo = DateTime.Now.AddDays(-56);
        for(int i =0; i < 56; i++)
        {
            if(i % 2 == 0)
            {
                _workouts.Add(new WorkoutModel 
                { 
                    didWorkout = true, 
                    StartTime = fiftySixDaysAgo.AddDays(i),
                    Type = "Murder",
                    DurationMintues = 666
                });
            }
        }
        return _workouts;
    }
}