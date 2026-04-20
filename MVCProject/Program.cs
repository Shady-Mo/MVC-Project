using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.Services;
using MVCProject.Services.ImgAddingService;
using System.Reflection;
using System.Security.Claims;

namespace MVCProject {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("mvccon"))
            );

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddOutputCache(options => {
                options.AddPolicy("GlobalExpiry", builder =>
                    builder.Expire(TimeSpan.FromMinutes(60))
                        .SetLocking(true)
                        .Tag("Global")
                );

                options.AddPolicy("PrivateExpiry", builder =>
                    builder.Expire(TimeSpan.FromMinutes(1))
                        .VaryByValue(context =>
                            new KeyValuePair<string, string>(
                                "user_id", context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "anonymous"
                            )
                        )
                );
            });

            builder.Services.Configure<IdentityOptions>(options => {
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

                options.Lockout.AllowedForNewUsers = true;
            });

            var app = builder.Build();

            await SeedService.SeedDatabase(app.Services);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseOutputCache();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
