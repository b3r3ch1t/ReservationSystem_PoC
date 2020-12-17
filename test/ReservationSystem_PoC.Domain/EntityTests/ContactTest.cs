using Bogus;
using ReservationSystem_PoC.Common.Identities;
using Xunit;

namespace ReservationSystem_PoC.Domain.Test.EntityTests
{
    public class ContactTest
    {
        [Fact]
        public void ContactOk()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();

            //act
            var result = contact.IsValid();

            //assert
            Assert.True(result);

        }


        [Fact]
        public void ContactNameNullFalse()
        {
            //arrange
            var contact = ContactFaker.GetContactContactNameNull();

            //act
            var result = contact.IsValid();
            //assert
            Assert.False(result);

        }


        [Fact]
        public void ContactNameEmptyFalse()
        {
            //arrange
            var contact = ContactFaker.GetContactContactNameEmpty();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }


        [Fact]
        public void ContactNameWhiteSpaceFalse()
        {
            //arrange
            var contact = ContactFaker.GetContactContactNameWhiteSpace();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }


        [Fact]
        public void ContactPhoneFalse()
        {
            //arrange
            var contact = ContactFaker.GetContactContactPhoneNull();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }

        [Fact]
        public void ContactBirthDateGreaterTodayFalse()
        {
            //arrange
            var contact = ContactFaker.GetContactContactBirthDateGreaterToday();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }



        [Fact]
        public void ContactReservationNullFalse()
        {
            //arrange
            var contact = ContactFaker.GetContactContactReservationNull();

            //act
            var result = contact.IsValid();

            //assert
            Assert.False(result);

        }



        [Fact]
        public void ContactChangeNameTrue()
        {
            var contact = ContactFaker.GetContactOk();

            var newName = new Faker().Name.FullName();

            contact.ChangeName(newName);

            Assert.True(contact.Name == newName);
        }

        [Fact]
        public void ContactChangePhoneTrue()
        {

            var contact = ContactFaker.GetContactOk();

            var newPhoneNumber = new Faker().Phone.PhoneNumber();

            contact.ChangePhoneNumber(newPhoneNumber);

            Assert.True(contact.PhoneNumber == newPhoneNumber);


        }

        [Fact]
        public void ContactChangeBirthDateOk()
        {
            var contact = ContactFaker.GetContactOk();

            var newBirthDate = new Faker().Date.Future();

            contact.ChangeBirthDate(newBirthDate);

            Assert.True(contact.BirthDate == newBirthDate);
        }

        [Fact]
        public void ContactNameGreaterFalse()
        {
            var contact = ContactFaker.GetContactContactNameGreater();

            var result = contact.IsValid();

            Assert.False(result);
        }

        [Fact]
        public void ContactNameLessFalse()
        {
            var contact = ContactFaker.GetContactContactNameLess();

            var result = contact.IsValid();

            Assert.False(result);
        }


        [Fact]
        public void Contact_ChangeContactType_Ok()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();
            var contactType = ContactTypeFaker.GetContactTypeOk();

            //act

            contact.ChangeContactType(contactType);



            //assert
            Assert.True(contact.ContactTypeId == contactType.Id);

        }

        [Fact]
        public void Contact_ChangeContactType_Null()
        {
            //arrange
            var contact = ContactFaker.GetContactOk();

            var contactTypeId = contact.ContactType.Id;
            //act

            contact.ChangeContactType(null);


            //assert
            Assert.True(contact.ContactTypeId == contactTypeId);

        }

    }
}
