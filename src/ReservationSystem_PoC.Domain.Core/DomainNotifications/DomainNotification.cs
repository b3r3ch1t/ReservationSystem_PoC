using MediatR;
using ReservationSystem_PoC.Domain.Core.Enums;
using System;

namespace ReservationSystem_PoC.Domain.Core.DomainNotifications
{
    public class DomainNotification : INotification
    {
        public Guid DomainNotificationId { get; }
        public string StackTrace { get; }
        public string Value { get; }

        public TypeDomainNotification TypeDomainNotification { get; }

        private DomainNotification(TypeDomainNotification typeDomainNotification, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            StackTrace = Environment.StackTrace;
            Value = value;

            TypeDomainNotification = typeDomainNotification;
        }

        public static DomainNotification Success(string msg)
        {
            return new DomainNotification(TypeDomainNotification.Success, msg);
        }

        public static DomainNotification Fail(string msg)
        {
            return new DomainNotification(TypeDomainNotification.Erro, msg);
        }


    }
}
