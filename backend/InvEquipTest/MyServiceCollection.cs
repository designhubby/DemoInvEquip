using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvEquip;
using InvEquip.Data.Repository;
using InvEquip.Data;
using InvEquip.Data.Mapper;
using InvEquip.Logic;

namespace InvEquipTest
{
    public static class MyServiceCollection
    {
        static IServiceProvider serviceProvider { get; set; }
        public static IServiceProvider GetServiceProvider()
        {
            var servicesCollection = new ServiceCollection();
            ServiceConfiguration.ServiceProfiles(servicesCollection);
            serviceProvider = servicesCollection.BuildServiceProvider();
            
            return serviceProvider;
        }


    }
    public class ServiceConfiguration
    {
        public static void ServiceProfiles(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            serviceCollection.AddScoped<ApplicationDbContext>();
            serviceCollection.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
        }
    }
}
