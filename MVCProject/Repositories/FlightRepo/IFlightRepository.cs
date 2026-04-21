using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.FlightRepo
{
    public interface IFlightRepository : IBaseRepository<Flight>
    {
        (IEnumerable<Flight> flights, int totalCount) GetAllWithFilterBy(string searchQuery, string destination, DateTime? date,
                                                                                                   int pageNumber, int pageSize);

        List<Flight> GetByLocation(string location, string location2, DateTime bookingDate);

    }
}