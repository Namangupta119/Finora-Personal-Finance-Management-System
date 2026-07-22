using Finora.Application.Features.Budget.Queries.GetBudgets;
using Finora.Application.Features.Budget.Queries.GetBudgetVsActual;
using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Finora.Persistence.Seed.Categories;
using Microsoft.EntityFrameworkCore;

namespace Finora.Infrastructure.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public BudgetRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Budget budget)
        {
            await _context.Budgets.AddAsync(budget);
        }

        public async Task<bool> BudgetExistsAsync(Guid userId, Guid categoryId, int year, int month, Guid? excludedBudgetId = null)
        {
            return await _context.Budgets.AnyAsync(x => x.UserId == userId && x.CategoryId == categoryId && x.Year == year && x.Month == month && (!excludedBudgetId.HasValue || x.Id != excludedBudgetId.Value));
        }

        public Task DeleteAsync(Budget budget)
        {
            _context.Budgets.Remove(budget);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<BudgetDto>> GetAllBudgetAsync(Guid userId)
        {
            return await _context.Budgets.AsNoTracking().Include(x => x.Category).Where(x => x.UserId == userId).OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).Select(x => new BudgetDto
            {
                Id = x.Id,
                CategoryName = x.Category.Name,
                Amount = x.Amount,
                Year  = x.Year,
                Month = x.Month,
            }).ToListAsync();
        }

        public async Task<IReadOnlyList<BudgetVsActualDto>> GetBudgetVsActualAsync(Guid userId, int year, int month)
        {
            var expenseSummary = _context.Expenses.AsNoTracking().Where(e => e.UserId == userId && !e.IsArchived && e.ExpenseDate.Year == year && e.ExpenseDate.Month == month).GroupBy(e => e.CategoryId).Select(g => new
            {
                CategoryId = g.Key,
                ActualExpense = g.Sum(e => e.Amount)
            });

            var query = from budget in _context.Budgets.AsNoTracking()
                        where budget.UserId == userId && budget.Year == year && budget.Month == month
                        join expense in expenseSummary
                        on budget.CategoryId equals expense.CategoryId into expenseGroup

                        from expense in expenseGroup.DefaultIfEmpty()

                        select new BudgetVsActualDto
                        {
                            CategoryId = budget.CategoryId,
                            CategoryName = budget.Category.Name,
                            BudgetAmount = budget.Amount,
                            ActualExpense = expense == null ? 0 : expense.ActualExpense
                        };

                        return await query.ToListAsync();
        }

        public async Task<Budget?> GetByIdAsync(Guid budgetId, Guid userId)
        {
            return await _context.Budgets.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == budgetId && x.UserId == userId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Budget budget)
        {
            _context.Budgets.Update(budget);
            return Task.CompletedTask;
        }
    }
}
