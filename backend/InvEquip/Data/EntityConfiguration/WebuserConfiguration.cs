using InvEquip.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.EntityConfiguration
{
    public class WebuserConfiguration : IEntityTypeConfiguration<Webuser>
    {
        public void Configure(EntityTypeBuilder<Webuser> builder)
        {
            builder.HasKey(wu => wu.Id);
            builder.Property(wu => wu.Email).IsRequired(true);
            builder.HasIndex(wu => wu.Email).IsUnique();
        }
    }
}
