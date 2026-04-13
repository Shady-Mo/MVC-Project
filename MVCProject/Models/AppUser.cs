using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models {
    public class AppUser : IdentityUser {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Booking>? Bookings { get; set; } = new HashSet<Booking>();
    }
}
