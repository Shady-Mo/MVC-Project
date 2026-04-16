using MVCProject.Data;

namespace MVCProject.Repositories.BaseRepo {
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Add(T item)
        {
            context.Set<T>().Add(item);
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
                context.Set<T>().Remove(item);
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            context.Set<T>().Update(item);
        }
    }
}
