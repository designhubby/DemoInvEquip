using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvEquip.Data.Entity;

namespace InvEquip.Data.EntityConfiguration
{
    public class PersonDevicesConfiguration : IEntityTypeConfiguration<PersonDevice>
    {
        public void Configure(EntityTypeBuilder<PersonDevice> builder)
        {
            
            //builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.Property(pd => pd.StartDate).HasColumnType("date").IsRequired(false);
            builder.Property(pd => pd.EndDate).HasColumnType("date").IsRequired(false);  

        }
    }
}
