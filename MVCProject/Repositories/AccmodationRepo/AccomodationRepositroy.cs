using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.AccmodationRepo
{
    public class AccomodationRepositroy : BaseRepository<Accomodation>, IAccomodationRepositroy
    {
        public AccomodationRepositroy(AppDbContext context) : base(context)
        {
        }

        public (List<Accomodation> accomodations, int TotalCount) GetAllWithFilterBy(string searchQuery, decimal? maxPrice = null, int? minCapacity = null, int pageNumber = 1, int pageSize = 6)
        {
            var query = _context.Accomodations.AsQueryable();
            

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(a => a.Name.Contains(searchQuery) || a.Location.Contains(searchQuery));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(a => a.PricePerNight <= maxPrice.Value);
            }

            if (minCapacity.HasValue)
            {
                query = query.Where(a => a.AvailableRooms >= minCapacity.Value);
            }

            var accomodations = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return (accomodations, query.Count());
        }

        public List<Accomodation> GetByLocation(string location)
        {
            return _context.Accomodations.Where(l => l.Location == location).ToList();
        }
    }
}
