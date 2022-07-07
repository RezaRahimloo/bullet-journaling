using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class SmokingModel
    {
        [Required]
        [Display(Name ="Did you smoke today?")]
        public bool DidSmoke { get; set; }
        public int Number { get; set; } = 0;
        public DateOnly Date { get; set; } 
    }
}