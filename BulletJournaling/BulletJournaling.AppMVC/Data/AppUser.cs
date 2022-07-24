using BulletJournaling.AppMVC.Data.DatabaseModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletJournaling.AppMVC.Data
{
    public class AppUser : IdentityUser
    {
        public override string Id { get; set; }
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [ForeignKey("UserId")]
        public ICollection<DayLog> DayLogs { get; set; }
        [ForeignKey("UserId")]
        public ICollection<Mba> Mbas  { get; set; }
        [ForeignKey("UserId")]
        public ICollection<Smoking> Smokings { get; set; }
        [ForeignKey("UserId")]
        public ICollection<Workout> Workouts { get; set; }
    }
}
