namespace BulletJournaling.AppMVC.Data.DatabaseModels
{
    public class DayLog
    {
        public Guid Id { get; set; }
        public bool HasLog { get; set; }
        public DateOnly day { get; set; }
        public ICollection<Log> Logs { get; set; }
    }
}
