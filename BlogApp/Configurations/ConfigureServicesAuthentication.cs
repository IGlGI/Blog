using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.DependencyInjection;
using Flurl;
using Flurl.Http;
using BlogApp.Common.Constants;
using BlogApp.Common.Entities;

namespace BlogApp.Configurations
{
    public static class ConfigureServicesAuthentication
    {
        public static void ConfigureAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                option.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.Authority = appSettings.IdentityServerConnectionString;
                    option.Audience = appSettings.IdentityServerConnectionString.AppendPathSegment(IdentityServerConstants.ResourcesRelativePath);
                    option.TokenValidationParameters.ValidateAudience = false;
                })
                .AddCookie();
        }
    }
}
