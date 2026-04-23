using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MVCProject.Models;
using MVCProject.Services.BaseService;
using MVCProject.Services.EmailService;
using MVCProject.ViewModels.AccountViewModels;
using System.Security.Claims;

namespace MVCProject.Services.AccountService {
    public class AccountService : IAccountService {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager,
                IEmailService emailService) {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<ResultService> LoginAsync(LoginViewModel loginViewModel) {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null) {
                return ResultService.Failure("Email is incorrect.", false);
            }

            var result = await _signInManager
                .PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded) {
                return ResultService.Success();
            }

            if (result.IsLockedOut) {
                return ResultService.Failure("Account is locked, please try again after 30 seconds.", true);
            }

            return ResultService.Failure("Invalid attempt.", false);
        }

        public async Task<ResultService> RegisterAsync(RegisterViewModel registerViewModel) {
            if (await _userManager.FindByEmailAsync(registerViewModel.Email) != null) {
                return ResultService.Failure("This email already exist.", false, "Email");
            }

            if (await _userManager.FindByNameAsync(registerViewModel.UserName) != null) {
                return ResultService.Failure("This username already exist.", false, "UserName");
            }

            var user = registerViewModel.Adapt<AppUser>();

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, "Customer");
                await _signInManager.SignInAsync(user, false);

                return ResultService.Success();
            }

            return ResultService.Failure(result.Errors);
        }

        public AuthenticationProperties ConfigureExternalLogin(string provider, string redirectUrl, bool rememberMe) {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            properties.IsPersistent = rememberMe;
            return properties;
        }

        public async Task<ExternalLoginCallbackResult> ExternalLoginCallbackAsync(string returnUrl = null, string remoteError = null) {
            if (remoteError != null) {
                return new ExternalLoginCallbackResult {
                    Succeeded = false,
                    ErrorMessage = $"Error from external provider: {remoteError}"
                };
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) {
                return new ExternalLoginCallbackResult {
                    Succeeded = false,
                    ErrorMessage = "Unable to load external login information."
                };
            }

            bool isPersistent = info.AuthenticationProperties.IsPersistent;
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent);

            if (result.Succeeded) {
                return new ExternalLoginCallbackResult {
                    Succeeded = true,
                    ReturnUrl = returnUrl
                };
            }

            if (result.IsLockedOut) {
                return new ExternalLoginCallbackResult {
                    Succeeded = false,
                    IsLockedOut = true
                };
            }

            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email)) {
                var externalLoginConfirmationViewModel = info.Adapt<ExternalLoginConfirmationViewModel>();

                return new ExternalLoginCallbackResult {
                    Succeeded = false,
                    ReturnUrl = returnUrl,
                    ExternalLoginConfirmationViewModel = externalLoginConfirmationViewModel
                };
            }

            return new ExternalLoginCallbackResult {
                Succeeded = false,
                ErrorMessage = "Unable to retrieve email from external provider."
            };
        }

        public async Task<ResultService> ExternalLoginConfirmationAsync(ExternalLoginConfirmationViewModel externalLoginConfirmationViewModel,
                string returnUrl = null) {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) {
                return ResultService.Failure("Unable to load external login information.", false);
            }

            var user = externalLoginConfirmationViewModel.Adapt<AppUser>();

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded) {
                return ResultService.Failure(createResult.Errors);
            }

            var addLoginResult = await _userManager.AddLoginAsync(user, info);
            if (!addLoginResult.Succeeded) {
                return ResultService.Failure(addLoginResult.Errors);
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            var loginResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                    externalLoginConfirmationViewModel.RememberMe);

            if (loginResult.Succeeded) {
                return ResultService.Success();
            }

            return ResultService.Failure("Failed to sign in after registration.", false);
        }

        public async Task<ResultService> VerifyEmailAsync(VerifyEmailViewModel verifyEmailViewModel, string resetLink) {
            var user = await _userManager.FindByEmailAsync(verifyEmailViewModel.Email);

            if (user == null) {
                return ResultService.Failure("This email does not exist.", false, "Email");
            }

            var subject = "Reset Password";
            var body = $"Please reset your password by clicking here: <a href='{resetLink}'>{subject}</a>";

            await _emailService.SendEmailAsync(user.Email, subject, body);

            return ResultService.Success();
        }

        public async Task<ResultService> ForgetPasswordAsync(ForgetPasswordViewModel forgetPasswordViewModel) {
            var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
            if (user == null) {
                return ResultService.Failure("This email does not exist.", false, "Email");
            }

            var result = await _userManager
                .ResetPasswordAsync(user, forgetPasswordViewModel.Token, forgetPasswordViewModel.NewPassword);

            if (result.Succeeded) {
                return ResultService.Success();
            }

            return ResultService.Failure(result.Errors);
        }

        public async Task LogoutAsync() {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GeneratePasswordTokenAsync(string email) {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) {
                return null;
            }

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}
