using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvEquip.Data.Entity;
using InvEquip.Data.Seed;
using InvEquip.Data.EntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using InvEquip.Data.Authentication;
using Microsoft.Extensions.Options;
using InvEquip.Configuration;

namespace InvEquip.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        private readonly ConnectionStrings _azstring;
         public ApplicationDbContext(IOptionsMonitor<ConnectionStrings> optionsMonitor)
        {
            _azstring = optionsMonitor.CurrentValue;
        }
        //Configuration
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies().UseSqlServer(_azstring.DataAPI);

            //options.UseLazyLoadingProxies().UseSqlite("Data Source=C:\\database\\InvEquipDb.db");
        }

        //entities

        DbSet<Person> People { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<PersonDevice> PersonDevices { get; set; }
        DbSet<HwModel> HwModels { get; set; }
        DbSet<Role> Roles { get; set; }
        //DbSet<Webuser> Webusers { get; set; }

        //entity config fluent

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property("Deleted")
                    .HasDefaultValue(false);
            }
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new PersonDevicesConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new HwModelConfiguration());
            modelBuilder.Seed();
            modelBuilder.SeedTestData01();
            modelBuilder.SeedTestData02();
            modelBuilder.SeedTestData03();
            modelBuilder.SeedTestData04();
            //modelBuilder.ApplyConfiguration(new WebuserConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        }


    }
}
