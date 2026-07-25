using Finora.Application.Features.Budget.Queries.GetBudgets;
using Finora.Application.Features.Budget.Queries.GetBudgetVsActual;
using Finora.Domain.Entities;

namespace Finora.Application.Interfaces.Repositories
{
    public interface IBudgetRepository
    {
        Task AddAsync(Budget budget);
        Task UpdateAsync(Budget budget);
        Task DeleteAsync(Budget budget);
        Task<Budget?> GetByIdAsync(Guid budgetId, Guid userId);
        Task<IReadOnlyList<BudgetDto>> GetAllBudgetAsync(Guid userId);
        Task<bool> BudgetExistsAsync(Guid userId, Guid categoryId, int year, int month, Guid? excludedBudgetId = null);
        Task<IReadOnlyList<BudgetVsActualDto>> GetBudgetVsActualAsync(Guid userId, int year, int month);
        Task<Budget?> GetBudgetByCategoryAndMonthAsync(Guid userId,Guid categoryId,int year,int month);

        //Task SaveChangesAsync();
    }
}
