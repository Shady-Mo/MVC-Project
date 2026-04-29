using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.BookingAccomodationRepo
{
    public class BookingAccomodationRepository : BaseRepository<BookingAccomodation, int>, IBookingAccomodationRepository
    {
        public BookingAccomodationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
