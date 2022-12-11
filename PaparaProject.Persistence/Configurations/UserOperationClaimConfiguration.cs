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
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.ToTable("UserOperationClaims");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                .HasColumnName("UserId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.OperationClaimId)
               .HasColumnName("OperationClaimId")
               .HasColumnType("int")
               .IsRequired();

            builder.HasOne(p => p.User);
            builder.HasOne(p => p.OperationClaim);
        }
    }
}

