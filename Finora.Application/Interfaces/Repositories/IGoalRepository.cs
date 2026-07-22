using Finora.Application.Features.DTOs.Goal;
using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Repositories
{
    public interface IGoalRepository
    {
        Task<Goal?> GetByIdAsync(Guid goalId);

        Task<IReadOnlyList<Goal>> GetByUserIdAsync(Guid userId);

        Task AddAsync(Goal goal);

        Task UpdateAsync(Goal goal);

        Task<bool> ExistsByTitleAsync(Guid userId, string title);
        Task<IReadOnlyList<GoalListDto>> GetAllByUserIdAsync(Guid userId);
        Task<GoalDetailsDto?> GetByIdAsync(Guid goalId, Guid userId, CancellationToken cancellationToken = default);
        Task<Goal?> GetEntityByIdAsync(Guid goalId, Guid userId);

        Task<bool> ExistsByTitleAsync(Guid userId, string title, Guid excludeGoalId);
    }
}
