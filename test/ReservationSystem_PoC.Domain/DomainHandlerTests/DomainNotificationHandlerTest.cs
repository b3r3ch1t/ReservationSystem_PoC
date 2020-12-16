using Bogus;
using ReservationSystem_PoC.Domain.Core.DomainHandlers;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using System.Linq;
using System.Threading;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.DomainHandlerTests
{
    public class DomainNotificationHandlerTest
    {


        [Fact]
        public async void AddDomainNotificationSuccess_Ok()
        {
            var faker = new Faker();

            var message = faker.Lorem.Sentence();

            var handler = new DomainNotificationHandler();

            var domainNotification = DomainNotification.Success(message);

            await handler.Handle(domainNotification, new CancellationToken());

            var notifications = handler.GetNotificationsSuccess();

            Assert.True(handler.HasNotificationsSucess());

            Assert.True(notifications.Count == 1);
        }


        [Fact]
        public async void AddDomainNotificationError_Ok()
        {
            var faker = new Faker();

            var message = faker.Lorem.Sentence();

            var handler = new DomainNotificationHandler();

            var domainNotification = DomainNotification.Fail(message);

            await handler.Handle(domainNotification, new CancellationToken());

            var notifications = handler.GetNotificationsError();

            Assert.True(handler.HasNotificationsError());

            Assert.True(notifications.Count == 1);
        }

        [Fact]
        public async void DomainNotificationClear_Ok()
        {
            var faker = new Faker();


            var handler = new DomainNotificationHandler();


            var qtError = faker.Random.Int(min: 3, max: 10);
            var qtSuccess = faker.Random.Int(min: 3, max: 10);


            for (var i = 0; i < qtError; i++)
            {
                var message = faker.Lorem.Sentence();

                var domainNotification = DomainNotification.Fail(message);
                await handler.Handle(domainNotification, new CancellationToken());

            }

            for (var i = 0; i < qtSuccess; i++)
            {
                var message = faker.Lorem.Sentence();

                var domainNotification = DomainNotification.Success(message);
                await handler.Handle(domainNotification, new CancellationToken());

            }

            handler.Clear();

            var notificationsError = handler.GetNotificationsError();
            var notificationsSuccess = handler.GetNotificationsSuccess();

            Assert.False(handler.HasNotificationsError());
            Assert.False(handler.HasNotificationsSucess());


            Assert.True(notificationsError.Count == 0);
            Assert.True(notificationsSuccess.Count == 0);



        }

        [Fact]
        public async void GetNotificationsError_Ok()
        {
            var faker = new Faker();

            var message = faker.Lorem.Sentence();

            var handler = new DomainNotificationHandler();

            var domainNotification = DomainNotification.Fail(message);

            await handler.Handle(domainNotification, new CancellationToken());

            var notifications = handler.GetNotificationsError();

            var result = notifications.Select(x => x.Value);


            Assert.Contains(message, result);

        }

        [Fact]
        public async void GetNotificationsSucess_Ok()
        {
            var faker = new Faker();

            var message = faker.Lorem.Sentence();

            var handler = new DomainNotificationHandler();

            var domainNotification = DomainNotification.Success(message);

            await handler.Handle(domainNotification, new CancellationToken());

            var notifications = handler.GetNotificationsSuccess();

            var result = notifications.Select(x => x.Value);


            Assert.Contains(message, result);

        }

    }
}
