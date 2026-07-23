using Finora.Application.Features.DTOs.GoalContribution;
using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Repositories
{
    public interface IGoalContributionRepository
    {
        Task<GoalContribution?> GetByIdAsync(Guid contributionId);

        Task<IReadOnlyList<GoalContribution>> GetByGoalIdAsync(Guid goalId);

        Task AddAsync(GoalContribution contribution);

        Task UpdateAsync(GoalContribution contribution);

        Task<decimal> GetTotalContributionAsync(Guid goalId);
        Task<IReadOnlyList<GoalContributionDto>> GetByGoalIdAsync(Guid goalId, Guid userId);
        Task<GoalContribution?> GetEntityByIdAsync(Guid contributionId, Guid userId);
    }
}
