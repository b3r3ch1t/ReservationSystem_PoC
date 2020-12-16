using MediatR;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.DomainHandlers
{

    public class DomainNotificationHandler : INotificationHandler<DomainNotification>, IDisposable
    {
        private List<DomainNotification> _notificationsError;
        private List<DomainNotification> _notificationsSuccess;


        public DomainNotificationHandler()
        {
            _notificationsError = new List<DomainNotification>();
            _notificationsSuccess = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            switch (message.TypeDomainNotification)
            {
                case TypeDomainNotification.Erro:
                    _notificationsError.Add(message);
                    break;
                case TypeDomainNotification.Success:
                    _notificationsSuccess.Add(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            return Task.CompletedTask;
        }

        public Dictionary<string, string[]> GetNotificationsErrorByKey()
        {
            var strings = _notificationsError.Select(s => s.DomainNotificationId).Distinct();
            var dictionary = new Dictionary<string, string[]>();
            foreach (var str in strings)
            {
                var key = str;
                dictionary[key.ToString()] = _notificationsError
                    .Where(w => w.DomainNotificationId == key)
                    .Select(s => s.Value)

                    .ToArray();
            }
            return dictionary;
        }


        public List<DomainNotification> GetNotificationsError()
        {
            return _notificationsError;
        }

        public List<DomainNotification> GetNotificationsSuccess()
        {
            return _notificationsSuccess;
        }

        public bool HasNotificationsError()
        {
            return GetNotificationsError().Any();
        }

        public bool HasNotificationsSucess()
        {
            return GetNotificationsSuccess().Any();
        }

        public void Dispose()
        {
            _notificationsError = new List<DomainNotification>();
            _notificationsSuccess = new List<DomainNotification>();
        }

        public void Clear()
        {
            _notificationsError = new List<DomainNotification>();
            _notificationsSuccess = new List<DomainNotification>();

        }
    }

}
