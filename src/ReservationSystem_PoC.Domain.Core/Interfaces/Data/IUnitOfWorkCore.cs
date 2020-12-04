using FluentValidation.Results;
using ReservationSystem_PoC.Domain.Core.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.Interfaces.Data
{
    public interface IUnitOfWorkCore : IDisposable
    {
        Task<CommitResponse> Commit();

        Task NotifyValidationErrors(IEnumerable<ValidationFailure> validationResultErrors);


    }
}
