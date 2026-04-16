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
    }
}
