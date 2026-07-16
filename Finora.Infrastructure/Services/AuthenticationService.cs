using Finora.Application.DTOs.Authentication;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Security;
using Finora.Application.Interfaces.Services;
using Finora.Domain.Entities.Identity;
using Finora.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public AuthenticationService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshTokenRequest> RefreshTokenAsync(RefreshTokenRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var emailExists = await _userRepository.ExistsByEmailAsync(request.Email);

            if (emailExists)
            {
                throw new Exception("An account with this email already exists.");
            }

            var user = new User
            {
                Email = request.Email,
                EmailConfirmed = false,
                IsActive = true,

                Profile = new Profile
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                },

                UserSetting = new UserSetting
                {
                    Language = "en",
                    Currency = "INR",
                    TimeZone = "Asia/Kolkata",
                    Theme = Theme.Light,
                    DateFormat = "dd/MM/yyyy",
                    NumberFormat = "en-IN"
                }
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            await _userRepository.AddAsync(user);

            await _userRepository.SaveChangesAsync();

            return new RegisterResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Message = "Registration completed successfully."
            };
        }
    }
}
