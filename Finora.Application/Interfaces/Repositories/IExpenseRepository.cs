using Finora.Application.Features.Dashboard.Queries.GetExpenseAnalytics;
using Finora.Application.Features.Dashboard.Queries.GetMonthlyIncomeExpense;
using Finora.Application.Features.Reports.Queries.GetCategoryWiseExpenseReport;
using Finora.Application.Features.Reports.Queries.GetMonthlyExpenseReport;
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
        Task<bool> ExistsRecurringExpenseAsync(Guid recurringTransactionId, DateTimeOffset occurrenceDate, CancellationToken cancellationToken = default);
        Task<decimal> GetTotalExpenseAsync(Guid userId,Guid categoryId,int year,int month);
        Task<decimal> GetTotalExpenseAsync(Guid userId,CancellationToken cancellationToken);
        Task<List<Expense>> GetExpensesByYearAsync(Guid userId,int year,CancellationToken cancellationToken);
        Task<List<MonthlyExpenseReportDto>> GetMonthlyExpenseReportAsync(Guid userId,int year,CancellationToken cancellationToken);
        Task<List<CategoryWiseExpenseReportDto>> GetCategoryWiseExpenseReportAsync(Guid userId,int year,CancellationToken cancellationToken);
        Task<decimal> GetTotalExpenseByDateRangeAsync(Guid userId,DateTimeOffset startDate,DateTimeOffset endDate,CancellationToken cancellationToken);
        Task<int> GetExpenseTransactionCountByDateRangeAsync(Guid userId,DateTimeOffset startDate,DateTimeOffset endDate,CancellationToken cancellationToken);
    }
}
