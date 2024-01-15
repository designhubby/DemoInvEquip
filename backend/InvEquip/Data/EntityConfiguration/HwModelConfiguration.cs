using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvEquip.Data.Entity;

namespace InvEquip.Data.EntityConfiguration
{
    public class HwModelConfiguration :  IEntityTypeConfiguration<HwModel>
    {
        public void Configure(EntityTypeBuilder<HwModel> builder)
        {
            builder.Property(hm => hm.HwModelName).IsRequired(true);
            builder.Property(hm => hm.DeviceTypeId).HasDefaultValue(1);
            builder.Property(hm => hm.VendorId).HasDefaultValue(1);

            builder.HasOne(hm => hm.Vendor)
                .WithMany(v => v.HwModels)
                .HasForeignKey(hm => hm.VendorId)
                .IsRequired(false);
            
            builder.HasOne(hm => hm.DeviceType)
                .WithMany(dt => dt.HwModels)
                .HasForeignKey(hm => hm.DeviceTypeId)
                .IsRequired(false);

        }
    }
}
