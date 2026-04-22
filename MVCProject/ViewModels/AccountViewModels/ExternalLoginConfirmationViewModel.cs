using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.AccountViewModels {
    public class ExternalLoginConfirmationViewModel {
        public string Email { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        public bool RememberMe { get; set; }
    }
}
