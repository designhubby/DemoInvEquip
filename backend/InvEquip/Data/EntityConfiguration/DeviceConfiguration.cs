using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvEquip.Data.Entity;

namespace InvEquip.Data.EntityConfiguration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            //builder.Property(x => x.Deleted).HasDefaultValue(false);

            builder.HasOne(d => d.HwModel)
                .WithMany(hm => hm.Devices)
                .HasForeignKey(d => d.HwModelId)
                .IsRequired(false);
            builder.HasOne(d => d.Contract)
                .WithMany(c => c.Devices)
                .HasForeignKey(d => d.ContractId)
                .IsRequired(false);
        }
    }
}
