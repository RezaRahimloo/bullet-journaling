using BulletJournaling.AppMVC.Models;

namespace TestServices;
public class SmokesProvider
{
    private List<SmokingModel> _smokings = new();
    public void AddOne()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        SmokingModel todaySmoking = _smokings.FirstOrDefault(day => day.Date == today);
        if(todaySmoking is not null)
        {
            todaySmoking.Number++;
        }
        else
        {
            todaySmoking = new SmokingModel
            {
                DidSmoke = true,
                Date = today,
                Number = 1
            };
        }
    }
    public List<SmokingModel> AddToday(SmokingModel smoking)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        SmokingModel todaySmoking = _smokings.FirstOrDefault(day => day.Date == today);
        if(todaySmoking is not null)
        {
            todaySmoking.DidSmoke = smoking.DidSmoke;
            todaySmoking.Number = smoking.Number;
            // todaySmoking = new SmokingModel
            // {
            //     DidSmoke = smoking.DidSmoke,
            //     Date = smoking.Date,
            //     Number = smoking.Number
            // };
        }
        else
        {
            _smokings.Add(smoking);
        }
        return _smokings;
    }
    public void DeleteToday()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        SmokingModel todaySmoking = _smokings.FirstOrDefault(day => day.Date == today);
        todaySmoking.DidSmoke = false;
        todaySmoking.Number = 0;
    }
    public List<SmokingModel> GetSmokings()
    {
        if(_smokings.Count > 0)
        {
            return _smokings;
        }
        for (int i = -3; i < 1; i++)
        {
            DateOnly fourMonthsAgo = DateOnly.FromDateTime(DateTime.Now).AddMonths(i);
            DateOnly lastFourMonths = new(fourMonthsAgo.Year, fourMonthsAgo.Month, 1);
            for (int j = 0; j < DateTime.DaysInMonth(lastFourMonths.Year, lastFourMonths.Month); j++)
            {
                if(j%2 == 3)
                {
                    _smokings.Add(new SmokingModel 
                    { 
                        DidSmoke = false,
                        Number = 0,
                        Date = lastFourMonths.AddDays(j)
                    });
                }
                else
                {
                    _smokings.Add(new SmokingModel
                    {
                        DidSmoke = true,
                        Number = new Random().Next(0, 13),
                        Date = lastFourMonths.AddDays(j)
                    });
                }
            }
        }
        return _smokings;
    }
}