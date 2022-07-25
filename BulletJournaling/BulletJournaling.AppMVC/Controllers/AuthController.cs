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
        [Route("/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            model.LoginInvalid = "true";
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
                else
                {
                    model.LoginInvalid = "false";
                    return PartialView("_LoginFormPartial", model);
                }
            }
            // ModelState["UserName"].Errors.Clear();
            // ModelState["Password"].Errors.Clear();
            model.LoginInvalid = "true";
            ModelState.AddModelError("Failed","Wrong username or password!");
            return PartialView("_LoginFormPartial", model);
        }
        [AllowAnonymous]//don't need special authorization to access this method
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/register")]
        public async Task<IActionResult> RegisterUser(RegistrationModel registrationModel)
        {
            Console.WriteLine("Register method ran!");
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
                    return PartialView("_RegisterFormPartial", registrationModel);
                }
                AddErrorsToModelState(result);
            }
            Console.WriteLine("Model not valid return partial");
            return PartialView("_RegisterFormPartial", registrationModel);
        }
        [AllowAnonymous]//don't need special authorization to access this method
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/account/logout")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            Console.WriteLine("logouting");
            await _signInManager.SignOutAsync();
            Console.WriteLine("out");
            if(returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
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
