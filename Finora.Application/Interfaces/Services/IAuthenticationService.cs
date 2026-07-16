using Finora.Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RefreshTokenRequest> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
