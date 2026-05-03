using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.ActivityRepo
{
    public interface IActivityRepository: IBaseRepository<Activity, int>
    {
        (List<Activity> Activities, int TotalCount) GetAllWithFilterBy(string searchQuery, decimal? maxPrice = null, int? minCapacity = null, int pageNumber = 1, int pageSize = 6);
        (List<Activity> Activities, int TotalCount) GetAllWithFilterBySellerId(string sellerId, string searchQuery, decimal? maxPrice = null, int? minCapacity = null, int pageNumber = 1, int pageSize = 6);
        List<Activity> GetByLocation(string location);

        List<Activity> GetLatestFiveBySellerId(string sellerId);

        int ConuntBySeller(string sellerId);

    }
}
