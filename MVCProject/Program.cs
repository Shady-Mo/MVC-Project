using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Helpers;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.Services;
using MVCProject.Services.AccountService;
using MVCProject.Services.BaseService;
using MVCProject.Services.EmailService;
using MVCProject.Services.ImgAddingService;
using Stripe;
using System.Reflection;
using System.Security.Claims;
using AccountService = MVCProject.Services.AccountService.AccountService;
using FileService = MVCProject.Services.ImgAddingService.FileService;

namespace MVCProject {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("mvccon"))
            );

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<UnitOfWork>()
                .AddScoped<IFileService, FileService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IUserClaimsPrincipalFactory<AppUser>, MyUserClaimsPrincipalFactory>();

            builder.Services.AddOutputCache(options => {
                options.MaximumBodySize = 20 * 1024 * 1024;

                options.SizeLimit = 100 * 1024 * 1024;

                options.AddPolicy("GlobalExpiry", builder =>
                    builder.Expire(TimeSpan.FromMinutes(30))
                        .SetLocking(true)
                        .Tag("GlobalExpiry")
                );

                options.AddPolicy("GlobalExpiryWithFilteration", builder =>
                    builder.Expire(TimeSpan.FromMinutes(30))
                        .SetLocking(true)
                        .Tag("GlobalWithFilteration")
                        .SetVaryByQuery("location", "price")
                );

                options.AddPolicy("ProfileExpiry", builder =>
                    builder.Expire(TimeSpan.FromMinutes(10))
                        .VaryByValue(context => {
                            var identityCookie = context.Request.Cookies[".AspNetCore.Identity.Application"];

                            return new KeyValuePair<string, string>(
                                "userStatus", identityCookie ?? "Anonymous"
                            );
                        })
                );
            });

            builder.Services.Configure<IdentityOptions>(options => {
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

                options.Lockout.AllowedForNewUsers = true;
            });

            builder.Services.AddAuthentication()
                .AddGoogle(options => {
                    IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthSection["ClientId"];
                    options.ClientSecret = googleAuthSection["ClientSecret"];

                    options.Events.OnRemoteFailure = context => {
                        context.Response.Redirect("/Account/Login?remoteError=" + context.Failure.Message);
                        context.HandleResponse();

                        return Task.CompletedTask;
                    };
                }
            );

            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
            builder.Services.AddTransient<IEmailService, EmailService>();

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

            builder.Services.AddHttpClient();
            //builder.Services.AddHostedService<CacheWarmerService>();

            var app = builder.Build();

            await SeedService.SeedDatabase(app.Services);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
            }

            StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeSettings:SecretKey").Get<string>();

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
