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
        Task SaveChangesAsync();
    }
}
