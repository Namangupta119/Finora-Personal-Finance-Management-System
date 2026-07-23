using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Finora.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsArchived && (c.UserId == null || c.UserId == userId));
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync(Guid userId)
        {
            return await _context.Categories
            .Where(c => !c.IsArchived && (c.UserId == null || c.UserId == userId))
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.Name)
            .ToListAsync();
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task<bool> ExistsAsync(string name, Guid userId)
        {
            var normalizedName = name.Trim();

            return await _context.Categories.AnyAsync(c => !c.IsArchived && (c.UserId == null || c.UserId == userId) && c.Name == normalizedName);
        }
        public async Task<bool> ExistsByIdAsync(Guid categoryId, Guid userId)
        {
            return await _context.Categories.AnyAsync(c =>
        !c.IsArchived &&
        c.Id == categoryId &&
        (c.UserId == null || c.UserId == userId));
        }
    }
}
