using Finora.Application.Common.Settings;
using Finora.Application.Interfaces;
using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Security;
using Finora.Application.Interfaces.Services;
using Finora.Infrastructure.BackgroundServices;
using Finora.Infrastructure.Persistence;
using Finora.Infrastructure.Repositories;
using Finora.Infrastructure.Security;
using Finora.Infrastructure.Services;
using Finora.Persistence.Seed;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IBudgetRepository, BudgetRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IGoalCategoryRepository, GoalCategoryRepository>();
            services.AddScoped<IGoalContributionRepository, GoalContributionRepository>();
            services.AddScoped<IRecurringTransactionRepository, RecurringTransactionRepository>();
            services.AddScoped<IRecurringTransactionService, RecurringTransactionService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHostedService<RecurringTransactionProcessor>();

            //Security
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ApplicationDbSeeder>();

            services.AddHttpContextAccessor();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            //current user
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
