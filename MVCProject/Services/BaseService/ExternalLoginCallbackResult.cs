using MVCProject.ViewModels.AccountViewModels;

namespace MVCProject.Services.BaseService {
    public class ExternalLoginCallbackResult : ResultService {
        public string ReturnUrl { get; set; }
        public ExternalLoginConfirmationViewModel ExternalLoginConfirmationViewModel { get; set; }
    }
}
