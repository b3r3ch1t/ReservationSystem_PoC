using FluentValidation;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Extensions;

namespace ReservationSystem_PoC.Domain.Core.Validators
{
    internal class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {

            ValidateMessage();
            ValidadeContact();
        }

        private void ValidadeContact()
        {
            RuleFor(x => x.Contact)
                .NotNull()
                .WithMessage("The Contact is not valid .");




            When(x => x.Contact != null, () =>
           {
               RuleFor(x => x.ContactId.ToString())
                   .Must(x => x.IsValidGuid())
                   .WithMessage("The contact is not valid .");
           });

        }

        private void ValidateMessage()
        {
            RuleFor(x => x.Message)
                .NotNull()
                .NotEmpty()
                .WithMessage("The message of reservation can not be null.")

                .Length(min: Reservation.MinDescriptionSize, max: Reservation.MaxDescriptionSize)
                .WithMessage($"The name of contact must have between {Reservation.MinDescriptionSize} and {Reservation.MaxDescriptionSize}");
        }
    }
}
