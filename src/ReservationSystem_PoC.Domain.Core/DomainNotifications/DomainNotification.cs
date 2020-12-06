using MediatR;
using System;

namespace ReservationSystem_PoC.Domain.Core.DomainNotifications
{
    public class DomainNotification : INotification
    {
        public Guid DomainNotificationId { get; }
        public string Key { get; }
        public string Value { get; }

        public DomainNotification(string key, string value)
        {
            DomainNotificationId = Guid.NewGuid();
            Key = key;
            Value = value;
        }
    }
}
