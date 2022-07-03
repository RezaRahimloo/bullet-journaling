using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestServices;

namespace BulletJournaling.AppMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly List<DayLogModel> _logs = new();
        public LogController()
        {
            PopulateLogs();
        }
        public IActionResult Index()
        {
            return View(_logs);
        }
        private void PopulateLogs()
        {
            
            for (int i = -3; i < 1; i++)
            {
                DateOnly fourMonthsAgo = DateOnly.FromDateTime(DateTime.Now).AddMonths(i);
                DateOnly lastFourMonths = new(fourMonthsAgo.Year, fourMonthsAgo.Month, 1);
                for (int j = 0; j < DateTime.DaysInMonth(lastFourMonths.Year, lastFourMonths.Month); j++)
                {
                    if(false)
                    {
                        _logs?.Add(new DayLogModel 
                        { 
                            HasLog = false
                        });
                    }
                    else
                    {
                        List<LogModel> logs = new();
                        logs.Add(new LogModel
                        {
                            Title = "Learend DI",
                            Description = "Did sadskajsajdlksajdsa askl jdlaskj slak jals jaslj skadjals jds jask jaslkdj aslj sald jaskdj asdj sakj lasj sakjdsak jksaj kas k jdasj ash asj lasj lsaj lksaj lasj lkasj lasj saj dj j as hidushf isudh suidhfsd hfsdi hisd fhiudch fiu sdhsdihf dihfiudh iudjh i h hdi hfd  hidh fid hiu hsihdf hidf hilfdh udf fhiud idf hiudf hil sfiug fiu hdfgif hfd hidf hiu hifdif gifh gfi hfui this and that",
                            DurationMinutes = new Random().Next(1, 12)
                        });
                        logs.Add(new LogModel
                        {
                            Title = "Learend CI/CD",
                            Description = "Did this and that",
                            DurationMinutes = new Random().Next(1, 12)
                        });
                        _logs.Add(new DayLogModel
                        {
                            HasLog = true,
                            Logs = logs,
                            day = lastFourMonths.AddDays(j)
                        });
                    }
                }
            }
        }
    }
}