using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.Data.Context;
using System;
using System.Text;

namespace ReservationSystem_PoC.Common.Context
{
    public class ReservarionSystemDbContextFaker
    {
        public static ReservarionSystemDbContext GetDatabaseInMemory()
        {
            return TestWithSqlite();


            var builder = new DbContextOptionsBuilder<ReservarionSystemDbContext>();
            //builder.UseInMemoryDatabase(databaseName: "ReservarionSystemDbContext");

            //builder.UseSqlite(@"Data Source='Some Source Folder'\Database.db");

            var dbContextOptions = builder.Options;
            var dbContextInMemory = new ReservarionSystemDbContext(dbContextOptions);


            // Delete existing db before creating a new one
            dbContextInMemory.Database.EnsureDeleted();
            dbContextInMemory.Database.EnsureCreated();
            dbContextInMemory.Database.ExecuteSqlRaw(AddStoreProcedure());

            return dbContextInMemory;
        }

        private static string AddStoreProcedure()
        {

            var sb = new StringBuilder();

            sb.AppendLine(" CREATE PROCEDURE UpdateContactType @Description nvarchar(512), @Valid BIT,@DateOfChange DATETIME2 (7), @DateOfCreation  DATETIME2 (7), @Id UNIQUEIDENTIFIER ");
            sb.AppendLine(" AS ");
            sb.AppendLine(" BEGIN ");
            sb.AppendLine("	SET NOCOUNT ON; ");
            sb.AppendLine(" 	UPDATE [dbo].[ContactType] ");
            sb.AppendLine("                SET ");
            sb.AppendLine(" 					[Description] = @Description,");
            sb.AppendLine("					    [Valid] = @Valid,  ");
            sb.AppendLine("					    [DateOfChange] = @DateOfChange , ");
            sb.AppendLine("					    [DateOfCreation] = @DateOfCreation");
            sb.AppendLine("                WHERE (Id = @Id)");
            sb.AppendLine("END");
            sb.AppendLine("GO");

            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();

            return sb.ToString();

        }

        public static ReservarionSystemDbContext TestWithSqlite()
        {
            var InMemoryConnectionString = "DataSource=:memory:";

            var connection = new SqliteConnection(InMemoryConnectionString);

            try
            {
                connection.Open();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            var options = new DbContextOptionsBuilder<ReservarionSystemDbContext>()
                .UseSqlite(connection)
                .Options;
            var reservarionSystemDbContext = new ReservarionSystemDbContext(options);
            reservarionSystemDbContext.Database.EnsureCreated();

            return reservarionSystemDbContext;
        }
    }
}