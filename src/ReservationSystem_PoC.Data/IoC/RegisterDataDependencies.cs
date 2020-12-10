using Microsoft.Extensions.DependencyInjection;
using ReservationSystem_PoC.Data.Repositories;
using ReservationSystem_PoC.Domain.Core.Repositories;

namespace ReservationSystem_PoC.Data.IoC
{
    public static class RegisterDependencies
    {

        public static IServiceCollection RegisterDataDependencies(
            this IServiceCollection services)
        {

            services.AddScoped<IContactTypeRepository, ContactTypeRepository>();


            services.AddScoped<IReservationRepository, ReservationRepository>();

            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
