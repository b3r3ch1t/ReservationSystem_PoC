using Bogus;
using ReservationSystem_PoC.Domain.Core.Entities;

namespace ReservationSystem_PoC.Domain.Test.Fakers
{
    internal static class ContactTypeFaker
    {
        internal static ContactType Get_ContactType_Ok()
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
        internal static ContactType Get_ContactType_MessageNull()
        {
            return new ContactType(description: null);
        }
        internal static ContactType Get_ContactType_MessageEmpty()
        {
            return new ContactType(description: null);
        }
        internal static ContactType Get_ContactType_MessageMessageGreaterThanLimit()
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
        internal static ContactType Get_ContactType_MessageGreaterLessLimite()
        {
            var faker = new Faker();
            var length = Randomizer.Seed.Next(ContactType.MinDescriptionSize);

            //Create a random text with max=3  
            var description = faker.Random.String2(length: length);

            return new ContactType(description: description);
        }
    }
}
