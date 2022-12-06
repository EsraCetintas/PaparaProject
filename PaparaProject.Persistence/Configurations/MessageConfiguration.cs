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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
             .HasColumnName("Title")
             .HasColumnType("varchar(100)")
             .IsRequired();

            builder.Property(p => p.Context)
             .HasColumnName("Context")
             .HasColumnType("varchar(100)")
             .IsRequired();

            builder.Property(p => p.IsReaded)
            .HasColumnName("IsReaded")
            .HasColumnType("tinyint")
            .IsRequired();

            builder.Property(p => p.IsNew)
            .HasColumnName("IsNew")
            .HasColumnType("tinyint")
            .IsRequired();
        }
    }
}
