using ReservationSystem_PoC.Common.IoC;
using ReservationSystem_PoC.Data.Repositories;

namespace ReservationSystem_PoC.Common.Repositories
{
    public static class ContactTypeRepositoryFaker
    {

        public static ContactTypeRepository GetContactTypeRepository()
        {

            var dependencyResolverFaker = DependencyResolverFaker.GetDependencyResolver();

            var contractTypeRepository = new ContactTypeRepository(dependencyResolverFaker);

            return contractTypeRepository;
        }
    }
}
