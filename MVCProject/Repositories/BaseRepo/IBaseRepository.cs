namespace MVCProject.Repositories.BaseRepo {
    public interface IBaseRepository<T> where T : class {
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        List<T> GetAll();
        T GetById(int id);
    }
}
