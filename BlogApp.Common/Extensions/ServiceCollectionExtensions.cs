using BlogApp.Common.Constants;
using BogApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BlogApp.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton(new AppSettings
            {
                AppConnectionString = configuration.GetSection(EnvironmentConstants.AppConnection)?.Value,
                DbConnectionString = configuration.GetSection(EnvironmentConstants.DbServerConnection)?.Value,
                DbName = configuration.GetSection(EnvironmentConstants.DbName)?.Value,
                IdentityServerConnectionString = configuration.GetSection(EnvironmentConstants.IdentityServerConnection)?.Value,
                AllowedCors = configuration.GetSection(CorsConstants.Section).GetSection(CorsConstants.Origins)?.Value?.Split(CorsConstants.Separator)?.ToList()
            });
    }
}
