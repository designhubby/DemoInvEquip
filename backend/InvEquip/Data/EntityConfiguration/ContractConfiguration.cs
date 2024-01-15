using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvEquip.Data.Entity;

namespace InvEquip.Data.EntityConfiguration
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.Property(c => c.ContractName).IsRequired(true);
            builder.Property(c => c.VendorId).HasDefaultValue(1);
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Vendor)
                .WithMany(v => v.Contracts)
                .HasForeignKey(c => c.VendorId);
            builder.Property(pd => pd.StartDate).HasColumnType("date").IsRequired(false);
            builder.Property(pd => pd.EndDate).HasColumnType("date").IsRequired(false);
        }
    }
}
