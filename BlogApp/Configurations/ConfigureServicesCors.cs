using BlogApp.Common.Constants;
using BlogApp.Common.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BlogApp.Configurations
{
    public static class ConfigureServicesCors
    {
        public static void ConfigureCors(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsConstants.Policy, builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    if (appSettings.AllowedCors != null && appSettings.AllowedCors.Any())
                    {
                        if (appSettings.AllowedCors.Contains(CorsConstants.AllowOrigins))
                        {
                            builder.AllowAnyHeader();
                            builder.AllowAnyMethod();
                            builder.SetIsOriginAllowed(host => true);
                            builder.AllowCredentials();
                        }
                        else
                        {
                            foreach (var origin in appSettings.AllowedCors)
                            {
                                builder.WithOrigins(origin);
                            }
                        }
                    }
                });
            });
        }
    }
}
