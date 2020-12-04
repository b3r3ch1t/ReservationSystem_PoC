using MediatR;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Bus;
using ReservationSystem_PoC.Domain.Core.Responses;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.Bus
{
    public sealed class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IDependencyResolver dependencyResolver)
        {
            _mediator = dependencyResolver.Resolve<IMediator>();
        }

        public Task<CommandResponse> SendCommandAsync<T>(T command) where T : Command
        {
            var result = _mediator.Send(command);
            return result;

        }

        public Task RaiseEventAsync<T>(T @event) where T : Event
        {
            return _mediator.Publish(@event);
        }


    }
}
