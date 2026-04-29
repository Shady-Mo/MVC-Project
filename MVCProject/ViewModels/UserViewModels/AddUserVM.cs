using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.UserViewModels
{
    public class AddUserVM
    {
        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select a role")]
        public string SelectedRole { get; set; }

        // This will be populated by the Controller via a loop
        public List<SelectListItem> RoleList { get; set; } = new List<SelectListItem>();
    }
}
