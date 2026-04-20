using Mapster;
using MVCProject.Models;
using MVCProject.ViewModels.AuthViewModels;

namespace MVCProject.MappingRegisters {
    public class AuthRegister : IRegister {
        public void Register(TypeAdapterConfig config) {
            /* Mapping AppUser Into RegisterViewModel */
            config.NewConfig<RegisterViewModel, AppUser>()
                .Map(d => d.FullName, s => s.FirstName + " " + s.LastName);
        }
    }
}
