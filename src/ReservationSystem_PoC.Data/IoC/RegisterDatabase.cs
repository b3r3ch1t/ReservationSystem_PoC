using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem_PoC.Data.Context;

namespace ReservationSystem_PoC.Data.IoC
{
    public static class RegisterDatabase
    {
        public static IServiceCollection RegisterDataBaseSqlServe(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ReservarionSystemDbContext>
            (opt =>

                opt.UseSqlServer(connectionString)

            );

            return services;

        }

    }
}
