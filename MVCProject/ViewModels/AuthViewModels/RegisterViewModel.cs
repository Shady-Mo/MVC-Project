using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.AuthViewModels {
    public class RegisterViewModel {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at most {1}.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at most {1}.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} must be at least {2} and at most {1}.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }
    }
}
