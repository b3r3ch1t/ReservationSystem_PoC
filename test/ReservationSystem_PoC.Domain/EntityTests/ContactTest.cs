using Bogus;
using ReservationSystem_PoC.Common.Identities;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.EntityTests
{
    public class ContactTest
    {
        [Fact]
        public void Contact_Ok()
        {
            //arrange
            var contact = ContactFaker.Get_Contact_Ok();

            //act
            var result = contact.IsValid();

            //assert
            Assert.True(result);

        }


        [Fact]
        public void ContactNameNull_False()
        {
            //arrange
            var contact = ContactFaker.Get_Contact_ContactNameNull();

            //act
            var result = contact.IsValid();
            //assert
            Assert.False(result);

        }


        [Fact]
        public void ContactNameEmpty_False()
        {
            //arrange
            var contact = ContactFaker.Get_Contact_ContactNameEmpty();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }


        [Fact]
        public void ContactNameWhiteSpace_False()
        {
            //arrange
            var contact = ContactFaker.Get_Contact_ContactNameWhiteSpace();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }


        [Fact]
        public void ContactPhone_False()
        {
            //arrange
            var contact = ContactFaker.Get_Contact_ContactPhoneNull();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }

        [Fact]
        public void ContactBirthDateGreaterToday_False()
        {
            //arrange
            var contact = ContactFaker.Get_Contact_ContactBirthDateGreaterToday();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }

        [Fact]
        public void Contact_ContactTypeInvalid_False()
        {
            //arrange
            var contact = ContactFaker.Get_Contact_Contact_ContactTypeInvalid();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }


        [Fact]
        public void Contact_Reservation_Null_False()
        {
            //arrange
            var contact = ContactFaker.Get_Contact_Contact_Reservation_Null();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }

        [Fact]
        public void Contact_ContactTypeNull_False()
        {
            //arrange
            var contact = ContactFaker.Get_Contact_Contact_ContactTypeNull();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }


        [Fact]
        public void Contact_ChangeName_True()
        {
            var contact = ContactFaker.Get_Contact_Ok();

            var newName = new Faker().Name.FullName();

            contact.ChangeName(newName);

            Assert.True(contact.Name == newName);
        }

        [Fact]
        public void Contact_ChangePhone_True()
        {

            var contact = ContactFaker.Get_Contact_Ok();

            var newPhoneNumber = new Faker().Phone.PhoneNumber();

            contact.ChangePhoneNumber(newPhoneNumber);

            Assert.True(contact.PhoneNumber == newPhoneNumber);


        }

        [Fact]
        public void Contact_ChangeBirthDate_Ok()
        {
            var contact = ContactFaker.Get_Contact_Ok();

            var newBirthDate = new Faker().Date.Future();

            contact.ChangeBirthDate(newBirthDate);

            Assert.True(contact.BirthDate == newBirthDate);
        }

        [Fact]
        public void Contact_NameGreater_False()
        {
            var contact = ContactFaker.Get_Contact_ContactNameGreater();

            var result = contact.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void Contact_NameLess_False()
        {
            var contact = ContactFaker.Get_Contact_ContactNameLess();

            var result = contact.IsValid();

            Assert.False(result);
        }
    }
}
