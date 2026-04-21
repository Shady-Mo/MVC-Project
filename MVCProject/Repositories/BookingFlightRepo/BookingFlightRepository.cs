using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.BookingFlightRepo
{
    public class BookingFlightRepository : BaseRepository<BookingFlight>, IBookingFlightRepository
    {
        public BookingFlightRepository(AppDbContext context) : base(context)
        {
        }
    }
}
