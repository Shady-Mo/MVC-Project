using MVCProject.Data;
using MVCProject.Models;
using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.AdminRepo
{
    public class UserRepository : BaseRepository<AppUser, string>, IUserRepository
    {
        private readonly AppDbContext context;
        public UserRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
