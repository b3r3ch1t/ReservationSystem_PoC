using ReservationSystem_PoC.Domain.Core.Bus;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Responses;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.Interfaces.Bus
{
    public interface IMediatorHandler
    {
        Task<CommandResponse> SendCommandAsync<T>(T command) where T : Command;
        Task RaiseEventAsync<T>(T @event) where T : Event;

        Task NotifyDomainNotification<T>(T domainNotification) where T : DomainNotification;
    }
}
