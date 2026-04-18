using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.AccmodationRepo
{
    public interface IAccomodationRepositroy: IBaseRepository<Accomodation>
    {
        (List<Accomodation> accomodations, int TotalCount) GetAllWithFilterBy(string searchQuery, decimal? maxPrice = null, int? minCapacity = null, int pageNumber = 1, int pageSize = 6);
    }
}
