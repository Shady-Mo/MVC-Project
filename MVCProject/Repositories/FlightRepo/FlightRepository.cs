using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.FlightRepo
{
    public class FlightRepository : BaseRepository<Flight>, IFlightRepository
    {
        private readonly AppDbContext context;

        public FlightRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public (IEnumerable<Flight> flights, int totalCount) GetAllWithFilterBy(
    string searchQuery, string destination, DateTime? date,
    int pageNumber, int pageSize, string sortBy = "default")
        {
            var query = context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
                query = query.Where(f => f.Airline.Contains(searchQuery) ||
                                         f.DepartureAirport.Contains(searchQuery));

            if (!string.IsNullOrEmpty(destination))
                query = query.Where(f => f.DestinationAirport.Contains(destination));
            // never show departed flights
            query = query.Where(f => f.DepartureDateTime > DateTime.Now);

            if (date.HasValue)
                query = query.Where(f => f.DepartureDateTime.Date == date.Value.Date);

            // sorting
            query = sortBy switch
            {
                "price-low" => query.OrderBy(f => f.Price),
                "price-high" => query.OrderByDescending(f => f.Price),
                "date" => query.OrderBy(f => f.DepartureDateTime),
                _ => query
            };

            int totalCount = query.Count();
            var flights = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (flights, totalCount);
        }



        public List<Flight> GetByLocation(string location, string location2, DateTime bookingDate)
        {
            return _context.Flights.Where(f => (f.DepartureAirport == location || f.DepartureAirport == location2) && (f.DestinationAirport == location2 || f.DestinationAirport == location) && f.DepartureDateTime >= bookingDate && f.AvailableSeats > 0).ToList();
        }
    }
}