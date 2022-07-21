using System.ComponentModel.DataAnnotations.Schema;

namespace BulletJournaling.AppMVC.Data.DatabaseModels
{
    public class DayLog
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public bool HasLog { get; set; }
        public DateOnly day { get; set; }
        [ForeignKey("DayLogId")]
        public ICollection<Log> Logs { get; set; }
    }
}
