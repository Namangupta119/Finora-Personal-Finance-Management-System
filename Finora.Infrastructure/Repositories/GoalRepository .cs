using Finora.Application.Features.DTOs.Goal;
using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Finora.Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly ApplicationDbContext _context;

        public GoalRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Goal goal)
        {
            await _context.Goals.AddAsync(goal);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByTitleAsync(Guid userId, string title)
        {
            var normalizedTitle = title.Trim().ToUpper();

            return await _context.Goals.AnyAsync(x =>
                x.UserId == userId &&
                !x.IsArchived &&
                x.Title.ToUpper() == normalizedTitle);
        }

        public async Task<IReadOnlyList<GoalListDto>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Goals
           .AsNoTracking()
           .Where(x => x.UserId == userId && !x.IsArchived)
           .OrderByDescending(x => x.CreatedOn)
           .Select(x => new GoalListDto
           {
               Id = x.Id,
               Title = x.Title,
               Description = x.Description,
               GoalCategoryName = x.GoalCategory.Name,
               TargetAmount = x.TargetAmount,
               CurrentAmount = x.GoalContributions
                   .Where(gc => !gc.IsArchived)
                   .Sum(gc => (decimal?)gc.Amount) ?? 0,
               Status = x.Status,
               TargetDate = x.TargetDate
           })
           .ToListAsync();
        }

        public async Task<Goal?> GetByIdAsync(Guid goalId)
        {
            return await _context.Goals
                            .FirstOrDefaultAsync(x => x.Id == goalId && !x.IsArchived);
        }

        public async Task<IReadOnlyList<Goal>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Goals.Where(x => x.UserId == userId && !x.IsArchived).OrderByDescending(x => x.CreatedOn).ToListAsync();
        }

        public async Task UpdateAsync(Goal goal)
        {
            _context.Goals.Update(goal);
            await _context.SaveChangesAsync();
        }

        public async Task<GoalDetailsDto?> GetByIdAsync(Guid goalId, Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Goals.AsNoTracking().Where(x => x.Id == goalId && x.UserId == userId && !x.IsArchived).Select(x => new GoalDetailsDto
            {
                Id = x.Id,
                GoalCategoryId = x.GoalCategoryId,
                GoalCategoryName = x.GoalCategory.Name,
                Title = x.Title,
                Description = x.Description,
                TargetAmount = x.TargetAmount,
                CurrentAmount = x.GoalContributions.Where(g => !g.IsArchived).Sum(g => (decimal?)g.Amount) ?? 0,
                Status = x.Status,
                TargetDate = x.TargetDate,
            }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Goal?> GetEntityByIdAsync(Guid goalId, Guid userId)
        {
            return await _context.Goals.FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId && !g.IsArchived);
        }

        public async Task<bool> ExistsByTitleAsync(Guid userId, string title, Guid excludeGoalId)
        {
            return await _context.Goals.AnyAsync(g => g.UserId == userId && !g.IsArchived && g.Id != excludeGoalId && g.Title == title);
        }
    }
}
