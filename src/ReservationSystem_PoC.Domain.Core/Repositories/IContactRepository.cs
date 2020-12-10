using ReservationSystem_PoC.Domain.Core.Dto;
using ReservationSystem_PoC.Domain.Core.Entities;
using System.Linq;

namespace ReservationSystem_PoC.Domain.Core.Repositories
{
    public interface IContactRepository : IRepositoryBase<Contact>
    {
        IQueryable<ContactDto> GetAllDto();
    }
}