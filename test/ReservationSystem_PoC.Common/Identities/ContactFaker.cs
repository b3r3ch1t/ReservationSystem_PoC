using Bogus;
using ReservationSystem_PoC.Domain.Core.Entities;
using System.Collections.Generic;

namespace ReservationSystem_PoC.Common.Identities
{
    public static class ContactFaker
    {

        private static readonly Faker Faker = new Faker();


        public static Contact Get_Contact_Ok()
        {
            var faker = new Faker();

            var name = faker.Name.FullName();
            var phoneNumer = "555 555 1212";
            var birthDate = faker.Date.Past();
            var contactType = ContactTypeFaker.Get_ContactType_Ok();


            return new Contact(name: name,
                phoneNumber: phoneNumer,
                birthDate: birthDate,
                contactType: contactType);
        }

        public static Contact Get_Contact_ContactNameEmpty()
        {
            var contact = Get_Contact_Ok();

            contact.ChangeName(string.Empty);

            return contact;
        }

        public static Contact Get_Contact_ContactNameNull()
        {
            var contact = Get_Contact_Ok();

            contact.ChangeName(null);

            return contact;
        }

        public static Contact Get_Contact_ContactPhoneNull()
        {
            var contact = Get_Contact_Ok();

            contact.ChangePhoneNumber(null);

            return contact;
        }

        public static Contact Get_Contact_ContactNameWhiteSpace()
        {
            var contact = Get_Contact_Ok();

            contact.ChangeName(" ");

            return contact;
        }

        public static Contact Get_Contact_ContactBirthDateGreaterToday()
        {
            var contact = Get_Contact_Ok();

            var birthDate = Faker.Date.Future();

            contact.ChangeBirthDate(birthDate);

            return contact;
        }

        public static Contact Get_Contact_Contact_ContactTypeInvalid()
        {
            var contact = Get_Contact_Ok();

            var contactType = ContactTypeFaker.Get_ContactType_MessageEmpty();

            return Contact.Factory.GetContact(
                name: contact.Name,
                phoneNumber: contact.PhoneNumber,
                birthDate: contact.BirthDate,
                contactType: contactType,
                new List<Reservation>()
            );
        }

        public static Contact Get_Contact_Contact_Reservation_Null()
        {
            var contact = Get_Contact_Ok();

            var contactType = ContactTypeFaker.Get_ContactType_Ok();

            return Contact.Factory.GetContact(
                name: contact.Name,
                phoneNumber: contact.PhoneNumber,
                birthDate: contact.BirthDate,
                contactType: contactType,
                null
            );
        }

        public static Contact Get_Contact_Contact_ContactTypeNull()
        {
            var contact = Get_Contact_Ok();


            return Contact.Factory.GetContact(
                name: contact.Name,
                phoneNumber: contact.PhoneNumber,
                birthDate: contact.BirthDate,
                contactType: null,
                new List<Reservation>()
            );
        }

        public static Contact Get_Contact_ContactNameGreater()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(1024);

            while (length <= Contact.MaxNameSize)
            {
                length = Randomizer.Seed.Next(1024);
            }

            //Create a random text with min=3 and ContactType.MaxDescriptionSize
            var name = faker.Random.String2(length: length);

            var contact = Get_Contact_Ok();

            contact.ChangeName(name);

            return contact;
        }

        public static Contact Get_Contact_ContactNameLess()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(Contact.MinNameSize);

            //Create a random text with max=3  
            var name = faker.Random.String2(length: length);

            var contact = Get_Contact_Ok();

            contact.ChangeName(name);

            return contact;
        }
    }
}
