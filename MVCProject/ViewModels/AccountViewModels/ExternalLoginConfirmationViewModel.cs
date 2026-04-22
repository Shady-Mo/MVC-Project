using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.AccountViewModels {
    public class ExternalLoginConfirmationViewModel {
        public string Email { get; set; }
        [Required(ErrorMessage = "Full name is required.")]
        [Display(Name = "Full Name")]
        [StringLength(31, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at most {1}.")]
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
