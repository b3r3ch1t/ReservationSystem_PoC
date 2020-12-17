using Bogus;
using Moq;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;
using ReservationSystem_PoC.Common.Identities;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.DomainHandlers.ContactHandlers;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Bus;
using ReservationSystem_PoC.Domain.Core.Repositories;
using ReservationSystem_PoC.Domain.Core.Responses;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.DomainHandlerTests
{
    public class ContactCommandHandlerTest
    {
        private readonly Mock<IContactRepository> _contactRepositoryMock;

        private readonly Mock<IContactTypeRepository> _contactTypeRepositoryMock;
        private readonly Mock<IDependencyResolver> _dependencyResolverMock;
        private readonly Mock<IMediatorHandler> _mediatorHandler;

        public ContactCommandHandlerTest()
        {
            _dependencyResolverMock = new Mock<IDependencyResolver>();

            _contactRepositoryMock = new Mock<IContactRepository>();

            _contactTypeRepositoryMock = new Mock<IContactTypeRepository>();
            _mediatorHandler = new Mock<IMediatorHandler>();
        }

        [Fact]
        public async void EditContact_True()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var faker = new Faker();

            var contactName = faker.Person.FullName;

            var randomizerTextRegex = RandomizerFactory
                .GetRandomizer(new FieldOptionsTextRegex
                {
                    Pattern = @"^\(999\) 999-\d{4}$"
                });

            var contactPhone = randomizerTextRegex.Generate().ToUpper();

            var contactBirthDate = faker.Date.Past();


            var editContactCommand = new EditContactCommand(
                contactId: contact.Id,
                contactName: contactName,
                contactPhone: contactPhone,
                contactBirthDate: contactBirthDate,
                contactTypeId: contactType.Id
            );


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);


            _contactRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(editContactCommand.ContactId))
                .Returns(Task.FromResult(contact));

            _contactTypeRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(editContactCommand.ContactTypeId))
                .Returns(Task.FromResult(contactType));



            _contactRepositoryMock
                .Setup(x =>
                    x.CommitAsync())
                .Returns(Task.FromResult(CommitResponse.Ok()));


            var handler = new ContactCommandHandler(_dependencyResolverMock.Object);

            //act 
            var result = await handler.Handle(editContactCommand, new CancellationToken());



            //result 

            Assert.True(result.Success);

            _contactRepositoryMock.Verify(x => x.GetByIdAsync(editContactCommand.ContactId), Times.Once);
            _contactTypeRepositoryMock.Verify(x => x.GetByIdAsync(editContactCommand.ContactTypeId), Times.Once);
            _contactRepositoryMock.Verify(x => x.Update(contact), Times.Once);
            _contactRepositoryMock.Verify(x => x.CommitAsync(), Times.Once);

            _mediatorHandler.Verify(x => x.NotifyDomainNotification(It.IsAny<DomainNotification>()), Times.Never);
        }


        [Fact]
        public async void EditContact_ContactInvalid_False()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var faker = new Faker();

            var contactName = faker.Person.FullName;

            var randomizerTextRegex = RandomizerFactory
                .GetRandomizer(new FieldOptionsTextRegex
                {
                    Pattern = @"^\(999\) 999-\d{4}$"
                });

            var contactPhone = randomizerTextRegex.Generate().ToUpper();

            var contactBirthDate = faker.Date.Past();


            var editContactCommand = new EditContactCommand(
                contactId: contact.Id,
                contactName: contactName,
                contactPhone: contactPhone,
                contactBirthDate: contactBirthDate,
                contactTypeId: contactType.Id
            );


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);

            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IMediatorHandler>())
                .Returns(_mediatorHandler.Object);

            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);



            _contactRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(editContactCommand.ContactId))
                .Returns(Task.FromResult((Contact)null));


            var handler = new ContactCommandHandler(_dependencyResolverMock.Object);

            //act 
            var result = await handler.Handle(editContactCommand, new CancellationToken());



            //result 

            Assert.False(result.Success);

            _contactRepositoryMock.Verify(x => x.GetByIdAsync(editContactCommand.ContactId), Times.Once);
            _contactTypeRepositoryMock.Verify(x => x.GetByIdAsync(editContactCommand.ContactTypeId), Times.Never);
            _contactRepositoryMock.Verify(x => x.Update(contact), Times.Never);
            _contactRepositoryMock.Verify(x => x.CommitAsync(), Times.Never);
            _mediatorHandler.Verify(x => x.NotifyDomainNotification(It.IsAny<DomainNotification>()), Times.Once);


        }



        [Fact]
        public async void EditContact_ContactTypeInvalid_False()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var faker = new Faker();

            var contactName = faker.Person.FullName;

            var randomizerTextRegex = RandomizerFactory
                .GetRandomizer(new FieldOptionsTextRegex
                {
                    Pattern = @"^\(999\) 999-\d{4}$"
                });

            var contactPhone = randomizerTextRegex.Generate().ToUpper();

            var contactBirthDate = faker.Date.Past();


            var editContactCommand = new EditContactCommand(
                contactId: contact.Id,
                contactName: contactName,
                contactPhone: contactPhone,
                contactBirthDate: contactBirthDate,
                contactTypeId: contactType.Id
            );


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);

            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IMediatorHandler>())
                .Returns(_mediatorHandler.Object);

            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);



            _contactRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(editContactCommand.ContactId))
                .Returns(Task.FromResult(contact));

            _contactTypeRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(editContactCommand.ContactTypeId))
                .Returns(Task.FromResult((ContactType)null));


            var handler = new ContactCommandHandler(_dependencyResolverMock.Object);

            //act 
            var result = await handler.Handle(editContactCommand, new CancellationToken());



            //result 

            Assert.False(result.Success);

            _contactRepositoryMock.Verify(x => x.GetByIdAsync(editContactCommand.ContactId), Times.Once);
            _contactTypeRepositoryMock.Verify(x => x.GetByIdAsync(editContactCommand.ContactTypeId), Times.Once);
            _contactRepositoryMock.Verify(x => x.Update(contact), Times.Never);
            _contactRepositoryMock.Verify(x => x.CommitAsync(), Times.Never);
            _mediatorHandler.Verify(x => x.NotifyDomainNotification(It.IsAny<DomainNotification>()), Times.Once);


        }


        [Fact]
        public async void EditContact_ContactNameInvalid_False()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var faker = new Faker();

            var randomizerTextRegex = RandomizerFactory
                .GetRandomizer(new FieldOptionsTextRegex
                {
                    Pattern = @"^\(999\) 999-\d{4}$"
                });

            var contactPhone = randomizerTextRegex.Generate().ToUpper();

            var contactBirthDate = faker.Date.Past();


            var editContactCommand = new EditContactCommand(
                contactId: contact.Id,
                contactName: string.Empty,
                contactPhone: contactPhone,
                contactBirthDate: contactBirthDate,
                contactTypeId: contactType.Id
            );


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);

            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IMediatorHandler>())
                .Returns(_mediatorHandler.Object);

            _contactRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(editContactCommand.ContactId))
                .Returns(Task.FromResult(contact));

            _contactTypeRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(editContactCommand.ContactTypeId))
                .Returns(Task.FromResult(contactType));




            var handler = new ContactCommandHandler(_dependencyResolverMock.Object);


            //act 
            var result = await handler.Handle(editContactCommand, new CancellationToken());


            //result 

            Assert.False(result.Success);

            _contactRepositoryMock.Verify(x => x.GetByIdAsync(editContactCommand.ContactId), Times.Once);
            _contactTypeRepositoryMock.Verify(x => x.GetByIdAsync(editContactCommand.ContactTypeId), Times.Once);
            _contactRepositoryMock.Verify(x => x.Update(contact), Times.Never);
            _contactRepositoryMock.Verify(x => x.CommitAsync(), Times.Never);

            _mediatorHandler.Verify(x => x.NotifyDomainNotification(It.IsAny<DomainNotification>()), Times.AtLeastOnce);
        }


        [Fact]
        public async void DeleteContact_True()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();

            var deleteContactCommand = new DeleteContactCommand(contact.Id);

            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);

            _contactRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(deleteContactCommand.ContactId))
                .Returns(Task.FromResult(contact));


            _contactRepositoryMock
                .Setup(x =>
                    x.CommitAsync())
                .Returns(Task.FromResult(CommitResponse.Ok()));



            var handler = new ContactCommandHandler(_dependencyResolverMock.Object);

            //act 
            var result = await handler.Handle(deleteContactCommand, new CancellationToken());



            //Assert

            Assert.True(result.Success);
            _contactRepositoryMock.Verify(x => x.GetByIdAsync(deleteContactCommand.ContactId), Times.Once);
            _contactRepositoryMock.Verify(x => x.Update(contact), Times.Once);

            _contactRepositoryMock.Verify(x => x.CommitAsync(), Times.Once);
            _mediatorHandler.Verify(x => x.NotifyDomainNotification(It.IsAny<DomainNotification>()), Times.Never);

        }



        [Fact]
        public async void DeleteContact_ContactNull_False()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();

            var deleteContactCommand = new DeleteContactCommand(contact.Id);

            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IMediatorHandler>())
                .Returns(_mediatorHandler.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);



            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);

            _contactRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(deleteContactCommand.ContactId))
                .Returns(Task.FromResult((Contact)null));


            _contactRepositoryMock
                .Setup(x =>
                    x.CommitAsync())
                .Returns(Task.FromResult(CommitResponse.Ok()));



            var handler = new ContactCommandHandler(_dependencyResolverMock.Object);

            //act 
            var result = await handler.Handle(deleteContactCommand, new CancellationToken());



            //Assert

            Assert.False(result.Success);
            _contactRepositoryMock.Verify(x => x.GetByIdAsync(deleteContactCommand.ContactId), Times.Once);
            _contactRepositoryMock.Verify(x => x.Update(contact), Times.Never);

            _contactRepositoryMock.Verify(x => x.CommitAsync(), Times.Never);
            _mediatorHandler.Verify(x => x.NotifyDomainNotification(It.IsAny<DomainNotification>()), Times.Once);

        }
    }
}
