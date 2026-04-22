using Mapster;
using Microsoft.AspNetCore.Identity;
using MVCProject.Models;
using MVCProject.ViewModels.AccountViewModels;
using System.Security.Claims;

namespace MVCProject.MappingRegisters {
    public class AccountRegister : IRegister {
        public void Register(TypeAdapterConfig config) {
            /* Mapping AppUser Into RegisterViewModel */
            config.NewConfig<RegisterViewModel, AppUser>()
                .Map(d => d.FullName, s => s.FirstName + " " + s.LastName);

            /* Mapping ExternalLoginInfo Into AppUser */
            config.NewConfig<ExternalLoginInfo, ExternalLoginConfirmationViewModel>()
                .Map(d => d.Email, s => s.Principal.FindFirstValue(ClaimTypes.Email))
                .Map(d => d.FullName, s => s.Principal.FindFirstValue(ClaimTypes.Name));

            /* Mapping ExternalLoginConfirmationViewModel Into AppUser */
            config.NewConfig<ExternalLoginConfirmationViewModel, AppUser>()
                .Map(d => d.UserName, s => s.Email);
        }
    }
}
