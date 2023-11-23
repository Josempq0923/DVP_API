using App.Config.Helpers;
using App.Domain.Interfaces;
using App.Domain.Services;
using App.Infrastructure.Interfaces;
using App.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace App.Dependences.IoC
{
    public static class DependecyContainer
    {
        public static IServiceCollection DependencyInjections(this IServiceCollection services)
        {
            #region DependecyInjections

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUsersService, UserService>();

            services.AddScoped<IPersonsRepository, PersonsRepository>();
            services.AddScoped<IPersonsService, PersonsService>();
            #endregion

            return services;
        }
    }
}
