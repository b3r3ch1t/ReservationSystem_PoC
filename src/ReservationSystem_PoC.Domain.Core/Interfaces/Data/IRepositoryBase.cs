using ReservationSystem_PoC.Domain.Core.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.Interfaces.Data
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        void Update(TEntity obj);
        void Remove(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<CommitResponse> CommitAsync();

    }
}