using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class LogModel
    {
        [Required]
        public bool HasLog { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly day { get; set; }
        public int DurationMinutes { get; set; }
    }
}