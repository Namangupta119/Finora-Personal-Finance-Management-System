using Finora.Domain.Entities;

namespace Finora.Application.Interfaces.Repositories
{
    public interface IGoalCategoryRepository
    {
        Task<bool> ExistsAsync(Guid categoryId);

        Task<IReadOnlyList<GoalCategory>> GetAllAsync();
    }
}
