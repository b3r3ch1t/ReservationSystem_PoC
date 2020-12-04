using FluentValidation;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Extensions;
using System;

namespace ReservationSystem_PoC.Domain.Core.Validators
{
    internal class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            ValidateName();
            ValidatePhoneNumber();
            ValidateBirthDate();
            ValidateContactType();
            ValidateReservations();
        }

        private void ValidateReservations()
        {
            RuleFor(x => x.Reservations)
                .NotNull()
                .WithMessage("The list of reservation can not be null; ");
        }

        private void ValidateContactType()
        {

            RuleFor(x => x.ContactType)
                .NotNull()
                .WithMessage("The Type of Contact is not valid .");


            When(x => x.ContactType != null, () =>
            {
                RuleFor(x => x.ContactType)
                    .Must(x => x.IsValid())
                    .WithMessage("The Type of Contact is not valid .");
            });

        }

        private void ValidateBirthDate()
        {
            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Now)
                .WithMessage("The birthdate is must to be less than now.")
                ;
        }

        private void ValidatePhoneNumber()
        {
            RuleFor(x => x.PhoneNumber)
                .Must(x => x.IsPhoneValid());
        }

        private void ValidateName()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("The name of contact can not be null.")

                .Length(min: Contact.MinNameSize, max: Contact.MaxNameSize)
                .WithMessage($"The name of contact must have between {Contact.MinNameSize} and {Contact.MaxNameSize}");
        }
    }
}
