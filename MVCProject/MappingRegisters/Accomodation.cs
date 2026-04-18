using Mapster;
using MVCProject.ViewModels.AccomodationViewModels;

namespace MVCProject.MappingRegisters
{
    public class Accomodation : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddAcccomodationVM, Accomodation>();
            config.NewConfig<Accomodation, AddAcccomodationVM>();

            config.NewConfig<DisplayAccomodationVM, Accomodation>();
            config.NewConfig<Accomodation, DisplayAccomodationVM>();

            config.NewConfig<EditAccomodationVM, Accomodation>();
            config.NewConfig<Accomodation, EditAccomodationVM>();
        }
    }
}
