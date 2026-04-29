using System.ComponentModel.DataAnnotations;

namespace MVCProject.ViewModels.UserViewModels
{
    public class DisplayUserVM
    {
        public string Id { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        [Display(Name = "Status")]
        public bool IsBanned { get; set; }
    }
}
