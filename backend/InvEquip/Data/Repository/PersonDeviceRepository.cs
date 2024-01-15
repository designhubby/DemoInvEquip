using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Data.Entity;
using InvEquip.Logic.DomainModel;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using InvEquip.Data.Repository.QueryObject;

namespace InvEquip.Data.Repository
{
    public class PersonDeviceRepository : GenericRepository<PersonDevice, PersonDeviceModel>
    {

        public PersonDeviceRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext) { }

        public IQueryable<Device> GetAllUnassociatedNonRetiredDevices()
        {

            var associatedPersonDevices = _context.Set<PersonDevice>()
                .Where(pd => pd.EndDate == null)
                .GroupBy(pd => pd.DeviceId)
                .Select(pd => new { pd.Key });
            var unassociatedDevices = _context.Set<Device>()
                .Where(d => associatedPersonDevices.All(aPd => aPd.Key != d.Id) && d.Deleted == false);

            
            return unassociatedDevices;
        }
        public IQueryable<Device> GetAllAssociatedNonRetiredDevices()
        {
            var associatedPersonDevices = _context.Set<PersonDevice>()
                .Where(pd => pd.EndDate == null)
                .GroupBy(pd => pd.DeviceId)
                .Select(pd => new { DeviceId = pd.Key });
            var associatedDevices = _context.Set<Device>()
                .Where(d => associatedPersonDevices.Any(aPd => aPd.DeviceId == d.Id) && d.Deleted == false);
            return associatedDevices;
        }
        public IQueryable<Device> GetAllDevices()
        {
            var allDevices = _context.Set<Device>();
            return allDevices;
        }



    }


    public static class FilterExtension
    {
        public static IQueryable<TEntity> AddFilters<TEntity>(this IQueryable<TEntity> queryableEntities, IEnumerable<Expression<Func<TEntity, bool>>> filterExpressionTree)
        {
            foreach(Expression<Func<TEntity, bool>> indiv in filterExpressionTree)
            {
                if(indiv is not null)
                {
                    queryableEntities = queryableEntities.Where(indiv);
                }
            }
            return queryableEntities;
        }

        public static IEnumerable<TDomainModel> ToDomainModels<TEntity, TDomainModel>(this IEnumerable<TEntity> enumerableEntities)
        {
            IEnumerable<TDomainModel> domainModels = enumerableEntities.Select(indiv => (TDomainModel)Activator.CreateInstance(typeof(TDomainModel), indiv));
            return domainModels;
        }

    }
}
