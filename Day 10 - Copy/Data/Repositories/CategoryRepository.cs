using Day_11.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Day_11.Data.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllIncludedAsync();
    }
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAllIncludedAsync()
        {
            return await _entities.Include(c => c.Products).ToListAsync();
        }
    }
}