using BulletJournaling.AppMVC.Data;
using BulletJournaling.AppMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BulletJournaling.AppMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly AppDb _db;
        public AuthController(UserManager<AppUser> userManager, 
                              IConfiguration configuration, 
                              SignInManager<AppUser> signInManager,
                              AppDb db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _db = db;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
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
        [AllowAnonymous]//don't need special authorization to access this method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegistrationModel registrationModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = registrationModel.Email,
                    Email = registrationModel.Email,
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName
                };

                var result = await _userManager.CreateAsync(user, registrationModel.Password);
                
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return PartialView("_UserRegistrationPartial", registrationModel);
                }
                AddErrorsToModelState(result);
            }

            return PartialView("_UserRegistrationPartial", registrationModel);
        }
        public async Task<bool> UserNameExists(string userName)
        {
            bool userNameExists = await _db.Users.AnyAsync(u => u.UserName.ToUpper() == userName.ToUpper());
            return userNameExists;
        }
        private void AddErrorsToModelState(IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
