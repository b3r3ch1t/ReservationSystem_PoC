using System;

namespace ReservationSystem_PoC.Domain.Core.Commands
{
    public class CreateContactCommand : EditContactCommand
    {
        public CreateContactCommand(
            Guid contactId,
            string contactName,
            string contactPhone,
            DateTime contactBirthDate,
            Guid contactTypeId
            ) : base(
            contactId,
            contactName,
            contactPhone,
            contactBirthDate,
            contactTypeId)
        {
        }
    }
}
