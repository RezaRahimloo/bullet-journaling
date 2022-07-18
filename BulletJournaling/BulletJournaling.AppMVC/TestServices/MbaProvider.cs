
using BulletJournaling.AppMVC.Models;

namespace TestServices;
public class MbaProvider
{
    private List<MbaModel> _mbas = new();
    public bool AddMba(MbaModel mba)
    {
        if(mba is null)
        {
            return false;
        }
        _mbas.Add(mba);
        return true;
    }
    public bool AddLesson(string lesson)
    {
        if(string.IsNullOrEmpty(lesson))
        {
            return false;
        }
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        var todayMba = _mbas.FirstOrDefault(mba => mba.Date == today);
        if(todayMba == null)
        {
            return false;
        }
        todayMba.ImportantLessons.Add(lesson);
        return true;
    }
    public List<MbaModel> GetMbaModels()
    {
        if(_mbas.Count > 0)
        {
            return _mbas;
        }
        for (int i = -3; i < 1; i++)
        {
            DateOnly fourMonthsAgo = DateOnly.FromDateTime(DateTime.Now).AddMonths(i);
            DateOnly lastFourMonths = new(fourMonthsAgo.Year, fourMonthsAgo.Month, 1);
            for (int j = 0; j < DateTime.DaysInMonth(lastFourMonths.Year, lastFourMonths.Month); j++)
            {
                if(j%4 == 0)
                {
                    _mbas?.Add(new MbaModel 
                    { 
                        DidDo = false
                    });
                }
                else
                {
                    _mbas?.Add(new MbaModel
                    {
                        DidDo = true,
                        Type = "cringe",
                        Date = lastFourMonths.AddDays(j),
                        Part = new Random().Next(1, 10),
                        ImportantLessons = new List<string>
                        {
                            "lesson1: rfd js fsdj jdfio sjdoijsdoig josdgios jodfjug ",
                            "lesson2: sdj sdfu sdhfiudhsiuh ifh ui haih guih giudi gydfiu hgih giluh d",
                            "lesson3: ds fjhosudh iudsh uifdhui hduif huidfs dflfdg fd fh ilfd lifhgl fdl fd hldfh lidfuh glfih glfhgld fhglfh gfglifh lidfh lifuhli l fgh uif hiduu8rsrytseryh terot srofuis ghduih giufodfgj ifdj oifji ojfdio jfiojfdoigdouriodugjoi"
                        }
                    });
                }
            }
        }
        return _mbas;
    }
}