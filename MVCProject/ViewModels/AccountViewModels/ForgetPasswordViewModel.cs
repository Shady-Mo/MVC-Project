using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.AccountViewModels {
    public class ForgetPasswordViewModel {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "New password is required.")]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm New Password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Password does not match.")]
        [Required(ErrorMessage = "Confirm new password is required.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
