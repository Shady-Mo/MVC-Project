using Microsoft.EntityFrameworkCore;
using MVCProject.Data;

namespace MVCProject.Repositories.BaseRepo {
    public class BaseRepository<T> : IBaseRepository<T> where T : class {
        public readonly AppDbContext _context;
        public readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context) {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity) {
            _dbSet.Add(entity);
        }

        public void Delete(int id) {
            var item = GetById(id);

            if (item != null)
                _dbSet.Remove(item);
        }

        public IQueryable<T> GetAll() {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public T GetById(int id) {
            return _dbSet.Find(id);
        }

        public void Save() {
            _context.SaveChanges();
        }

        public void Update(T item) {
            _dbSet.Update(item);
        }
    }
}
