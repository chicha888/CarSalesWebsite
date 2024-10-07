using CarSalesWebsite.Data;
using CarSalesWebsite.Models;
using CarSalesWebsite.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);

            var user = await _userManager.FindByEmailAsync(login.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["Error"] = "Wrong cridentials. Try again";
                    return View(login);
                }
            }

            TempData["Error"] = "Wrong cridentials. Try again";
            return View(login);
        }

        public IActionResult Register()
        {
            var response = new RegisterVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(register.Email);
                if (user != null) 
                {
                    TempData["Error"] = "This email is already in use!";
                    return View(register);
                }

                var newUser = new AppUser 
                { 
                    UserName = register.Email,
                    Email = register.Email,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    PhoneNumber = register.PhoneNumber,
                    CreatedDate = DateTime.Now
                };

                var registerNewUser = await _userManager.CreateAsync(newUser, register.Password);
                if (registerNewUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in registerNewUser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }


            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
