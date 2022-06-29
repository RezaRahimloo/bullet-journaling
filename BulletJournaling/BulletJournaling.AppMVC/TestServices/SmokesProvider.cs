using BulletJournaling.AppMVC.Models;

namespace TestServices;
public class SmokesProvider
{
    private List<SmokingModel> _smokings = new();
    public List<SmokingModel> GetSmokings()
    {
        DateTime sixteenWeeksAgo = DateTime.Now.AddDays(-111);
        for(int i =0; i < 112; i++)
        {
            if(i % 4 == 0)
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
        return _smokings;
    }
}