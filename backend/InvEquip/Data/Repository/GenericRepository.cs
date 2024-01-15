using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InvEquip.Data.Entity;
using InvEquip.Logic.DomainModel;

namespace InvEquip.Data.Repository
{
    public class GenericRepository<TEntity, TDomain> : BaseRepository<TEntity>, IGenericRepository<TEntity, TDomain> where TEntity : BaseEntity, new() where TDomain : BaseDomainModel<TEntity>
    {
        public GenericRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool>CheckEntityExists(int id)
        {
            bool result = await EntityExists(id);
            return result;
        }
        public async Task<bool>CheckEntityExistsWhere(Expression<Func<TEntity,bool>> predicate)
        {
            bool result = await EntityExistsWhere(predicate);
            return result;
        }
        public TDomain GetDomainModelById(int id)
        {
            var entity = GetEntityById(id);
            if (entity == null)
            {
                return null;
            }
            return (TDomain)Activator.CreateInstance(typeof(TDomain), entity);
        }

        public async Task<TDomain> GetDomainModelByIdAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);
            if(entity== null)
            {
                return null;
            }
            return (TDomain)Activator.CreateInstance(typeof(TDomain), entity);
        }
        public async Task<TDomain> GetDomainModelByIdWithIncludesAsync(int id, IList<Expression<Func<TEntity, object>>> includes)
        {
            TEntity entity = await GetEntityByIdWithIncludesAsync(id, includes);
            TDomain resultModel = (TDomain)Activator.CreateInstance(typeof(TDomain), entity);
            return resultModel;
        }

        public async Task<IEnumerable<TDomain>> GetAllDomainModelsAsync()
        {
            var entities = await GetAllEntitiesAsync();
            IList<TDomain> DomainModelObjects = new List<TDomain>();
            foreach (TEntity entity in entities)
            {
                TDomain domainObject = (TDomain)Activator.CreateInstance(typeof(TDomain), entity);
                DomainModelObjects.Add(domainObject);
            }
            return DomainModelObjects;

        }
        public async Task<IEnumerable<TDomain>> GetAllDomainModelsWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {

            var entities = await GetAllEntitiesWhereAsync(predicate);
            if(entities == null)
            {
                return null;
            }
            IList<TDomain> DomainModelObjects = new List<TDomain>();
            foreach (TEntity entity in entities)
            {
                TDomain domainObject = (TDomain)Activator.CreateInstance(typeof(TDomain), entity);
                DomainModelObjects.Add(domainObject);
            }
            return DomainModelObjects;

        }

        public async Task<TDomain> AddDomainAsync(TDomain domainModel)
        {
            var entity = await AddEntityAsync(domainModel._entity);
            return domainModel; //check if the _entity now has the Id
            //return (TDomain)Activator.CreateInstance(typeof(TDomain), entity);
        }
        public async Task<bool> AddDomainRangeAsync(IEnumerable<TDomain> domainModels)
        {
            IEnumerable<TEntity> entities = domainModels.Select(indiv => indiv._entity);
            bool result = await AddEntitiesAsync(entities);
            return result; 
        }
        public async Task UpdateDomainAsync(TDomain domainModel)
        {
            await UpdateEntityAsync(domainModel._entity);
        }
        
        




    }
}
