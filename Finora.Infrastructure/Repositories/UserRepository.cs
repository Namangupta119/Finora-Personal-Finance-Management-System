using Finora.Application.Interfaces.Repositories;
using Finora.Domain.Entities.Identity;
using Finora.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Finora.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.Include(x => x.Profile).Include(x => x.UserSetting).Include(x => x.RefreshTokens).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
