using ReservationSystem_PoC.Common.Identities;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.EntityTests
{
    public class ContactTypeTest
    {

        [Fact]
        public void ContactType_Valid()
        {
            //arrange
            var contactType = ContactTypeFaker.GetContactTypeOk();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.True(result);

        }

        [Fact]
        public void ContactType_MessageNull_False()
        {
            //arrange
            var contactType = ContactTypeFaker.GetContactTypeMessageNull();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.False(result);

        }

        [Fact]
        public void ContactType_MessageEmpty_False()
        {
            //arrange
            var contactType = ContactTypeFaker.GetContactTypeMessageEmpty();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.False(result);

        }


        [Fact]
        public void ContactType_MessageGreaterThanLimit_False()
        {
            //arrange
            var contactType = ContactTypeFaker.GetContactTypeMessageMessageGreaterThanLimit();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.False(result);

        }

        [Fact]
        public void ContactType_MessageGreaterLessLimite_False()
        {
            //arrange
            var contactType = ContactTypeFaker.GetContactTypeMessageGreaterLessLimite();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.False(result);

        }
    }
}
