using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem_PoC.API.Responses
{
    public class RequestResponse<T>
    {


        public IList<string> MessageSuccess { get; }
        public IList<string> MessageFailure { get; }

        public bool Sucess => !MessageFailure.Any();
        public bool Fail => MessageFailure.Any();

        public T Result { get; }

        public RequestResponse(T result)
        {
            Result = result;
            MessageSuccess = new List<string>();
            MessageFailure = new List<string>();
        }


        public void AddMessageSucess(string message)
        {
            MessageSuccess.Add(message);
        }


        public void AddMessageFailure(string message)
        {
            MessageFailure.Add(message);
        }
    }
}
