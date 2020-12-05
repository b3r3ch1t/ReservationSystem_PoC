using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.Data.Context;

namespace ReservationSystem_PoC.Common.Context
{
    public class ReservarionSystemDbContextFaker
    {
        public static ReservarionSystemDbContext GetDatabaseInMemory()
        {
            var builder = new DbContextOptionsBuilder<ReservarionSystemDbContext>();
            builder.UseInMemoryDatabase(databaseName: "LibraryDbInMemory");

            var dbContextOptions = builder.Options;
            var dbContextInMemory = new ReservarionSystemDbContext(dbContextOptions);
            // Delete existing db before creating a new one
            dbContextInMemory.Database.EnsureDeleted();
            dbContextInMemory.Database.EnsureCreated();

            return dbContextInMemory;
        }
    }
}
