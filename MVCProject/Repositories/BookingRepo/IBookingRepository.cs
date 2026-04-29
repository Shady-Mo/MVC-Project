using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.BookingRepo
{
    public interface IBookingRepository: IBaseRepository<Booking, int>
    {
        (List<Booking> bookings, int TotalCount) GetAllWithFilterBy(string searchQuery, int pageNumber = 1, int pageSize = 6);
        (List<Booking> bookings, int TotalCount) GetAllWithFilterByUserId(string uderId, string searchQuery, int pageNumber = 1, int pageSize = 6);

        Booking GetByIdIncluded(int id);
    }
}
