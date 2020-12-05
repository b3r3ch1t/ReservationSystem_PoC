using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem_PoC.Domain.Core.Entities;

namespace ReservationSystem_PoC.Data.Mappings
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            // Primary key
            builder.HasKey(x => x.Id);

            builder.ToTable("Contacts");

            builder.HasIndex(x => x.DateOfChange);
            builder.HasIndex(x => x.DateOfCreation);
            builder.HasIndex(x => x.Valid);
            builder.HasIndex(x => x.BirthDate);
            builder.HasIndex(x => x.PhoneNumber);


            builder.HasIndex(x => x.Name);

            builder.Property(p => p.Name)
                .HasMaxLength(Contact.MaxNameSize)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder.Ignore(x => x.ValidationResult);


            builder
                .HasOne(s => s.ContactType)
                .WithMany(g => g.Contacts)
                .HasForeignKey(s => s.ContactTypeId);



        }
    }
}