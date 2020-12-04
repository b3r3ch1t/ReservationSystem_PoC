using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Repositories;

namespace ReservationSystem_PoC.Data.Repositories
{
    public class ContactTypeRepository : RepositoryBase<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }
    }
}
