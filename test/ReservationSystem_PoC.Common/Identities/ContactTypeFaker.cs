using Bogus;
using ReservationSystem_PoC.Domain.Core.Entities;

namespace ReservationSystem_PoC.Common.Identities
{
    public static class ContactTypeFaker
    {
        public static ContactType GetContactTypeOk()
        {
            var faker = new Faker();

            //Create a random text with min=3 and ContactType.MaxDescriptionSize
            var description = faker
                .Lorem.Paragraph(min: 3)
               ;

            if (description.Length >= ContactType.MaxDescriptionSize)
            {
                description = description.Substring(0, ContactType.MaxDescriptionSize);
            }

            return new ContactType(description: description);
        }
        public static ContactType GetContactTypeMessageNull()
        {
            return new ContactType(description: null);
        }
        public static ContactType GetContactTypeMessageEmpty()
        {
            return new ContactType(description: null);
        }
        public static ContactType GetContactTypeMessageMessageGreaterThanLimit()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(1024);

            while (length <= ContactType.MaxDescriptionSize)
            {
                length = Randomizer.Seed.Next(1024);
            }

            //Create a random text with min=3 and ContactType.MaxDescriptionSize
            var description = faker.Random.String2(length: length);

            return new ContactType(description: description);
        }
        public static ContactType GetContactTypeMessageGreaterLessLimite()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(ContactType.MinDescriptionSize);

            //Create a random text with max=3  
            var description = faker.Random.String2(length: length);

            return new ContactType(description: description);
        }
    }
}
