using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class MbaModel
    {
        [Required]
        public bool DidDo { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Part { get; set; }
        public DateOnly Date { get; set; }
        public List<string> ImportantLessons { get; set; }
    }
}