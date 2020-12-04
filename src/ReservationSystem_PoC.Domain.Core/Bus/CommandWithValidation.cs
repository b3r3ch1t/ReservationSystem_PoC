using FluentValidation.Results;

namespace ReservationSystem_PoC.Domain.Core.Bus
{
    public abstract class CommandWithValidation : Command
    {
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();

        protected CommandWithValidation()
        {
            ValidationResult = new ValidationResult();
        }
    }
}
