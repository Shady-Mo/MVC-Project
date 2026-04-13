using Microsoft.AspNetCore.Identity;
using MVCProject.Data;

namespace MVCProject.Services {
    public class SeedService {
        public async Task SeedDatabase(IServiceProvider serviceProvider) {
            using (var scope = serviceProvider.CreateScope()) {
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            }
        }
    }
}
