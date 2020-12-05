using Microsoft.Extensions.DependencyInjection;

namespace ReservationSystem_PoC.Data.IoC
{
    public static class RegisterDependencies
    {

        public static IServiceCollection RegisterDataDependencies(
            this IServiceCollection services)
        {

            return services;
        }
    }
}
