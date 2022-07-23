using BulletJournaling.AppMVC.Data;
using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulletJournaling.AppMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username,
                                                                      model.Password,
                                                                      model.RememberMe,
                                                                      lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }
            return Unauthorized("Wrong user name or password!");
        }
    }
}
