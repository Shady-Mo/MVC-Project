using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.ActivityRepo
{
    public interface IActivityRepository: IBaseRepository<Activity>
    {
        (List<Activity> Activities, int TotalCount) GetAllWithFilterBy(string searchQuery, decimal? maxPrice = null, int? minCapacity = null, int pageNumber = 1, int pageSize = 6);
    }
}
