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
    public class FlatTypeConfiguration : IEntityTypeConfiguration<FlatType>
    {
        public void Configure(EntityTypeBuilder<FlatType> builder)
        {
            builder.ToTable("FlatTypes");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FlatTypeName)
                .HasColumnName("FlatTypeName")
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
