using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Infrastructure.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext _context;

        public IncomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Income income)
        {
            await _context.Incomes.AddAsync(income);
        }

        public async Task<Income?> GetByIdAsync(Guid id, Guid userId)
        {
            return await _context.Incomes.FirstOrDefaultAsync(x => x.Id == id && !x.IsArchived && x.UserId == userId);

        }

        public async Task<IReadOnlyList<Income>> GetIncomesAsync(Guid userId)
        {
            return await _context.Incomes.Where(x => !x.IsArchived && x.UserId == userId).OrderByDescending(x => x.IncomeDate).ToListAsync();
        }

        public void Remove(Income income)
        {
            income.IsArchived = true;

            _context.Incomes.Update(income);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Income income)
        {
            _context.Incomes.Update(income);
        }
    }
}
