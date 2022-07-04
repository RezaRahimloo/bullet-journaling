using BulletJournaling.AppMVC.Models;

namespace TestServices;
public class WorkoutProvider
{
    private List<WorkoutModel>? _workouts = new List<WorkoutModel>();

    public List<WorkoutModel> GetWorkouts()
    {
        for (int i = -3; i < 1; i++)
        {
            DateOnly fourMonthsAgo = DateOnly.FromDateTime(DateTime.Now).AddMonths(i);
            DateOnly lastFourMonths = new(fourMonthsAgo.Year, fourMonthsAgo.Month, 1);
            for (int j = 0; j < DateTime.DaysInMonth(lastFourMonths.Year, lastFourMonths.Month); j++)
            {
                if(j%4 == 0)
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
                        StartTime = lastFourMonths.AddDays(j).ToDateTime(new TimeOnly(
                            hour: 4,
                            minute: 30
                        )),
                        Type = "Murder",
                        DurationMintues = 666
                    });
                }
            }
        }
        return _workouts;
    }
}