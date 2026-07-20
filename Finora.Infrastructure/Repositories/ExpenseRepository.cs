using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
            _context.Expenses.Remove(expense);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Expense expense)
        {
            _context.Expenses.Update(expense);
        }
    }
}
