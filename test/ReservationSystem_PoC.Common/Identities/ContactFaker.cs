using Bogus;
using ReservationSystem_PoC.Domain.Core.Entities;
using System.Collections.Generic;

namespace ReservationSystem_PoC.Common.Identities
{
    public static class ContactFaker
    {

        private static readonly Faker Faker = new Faker();


        public static Contact GetContactOk()
        {
            var faker = new Faker();

            var name = faker.Name.FullName();
            var phoneNumer = "555 555 1212";
            var birthDate = faker.Date.Past();
            var contactType = ContactTypeFaker.GetContactTypeOk();


            return Contact.Factory.GetContact(name: name,
                phoneNumber: phoneNumer,
                birthDate: birthDate,
                contactType: contactType,
                reservations: new List<Reservation>());
        }

        public static Contact GetContactContactNameEmpty()
        {
            var contact = GetContactOk();

            contact.ChangeName(string.Empty);

            return contact;
        }

        public static Contact GetContactContactNameNull()
        {
            var contact = GetContactOk();

            contact.ChangeName(null);

            return contact;
        }

        public static Contact GetContactContactPhoneNull()
        {
            var contact = GetContactOk();

            contact.ChangePhoneNumber(null);

            return contact;
        }

        public static Contact GetContactContactNameWhiteSpace()
        {
            var contact = GetContactOk();

            contact.ChangeName(" ");

            return contact;
        }

        public static Contact GetContactContactBirthDateGreaterToday()
        {
            var contact = GetContactOk();

            var birthDate = Faker.Date.Future();

            contact.ChangeBirthDate(birthDate);

            return contact;
        }

        public static Contact GetContactContactContactTypeInvalid()
        {
            var contact = GetContactOk();

            var contactType = ContactTypeFaker.GetContactTypeMessageEmpty();

            return Contact.Factory.GetContact(
                name: contact.Name,
                phoneNumber: contact.PhoneNumber,
                birthDate: contact.BirthDate,
                contactType: contactType,
                new List<Reservation>()
            );
        }

        public static Contact GetContactContactReservationNull()
        {
            var contact = GetContactOk();

            var contactType = ContactTypeFaker.GetContactTypeOk();

            return Contact.Factory.GetContact(
                name: contact.Name,
                phoneNumber: contact.PhoneNumber,
                birthDate: contact.BirthDate,
                contactType: contactType,
                null
            );
        }

        public static Contact GetContactContactContactTypeNull()
        {
            var contact = GetContactOk();


            return Contact.Factory.GetContact(
                name: contact.Name,
                phoneNumber: contact.PhoneNumber,
                birthDate: contact.BirthDate,
                contactType: null,
                new List<Reservation>()
            );
        }

        public static Contact GetContactContactNameGreater()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(1024);

            while (length <= Contact.MaxNameSize)
            {
                length = Randomizer.Seed.Next(1024);
            }

            //Create a random text with min=3 and ContactType.MaxDescriptionSize
            var name = faker.Random.String2(length: length);

            var contact = GetContactOk();

            contact.ChangeName(name);

            return contact;
        }

        public static Contact GetContactContactNameLess()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(Contact.MinNameSize);

            //Create a random text with max=3  
            var name = faker.Random.String2(length: length);

            var contact = GetContactOk();

            contact.ChangeName(name);

            return contact;
        }
    }
}
