using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.BookingRepo
{
    public class BookingRepository : BaseRepository<Booking, int>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context)
        {
        }

        public (List<Booking> bookings, int TotalCount) GetAllWithFilterBy(string searchQuery, int pageNumber = 1, int pageSize = 6)
        {
            var query = _context.Bookings.AsQueryable().Include(b => b.Flight).Include(b=>b.bookingAccomodations).Include(b=> b.BookingActivities);


            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(a => a.Country.Contains(searchQuery) || a.AppUser.UserName.Contains(searchQuery)).Include(b => b.Flight).Include(b => b.bookingAccomodations).Include(b => b.BookingActivities); ;
            }

            var accomodations = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return (accomodations, query.Count());
        }

        public (List<Booking> bookings, int TotalCount) GetAllWithFilterByUserId(string userId, string searchQuery, int pageNumber = 1, int pageSize = 6)
        {
            var query = _context.Bookings.Where(b => b.UserId == userId).AsQueryable().Include(b => b.Flight).Include(b => b.bookingAccomodations).Include(b => b.BookingActivities);


            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(b => b.UserId == userId && b.Country.Contains(searchQuery) || b.AppUser.UserName.Contains(searchQuery)).Include(b => b.Flight).Include(b => b.bookingAccomodations).Include(b => b.BookingActivities); ;
            }

            var accomodations = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return (accomodations, query.Count());
        }

        public Booking GetByIdIncluded(int id)
        {
            return _context.Bookings.Include(b => b.Flight).Include(b => b.bookingAccomodations).Include(b => b.BookingActivities).FirstOrDefault(b => b.Id == id);
        }

        public List<Booking> GetBySellerId(string sellerId)
        {
            return _context.Bookings.Include(b => b.bookingAccomodations).ThenInclude(a => a.Accomodation)
                    .Include(b => b.BookingActivities).ThenInclude(a => a.Activity)
                    .Include(b => b.Flight).Where(b => b.bookingAccomodations.Any(ba => ba.Accomodation.SellerId == sellerId) || b.BookingActivities.Any(ba => ba.Activity.SellerId == sellerId) || b.Flight.SellerId == sellerId).ToList();
        }
    }
}
