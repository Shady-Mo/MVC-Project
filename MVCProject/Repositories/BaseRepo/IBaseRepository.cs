namespace MVCProject.Repositories.BaseRepo {
    public interface IBaseRepository<T, TKey> where T : class {
        void Add(T item);
        void Update(T item);
        void Delete(TKey id);
        IQueryable<T> GetAll();
        T GetById(TKey id);
        void Save();
    }
}
