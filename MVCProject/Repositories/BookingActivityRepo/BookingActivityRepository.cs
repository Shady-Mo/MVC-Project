using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.BookingActivityRepo
{
    public class BookingActivityRepository : BaseRepository<BookingActivity>, IBookingActivityRepository
    {
        public BookingActivityRepository(AppDbContext context) : base(context)
        {
        }

       
    }
}
