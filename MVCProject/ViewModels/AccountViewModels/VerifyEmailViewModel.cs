using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.AccountViewModels {
    public class VerifyEmailViewModel {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        public string? RequestHost { get; set; }
    }
}
