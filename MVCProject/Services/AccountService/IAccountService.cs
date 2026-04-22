using Microsoft.AspNetCore.Authentication;
using MVCProject.Services.BaseService;
using MVCProject.ViewModels.AccountViewModels;

namespace MVCProject.Services.AccountService {
    public interface IAccountService {
        Task<ResultService> LoginAsync(LoginViewModel loginViewModel);

        Task<ResultService> RegisterAsync(RegisterViewModel registerViewModel);

        AuthenticationProperties ConfigureExternalLogin(string provider, string redirectUrl, bool rememberMe);

        Task<ExternalLoginCallbackResult> ExternalLoginCallbackAsync(string returnUrl = null, string remoteError = null);

        Task<ResultService> ExternalLoginConfirmationAsync(ExternalLoginConfirmationViewModel externalLoginConfirmationViewModel, string returnUrl = null);

        Task<ResultService> VerifyEmailAsync(VerifyEmailViewModel verifyEmailViewModel, string scheme);

        Task<ResultService> ForgetPasswordAsync(ForgetPasswordViewModel forgetPasswordViewModel);

        Task LogoutAsync();
    }
}
