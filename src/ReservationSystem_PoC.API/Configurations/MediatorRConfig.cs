using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ReservationSystem_PoC.API.Configurations
{
    internal static class MediatRConfig
    {

        internal static void AddMediatRConfiguration(this IServiceCollection services)
        {
            //https://github.com/jbogard/MediatR

            services.AddMediatR(typeof(Startup));

        }
    }
}
