using Mapster;
using MVCProject.Models;
using MVCProject.ViewModels.UserViewModels;

namespace MVCProject.MappingRegisters {
    public class UserRegister : IRegister {
        public void Register(TypeAdapterConfig config) {

            config.NewConfig<AddUserVM, AppUser>()
            .Map(dest => dest.UserName, src => src.Email)
            .Ignore(dest => dest.PasswordHash);

            config.NewConfig<EditUserVM, AppUser>()
                .Map(dest => dest.UserName, src => src.Email)
                .Ignore(dest => dest.PasswordHash)
                .Ignore(dest => dest.Id);
        }
    }
}
