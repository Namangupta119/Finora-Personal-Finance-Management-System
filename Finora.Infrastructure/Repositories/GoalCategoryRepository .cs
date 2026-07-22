using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Finora.Infrastructure.Repositories
{
    public class GoalCategoryRepository : IGoalCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public GoalCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid categoryId)
        {
            return await _context.GoalCategories
                .AnyAsync(x => x.Id == categoryId && x.IsActive);
        }

        public async Task<IReadOnlyList<GoalCategory>> GetAllAsync()
        {
            return await _context.GoalCategories
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }
    }
}
