using Bogus;
using ReservationSystem_PoC.Domain.Core.Entities;
using System;

namespace ReservationSystem_PoC.Common.Identities
{
    public static class ReservationFaker
    {
        private static readonly Faker Faker = new Faker();

        public static Reservation GetReservationOk()
        {
            var message = Faker.Lorem.Paragraph(min: Reservation.MinMessageSize);

            if (message.Length >= Reservation.MaxMessageSize)
            {
                message = message.Substring(0, Reservation.MaxMessageSize);
            }

            var contact = ContactFaker.GetContactOk();

            var ranking = Faker.Random.Int(min: 1, max: 5);

            var favorited = Faker.Random.Bool();

            var reservation = new Reservation(
                id: Guid.NewGuid(),
                message: message,
                contact: contact,
                ranking: ranking,
                favorited: favorited
            );

            return reservation;

        }

        public static Reservation GetReservationMessageNull()
        {
            var reservation = GetReservationOk();

            reservation.ChangeMessage(null);

            return reservation;
        }

        public static Reservation GetReservationMessageEmpty()
        {
            var reservation = GetReservationOk();

            reservation.ChangeMessage(string.Empty);

            return reservation;
        }

        public static Reservation GetReservationMessageLess()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(ContactType.MinDescriptionSize);

            //Create a random text with max=3  
            var message = faker.Random.String2(length: length);

            var reservation = GetReservationOk();

            reservation.ChangeMessage(message);

            return reservation;
        }

        public static Reservation GetReservationMessageGreater()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(1024);

            while (length <= Reservation.MaxMessageSize)
            {
                length = Randomizer.Seed.Next(1024);
            }

            //Create a random text with min=3 and ContactType.MaxDescriptionSize
            var message = faker.Random.String2(length: length);

            var reservation = GetReservationOk();

            reservation.ChangeMessage(message);

            return reservation;
        }

        public static Reservation GetReservationRankingLess()
        {
            var reservation = GetReservationOk();

            var ranking = Faker.Random.Int(min: 0, Reservation.MinRanking - 1);

            reservation.ChangeRanking(ranking);

            return reservation;
        }

        public static Reservation GetReservationRankingGreater()
        {
            var reservation = GetReservationOk();

            var ranking = Faker.Random.Int(min: Reservation.MaxRanking, Reservation.MaxRanking + 10);

            reservation.ChangeRanking(ranking);

            return reservation;
        }
    }
}
