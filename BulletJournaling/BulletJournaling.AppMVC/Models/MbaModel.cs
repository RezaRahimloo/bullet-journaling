using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class MbaModel
    {
        [Required]
        public bool DidDo { get; set; }
        public string Type { get; set; }
        public int Part { get; set; }
        public List<string> ImportantLessons { get; set; }
    }
}