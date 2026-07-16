using Finora.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
