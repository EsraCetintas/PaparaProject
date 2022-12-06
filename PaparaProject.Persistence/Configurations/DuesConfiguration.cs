using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PaparaProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Persistence.Configurations
{
    public class DuesConfiguration : IEntityTypeConfiguration<Dues>
    {

        public void Configure(EntityTypeBuilder<Dues> builder)
        {
            builder.ToTable("Dues");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FlatId)
                .HasColumnName("FlatId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.AmountOfDues)
               .HasColumnName("AmountOfDues")
               .HasColumnType("decimal")
               .IsRequired();

            builder.Property(p => p.PaymentDate)
               .HasColumnName("PaymentDate")
               .HasColumnType("datetime")
               .IsRequired();

            builder.Property(p => p.Deadline)
              .HasColumnName("Deadline")
              .HasColumnType("datetime")
              .IsRequired();

            builder.HasOne(p => p.Flat);
        }
    }
}
