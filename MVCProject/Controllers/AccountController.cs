using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.ViewModels.AccountViewModels;
using System.Security.Claims;

namespace MVCProject.Controllers {
    public class AccountController : Controller {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, 
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
        public IActionResult ExternalLogin(string provider, bool rememberMe, string returnUrl = null) {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            properties.IsPersistent = rememberMe;
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null) {
            if (remoteError != null) {
                ModelState.AddModelError("", $"Error from external provider: {remoteError}");
                return RedirectToAction("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) {
                return RedirectToAction("Login");
            }

            bool isPersistent = info.AuthenticationProperties.IsPersistent;
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent);
            if (result.Succeeded) {
                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut) {
                return RedirectToAction("Login");
            }

            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email)) {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                var externalLoginConfirmationViewModel = info.Adapt<ExternalLoginConfirmationViewModel>();

                ViewData["ReturnUrl"] = returnUrl;

                return View("ExternalLoginConfirmation", externalLoginConfirmationViewModel);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel externalLoginConfirmationViewModel,
                string returnUrl = null) {
            if (ModelState.IsValid) {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null) {
                    return RedirectToAction("Login");
                }

                var user = externalLoginConfirmationViewModel.Adapt<AppUser>();

                var createResult = await _userManager.CreateAsync(user);
                if (createResult.Succeeded) {
                    var addLoginResult = await _userManager.AddLoginAsync(user, info);
                    if (addLoginResult.Succeeded) {
                        await _userManager.AddToRoleAsync(user, "Customer");

                        var loginResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                                externalLoginConfirmationViewModel.RememberMe);
                        if (loginResult.Succeeded) {
                            return RedirectToLocal(returnUrl);
                        }
                    }
                }
            }

            return View(externalLoginConfirmationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
