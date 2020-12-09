using FluentValidation;
using ReservationSystem_PoC.Domain.Core.Entities;

namespace ReservationSystem_PoC.Domain.Core.Validators
{
    internal class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator()
        {

            ValidateMessage();

            ValidadeRacking();
        }

        private void ValidadeRacking()
        {
            RuleFor(x => x.Ranking)
                .GreaterThanOrEqualTo(Reservation.MinRanking)
                .LessThanOrEqualTo(Reservation.MaxRanking)
                .WithMessage($"The ranking must be between {Reservation.MinRanking} and {Reservation.MaxRanking}");

        }

        private void ValidateMessage()
        {
            RuleFor(x => x.Message)
                .NotNull()
                .NotEmpty()
                .WithMessage("The message of reservation can not be null.")

                .Length(min: Reservation.MinMessageSize, max: Reservation.MaxMessageSize)
                .WithMessage($"The name of contact be have between {Reservation.MinMessageSize} and {Reservation.MaxMessageSize}");
        }
    }
}
