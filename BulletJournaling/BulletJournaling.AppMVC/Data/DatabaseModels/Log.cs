using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Data.DatabaseModels
{
    public class Log
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
    }
}
