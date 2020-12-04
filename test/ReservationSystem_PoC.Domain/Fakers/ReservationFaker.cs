using Bogus;
using ReservationSystem_PoC.Domain.Core.Entities;

namespace ReservationSystem_PoC.Domain.Test.Fakers
{
    internal static class ReservationFaker
    {
        private static readonly Faker Faker = new Faker();

        internal static Reservation Get_Reservation_OK()
        {
            var message = Faker.Lorem.Paragraph(min: Reservation.MinDescriptionSize);

            if (message.Length >= Reservation.MaxDescriptionSize)
            {
                message = message.Substring(0, Reservation.MaxDescriptionSize);
            }

            var contact = ContactFaker.Get_Contact_Ok();

            var ranking = Faker.Random.Int(min: 1, max: 5);

            var favorited = Faker.Random.Bool();

            var reservation = new Reservation(
                message: message,
                contact: contact,
                ranking: ranking,
                favorited: favorited
            );

            return reservation;

        }

        public static Reservation Get_Reservation_MessageNull()
        {
            var reservation = Get_Reservation_OK();

            reservation.ChangeMessage(null);

            return reservation;
        }

        public static Reservation Get_Reservation_MessageEmpty()
        {
            var reservation = Get_Reservation_OK();

            reservation.ChangeMessage(string.Empty);

            return reservation;
        }

        public static Reservation Get_Reservation_MessageLess()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(ContactType.MinDescriptionSize);

            //Create a random text with max=3  
            var message = faker.Random.String2(length: length);

            var reservation = Get_Reservation_OK();

            reservation.ChangeMessage(message);

            return reservation;
        }

        public static Reservation Get_Reservation_MessageGreater()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(1024);

            while (length <= ContactType.MaxDescriptionSize)
            {
                length = Randomizer.Seed.Next(1024);
            }

            //Create a random text with min=3 and ContactType.MaxDescriptionSize
            var message = faker.Random.String2(length: length);

            var reservation = Get_Reservation_OK();

            reservation.ChangeMessage(message);

            return reservation;
        }

        public static Reservation Get_Reservation_Ranking_Less()
        {
            var reservation = Get_Reservation_OK();

            var ranking = Faker.Random.Int(min: 0, Reservation.MinRanking - 1);

            reservation.ChangeRanking(ranking);

            return reservation;
        }

        public static Reservation Get_Reservation_Ranking_Greater()
        {
            var reservation = Get_Reservation_OK();

            var ranking = Faker.Random.Int(min: Reservation.MaxRanking, Reservation.MaxRanking + 10);

            reservation.ChangeRanking(ranking);

            return reservation;
        }
    }
}
