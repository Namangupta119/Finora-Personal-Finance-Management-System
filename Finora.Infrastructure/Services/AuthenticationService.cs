using Finora.Application.DTOs.Authentication;
using Finora.Application.Exceptions;
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
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthenticationService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Invalid email or password.");

            var result = _passwordHasher.VerifyPassword(user, request.Password, user.PasswordHash);

            if (!result)
                throw new UnauthorizedException("Invalid email or password.");

            if (!user.IsActive)
                throw new UnauthorizedException("Account is disabled.");

            var accessToken = _jwtService.GenerateAccessToken(user);

            var refreshToken = _jwtService.GenerateRefreshToken(user, "Web");

            user.RefreshTokens.Add(refreshToken);
            user.LastLoginOn = DateTimeOffset.UtcNow;

            await _userRepository.SaveChangesAsync();

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresOn = refreshToken.ExpiresOn,
                UserId = user.Id,
                Email = user.Email
            };
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var refreshToken = await _refreshTokenRepository.FindByTokenAsync(request.RefreshToken);

            if (refreshToken == null)
                throw new UnauthorizedException("No Token found");


            if (refreshToken.ExpiresOn <= DateTimeOffset.UtcNow)
                throw new UnauthorizedException("Your Token has Expired");

            if (refreshToken.RevokedOn != null)
                throw new UnauthorizedException("You token has revoked");

            var user = refreshToken.User;

            if (!user.IsActive)
                throw new UnauthorizedException("Your account has been disbled.");

            var accessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken(user, "Web");

            refreshToken.RevokedOn = DateTimeOffset.UtcNow;
            user.RefreshTokens.Add(newRefreshToken);

            await _refreshTokenRepository.SaveChangesAsync();

            return new RefreshTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken.Token,
                ExpiresOn = newRefreshToken.ExpiresOn
            };
                
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var emailExists = await _userRepository.ExistsByEmailAsync(request.Email);

            if (emailExists)
            {
                throw new ConflictException("Email already exists.");
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
