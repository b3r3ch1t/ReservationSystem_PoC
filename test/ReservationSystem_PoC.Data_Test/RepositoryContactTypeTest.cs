using Bogus;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.Common.Identities;
using ReservationSystem_PoC.Common.IoC;
using ReservationSystem_PoC.Common.Repositories;
using ReservationSystem_PoC.Data.Context;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ReservationSystem_PoC.Data.Test
{
    public class RepositoryContactTypeTest
    {
        private readonly ReservarionSystemDbContext _db;
        private readonly IRepositoryBase<ContactType> _contactTypeRepository;
        public RepositoryContactTypeTest()
        {
            var dependencyResolver = DependencyResolverFaker.GetDependencyResolver();

            _db = dependencyResolver.Resolve<ReservarionSystemDbContext>();

            _contactTypeRepository = ContactTypeRepositoryFaker.GetContactTypeRepository();

        }


        [Fact]
        public async Task ContactAddAsyncOk()
        {
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var count = await _db.ContactTypes.CountAsync();

            await _contactTypeRepository.AddAsync(contactType);

            await _contactTypeRepository.CommitAsync();

            var countNew = await _db.ContactTypes.CountAsync();

            var contactFromDatabase = await _db.ContactTypes.FindAsync(contactType.Id);

            Assert.True(countNew == count + 1);

            Assert.NotNull(contactFromDatabase);
        }

        [Fact]
        public async Task GetByIdAsyncOk()
        {
            var contactType = _db.ContactTypes.First();

            var result = await _contactTypeRepository.GetByIdAsync(contactType.Id);

            Assert.Equal(contactType.Id, result.Id);

        }

        [Fact]
        public async Task UpdateOk()
        {
            var contactType = ContactTypeFaker.GetContactTypeOk();

            await _db.ContactTypes.AddAsync(contactType);

            await _db.SaveChangesAsync();

            var newContactType = ContactTypeFaker.GetContactTypeOk();

            var dateOfchange = contactType.DateOfChange;
            var dateOfCreation = contactType.DateOfCreation;



            contactType.ChangeDateOfChange();
            contactType.ChangeDateOfCreation();
            contactType.ChangeDescription(newContactType.Description);

            _contactTypeRepository.Update(contactType);

            await _contactTypeRepository.CommitAsync();


            Assert.NotEqual(contactType.DateOfChange, dateOfchange);
            Assert.NotEqual(contactType.DateOfCreation, dateOfCreation);
            Assert.Equal(contactType.Description, newContactType.Description);

        }


        [Fact]
        public async Task RemoveOk()
        {
            var contactType = ContactTypeFaker.GetContactTypeOk();

            await _db.ContactTypes.AddAsync(contactType);

            await _db.SaveChangesAsync();

            _contactTypeRepository.Remove(contactType.Id);

            await _contactTypeRepository.CommitAsync();


            var result = await _contactTypeRepository.GetByIdAsync(contactType.Id);


            Assert.Null(result);

        }

        [Fact]
        public async Task GetAllOk()
        {

            if (!_db.ContactTypes.Any())
            {
                await CreateListOfContactType();
            }

            var listFromRepository = await _contactTypeRepository.GetAllAsync();

            var listFromDatabase = _db.ContactTypes.Where(x => x.Valid);

            Assert.Equal(listFromRepository.Count(), listFromDatabase.Count());

        }

        private async Task CreateListOfContactType()
        {
            var faker = new Faker();

            //Create a random text with min=3 and ContactType.MaxDescriptionSize
            var description = faker
                    .Lorem.Paragraph(min: 3)
                ;

            if (description.Length >= ContactType.MaxDescriptionSize)
            {
                description = description.Substring(0, ContactType.MaxDescriptionSize);
            }

            var fakerType = new Faker<ContactType>()
                .RuleFor(p => p.Description,
                    faker.Random.AlphaNumeric(
                        faker.Random.Int(ContactType.MinDescriptionSize, ContactType.MaxDescriptionSize)
                    )
                );

            var listFaker = fakerType.Generate(faker.Random.Int(max: 10));
            await _db.ContactTypes.AddRangeAsync(listFaker);

            await _db.SaveChangesAsync();
        }
    }
}
