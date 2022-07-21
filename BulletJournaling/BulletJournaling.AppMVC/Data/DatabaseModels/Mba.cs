using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletJournaling.AppMVC.Data.DatabaseModels
{
    public class Mba
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public bool DidDo { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Part { get; set; }
        public DateOnly Date { get; set; }
        [ForeignKey("MbaId")]
        public ICollection<Lesson> ImportantLessons { get; set; }
    }
}
