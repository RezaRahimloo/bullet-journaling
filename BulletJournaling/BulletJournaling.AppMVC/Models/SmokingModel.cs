using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class SmokingModel
    {
        [Required]
        public bool DidSmoke { get; set; }
        public int Number { get; set; } = 0;
        public DateOnly Date { get; set; } 
    }
}