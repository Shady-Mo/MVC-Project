using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.ViewModels.AuthViewModels;

namespace MVCProject.Controllers {
    public class AuthController : Controller {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, 
                SignInManager<AppUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user == null) {
                    ModelState.AddModelError("", "Email is incorrect.");
                    return View(loginViewModel);
                }

                var result = await _signInManager
                    .PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }

                if (result.IsLockedOut) {
                    ModelState.AddModelError("", "Account is locked, please try again after 30 seconds.");
                    return View(loginViewModel);
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel) {
            if (ModelState.IsValid) {
                if (await _userManager.FindByEmailAsync(registerViewModel.Email) != null) {
                    ModelState.AddModelError("Email", "This email already exist.");
                    return View(registerViewModel);
                }

                if (await _userManager.FindByNameAsync(registerViewModel.UserName) != null) {
                    ModelState.AddModelError("UserName", "This username already exist.");
                    return View(registerViewModel);
                }

                //if (await _userManager.Users.AnyAsync(u => u.PhoneNumber == registerViewModel.PhoneNumber) != null) {
                //    ModelState.AddModelError("PhoneNumber", "This phone number already exist.");
                //    return View(registerViewModel);
                //}

                var user = registerViewModel.Adapt<AppUser>();

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded) {
                    await _userManager.AddToRoleAsync(user, "Customer");
                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
