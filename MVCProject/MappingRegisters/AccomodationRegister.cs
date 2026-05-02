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

            config.NewConfig<EditAccomodationVM, Accomodation>()
                .Map(dest => dest.Location, src => $"{src.City}, {src.Country}");

            config.NewConfig<Accomodation, EditAccomodationVM>()
                .Map(dest => dest.Country, src => src.Location != null && src.Location.Contains(",") 
                    ? src.Location.Substring(src.Location.LastIndexOf(",") + 1).Trim() 
                    : "")
                .Map(dest => dest.City, src => src.Location != null && src.Location.Contains(",") 
                    ? src.Location.Substring(0, src.Location.LastIndexOf(",")).Trim() 
                    : "");

            config.NewConfig<Accomodation, MostBookedAccommodationsViewModel>()
                .Map(d => d.BookingsHistory, s => s.bookingAccomodations.Count());
        }
    }
}
