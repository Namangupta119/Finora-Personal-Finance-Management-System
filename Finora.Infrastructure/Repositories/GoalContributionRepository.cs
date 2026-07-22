using Finora.Application.Features.DTOs.GoalContribution;
using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Finora.Infrastructure.Repositories
{
    public class GoalContributionRepository : IGoalContributionRepository
    {
        private readonly ApplicationDbContext _context;

        public GoalContributionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GoalContribution?> GetByIdAsync(Guid contributionId)
        {
            return await _context.GoalContributions
                .FirstOrDefaultAsync(x => x.Id == contributionId && !x.IsArchived);
        }

        public async Task<IReadOnlyList<GoalContribution>> GetByGoalIdAsync(Guid goalId)
        {
            return await _context.GoalContributions
                .Where(x => x.GoalId == goalId && !x.IsArchived)
                .OrderByDescending(x => x.ContributionDate)
                .ToListAsync();
        }

        public async Task AddAsync(GoalContribution contribution)
        {
            await _context.GoalContributions.AddAsync(contribution);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GoalContribution contribution)
        {
            _context.GoalContributions.Update(contribution);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalContributionAsync(Guid goalId)
        {
            return await _context.GoalContributions
                .Where(x => x.GoalId == goalId && !x.IsArchived)
                .SumAsync(x => x.Amount);
        }

        public async Task<IReadOnlyList<GoalContributionDto>> GetByGoalIdAsync(Guid goalId, Guid userId)
        {
            return await _context.GoalContributions
                .AsNoTracking()
                .Where(gc =>
                    gc.GoalId == goalId &&
                    !gc.IsArchived &&
                    gc.Goal.UserId == userId &&
                    !gc.Goal.IsArchived)
                .OrderByDescending(gc => gc.ContributionDate)
                .Select(gc => new GoalContributionDto
                {
                    Id = gc.Id,
                    Amount = gc.Amount,
                    ContributionDate = gc.ContributionDate,
                    Notes = gc.Notes
                })
                .ToListAsync();
        }
    }
}
