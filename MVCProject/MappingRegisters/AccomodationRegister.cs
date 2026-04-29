using Mapster;
using MVCProject.Models;
using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.HomeViewModels;

namespace MVCProject.MappingRegisters {
    public class AccommodationRegister : IRegister {
        public void Register(TypeAdapterConfig config) {
            config.NewConfig<AddAcccomodationVM, Accomodation>();
            config.NewConfig<Accomodation, AddAcccomodationVM>();

            config.NewConfig<DisplayAccomodationVM, Accomodation>();

            config.NewConfig<Accomodation, DisplayAccomodationVM>()
                .Map(dest => dest.SellerName, src => (src.Seller != null) ? src.Seller.FullName : "");

            config.NewConfig<EditAccomodationVM, Accomodation>();
            config.NewConfig<Accomodation, EditAccomodationVM>();

            config.NewConfig<Accomodation, MostBookedAccommodationsViewModel>()
                .Map(d => d.BookingsHistory, s => s.bookingAccomodations.Count());
        }
    }
}
