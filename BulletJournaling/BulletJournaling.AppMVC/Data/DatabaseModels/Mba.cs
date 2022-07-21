using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Data.DatabaseModels
{
    public class Mba
    {
        public Guid Id { get; set; }
        [Required]
        public bool DidDo { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Part { get; set; }
        public DateOnly Date { get; set; }
        public ICollection<Lesson> ImportantLessons { get; set; }
    }
}
