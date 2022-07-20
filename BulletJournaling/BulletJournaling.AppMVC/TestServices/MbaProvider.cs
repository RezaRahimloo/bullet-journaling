
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
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        var todayMba = _mbas.FirstOrDefault(mba => mba.Date == today);
        if(todayMba is null)
        {
            _mbas.Add(mba);
        }
        else
        {
            todayMba.DidDo = true;
            todayMba.Type = mba.Type;
            todayMba.Part = mba.Part;
        }
        
        return true;
    }
    public bool AddLesson(LessonModel lesson)
    {
        if(string.IsNullOrEmpty(lesson.Lesson))
        {
            Console.WriteLine("String Empty");
            return false;
        }
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        var todayMba = _mbas.FirstOrDefault(mba => mba.Date == today);
        if(todayMba == null)
        {
            return false;
        }
        var importantLessons = todayMba.ImportantLessons;
        if(importantLessons.Count > 0)
        {
            lesson.Id = importantLessons[importantLessons.Count - 1 ].Id + 1;
        }
        else
        {
            lesson.Id = 0;
        }
        todayMba.ImportantLessons.Add(lesson);
        return true;
    }
    public bool ClearToday()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        var todayMba = _mbas.FirstOrDefault(mba => mba.Date == today);
        if(todayMba is null)
        {
            return false;
        }
        todayMba.DidDo = false;
        return true;
    }
    public bool DeleteLessonToday(int id)
    {
        if(id < 0)
        {
            return false;
        }
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        var todayMba = _mbas.FirstOrDefault(mba => mba.Date == today);
        if(todayMba is null)
        {
            return false;
        }
        int deletingIndex = todayMba.ImportantLessons.FindIndex(lesson => lesson.Id == id);
        todayMba.ImportantLessons.RemoveAt(deletingIndex);
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
                        ImportantLessons = new List<LessonModel>
                        {
                            new LessonModel 
                            {
                                Id = 0,
                                Lesson = "lesson1: rfd js fsdj jdfio sjdoijsdoig josdgios jodfjug "
                            },
                            new LessonModel 
                            {
                                Id = 0,
                                Lesson = "lesson2: sdj sdfu sdhfiudhsiuh ifh ui haih guih giudi gydfiu hgih giluh d"
                            },
                            new LessonModel 
                            {
                                Id = 0,
                                Lesson = "lesson3: ds fjhosudh iudsh uifdhui hduif huidfs dflfdg fd fh ilfd lifhgl fdl fd hldfh lidfuh glfih glfhgld fhglfh gfglifh lidfh lifuhli l fgh uif hiduu8rsrytseryh terot srofuis ghduih giufodfgj ifdj oifji ojfdio jfiojfdoigdouriodugjoi"
                            }
                        }
                    });
                }
            }
        }
        return _mbas;
    }
}