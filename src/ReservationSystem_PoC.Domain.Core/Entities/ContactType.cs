using ReservationSystem_PoC.Domain.Core.Validators;
using System.Collections.Generic;

namespace ReservationSystem_PoC.Domain.Core.Entities
{
    public class ContactType : EntityBase<ContactType>
    {
        public const int MinDescriptionSize = 3;
        public const int MaxDescriptionSize = 512;

        public string Description { get; protected set; }
        public IList<Contact> Contacts { get; set; }

        public ContactType(string description)
        {
            Description = description;

            Contacts = new List<Contact>();
        }
        public override void Validate()
        {
            ValidationResult = new ContactTypeValidator().Validate(this);
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }
    }
}
