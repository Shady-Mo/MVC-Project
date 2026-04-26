using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MVCProject.Models;
using System.Security.Claims;

namespace MVCProject.Helpers {
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole> {
        public MyUserClaimsPrincipalFactory(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : 
            base(userManager, roleManager, options) {
            
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user) {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("PhoneNumber", user.PhoneNumber));

            return identity;
        }
    }
}