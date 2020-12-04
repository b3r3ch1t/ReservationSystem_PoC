using ReservationSystem_PoC.Domain.Core.Validators;

namespace ReservationSystem_PoC.Domain.Core.Entities
{
    public class ContactType : EntityBase<ContactType>
    {
        public const int MinDescriptionSize = 3;
        public const int MaxDescriptionSize = 512;

        public string Description { get; protected set; }

        public ContactType(string description)
        {
            Description = description;
        }
        public override void Validate()
        {
            ValidationResult = new ContactTypeValidator().Validate(this);
        }
    }
}
