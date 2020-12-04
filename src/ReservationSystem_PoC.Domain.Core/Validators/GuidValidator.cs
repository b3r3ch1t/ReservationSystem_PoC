using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace ReservationSystem_PoC.Domain.Core.Validators
{
    public class GuidValidator : AbstractValidator<string>
    {
        private static readonly Regex IsGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        public static bool IsValidGuid(string guidCandidate)
        {
            var isValid = false;
            if (guidCandidate == default(Guid).ToString() || guidCandidate == Guid.Empty.ToString()) return false;


            if (IsGuid.IsMatch(guidCandidate))
            {
                isValid = true;
            }
            return isValid;
        }


        public GuidValidator()
        {
            RuleFor(x => x.ToString()).Must(IsValidGuid);
        }


    }
}
