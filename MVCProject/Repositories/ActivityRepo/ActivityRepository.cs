using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MVCProject.Repositories.ActivityRepo
{
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        private readonly AppDbContext context;

        public ActivityRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public (List<Activity> Activities, int TotalCount) GetAllWithFilterBy(string searchQuery, decimal? maxPrice = null, int? minCapacity = null, int pageNumber = 1, int pageSize = 6)
        {
            var query = context.Activities.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(a => a.Name.Contains(searchQuery) || a.Location.Contains(searchQuery));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(a => a.Price <= maxPrice.Value);
            }

            if (minCapacity.HasValue)
            {
                query = query.Where(a => a.Capacity >= minCapacity.Value);
            }

            int totalCount = query.Count();
            var activities = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return (activities, totalCount);
        }


        public List<Activity> GetByLocation(string location)
        {
            return _context.Activities.Where(l => l.Location == location && l.Capacity > 0).ToList();
        }
    }
}
