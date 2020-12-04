using Bogus;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Test.Fakers;
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
            var reservation = ReservationFaker.Get_Reservation_MessageNull_False();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_MessageEmpty_False()
        {
            var reservation = ReservationFaker.Get_Reservation_MessageEmpty_False();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_MessageLess_False()
        {
            var reservation = ReservationFaker.Get_Reservation_MessageLess_False();

            var result = reservation.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Reservation_MessageGreater_False()
        {
            var reservation = ReservationFaker.Get_Reservation_MessageGreater_False();

            var result = reservation.IsValid();

            Assert.False(result);
        }
    }
}
