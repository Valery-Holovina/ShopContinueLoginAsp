using Microsoft.EntityFrameworkCore;

namespace Shop_P41.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAllAsync();
        public Task<Category> CreateAsync(Category category);
        public Task<Category> GetByIdAsync(int id);
        public Task<Category> UpdateAsync(int id, Category category);
        public Task<Category> DeleteAsync(int id);

    }
    public class CategoryService : ICategoryService
    {
        private readonly ShopContext _context;
        public CategoryService(ShopContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            if(category != null)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            throw new Exception("Category is null");
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return category;
            }
            throw new Exception("Category not found");
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _context.Categories.ToListAsync();

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                return category;
            }
            throw new Exception("Category not found ...");
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            var category_for_update = await _context.Categories.FindAsync(id);
            if (category_for_update != null)
            {
                category_for_update.Name = category.Name;
                await _context.SaveChangesAsync();
                return category_for_update;
            }
            throw new Exception("Category not found ...");
        }
    }
}