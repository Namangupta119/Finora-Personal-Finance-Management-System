using Finora.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Repositories
{
    public interface IInvestmentRepository
    {
        Task AddAsync(Investment investment);

        Task UpdateAsync(Investment investment);

        Task DeleteAsync(Investment investment);

        Task<Investment?> GetByIdAsync(Guid investmentId,Guid userId,CancellationToken cancellationToken);

        Task<List<Investment>> GetByUserIdAsync(Guid userId,int pageNumber,int pageSize,CancellationToken cancellationToken);

        Task<int> GetTotalCountAsync(Guid userId,CancellationToken cancellationToken);
        Task<List<Investment>> GetAllByUserIdAsync(Guid userId,CancellationToken cancellationToken);
    }
}
