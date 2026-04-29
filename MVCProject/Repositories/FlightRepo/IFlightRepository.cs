using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.FlightRepo
{
    public interface IFlightRepository : IBaseRepository<Flight, int>
    {
        (IEnumerable<Flight> flights, int totalCount) GetAllWithFilterBy(string searchQuery, string destination, DateTime? date,
                                                                                                   int pageNumber, int pageSize, string sortBy = "default");

        List<Flight> GetByLocation(string location, string location2, DateTime bookingDate);

    }
}