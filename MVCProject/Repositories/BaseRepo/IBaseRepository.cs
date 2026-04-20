namespace MVCProject.Repositories.BaseRepo {
    public interface IBaseRepository<T> where T : class {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        IQueryable<T> GetAll();
        T GetById(int id);
        void Save();
    }
}
