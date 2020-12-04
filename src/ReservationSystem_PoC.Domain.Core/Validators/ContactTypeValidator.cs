using FluentValidation;
using ReservationSystem_PoC.Domain.Core.Entities;

namespace ReservationSystem_PoC.Domain.Core.Validators
{
    internal class ContactTypeValidator : AbstractValidator<ContactType>
    {
        public ContactTypeValidator()
        {
            ValidateDescription();
        }

        private void ValidateDescription()
        {
            RuleFor(x => x.Description).NotNull()
                .WithMessage("The description of Contact Type can not be null.")

                .Length(min: ContactType.MinDescriptionSize, max: ContactType.MaxDescriptionSize)
                .WithMessage($"The description of Contact Type must have between {Contact.MinNameSize} and {Contact.MaxNameSize}");
        }
    }
}
