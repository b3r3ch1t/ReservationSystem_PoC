using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReservationSystem_PoC.Domain.Core.Entities;
using System.Collections.Generic;

namespace ReservationSystem_PoC.Data.Mappings
{
    internal class ContactTypeMappingc : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
        {
            // Primary key
            builder.HasKey(x => x.Id);

            builder.ToTable("ContactType");

            builder.HasIndex(x => x.DateOfChange);
            builder.HasIndex(x => x.DateOfCreation);
            builder.HasIndex(x => x.Valid);
            builder.HasIndex(x => x.Description);


            builder.Property(p => p.Description)
                .HasMaxLength(ContactType.MaxDescriptionSize)
                .IsRequired();

            builder.HasData(new List<ContactType>()
            {
                new ContactType("Contact Type 1"),
                new ContactType("Contact Type 2"),
                new ContactType("Contact Type 3"),
            });
        }
    }
}
