using MediatR;
using System;

namespace ReservationSystem_PoC.Domain.Core.Bus
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; }

        protected Event()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}