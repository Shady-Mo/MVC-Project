//using Microsoft.EntityFrameworkCore;
//using MVCProject.Data;
//using MVCProject.Models;
//using MVCProject.Repositories.BaseRepo;

//namespace MVCProject.Repositories.BookingFlightRepo
//{
//    public class BookingFlightRepository : BaseRepository<BookingFlight>, IBookingFlightRepository
//    {
//        public BookingFlightRepository(AppDbContext context) : base(context)
//        {
//        }

//        public List<BookingFlight> GetByBookingId(int bookingId)
//        {
//            return _context.BookingFlights.Where(bf => bf.BookingId == bookingId).Include(bf => bf.Flight).ToList();
//        }
//    }
//}
