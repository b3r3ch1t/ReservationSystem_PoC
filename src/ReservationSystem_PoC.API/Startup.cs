using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReservationSystem_PoC.API.Configurations;
using ReservationSystem_PoC.Data;
using ReservationSystem_PoC.Data.IoC;
using ReservationSystem_PoC.Domain.Core.IoC;

namespace ReservationSystem_PoC.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            if (env.IsStaging() || env.IsProduction())
            {
                builder.AddEnvironmentVariables();
            }

            if (env.IsDevelopment())
            {

                //Try to use the userSecrets, if does not have a userSecrets use appsettings.json
                try
                {
                    builder.AddUserSecrets<Startup>();

                }
                catch
                {
                    builder.AddJsonFile("appsettings.json", true, true);

                }

            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(config =>
            {
                config.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers();

            // Swagger Config
            services.AddSwaggerConfiguration();

            // Adding MediatR for Domain Events and Notifications

            services.AddMediatRConfiguration();

            // AutoMapper Settings
            services.AddAutoMapperConfiguration();

            // Register Services for each Layer.
            RegisterServices(services, Configuration);
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterDomainCoreDependencies()
                .RegisterDataBaseSqlServer(configuration)
                .RegisterDataDependencies();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReservationSystem_PoC.API v1"));

                //Create database and seed some data

                app.UseSeedDatabase();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerSetup();
        }
    }
}
