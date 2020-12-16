// 10:55

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem_PoC.Domain.Core.Bus;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.DomainHandlers;
using ReservationSystem_PoC.Domain.Core.DomainHandlers.ReservationHandlers;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Bus;
using ReservationSystem_PoC.Domain.Core.Responses;

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
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            #endregion

            services
                .AddScoped<IRequestHandler<UpdateRankingOfReservationCommand, CommandResponse>,
                    ReservationCommandHandler>();

            services.AddScoped<IRequestHandler<CreateReservationCommand, CommandResponse>,
                ReservationCommandHandler>();

            return services;
        }
    }
}