using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Data.DatabaseModels
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public Guid MbaId { get; set; }
        [Required]
        public string LessonContext { get; set; }
        
    }
}
