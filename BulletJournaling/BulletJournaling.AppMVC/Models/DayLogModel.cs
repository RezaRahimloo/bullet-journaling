using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class DayLogModel
    {
        [Required]
        public bool HasLog { get; set; }
        public DateOnly day { get; set; }
        public List<LogModel>? Logs { get; set; }
    }
}