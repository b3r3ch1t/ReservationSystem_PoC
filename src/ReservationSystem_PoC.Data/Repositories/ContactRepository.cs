using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Repositories;

namespace ReservationSystem_PoC.Data.Repositories
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }
    }
}
