using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class LessonModel
    {
        [Required]
        public string Lesson { get; set; }
        [Required]
        public int Id { get; set; }
    }
}