using Finora.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> FindByTokenAsync(string token);
        Task SaveChangesAsync();
    }
}
