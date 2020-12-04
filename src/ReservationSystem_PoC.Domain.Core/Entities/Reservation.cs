using ReservationSystem_PoC.Domain.Core.Validators;
using System;

namespace ReservationSystem_PoC.Domain.Core.Entities
{
    public class Reservation : EntityBase<Reservation>
    {
        public const int MinDescriptionSize = 3;
        public const int MaxDescriptionSize = 255;
        public Contact Contact { get; protected set; }
        public Guid ContactId { get; protected set; }
        public string Message { get; protected set; }


        public Reservation(string message, Contact contact)
        {
            Message = message;
            Contact = contact;

            if (contact != null) ContactId = contact.Id;
        }


        public override void Validate()
        {
            ValidationResult = new ReservationValidator().Validate(this);
        }

        public void ChangeMessage(string message)
        {
            Message = message;
        }
    }
}
