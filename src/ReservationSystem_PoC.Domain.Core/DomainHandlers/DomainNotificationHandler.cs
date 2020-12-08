using MediatR;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.DomainHandlers
{

    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            _notifications.Add(message);
            return Task.CompletedTask;
        }

        public virtual Dictionary<string, string[]> GetNotificationsByKey()
        {
            var strings = _notifications.Select(s => s.DomainNotificationId).Distinct();
            var dictionary = new Dictionary<string, string[]>();
            foreach (var str in strings)
            {
                var key = str;
                dictionary[key.ToString()] = _notifications
                    .Where(w => w.DomainNotificationId == key)
                    .Select(s => s.Value)

                    .ToArray();
            }
            return dictionary;
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }

        public void Clear()
        {
            _notifications = new List<DomainNotification>();
        }
    }

}
