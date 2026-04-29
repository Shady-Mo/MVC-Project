using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Services.BaseService {
    public class SeedService {
        public static async Task SeedDatabase(IServiceProvider serviceProvider) {
            using (var scope = serviceProvider.CreateScope()) {
                AppDbContext _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                RoleManager<IdentityRole> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<AppUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                ILogger<SeedService> _logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

                try {
                    _logger.LogInformation("Ensuring the database is created.");
                    await _context.Database.MigrateAsync();

                    _logger.LogInformation("Seeding roles.");
                    await AddRoleAsync(_roleManager, "Admin");
                    await AddRoleAsync(_roleManager, "Seller");
                    await AddRoleAsync(_roleManager, "Customer");

                    _logger.LogInformation("Add admin email.");
                    string adminEmail = "admin@example.com";
                    string adminPassword = "Admin@123";
                    AppUser? user = await _userManager.FindByEmailAsync(adminEmail);

                    if (user == null) {
                        AppUser adminUser = new AppUser {
                            FullName = "Shady Mohamed",
                            Address = "Cairo",
                            Email = adminEmail,
                            EmailConfirmed = true,
                            NormalizedEmail = adminEmail.ToUpper(),
                            SecurityStamp = Guid.NewGuid().ToString(),
                            UserName = "Shady_Mo",
                            NormalizedUserName = "SHADY_MO",
                            PhoneNumber = "0123456789"
                        };

                        IdentityResult result = await _userManager.CreateAsync(adminUser, adminPassword);
                        
                        if (result.Succeeded) {
                            _logger.LogInformation("Assigning admin role to the admin user.");
                            await _userManager.AddToRoleAsync(adminUser, "Admin");
                        }
                        else {
                            _logger.LogError("Failed to create admin user: {Errors}",
                                string.Join(", ", result.Errors.Select(e => e.Description))
                            );
                        }
                    }
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }

        public static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName) {
            bool isRoleExist = await roleManager.RoleExistsAsync(roleName);

            if (!isRoleExist) {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(roleName));

                if (!result.Succeeded) {
                    throw new Exception($"Failed to create role {roleName} : {
                        string.Join(", ", result.Errors.Select(e => e.Description))
                    }");
                }
            }
        }
    }
}
