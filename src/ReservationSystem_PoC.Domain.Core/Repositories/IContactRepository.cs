using ReservationSystem_PoC.Domain.Core.Dto;
using ReservationSystem_PoC.Domain.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Domain.Core.Repositories
{
    public interface IContactRepository : IRepositoryBase<Contact>
    {
        IQueryable<ContactDto> GetAllDto();
        Task<ContactDto> GetContactById(Guid contactId);
    }
}