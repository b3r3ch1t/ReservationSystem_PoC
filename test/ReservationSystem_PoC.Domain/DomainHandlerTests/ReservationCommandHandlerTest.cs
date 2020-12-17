using Bogus;
using Moq;
using ReservationSystem_PoC.Common.Identities;
using ReservationSystem_PoC.Domain.Core.Commands;
using ReservationSystem_PoC.Domain.Core.DomainHandlers;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Bus;
using ReservationSystem_PoC.Domain.Core.Repositories;
using ReservationSystem_PoC.Domain.Core.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.DomainHandlerTests
{
    public class ReservationCommandHandlerTest
    {
        private readonly Mock<IDependencyResolver> _dependencyResolverMock;
        private readonly Mock<IMediatorHandler> _mediatorHandler;
        private readonly Mock<IReservationRepository> _reservationRepositoryMock;
        private readonly Mock<IContactRepository> _contactRepositoryMock;
        private readonly Mock<IContactTypeRepository> _contactTypeRepositoryMock;

        public ReservationCommandHandlerTest()
        {
            _dependencyResolverMock = new Mock<IDependencyResolver>();
            _contactRepositoryMock = new Mock<IContactRepository>();
            _contactTypeRepositoryMock = new Mock<IContactTypeRepository>();
            _mediatorHandler = new Mock<IMediatorHandler>();
            _reservationRepositoryMock = new Mock<IReservationRepository>();
        }

        [Fact]
        public async void CreateReservation_True()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var faker = new Faker();

            var message = faker.Lorem.Paragraph();

            var createReservationCommand = new CreateReservationCommand(
                contactId: contact.Id,
                contactName: contact.Name,
                contactPhone: contact.Name,
                contactBirthdate: contact.BirthDate,
                contactTypeId: contactType.Id,
                message: message
            );


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IReservationRepository>())
                .Returns(_reservationRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);


            _contactRepositoryMock.Setup(x => x.GetByIdAsync(createReservationCommand.ContactId.Value))
                .Returns(Task.FromResult(contact));

            _reservationRepositoryMock.Setup(x => x.CommitAsync())
                .Returns(Task.FromResult(CommitResponse.Ok()));

            var handler = new ReservationCommandHandler(_dependencyResolverMock.Object);

            //Act

            var result = await handler.Handle(createReservationCommand, new CancellationToken());


            //Assert 

            Assert.True(result.Success);
            _contactRepositoryMock.Verify(x => x.GetByIdAsync(createReservationCommand.ContactId.Value), Times.Once);
            _reservationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Reservation>()), Times.Once);
            _reservationRepositoryMock.Verify(x => x.CommitAsync(), Times.Once);

        }


        [Fact]
        public async void CreateReservation_ContactDoesNotExist_True()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var faker = new Faker();

            var message = faker.Lorem.Paragraph();

            var createReservationCommand = new CreateReservationCommand(
                contactId: contact.Id,
                contactName: contact.Name,
                contactPhone: contact.PhoneNumber,
                contactBirthdate: contact.BirthDate,
                contactTypeId: contactType.Id,
                message: message
            );


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IReservationRepository>())
                .Returns(_reservationRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);



            _contactRepositoryMock.Setup(x => x.GetByIdAsync(createReservationCommand.ContactId.Value))
                .Returns(Task.FromResult((Contact)null));


            _contactTypeRepositoryMock.Setup(x => x.GetByIdAsync(createReservationCommand.ContactTypeId))
                .Returns(Task.FromResult(contactType));


            _reservationRepositoryMock.Setup(x => x.CommitAsync())
                .Returns(Task.FromResult(CommitResponse.Ok()));

            _contactRepositoryMock.Setup(x => x.CommitAsync())
                .Returns(Task.FromResult(CommitResponse.Ok()));

            var handler = new ReservationCommandHandler(_dependencyResolverMock.Object);

            //Act

            var result = await handler.Handle(createReservationCommand, new CancellationToken());


            //Assert 

            Assert.True(result.Success);
            _contactRepositoryMock.Verify(x => x.GetByIdAsync(createReservationCommand.ContactId.Value), Times.Once);
            _reservationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Reservation>()), Times.Once);
            _reservationRepositoryMock.Verify(x => x.CommitAsync(), Times.Once);
            _contactRepositoryMock.Verify(x => x.CommitAsync(), Times.Once);
            _contactRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Contact>()), Times.Once);

        }

        [Fact]
        public async void CreateReservation_ContactIdNull_False()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var faker = new Faker();

            var message = faker.Lorem.Paragraph();

            var createReservationCommand = new CreateReservationCommand(
                contactId: null,
                contactName: contact.Name,
                contactPhone: contact.Name,
                contactBirthdate: contact.BirthDate,
                contactTypeId: contactType.Id,
                message: message
            );


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IReservationRepository>())
                .Returns(_reservationRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);




            var handler = new ReservationCommandHandler(_dependencyResolverMock.Object);

            //Act

            var result = await handler.Handle(createReservationCommand, new CancellationToken());


            //Assert 

            Assert.False(result.Success);
            _contactRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
            _reservationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Reservation>()), Times.Never);
            _reservationRepositoryMock.Verify(x => x.CommitAsync(), Times.Never);

        }


        [Fact]
        public async void CreateReservation_ContactInvalid_False()
        {
            //arrange
            var contact = ContactFaker.GetContactContactNameGreater();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var faker = new Faker();

            var message = faker.Lorem.Paragraph();

            var createReservationCommand = new CreateReservationCommand(
                contactId: contact.Id,
                contactName: contact.Name,
                contactPhone: contact.Name,
                contactBirthdate: contact.BirthDate,
                contactTypeId: contactType.Id,
                message: message
            );


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IReservationRepository>())
                .Returns(_reservationRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactRepository>())
                .Returns(_contactRepositoryMock.Object);


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IContactTypeRepository>())
                .Returns(_contactTypeRepositoryMock.Object);


            _contactRepositoryMock.Setup(x => x.GetByIdAsync(createReservationCommand.ContactId.Value))
                .Returns(Task.FromResult((Contact)null));


            _contactTypeRepositoryMock.Setup(x => x.GetByIdAsync(createReservationCommand.ContactTypeId))
                .Returns(Task.FromResult(contactType));

            var handler = new ReservationCommandHandler(_dependencyResolverMock.Object);

            //Act

            var result = await handler.Handle(createReservationCommand, new CancellationToken());


            //Assert 

            Assert.False(result.Success);
            _contactRepositoryMock.Verify(x => x.GetByIdAsync(createReservationCommand.ContactId.Value), Times.Once);
            _reservationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Reservation>()), Times.Never);
            _reservationRepositoryMock.Verify(x => x.CommitAsync(), Times.Never);

        }


        [Fact]
        public async void CreateReservation_ContactTypeInvalid_False()
        {
            //arrange
            var contact = ContactFaker.GetContactContactNameGreater();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            var faker = new Faker();

            var message = faker.Lorem.Paragraph();

            var createReservationCommand = new CreateReservationCommand(
                contactId: contact.Id,
                contactName: contact.Name,
                contactPhone: contact.Name,
                contactBirthdate: contact.BirthDate,
                contactTypeId: contactType.Id,
                message: message
            );


            _dependencyResolverMock
                .Setup(x =>
                    x.Resolve<IReservationRepository>())
                .Returns(_reservationRepositoryMock.Object);


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


            _contactRepositoryMock.Setup(x => x.GetByIdAsync(createReservationCommand.ContactId.Value))
                .Returns(Task.FromResult((Contact)null));


            _contactTypeRepositoryMock.Setup(x => x.GetByIdAsync(createReservationCommand.ContactTypeId))
                .Returns(Task.FromResult((ContactType)null));

            var handler = new ReservationCommandHandler(_dependencyResolverMock.Object);

            //Act

            var result = await handler.Handle(createReservationCommand, new CancellationToken());


            //Assert 

            Assert.False(result.Success);
            _contactRepositoryMock.Verify(x => x.GetByIdAsync(createReservationCommand.ContactId.Value), Times.Once);
            _reservationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Reservation>()), Times.Never);
            _reservationRepositoryMock.Verify(x => x.CommitAsync(), Times.Never);
            _mediatorHandler.Verify(x => x.NotifyDomainNotification(It.IsAny<DomainNotification>()), Times.Once);
        }


    }
}
