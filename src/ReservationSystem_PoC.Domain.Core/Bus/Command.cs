using System;

namespace ReservationSystem_PoC.Domain.Core.Bus
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; }


        protected Command()
        {
            Timestamp = DateTime.UtcNow;

        }


    }
}