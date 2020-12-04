using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using ReservationSystem_PoC.Data.Context;

namespace ReservationSystem_PoC.Data.Factory
{
    class ReservarionSystemDbContextFactory : IDesignTimeDbContextFactory<ReservarionSystemDbContext>
    {
        public ReservarionSystemDbContext CreateDbContext(string[] args)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ReservarionSystemDbContext>();


            optionsBuilder
                .UseSqlServer(connectionString);

            optionsBuilder.EnableSensitiveDataLogging();

            return new ReservarionSystemDbContext(optionsBuilder.Options);
        }
    }
}
