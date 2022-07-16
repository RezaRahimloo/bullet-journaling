
using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models;
public class WorkoutModel
{
    [Required]
    public bool didWorkout { get; set; }
    public string Type { get; set; }
    //public DateTime StartTime { get; set; }
    public int DurationMintues { get; set; }
}