using FluentValidation;
using System.Text.RegularExpressions;

namespace ReservationSystem_PoC.Domain.Core.Validators
{
    public class PhoneValidator : AbstractValidator<string>
    {
        private static readonly Regex IsGuid = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", RegexOptions.Compiled);

        public static bool IsPhoneValid(string phoneCandidate)
        {
            var isValid = false;
            if (string.IsNullOrWhiteSpace(phoneCandidate)) return false;

            if (IsGuid.IsMatch(phoneCandidate))
            {
                isValid = true;
            }
            return isValid;
        }


        public PhoneValidator()
        {
            RuleFor(x => x.ToString()).Must(IsPhoneValid);
        }

    }
}
