using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InvEquip.Data.Entity;

namespace InvEquip.Data.EntityConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            //builder.Property(p => p.Deleted).HasDefaultValue(false);
            builder.Property(p => p.RoleId).HasDefaultValue(1);

            builder.HasOne(p => p.Role)
                .WithMany(r => r.People)
                .HasForeignKey(p => p.RoleId);

            builder.HasMany(p => p.Devices)
                .WithMany(d => d.People)
                .UsingEntity<PersonDevice>(
                pd => pd
                .HasOne(pd => pd.Device)
                .WithMany(d => d.PeopleDevices)
                .HasForeignKey(pd => pd.DeviceId),
                pd => pd
                .HasOne(pd => pd.Person)
                .WithMany(p => p.PersonDevice)
                .HasForeignKey(pd => pd.PersonId),
                pd =>
                {
                    pd.HasKey(pd => new { pd.Id });
                });
        }
    }
}
