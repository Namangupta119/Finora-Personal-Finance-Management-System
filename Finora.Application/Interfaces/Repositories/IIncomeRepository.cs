using Finora.Application.Features.Dashboard.Queries.GetMonthlyIncomeExpense;
using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Repositories
{
    public interface IIncomeRepository
    {
        Task<IReadOnlyList<Income>> GetIncomesAsync(Guid userId);
        Task<Income?> GetByIdAsync(Guid id, Guid userId);
        Task AddAsync(Income income);
        void Update(Income income);
        void Remove(Income income);
        Task<decimal> GetTotalIncomeAsync(Guid userId);
        Task<IReadOnlyList<Income>> GetRecentIncomesAsync(Guid userId, int count);
        Task<IReadOnlyList<MonthlyAmountDto>> GetMonthlyIncomeAsync(Guid userId);
        Task<bool> ExistsRecurringIncomeAsync(Guid recurringTransactionId,DateTimeOffset occurrenceDate,CancellationToken cancellationToken = default);
    }
}
