using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models
{
    public class Flight: IComparable<Flight>
    {
        [Key]
        public int Id { get; set; }
        public string Airline { get; set; }
        public string DepartureAirport { get; set; }
        public string DestinationAirport { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }

        public ICollection<Booking> Bookings = new HashSet<Booking>();

        //public ICollection<BookingFlight> BookingFlights = new HashSet<BookingFlight>();

        public int CompareTo(Flight? other)
        {
            return this.DepartureDateTime.CompareTo(other.DepartureDateTime);
        }
    }
}
