using System;
using System.Collections.Generic;

namespace ReservationSystem_PoC.Domain.Core.Responses
{
    public class CommitResponse
    {
        private CommitResponse(bool isSuccess, int quantidadeRegistros, string errorMessage)
        {
            var doNotExistsErrorMessage = string.IsNullOrWhiteSpace(errorMessage);
            var doExistsErrorMessage = !doNotExistsErrorMessage;

            if (isSuccess)
            {
                if (doExistsErrorMessage)
                    throw new ArgumentException(ResultMessages.ErrorObjectIsProvidedForSuccess, nameof(errorMessage));
            }
            else
            {
                if (doNotExistsErrorMessage)
                    throw new ArgumentNullException(nameof(errorMessage), ResultMessages.ErrorObjectIsNotProvidedForFailure);
            }

            Success = isSuccess;
            Message = new List<string> { errorMessage };

            QuantityOfRecordsAffecteds = quantidadeRegistros;
        }

        public bool Success { get; }

        public IEnumerable<string> Message { get; }


        public int QuantityOfRecordsAffecteds { get; }


        public static CommitResponse Ok(int quantidadeRegistros = 0)
        {
            return new CommitResponse(true, quantidadeRegistros, string.Empty);
        }

        public static CommitResponse Fail(string errorMessage = "")
        {
            return new CommitResponse(false, 0, errorMessage);
        }


        internal static class ResultMessages
        {
            public static readonly string ErrorObjectIsNotProvidedForFailure =
                "You attempted to create a failure result, which must have an error, but a null error object was passed to the constructor.";

            public static readonly string ErrorObjectIsProvidedForSuccess =
                "You attempted to create a success result, which cannot have an error, but a non-null error object was passed to the constructor.";
        }
    }
}
