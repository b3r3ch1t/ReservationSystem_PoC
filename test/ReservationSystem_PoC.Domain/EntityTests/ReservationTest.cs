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
            var reservation = ReservationFaker.Get_Reservation_OK();

            var result = reservation.IsValid();

            Assert.True(result);
        }

        [Fact]
        public void Reservation_ChangeMessage()
        {
            var reservation = ReservationFaker.Get_Reservation_OK();

            var oldMessage = reservation.Message;

            var result = reservation.Message == oldMessage;

            Assert.True(result);
        }

        [Fact]
        public void Reservation_MessageNull_False()
        {
            var reservation = ReservationFaker.Get_Reservation_MessageNull();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_MessageEmpty_False()
        {
            var reservation = ReservationFaker.Get_Reservation_MessageEmpty();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_MessageLess_False()
        {
            var reservation = ReservationFaker.Get_Reservation_MessageLess();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_MessageGreater_False()
        {
            var reservation = ReservationFaker.Get_Reservation_MessageGreater();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_Ranking_Less()
        {
            var reservation = ReservationFaker.Get_Reservation_Ranking_Less();

            var result = reservation.IsValid();

            Assert.False(result);
        }


        [Fact]
        public void Reservation_ChangeRanking_True()
        {
            var reservation = ReservationFaker.Get_Reservation_OK();

            var ranking = new Faker().Random.Int();

            reservation.ChangeRanking(ranking);

            var result = reservation.Ranking == ranking;

            Assert.True(result);
        }

        [Fact]
        public void Reservation_Ranking_Greater()
        {
            var reservation = ReservationFaker.Get_Reservation_Ranking_Greater();

            var ranking = new Faker().Random.Int();

            reservation.ChangeRanking(ranking);

            var result = reservation.Ranking == ranking;

            Assert.True(result);
        }

        [Fact]
        public void Reservation_Ranking_ChangeFavorited_True()
        {
            var reservation = ReservationFaker.Get_Reservation_OK();

            var favorited = reservation.Favorited;

            reservation.ChangeFavorited();

            Assert.True(reservation.Favorited == !favorited);
        }
    }
}
