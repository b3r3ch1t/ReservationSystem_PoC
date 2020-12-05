using Microsoft.EntityFrameworkCore;
using Moq;
using ReservationSystem_PoC.Common.Context;
using ReservationSystem_PoC.Data.Context;
using ReservationSystem_PoC.Data.Repositories;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Data;

namespace ReservationSystem_PoC.Common.IoC
{
    public static class DependencyResolverFaker
    {
        public static IDependencyResolver GetDependencyResolver()
        {
            var dependencyResolver = new Mock<IDependencyResolver>();

            var dbContextInMemory = ReservarionSystemDbContextFaker.GetDatabaseInMemory();

            dependencyResolver.Setup(resolver =>
                resolver.Resolve<ReservarionSystemDbContext>())
                .Returns(dbContextInMemory);


            dependencyResolver.Setup(resolver =>
                    resolver.Resolve<DbSet<ContactType>>())
                .Returns(dbContextInMemory.ContactTypes);

            var getContactTypeRepository = GetContactTypeRepository(dependencyResolver.Object);



            dependencyResolver.Setup(resolver =>
                    resolver.Resolve<IRepositoryBase<ContactType>>())
                .Returns(getContactTypeRepository);



            return dependencyResolver.Object;

        }

        private static RepositoryBase<ContactType> GetContactTypeRepository(IDependencyResolver dependencyResolver)
        {
            var result = new RepositoryBase<ContactType>(dependencyResolver);

            return result;
        }

    }
}
