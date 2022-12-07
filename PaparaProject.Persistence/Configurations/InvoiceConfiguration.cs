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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {

            builder.ToTable("Invoices");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FlatId)
                .HasColumnName("FlatId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.InvoiceTypeId)
               .HasColumnName("InvoiceTypeId")
               .HasColumnType("int")
               .IsRequired();

            builder.Property(p => p.AmountOfInvoice)
               .HasColumnName("AmountOfInvoice")
               .HasColumnType("decimal")
               .IsRequired();


            builder.Property(p => p.PaymentDate)
                .HasColumnName("PaymentDate")
                .HasColumnType("datetime");

            builder.Property(p => p.Deadline)
              .HasColumnName("Deadline")
              .HasColumnType("datetime")
              .IsRequired();

            builder.HasOne(p => p.Flat);
            builder.HasOne(p => p.InvoiceType);

        }
    }
}
