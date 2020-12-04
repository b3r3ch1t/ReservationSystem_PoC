using ReservationSystem_PoC.Domain.Test.Fakers;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.EntityTests
{
    public class ContactTypeTest
    {

        [Fact]
        public void ContactType_Valid()
        {
            //arrange
            var contactType = ContactTypeFaker.Get_ContactType_Ok();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.True(result);

        }

        [Fact]
        public void ContactType_MessageNull_False()
        {
            //arrange
            var contactType = ContactTypeFaker.Get_ContactType_MessageNull();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.False(result);

        }

        [Fact]
        public void ContactType_MessageEmpty_False()
        {
            //arrange
            var contactType = ContactTypeFaker.Get_ContactType_MessageEmpty();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.False(result);

        }


        [Fact]
        public void ContactType_MessageGreaterThanLimit_False()
        {
            //arrange
            var contactType = ContactTypeFaker.Get_ContactType_MessageMessageGreaterThanLimit();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.False(result);

        }

        [Fact]
        public void ContactType_MessageGreaterLessLimite_False()
        {
            //arrange
            var contactType = ContactTypeFaker.Get_ContactType_MessageGreaterLessLimite();

            //act
            var result = contactType.IsValid();

            //assert
            Assert.False(result);

        }
    }
}
