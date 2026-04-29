using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.Services.AccountService;
using MVCProject.Services.EmailService;
using MVCProject.ViewModels.AccountViewModels;
using System.Security.Claims;

namespace MVCProject.Controllers {
    public class AccountController : Controller {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) {
            if (ModelState.IsValid) {
                var result = await _accountService.LoginAsync(loginViewModel);

                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(result.TargetProperty, result.ErrorMessage);
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
                var result = await _accountService.RegisterAsync(registerViewModel);

                if (result.Succeeded) {
                    return RedirectToAction("Login");
                }

                if (result.ErrorMessage != null) {
                    ModelState.AddModelError(result.TargetProperty, result.ErrorMessage);
                }
                else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, bool rememberMe, string returnUrl = null) {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
            var properties = _accountService.ConfigureExternalLogin(provider, redirectUrl, rememberMe);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null) {
            var result = await _accountService.ExternalLoginCallbackAsync(returnUrl, remoteError);

            if (result.ErrorMessage != null) {
                ModelState.AddModelError("", result.ErrorMessage);
                return RedirectToAction("Login");
            }

            if (result.IsLockedOut ?? false) {
                return RedirectToAction("Login");
            }

            if (result.Succeeded) {
                return RedirectToLocal(result.ReturnUrl);
            }

            if (result.ExternalLoginConfirmationViewModel != null) {
                ViewData["ReturnUrl"] = result.ReturnUrl;
                return View("ExternalLoginConfirmation", result.ExternalLoginConfirmationViewModel);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel externalLoginConfirmationViewModel,
                string returnUrl = null) {
            if (ModelState.IsValid) {
                var result = await _accountService.ExternalLoginConfirmationAsync(externalLoginConfirmationViewModel, returnUrl);

                if (result.Succeeded) {
                    return RedirectToLocal(returnUrl);
                }

                if (result.ErrorMessage != null) {
                    ModelState.AddModelError("", result.ErrorMessage);
                }
                else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(externalLoginConfirmationViewModel);
        }

        [HttpGet]
        public IActionResult VerifyEmail() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel verifyEmailViewModel) {
            if (ModelState.IsValid) {
                var resetToken = await _accountService.GeneratePasswordTokenAsync(verifyEmailViewModel.Email);
                if (resetToken == null) {
                    ModelState.AddModelError("Email", "This email does not exist.");
                    return View(verifyEmailViewModel);
                }
                var resetLink = Url.Action("ForgetPassword", "Account",
                    new { email = verifyEmailViewModel.Email, token = resetToken }, Request.Scheme);

                var result = await _accountService.VerifyEmailAsync(verifyEmailViewModel, resetLink);

                if (result.Succeeded) {
                    return RedirectToAction("EmailSent", new { email = verifyEmailViewModel.Email });
                }

                ModelState.AddModelError(result.TargetProperty, result.ErrorMessage);
            }

            return View(verifyEmailViewModel);
        }

        [HttpGet]
        public IActionResult EmailSent(string email) {
            if (string.IsNullOrEmpty(email)) {
                return RedirectToAction("VerifyEmail");
            }

            return View(new VerifyEmailViewModel { Email = email });
        }

        [HttpGet]
        public IActionResult ForgetPassword(string email, string token) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token)) {
                return RedirectToAction("VerifyEmail");
            }

            var forgetPasswordViewModel = new ForgetPasswordViewModel {
                Email = email,
                Token = token
            };

            return View(forgetPasswordViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel) {
            if (ModelState.IsValid) {
                var result = await _accountService.ForgetPasswordAsync(forgetPasswordViewModel);

                if (result.Succeeded) {
                    return RedirectToAction("Login");
                }

                if (result.ErrorMessage != null) {
                    ModelState.AddModelError(result.TargetProperty, result.ErrorMessage);
                }
                else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(forgetPasswordViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout() {
            await _accountService.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetUserInfo() {
            if (User.Identity.IsAuthenticated) {
                return Json(new {
                    isAuthenticated = true,
                    userName = User.Identity.Name,
                    email = User.FindFirst(ClaimTypes.Email)?.Value,
                    phoneNumber = User.FindFirst("PhoneNumber")?.Value
                });
            }
            return Json(new { isAuthenticated = false });
        }

        private IActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
