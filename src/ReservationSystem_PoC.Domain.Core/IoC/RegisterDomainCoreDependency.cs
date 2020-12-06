// 10:55

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem_PoC.Domain.Core.Bus;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Bus;

namespace ReservationSystem_PoC.Domain.Core.IoC
{
    public static class RegisterDomainCoreDependency
    {
        public static IServiceCollection RegisterDomainCoreDependencies(
            this IServiceCollection services)
        {

            services.AddScoped<IDependencyResolver, DependencyResolver>();


            #region Domain Bus (Mediator)

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddTransient<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            #endregion

            return services;
        }
    }
}