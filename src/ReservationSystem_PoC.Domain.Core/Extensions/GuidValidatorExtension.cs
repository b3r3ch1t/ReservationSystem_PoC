using ReservationSystem_PoC.Domain.Core.Validators;

namespace ReservationSystem_PoC.Domain.Core.Extensions
{
    public static class GuidValidatorExtension
    {
        public static bool IsValidGuid(this string guidCandidate)
        {

            if (string.IsNullOrWhiteSpace(guidCandidate)) return false;

            var result = new GuidValidator().Validate(guidCandidate).IsValid;

            return result;
        }

    }
}
