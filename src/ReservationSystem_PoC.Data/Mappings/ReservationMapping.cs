using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem_PoC.Domain.Core.Entities;

namespace ReservationSystem_PoC.Data.Mappings
{
    internal class ReservationMapping : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            // Primary key
            builder.HasKey(x => x.Id);

            builder.ToTable("Reservations");

            builder.HasIndex(x => x.DateOfChange);
            builder.HasIndex(x => x.DateOfCreation);
            builder.HasIndex(x => x.Valid);
            builder.HasIndex(x => x.Ranking);
            builder.HasIndex(x => x.Favorited);

            builder.Property(p => p.Message)
                .HasMaxLength(Reservation.MaxMessageSize)
                .IsRequired();


            builder
                .HasOne(s => s.Contact)
                .WithMany(g => g.Reservations)
                .HasForeignKey(s => s.ContactId);


        }
    }
}
