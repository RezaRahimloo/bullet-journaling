using BulletJournaling.AppMVC.Models;

namespace TestServices;
public class SmokesProvider
{
    private List<SmokingModel> _smokings = new();
    public List<SmokingModel> GetSmokings()
    {
        for (int i = -3; i < 1; i++)
        {
            DateOnly fourMonthsAgo = DateOnly.FromDateTime(DateTime.Now).AddMonths(i);
            DateOnly lastFourMonths = new(fourMonthsAgo.Year, fourMonthsAgo.Month, 1);
            for (int j = 0; j < DateTime.DaysInMonth(lastFourMonths.Year, lastFourMonths.Month); j++)
            {
                if(j%4 == 0)
                {
                    _smokings?.Add(new SmokingModel 
                    { 
                        DidSmoke = false
                    });
                }
                else
                {
                    _smokings?.Add(new SmokingModel
                    {
                        DidSmoke = true,
                        Number = new Random().Next(1, 13)
                    });
                }
            }
        }
        return _smokings;
    }
}