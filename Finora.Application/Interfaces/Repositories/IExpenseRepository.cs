using Finora.Application.Features.Dashboard.Queries.GetExpenseAnalytics;
using Finora.Application.Features.Dashboard.Queries.GetMonthlyIncomeExpense;
using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Repositories
{
    public interface IExpenseRepository
    {
        Task<IReadOnlyList<Expense>> GetExpensesAsync(Guid userId);
        Task<Expense?> GetByIdAsync(Guid id, Guid userId);
        Task AddAsync(Expense expense);
        void Update(Expense expense);
        void Remove(Expense expense);
        Task<decimal> GetTotalExpenseAsync(Guid userId);
        Task<IReadOnlyList<Expense>> GetRecentExpensesAsync(Guid userId, int count);
        Task<IReadOnlyList<ExpenseAnalyticsDto>> GetExpenseAnalyticsAsync(Guid userId);
        Task<IReadOnlyList<MonthlyAmountDto>> GetMonthlyExpenseAsync(Guid userId);
        //Task SaveChangesAsync();
    }
}
