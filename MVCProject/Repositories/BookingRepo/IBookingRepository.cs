using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.BookingRepo
{
    public interface IBookingRepository: IBaseRepository<Booking>
    {
        (List<Booking> bookings, int TotalCount) GetAllWithFilterBy(string searchQuery, int pageNumber = 1, int pageSize = 6);
    }
}
