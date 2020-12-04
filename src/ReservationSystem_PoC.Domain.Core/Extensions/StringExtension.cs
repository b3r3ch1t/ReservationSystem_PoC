using ReservationSystem_PoC.Domain.Core.Validators;

namespace ReservationSystem_PoC.Domain.Core.Extensions
{
    public static class StringExtension
    {
        public static bool IsPhoneValid(this string value)
        {
            return !string.IsNullOrEmpty(value) && new PhoneValidator().Validate(value).IsValid;
        }


    }
}
