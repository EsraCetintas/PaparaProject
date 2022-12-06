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
    public class InvoiceTypeConfiguration : IEntityTypeConfiguration<InvoiceType>
    {
        public void Configure(EntityTypeBuilder<InvoiceType> builder)
        {
            builder.ToTable("InvoiceTypes");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.InvoiceTypeName)
                .HasColumnName("InvoiceTypeName")
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
