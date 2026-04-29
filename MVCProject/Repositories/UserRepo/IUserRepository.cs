using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.AdminRepo
{
    public interface IUserRepository : IBaseRepository<AppUser, string>
    {
    }
}
