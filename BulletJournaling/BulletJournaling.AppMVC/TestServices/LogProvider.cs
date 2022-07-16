using BulletJournaling.AppMVC.Models;

namespace TestServices;
public class LogProvider
{
    private List<DayLogModel> _dayLogs = new();
    public List<DayLogModel> AddToday(LogModel log)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        DayLogModel todayLog = _dayLogs.FirstOrDefault(dayLog => dayLog.day == today);
        if(todayLog is not null)
        {
            var logs = todayLog.Logs;
            if(logs.Count > 0)
            {
                log.Id = logs[logs.Count - 1].Id + 1;
            }
            else
            {
                log.Id = 0;
            }
            todayLog.Logs.Add(log);
        }
        else
        {
            _dayLogs.Add(new DayLogModel
            {
                HasLog = true,
                day = today,
                Logs = new List<LogModel>
                {
                    new LogModel 
                    {
                        Id=0, 
                        Title = log.Title, 
                        Description = log.Description, 
                        DurationMinutes = log.DurationMinutes 
                    }
                }
            });
        }
        return _dayLogs;
    }
    public bool DeleteLog(int logId)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        DayLogModel todayLog = _dayLogs.FirstOrDefault(dayLog => dayLog.day == today);
        if(todayLog != null)
        {
            int deletingLog = todayLog.Logs.FindIndex(log => log.Id == logId);
            if(deletingLog > -1)
            {
                todayLog.Logs.RemoveAt(deletingLog);
                return true;
            }
        }
        return false;
    }
    public List<DayLogModel> GetDayLogs()
    {
        if(_dayLogs.Count > 0)
        {
            return _dayLogs;
        }
        for (int i = -3; i < 1; i++)
        {
            DateOnly fourMonthsAgo = DateOnly.FromDateTime(DateTime.Now).AddMonths(i);
            DateOnly lastFourMonths = new(fourMonthsAgo.Year, fourMonthsAgo.Month, 1);
            for (int j = 0; j < DateTime.DaysInMonth(lastFourMonths.Year, lastFourMonths.Month); j++)
            {
                if(j % 8 == 0)
                {
                    _dayLogs?.Add(new DayLogModel 
                    { 
                        HasLog = false
                    });
                }
                else
                {
                    List<LogModel> logs = new();
                    logs.Add(new LogModel
                    {
                        Id = 0,
                        Title = "Learend DI",
                        Description = "Did sadskajsajdlksajdsa askl jdlaskj slak jals jaslj skadjals jds jask jaslkdj aslj sald jaskdj asdj sakj lasj sakjdsak jksaj kas k jdasj ash asj lasj lsaj lksaj lasj lkasj lasj saj dj j as hidushf isudh suidhfsd hfsdi hisd fhiudch fiu sdhsdihf dihfiudh iudjh i h hdi hfd  hidh fid hiu hsihdf hidf hilfdh udf fhiud idf hiudf hil sfiug fiu hdfgif hfd hidf hiu hifdif gifh gfi hfui this and that",
                        DurationMinutes = new Random().Next(1, 12)
                    });
                    logs.Add(new LogModel
                    {
                        Id = 1,
                        Title = "Learend CI/CD",
                        Description = "Did this and that",
                        DurationMinutes = new Random().Next(1, 12)
                    });
                    _dayLogs.Add(new DayLogModel
                    {
                        HasLog = true,
                        Logs = logs,
                        day = lastFourMonths.AddDays(j)
                    });
                }
            }
        }
        return _dayLogs;
    }
}