using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlogApp.Common.Extensions;
using BlogApp.Infrastructure.Repositories;
using BlogApp.Infrastructure.Repositories.Interfaces;
using BlogApp.Configurations;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BlogApp.Common.Constants;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MongoDB.Driver;
using BogApp.Entities;

namespace BlogApp
{
    public class Startup
    {
        private readonly IHostEnvironment _environment;

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = services.AddSettings(_configuration);

            services.ConfigureApi(appSettings);
            services.ConfigureAuthentication(appSettings);
            services.AddAuthorization();
            services.AddCors();
            services.ConfigureCors(appSettings);
            services.AddHttpClient();

            services.AddAutoMapper(typeof(Post));
            services.AddTransient<IMongoClient>(_ => new MongoClient(appSettings.DbConnectionString));
            services.AddTransient(serviceProvider =>
            {
                using var scope = serviceProvider.CreateScope();
                var client = scope.ServiceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(appSettings.DbName);
            });

            services.AddTransient<IPostRepository, PostRepository>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddLogging(x => x.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(_configuration).CreateLogger()));
            services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = $"{CommonConstants.SpaDevFolderName}/{CommonConstants.BuildFolderName}";
            });
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSpaStaticFiles();
            }
            app.UseHttpsRedirection();

            //Dev mode
            app.UseCors(builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod());


            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    option.SwaggerEndpoint($"{description.GroupName}/{CommonConstants.SwaggerEndpointFileName}", description.GroupName.ToUpperInvariant());
                }

                option.OAuthClientId(IdentityServerConstants.Clients.AppApiId);
                option.OAuthClientSecret(IdentityServerConstants.Clients.AppApiSecret.Base64Decode());
                option.OAuthScopeSeparator(IdentityServerConstants.ScopeSeparator);
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (_environment.IsDevelopment())
                    spa.Options.SourcePath = CommonConstants.SpaDevFolderName;
                else
                    spa.Options.SourcePath = CommonConstants.BuildFolderName;

                if (_environment.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
