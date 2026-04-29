using MVCProject.Repositories.BaseRepo;

namespace MVCProject.Repositories.AccountRepo {
    public interface IAccountRepository<T> : IBaseRepository<T, int> where T : class {

    }
}
