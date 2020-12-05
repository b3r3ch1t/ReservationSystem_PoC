using Bogus;
using ReservationSystem_PoC.Common.Identities;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.EntityTests
{
    public class ReservationTest
    {
        [Fact]
        public void Reservation_OK()
        {
            var reservation = ReservationFaker.GetReservationOk();

            var result = reservation.IsValid();

            Assert.True(result);
        }

        [Fact]
        public void Reservation_ChangeMessage()
        {
            var reservation = ReservationFaker.GetReservationOk();

            var oldMessage = reservation.Message;

            var result = reservation.Message == oldMessage;

            Assert.True(result);
        }

        [Fact]
        public void Reservation_MessageNull_False()
        {
            var reservation = ReservationFaker.GetReservationMessageNull();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_MessageEmpty_False()
        {
            var reservation = ReservationFaker.GetReservationMessageEmpty();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_MessageLess_False()
        {
            var reservation = ReservationFaker.GetReservationMessageLess();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_MessageGreater_False()
        {
            var reservation = ReservationFaker.GetReservationMessageGreater();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_Ranking_Less()
        {
            var reservation = ReservationFaker.GetReservationRankingLess();

            var result = reservation.IsValid();

            Assert.False(result);
        }


        [Fact]
        public void Reservation_ChangeRanking_True()
        {
            var reservation = ReservationFaker.GetReservationOk();

            var ranking = new Faker().Random.Int();

            reservation.ChangeRanking(ranking);

            var result = reservation.Ranking == ranking;

            Assert.True(result);
        }

        [Fact]
        public void Reservation_Ranking_Greater()
        {
            var reservation = ReservationFaker.GetReservationRankingGreater();

            var ranking = new Faker().Random.Int();

            reservation.ChangeRanking(ranking);

            var result = reservation.Ranking == ranking;

            Assert.True(result);
        }

        [Fact]
        public void Reservation_Ranking_ChangeFavorited_True()
        {
            var reservation = ReservationFaker.GetReservationOk();

            var favorited = reservation.Favorited;

            reservation.ChangeFavorited();

            Assert.True(reservation.Favorited == !favorited);
        }
    }
}
