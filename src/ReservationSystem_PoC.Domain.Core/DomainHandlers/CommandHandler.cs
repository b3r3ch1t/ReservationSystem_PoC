using MediatR;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Bus;
using System;

namespace ReservationSystem_PoC.Domain.Core.DomainHandlers
{
    public class CommandHandler : IDisposable
    {

        protected readonly IMediatorHandler MediatorHandler;
        protected readonly DomainNotificationHandler Notifications;

        public CommandHandler(IDependencyResolver dependencyResolver)
        {

            Notifications = (DomainNotificationHandler)dependencyResolver.Resolve<INotificationHandler<DomainNotification>>();
            MediatorHandler = dependencyResolver.Resolve<IMediatorHandler>();
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
