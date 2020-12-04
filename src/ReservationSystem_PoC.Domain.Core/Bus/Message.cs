using MediatR;
using ReservationSystem_PoC.Domain.Core.Responses;
using System;

namespace ReservationSystem_PoC.Domain.Core.Bus
{
    public abstract class Message : IRequest<CommandResponse>, IDisposable
    {
        public string MessageType { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}