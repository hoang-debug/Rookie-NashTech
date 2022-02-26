using Day_11.Data.Entities;

namespace Day_11.Data.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
         Task<IEnumerable<T>> GetAllAsync();
         Task<T?> GetAsync(int id);
         Task<T> InsertAsync(T entity);
         Task<T> UpdateAsync(T entity);
         Task DeleteAsync(T entity);
        IEnumerable<T> GetAll();
        T? Get(int id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}