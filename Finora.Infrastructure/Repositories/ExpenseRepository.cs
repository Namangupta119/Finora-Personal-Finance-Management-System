using Finora.Application.Features.Dashboard.Queries.GetExpenseAnalytics;
using Finora.Application.Features.Dashboard.Queries.GetMonthlyIncomeExpense;
using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Finora.Infrastructure.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
        }

        public async Task<Expense?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Expenses.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id && !x.IsArchived && x.UserId == userId);
        }

        public async Task<IReadOnlyList<Expense>> GetExpensesAsync(Guid userId)
        {
            return await _context.Expenses.Include(x => x.Category).Where(x => !x.IsArchived && x.UserId == userId).OrderByDescending(x => x.ExpenseDate).ToListAsync();
        }

        public void Remove(Expense expense)
        {
            expense.IsArchived = true;

            _context.Expenses.Update(expense);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Expense expense)
        {
            _context.Expenses.Update(expense);
        }

        public async Task<decimal> GetTotalExpenseAsync(Guid userId)
        {
            return await _context.Expenses.Where(x => x.UserId == userId && !x.IsArchived).SumAsync(x => x.Amount);
        }

        public async Task<IReadOnlyList<Expense>> GetRecentExpensesAsync(Guid userId, int count)
        {
            return await _context.Expenses.Where(x => !x.IsArchived && x.UserId != userId).OrderByDescending(x => x.ExpenseDate).Take(count).ToListAsync();
        }

        public async Task<IReadOnlyList<ExpenseAnalyticsDto>> GetExpenseAnalyticsAsync(Guid userId)
        {
            return await _context.Expenses.Where(x => !x.IsArchived && x.UserId == userId).GroupBy(x => x.Category.Name).Select(g => new ExpenseAnalyticsDto
            {
                Category = g.Key,
                TotalAmount = g.Sum(x => x.Amount)
            }).OrderByDescending(x => x.TotalAmount).ToListAsync();
        }

        public async Task<IReadOnlyList<MonthlyAmountDto>> GetMonthlyExpenseAsync(Guid userId)
        {
            return await _context.Expenses.Where(x => !x.IsArchived && x.UserId == userId).GroupBy(x => new
            {
                x.ExpenseDate.Year,
                x.ExpenseDate.Month
            }).Select(g => new MonthlyAmountDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                TotalAmount = g.Sum(x => x.Amount)
            }).OrderBy(x => x.Year).ThenBy(x => x.Month).ToListAsync();
        }
    }
}
