using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.Data.Context;
using ReservationSystem_PoC.Domain.Core;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Data;
using ReservationSystem_PoC.Domain.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet.Where(x => x.Valid)
                .FirstOrDefaultAsync(x => id == x.Id);

        }

        public void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public void Remove(Guid id)
        {
            var obj = DbSet.Find(id);
            if (obj == null) return;


            // The system do not remove from database, just mark the flag valid as false

            obj.Remove();

            DbSet.Update(obj);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {

            return await DbSet
                .Where(p => p.Valid)
                .ToListAsync(); ;
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
