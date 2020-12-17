using System;

namespace ReservationSystem_PoC.Domain.Core.Commands
{
    public class CreateContactCommand : EditContactCommand
    {
        public CreateContactCommand(
            string contactName,
            string contactPhone,
            DateTime contactBirthDate,
            Guid contactTypeId
            ) : base(
            Guid.NewGuid(),
            contactName,
            contactPhone,
            contactBirthDate,
            contactTypeId)
        {
        }
    }
}
