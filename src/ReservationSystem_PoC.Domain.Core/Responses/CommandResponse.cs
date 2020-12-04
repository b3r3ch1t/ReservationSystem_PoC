using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem_PoC.Domain.Core.Responses
{
    public class CommandResponse
    {
        private CommandResponse(bool isSuccess, string errorMessage)
        {
            Success = isSuccess;
            if (errorMessage != null)
            {
                Message = new List<string> { errorMessage };

            }

            Message = new List<string>();
        }

        private CommandResponse(bool isSuccess, IEnumerable<string> errorMessage)
        {
            Success = isSuccess;

            Message = errorMessage.ToList();
        }


        public bool Success { get; }
        public bool Failure => !Success;

        public IList<string> Message { get; }


        public static CommandResponse Ok()
        {
            return new CommandResponse(true, string.Empty);
        }

        public static CommandResponse Fail(string errorMessage = "")
        {
            return new CommandResponse(false, errorMessage);
        }

        public static CommandResponse Fail(List<string> errorMessages)
        {
            return new CommandResponse(false, errorMessages);
        }

    }
}
