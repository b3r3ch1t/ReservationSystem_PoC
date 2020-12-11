using ReservationSystem_PoC.Domain.Core.Bus;
using System;

namespace ReservationSystem_PoC.Domain.Core.Commands
{
    public class CreateReservationCommand : Command
    {
        public Guid? ContactId { get; set; }
        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public DateTime ContactBirthdate { get; set; }

        public Guid ContactTypeId { get; set; }


        public CreateReservationCommand(
            Guid? contactId,
            string contactName,
            string contactPhone,
            DateTime contactBirthdate,
            Guid contactTypeId)
        {
            ContactId = contactId;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ContactBirthdate = contactBirthdate;
            ContactTypeId = contactTypeId;
        }
    }
}
