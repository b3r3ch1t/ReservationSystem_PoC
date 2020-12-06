using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using ReservationSystem_PoC.Domain.Core.Entities;
using ReservationSystem_PoC.Domain.Core.Extensions;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ReservationSystem_PoC.Data.Context
{
    public sealed  class ReservarionSystemDbContext : DbContext
    {

        public ReservarionSystemDbContext(DbContextOptions<ReservarionSystemDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }


        //Override of Save changes to change :
        // ChangeDateOfChange --> Modified
        // ChangeDateOfDateOfCreation --> Added
        //


        #region SaveChanges

        public override int SaveChanges()
        {


            UpdateData();
            var result = base.SaveChanges();


            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateData();

            var result = base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private void UpdateData()
        {
            var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (!(entry.Entity is IEntity trackable)) continue;
                switch (entry.State)
                {
                    case EntityState.Modified:

                        trackable.ChangeDateOfChange();

                        break;

                    case EntityState.Added:

                        trackable.ChangeDateOfCreation();
                        trackable.ChangeDateOfChange();
                        trackable.Ativate();

                        if (trackable.Id == Guid.Empty || !trackable.Id.IsValidGuid())
                        {
                            trackable.ChangeId();
                        }

                        break;

                }



            }

        }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            AddMappings(modelBuilder);


            modelBuilder.Ignore<ValidationFailure>();

            modelBuilder.Ignore<ValidationResult>();

             
        }


        //reflect to add Mappings in Assembly
        private static void AddMappings(ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(x => x.Namespace != null &&
                            x.Namespace
                                .Contains(value: typeof(ReservarionSystemDbContext).Namespace.Replace(".Context", "") +
                                                 ".Mappings") &&
                            !x.Namespace.Contains("<>"));


            foreach (var type in typesToRegister)
            {
                if (type.Name.StartsWith("<>")) return;

                dynamic configInstance = Activator.CreateInstance(type);

                try
                {
                    modelBuilder.ApplyConfiguration(configInstance);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
             
        }
    }

}
