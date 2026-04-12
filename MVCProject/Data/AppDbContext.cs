using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Data {
    public class AppDbContext : IdentityDbContext<AppUser> {
    }
}
