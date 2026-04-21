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
            int pageNumber = 1, int pageSize = 10)
        {
            var query = context.Flights.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
                query = query.Where(f => f.Airline.Contains(searchQuery) ||
                                         f.DepartureAirport.Contains(searchQuery));

            if (!string.IsNullOrEmpty(destination))
                query = query.Where(f => f.DestinationAirport.Contains(destination));

            if (date.HasValue)
                query = query.Where(f => f.DepartureDateTime.Date == date.Value.Date);

            int totalCount = query.Count();

            var flights = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (flights, totalCount);
        }

        public List<Flight> GetByLocation(string location, string location2, DateTime bookingDate)
        {
            return _context.Flights.Where(f => (f.DepartureAirport == location || f.DepartureAirport == location2) && (f.DestinationAirport == location2 || f.DestinationAirport == location) && f.DepartureDateTime >= bookingDate).ToList();
        }
    }
}