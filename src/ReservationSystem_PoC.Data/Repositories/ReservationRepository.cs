using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Repositories;

namespace ReservationSystem_PoC.Data.Repositories
{
    internal class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }
    }
}
