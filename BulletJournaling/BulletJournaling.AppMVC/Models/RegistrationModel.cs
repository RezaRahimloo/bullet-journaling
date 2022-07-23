using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "User Name")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength =8)]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(100, MinimumLength =2)]
        [Display(Name = "First Name")]

        public string FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

    }
}
