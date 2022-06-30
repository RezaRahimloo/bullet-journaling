using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class LogModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime day { get; set; }
        public int DurationMinutes { get; set; }
    }
}