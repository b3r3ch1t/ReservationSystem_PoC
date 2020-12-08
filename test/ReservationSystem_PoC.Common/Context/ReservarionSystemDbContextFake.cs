using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.Data;
using ReservationSystem_PoC.Data.Context;

namespace ReservationSystem_PoC.Common.Context
{
    public class ReservarionSystemDbContextFaker
    {
        public static ReservarionSystemDbContext GetDatabaseInMemory()
        {
            var InMemoryConnectionString = "DataSource=:memory:";

            var connection = new SqliteConnection(InMemoryConnectionString);


            connection.Open();



            var options = new DbContextOptionsBuilder<ReservarionSystemDbContext>()
                .UseSqlite(connection)
                .Options;
            var context = new ReservarionSystemDbContext(options);


            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            DataSeeder.CreateContactType(context);
            DataSeeder.CreateContacts(context);
            DataSeeder.CreateReservations(context);

            return context;
        }

    }
}