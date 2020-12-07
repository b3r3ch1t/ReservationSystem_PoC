using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem_PoC.Data.Context;
using ReservationSystem_PoC.Domain.Core.Entities;
using System;
using System.Linq;
using System.Text;

namespace ReservationSystem_PoC.Data
{
    public static class DataSeeder
    {

        public static void UseSeedDatabase(this IApplicationBuilder app)
        {
            using var scope =
                app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetService<ReservarionSystemDbContext>();
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.Database.EnsureDeleted();

            context.Database.Migrate();

            CreateContacts(context);
            CreateReservations(context);

            AddStoreProcedure(context);
        }

        private static void CreateReservations(ReservarionSystemDbContext context)
        {
            if (context.Reservations.Any()) return;

            if (!context.Contacts.Any()) return;

            var faker = new Faker();

            var quantity = faker.Random.Int(min: 10, max: 50);


            for (var i = 0; i < quantity - 1; i++)
            {
                faker = new Faker();

                var lengthMessage = faker.Random.Int(min: Reservation.MinMessageSize, max: Reservation.MaxMessageSize);
                var message = faker.Random.AlphaNumeric(length: lengthMessage);

                var contact = faker.PickRandom(context.Contacts.AsNoTracking().ToList());

                var ranking = faker.Random.Int(min: Reservation.MinRanking, max: Reservation.MaxRanking);

                var favorited = faker.Random.Bool();

                var reservation = new Reservation(
                    message: message,
                    contact: contact,
                    ranking: ranking,
                    favorited: favorited);

                context.Reservations.Add(reservation);


            }


            context.SaveChanges();


        }

        private static void CreateContacts(ReservarionSystemDbContext context)
        {
            if (!context.ContactTypes.Any()) CreateContactType(context);

            if (context.Contacts.Any()) return;

            var faker = new Faker();

            var quantity = faker.Random.Int(min: 10, max: 50);

            for (var i = 0; i < quantity - 1; i++)
            {

                faker = new Faker();

                var name = faker.Person.FullName;
                var phoneNumber = "555 555 1212";
                var birthDate = faker.Person.DateOfBirth;
                var contactType = context.ContactTypes.AsNoTracking().FirstOrDefault();

                var contact = new Contact(
                    name: name,
                    phoneNumber: phoneNumber,
                    birthDate: birthDate,
                    contactType: contactType
                );


                context.Contacts.Add(contact);


            }


            context.SaveChanges();


        }

        private static void CreateContactType(ReservarionSystemDbContext context)
        {
            if (!context.ContactTypes.Any()) return;
            var faker = new Faker();

            var quantity = faker.Random.Int(min: 10, max: 50);

            var lengthDescription =
                faker.Random.Int(min: ContactType.MinDescriptionSize, max: ContactType.MaxDescriptionSize);

            var fakerContactType = new Faker<ContactType>()
                .RuleFor(p => p.Description,
                    f =>
                        faker.Random.AlphaNumeric(length: lengthDescription)
                        )
                ;

            var listContactType = fakerContactType.Generate(quantity);

            context.ContactTypes.AddRange(listContactType);

            context.SaveChanges();
        }

        private static void AddStoreProcedure(ReservarionSystemDbContext context)
        {

            var sb = new StringBuilder();

            sb.Append(@" CREATE PROCEDURE UpdateContactType @Description nvarchar(512), @Valid BIT, @DateOfChange DATETIME2 (7), @DateOfCreation  DATETIME2 (7), @Id UNIQUEIDENTIFIER ");
            sb.Append(@" AS ");
            sb.Append(@" BEGIN ");
            sb.Append("	 SET NOCOUNT ON; ");
            sb.Append(@" UPDATE [dbo].[ContactType] ");
            sb.Append(@" SET ");
            sb.Append(@" [Description] = @Description,");
            sb.Append(@" [Valid] = @Valid,  ");
            sb.Append(@" [DateOfChange] = @DateOfChange , ");
            sb.Append(@" [DateOfCreation] = @DateOfCreation ");
            sb.Append(@" WHERE (Id = @Id); ");
            sb.Append(@" END ");

            try
            {
                context.Database.ExecuteSqlRaw(sb.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

    }
}
