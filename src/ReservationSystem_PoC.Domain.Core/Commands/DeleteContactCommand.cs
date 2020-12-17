using ReservationSystem_PoC.Domain.Core.Bus;
using System;

namespace ReservationSystem_PoC.Domain.Core.Commands
{
    public class DeleteContactCommand : Command
    {
        public Guid ContactId { get; set; }

        public DeleteContactCommand(Guid contactId)
        {
            ContactId = contactId;
        }


    }
}
