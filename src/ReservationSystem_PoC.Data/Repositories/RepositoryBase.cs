using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.Data.Context;
using ReservationSystem_PoC.Domain.Core;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Data;
using ReservationSystem_PoC.Domain.Core.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Data.Repositories
{

    //Repository Generic
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase<TEntity>
    {
        protected readonly ReservarionSystemDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(IDependencyResolver dependencyResolver)
        {
            Db = dependencyResolver.Resolve<ReservarionSystemDbContext>();
            DbSet = dependencyResolver.Resolve<DbSet<TEntity>>();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public void Remove(Guid id)
        {
            var obj = DbSet.Find(id);
            if (obj == null) return;

            DbSet.Remove(obj);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<CommitResponse> CommitAsync()
        {
            try
            {
                var rowsAffected = await Db.SaveChangesAsync();

                return CommitResponse.Ok(rowsAffected);

            }
            catch (Exception ex)
            {
                return CommitResponse.Fail(ex.Message);

            }
        }

    }
}
