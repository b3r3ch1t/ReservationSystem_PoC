using ReservationSystem_PoC.Domain.Core.Bus;
using System;

namespace ReservationSystem_PoC.Domain.Core.Commands
{
    public class EditContactCommand : Command
    {

        public Guid ContactId { get; set; }
        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public DateTime ContactBirthDate { get; set; }

        public Guid ContactTypeId { get; set; }


        public EditContactCommand(Guid contactId, string contactName, string contactPhone, DateTime contactBirthDate, Guid contactTypeId)
        {
            ContactId = contactId;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ContactBirthDate = contactBirthDate;
            ContactTypeId = contactTypeId;
        }
    }
}
