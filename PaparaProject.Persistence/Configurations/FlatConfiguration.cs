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
    public class FlatConfiguration : IEntityTypeConfiguration<Flat>
    {
        public void Configure(EntityTypeBuilder<Flat> builder)
        {
            builder.ToTable("Flats");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FlatTypeId)
               .HasColumnName("FlatTypeId")
               .HasColumnType("int")
               .IsRequired();

            builder.Property(p => p.BlockNo)
               .HasColumnName("BlockNo")
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder.Property(p => p.FloorNo)
             .HasColumnName("FloorNo")
             .HasColumnType("varchar(100)")
             .IsRequired();

            builder.Property(p => p.FlatNo)
            .HasColumnName("FlatNo")
            .HasColumnType("varchar(100)")
            .IsRequired();

            builder.Property(p => p.FlatState)
            .HasColumnName("FlatState")
            .HasColumnType("bit")
            .IsRequired();

            builder.HasOne(p => p.FlatType);
        }
    }
}
