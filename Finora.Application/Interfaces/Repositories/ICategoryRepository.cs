using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetCategoriesAsync(Guid userId);
        Task<Category?> GetByIdAsync(Guid id, Guid userId);
        Task AddAsync(Category category);
        void Update(Category category);
        void Remove(Category category);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(string name, Guid userId);
        Task<bool> ExistsByIdAsync(Guid categoryId, Guid userId);
    }
}
