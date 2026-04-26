using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.ProfileViewModels
{
    public class UpdateProfileViewModel
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
    }
}
