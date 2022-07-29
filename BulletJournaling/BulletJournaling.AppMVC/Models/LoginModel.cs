using System.ComponentModel.DataAnnotations;

namespace BulletJournaling.AppMVC.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
        public string LoginInvalid { get; set; }
    }
}
