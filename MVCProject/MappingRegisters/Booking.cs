using Mapster;
using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.BookingViewModel;

namespace MVCProject.MappingRegisters
{
    public class Booking : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Models.Booking, DisplayBookingVM>().Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.Flight, src => (src.Flight.Airline + $"from {src.Flight.DepartureAirport} to {src.Flight.DestinationAirport}"))
            .Map(dest => dest.AccommodationsCount, src => src.bookingAccomodations != null ? src.bookingAccomodations.Count : 0)
            .Map(dest => dest.ActivitiesCount, src => src.BookingActivities != null ? src.BookingActivities.Count : 0); ;
        }
    }
}
