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


            reservarionSystemDbContext.Database.EnsureDeleted();

            reservarionSystemDbContext.Database.EnsureCreated();

            //Create the Store Procedure 

            AddStoreProcedure(reservarionSystemDbContext);

            return reservarionSystemDbContext;
        }

        private static void AddStoreProcedure(ReservarionSystemDbContext context)
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



            context.Database.ExecuteSqlRaw(sb.ToString());

        }


    }
}