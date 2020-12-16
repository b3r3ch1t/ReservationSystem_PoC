using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using ReservationSystem_PoC.Common.Commands;
using ReservationSystem_PoC.Common.Context;
using ReservationSystem_PoC.Data.Context;
using ReservationSystem_PoC.Domain.Core.DomainHandlers.ReservationHandlers;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Repositories;
using ReservationSystem_PoC.Domain.Core.Responses;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.DomainHandlerTests
{
    public class ReservationCommandHandlerTest
    {

        private readonly Mock<IDependencyResolver> _dependencyResolverMock;
        private readonly ReservarionSystemDbContext _context;

        public ReservationCommandHandlerTest()
        {
            var dependencyResolverMock = new Mock<IDependencyResolver>();

            _context = ReservarionSystemDbContextFaker.GetDatabaseInMemory();

            dependencyResolverMock.Setup(resolver =>
                    resolver.Resolve<ReservarionSystemDbContext>())
                .Returns(_context);

            _dependencyResolverMock = dependencyResolverMock;

        }


        [Fact]
        public async void CommandOk()
        {
            var command = UpdateRankingOfReservationCommandFaker.UpdateRankingOfReservationCommandOk();

            var dependencyResolverMock = _dependencyResolverMock;

            var faker = new Faker();

            var reservation = faker.PickRandom(_context.Reservations
                .Include(x => x.Contact)
                .ToList());

            var repository = new Mock<IReservationRepository>();

            repository.Setup(x => x.GetByIdAsync(command.ReservationId)).Returns(Task.FromResult(reservation));
            repository.Setup(x => x.CommitAsync()).Returns(Task.FromResult(CommitResponse.Ok()));

            dependencyResolverMock.Setup(x => x.Resolve<IReservationRepository>()).Returns(repository.Object);

            var handler = new ReservationCommandHandler(dependencyResolverMock.Object);

            var commandResponse = await handler.Handle(command, new CancellationToken());



            Assert.True(commandResponse.Success);
            repository.Verify(x => x.GetByIdAsync(command.ReservationId), Times.Once);
            repository.Verify(x => x.Update(reservation), Times.Once);

            repository.Verify(x => x.CommitAsync(), Times.Once);

        }


        [Fact]
        public async void Command_ReservationNull_False()
        {
            var command = UpdateRankingOfReservationCommandFaker.UpdateRankingOfReservationCommandOk();

            var dependencyResolverMock = _dependencyResolverMock;

            var repository = new Mock<IReservationRepository>();

            repository.Setup(x => x.GetByIdAsync(command.ReservationId)).Returns(Task.FromResult((Reservation)null));

            dependencyResolverMock.Setup(x => x.Resolve<IReservationRepository>()).Returns(repository.Object);

            var handler = new ReservationCommandHandler(dependencyResolverMock.Object);

            var commandResponse = await handler.Handle(command, new CancellationToken());



            Assert.False(commandResponse.Success);
            repository.Verify(x => x.GetByIdAsync(command.ReservationId), Times.Once);


            repository.Verify(x => x.CommitAsync(), Times.Never);

        }
 
    }
}
