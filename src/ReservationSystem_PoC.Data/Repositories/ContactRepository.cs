using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.Domain.Core.Dto;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Data.Repositories
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }

        public IQueryable<ContactDto> GetAllDto()
        {
            var result = from c in Context.Contacts.Where(x => x.Valid)

                    .Include(x => x.ContactType)

                         select new ContactDto
                         {
                             ContactId = c.Id,
                             ContactName = c.Name,
                             ContactPhone = c.PhoneNumber,
                             ContactBirthdate = c.BirthDate,
                             ContactTypeName = c.ContactType.Description,
                             ContactTypeId = c.ContactType.Id
                         };


            return result;
        }

        public async Task<ContactDto> GetContactById(Guid contactId)
        {
            return await GetAllDto().FirstOrDefaultAsync(x => x.ContactId == contactId);
        }
    }
}
