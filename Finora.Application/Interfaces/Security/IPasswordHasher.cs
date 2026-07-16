using Finora.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string password, string passwordHash);
    }
}
