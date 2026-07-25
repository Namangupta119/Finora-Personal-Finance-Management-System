using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Finora.Infrastructure.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly ApplicationDbContext _context;

        public InvestmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Investment investment)
        {
            await _context.Investments.AddAsync(investment);
        }

        public Task UpdateAsync(Investment investment)
        {
            _context.Investments.Update(investment);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Investment investment)
        {
            investment.IsArchived = true;
            return Task.CompletedTask;
        }

        public async Task<Investment?> GetByIdAsync(Guid investmentId,Guid userId,CancellationToken cancellationToken)
        {
            return await _context.Investments
                .FirstOrDefaultAsync(x =>
                    x.Id == investmentId &&
                    x.UserId == userId &&
                    !x.IsArchived,
                    cancellationToken);
        }

        public async Task<List<Investment>> GetByUserIdAsync(Guid userId,int pageNumber,int pageSize,CancellationToken cancellationToken)
        {
            return await _context.Investments.AsNoTracking()
                .Where(x =>
                    x.UserId == userId &&
                    !x.IsArchived)
                .OrderByDescending(x => x.PurchaseDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetTotalCountAsync(Guid userId,CancellationToken cancellationToken)
        {
            return await _context.Investments.CountAsync(x =>
                    x.UserId == userId &&
                    !x.IsArchived,
                    cancellationToken);
        }

        public async Task<List<Investment>> GetAllByUserIdAsync(Guid userId,CancellationToken cancellationToken)
        {
            return await _context.Investments
                .AsNoTracking()
                .Where(x => x.UserId == userId && !x.IsArchived)
                .ToListAsync(cancellationToken);
        }
    }
}
