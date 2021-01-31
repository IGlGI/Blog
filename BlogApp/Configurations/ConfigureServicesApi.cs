using BlogApp.Common.Constants;
using BlogApp.Swagger;
using BogApp.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Flurl;
using Flurl.Http;

namespace BlogApp.Configurations
{
    public static class ConfigureServicesApi
    {
        public static void ConfigureApi(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddApiVersioning(option =>
            {
                option.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = CommonConstants.ApiVersioningNameFormat;
                option.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(option =>
            {
                option.OperationFilter<SwaggerDefaultValues>();
                option.IncludeXmlComments(XmlCommentsFilePath);
                option.AddSecurityDefinition(AuthorizationConstants.OAuth2, new OpenApiSecurityScheme
                {
                    Name = IdentityServerConstants.Clients.AppApiName,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = appSettings.IdentityServerConnectionString.AppendPathSegment(IdentityServerConstants.TokenUrlRelativePath).ToUri()
                        }
                    }
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = AuthorizationConstants.OAuth2,
                                Type = ReferenceType.SecurityScheme,
                            },
                            Scheme = AuthorizationConstants.OAuth2,
                            Name = IdentityServerAuthenticationDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
