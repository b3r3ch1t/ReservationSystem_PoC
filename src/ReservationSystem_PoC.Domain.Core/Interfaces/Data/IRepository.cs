using FluentValidation.Results;
using ReservationSystem_PoC.Domain.Core.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.Interfaces.Data
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        Task UpdateAsync(TEntity obj);
        Task Remove(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<CommitResponse> Commit();
        Task NotifyValidationErrors(IEnumerable<ValidationFailure> validationResultErrors);
    }
}