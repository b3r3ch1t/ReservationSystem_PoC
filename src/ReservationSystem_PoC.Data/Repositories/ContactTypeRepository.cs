using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Repositories;
using System;

namespace ReservationSystem_PoC.Data.Repositories
{
    public class ContactTypeRepository : RepositoryBase<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {

        }

        public new void Update(ContactType obj)
        {
            // DbSet.Update(obj);
            var descriptionParameter = new SqlParameter("@Description", obj.Description);
            var validParameter = new SqlParameter("@Valid", obj.Valid);
            var dateOfChangeParameter = new SqlParameter("@DateOfChange", obj.DateOfChange);
            var dateOfCreationParameter = new SqlParameter("@DateOfCreation", obj.DateOfCreation);
            var idParameter = new SqlParameter("@Id", obj.Id);

            try
            {
                Context.Database.ExecuteSqlRaw("UpdateContactType",
                  descriptionParameter,
                  validParameter,
                  dateOfChangeParameter,
                  dateOfCreationParameter,
                  idParameter
              );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

        }

    }
}
