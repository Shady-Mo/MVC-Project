using Mapster;
using MVCProject.ViewModels.AccomodationViewModels;
using MVCProject.ViewModels.BookingViewModel;

namespace MVCProject.MappingRegisters
{
    public class BookingRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Models.Booking, DisplayBookingVM>().Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.Flight, src => (src.Flight.Airline + $" from {src.Flight.DepartureAirport} to {src.Flight.DestinationAirport}"))
            .Map(dest => dest.FlightDate, src => ($"Departure {src.Flight.DepartureDateTime.ToString("dd MMM yyyy, hh:mm tt")},  Arrival {src.Flight.ArrivalDateTime.ToString("dd MMM yyyy, hh:mm tt")}"))
            .Map(dest => dest.AccommodationsCount, src => src.bookingAccomodations != null ? src.bookingAccomodations.Count : 0)
            .Map(dest => dest.ActivitiesCount, src => src.BookingActivities != null ? src.BookingActivities.Count : 0);
        }
    }
}
