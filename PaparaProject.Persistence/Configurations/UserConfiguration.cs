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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FlatId)
              .HasColumnName("FlatId")
              .HasColumnType("int")
              .IsRequired();

            builder.Property(p => p.Name)
             .HasColumnName("Name")
             .HasColumnType("varchar(100)")
             .IsRequired();

            builder.Property(p => p.SurName)
             .HasColumnName("SurName")
             .HasColumnType("varchar(100)")
             .IsRequired();

            builder.Property(p => p.IdentityNo)
            .HasColumnName("IdentityNo")
            .HasColumnType("varchar(11)")
            .IsRequired();

            builder.Property(p => p.EMail)
             .HasColumnName("EMail")
             .HasColumnType("varchar(100)")
             .IsRequired();

            builder.Property(p => p.PhoneNumber)
             .HasColumnName("PhoneNumber")
             .HasColumnType("varchar(11)")
             .IsRequired();

            builder.Property(p => p.NumberPlate)
            .HasColumnName("NumberPlate")
            .HasColumnType("varchar(20)");

            builder.Property(p => p.PasswordHash)
            .HasColumnName("PasswordHash")
            .HasColumnType("varbinary(MAX)")
            .IsRequired();

            builder.Property(p => p.PasswordSalt)
            .HasColumnName("PasswordSalt")
            .HasColumnType("varbinary(MAX)")
            .IsRequired();

            //Biz burda Bier-bir ilişki kurduk.
            builder.HasOne(p => p.Flat);
        }
    }
}
