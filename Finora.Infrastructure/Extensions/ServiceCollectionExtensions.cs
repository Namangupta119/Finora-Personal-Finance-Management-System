using Finora.Application.Interfaces.Repositories;
using Finora.Application.Interfaces.Security;
using Finora.Application.Interfaces.Services;
using Finora.Infrastructure.Repositories;
using Finora.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //services
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            //Security
            services.AddScoped<IPasswordHasher, IPasswordHasher>();
            services.AddScoped<IJwtService, IJwtService>();

            return services;
        }
    }
}
