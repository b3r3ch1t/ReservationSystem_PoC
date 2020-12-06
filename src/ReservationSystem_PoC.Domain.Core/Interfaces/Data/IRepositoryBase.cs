using ReservationSystem_PoC.Domain.Core.Responses;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.Interfaces.Data
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        void Update(TEntity obj);
        void Remove(Guid id);
        IQueryable<TEntity>  GetAll();
        Task<CommitResponse> CommitAsync();

    }
}