using Finora.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Security
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(User user, string deviceName);
    }
}
