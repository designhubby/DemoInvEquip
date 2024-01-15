using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using InvEquip.Data.Entity;
using InvEquip.Logic.DomainModel;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using InvEquip.ExceptionHandling.Exceptions;
using InvEquip.Dto;

namespace InvEquip.Data.Repository
{
    public class BaseRepository<TEntity> where TEntity: BaseEntity
    {

        //Could be improved by returning IQueryable instead of Loading the result to memory via ToList()
        public ApplicationDbContext _context;
        public DbSet<TEntity> _dbSet;
        public BaseRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public async Task<bool> EntityExists(int id)
        {
            bool exists = await _dbSet.AnyAsync(indiv => indiv.Id == id);
            return exists;
        }
        public async Task<bool> EntityExistsWhere(Expression<Func<TEntity,bool>> predicate)
        {
            bool exists = await _dbSet.AnyAsync(predicate);
            return exists;
        }
        public IEnumerable<PropertyInfo> GetNavigationPropertiesReflection() 
        {

            var modelData = _context.Model.GetEntityTypes(typeof(TEntity)).ToList()[0];
            var propertyInfoData = modelData.GetNavigations().Select(x => x.PropertyInfo);

            return propertyInfoData;
        } //Not used
        public IEnumerable<PropertyInfo> GetNavigationProperties()
        {
            var entityType = _context.Model.FindEntityType(typeof(TEntity));
            var collectionNavigations = entityType.GetNavigations()
                .Where(nav => nav.IsCollection)
                .Concat<INavigationBase>(entityType.GetSkipNavigations());
            var result = collectionNavigations.Select(r => r.PropertyInfo);
            return result;
        }
        public async Task<bool> HasDependenciesByIdAsync(int id)
        {
            TEntity entity = await GetEntityByIdAsync(id);
            if(entity is null)
            {
                throw new ExceptionEntityNotExists("Not Found");
            }
            IEnumerable<PropertyInfo> propertyInfoList = await Task.Run(()=>GetNavigationProperties());

            bool dependenciesExist = false;
            foreach (PropertyInfo property in propertyInfoList)
            {
                dependenciesExist = property.GetValue(entity) is IEnumerable<BaseEntity> collection && collection.Any(c => c.Deleted == false);
                if (dependenciesExist)
                {
                    break;
                };
            }
            return dependenciesExist;

        }
        public TEntity GetEntityById(int id)
        {
            var entity = _dbSet.Find(id);

            return entity;
        }
        public async Task<TEntity> GetEntityByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            
            return entity;
        }
        public async Task<TEntity> GetEntityByIdWithIncludesAsync(int id, IList<Expression<Func<TEntity, object>>> includes)
        {
            
            IQueryable<TEntity> query = _dbSet;
            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }
            var resultEntities = await query.FirstOrDefaultAsync(entity => entity.Id == id);

            return resultEntities;
        }


        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync()
        {
            var entities = await _dbSet.Where(e => e.Deleted == false).ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<TEntity>> GetAllEntitiesWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _dbSet.Where(e => e.Deleted == false).Where(predicate)?.ToListAsync();
            return entities;
        }

        public async Task<TEntity> AddEntityAsync(TEntity entity)
        {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
        }
        public async Task<bool> AddEntitiesAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) when (ex is NotSupportedException or DbUpdateException or InvalidOperationException)
            {
                return false;
            }


        }
        public async Task<ResultStatus> DeleteEntityAsyncById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if(entity != null)
            {
                bool hasDependencies = await HasDependenciesByIdAsync(id);
                if (!hasDependencies)
                {
                    _context.Entry<TEntity>(entity).Property(e => e.Deleted).CurrentValue = true;
                    await _context.SaveChangesAsync();
                    return ResultStatus.Success;
                }
                else
                {
                    return ResultStatus.NotAllowed;
                }
           
            }
            else
            {
                throw new BadHttpRequestException("Entity Not Found");
            }
        }
        public async Task<ResultStatus> NoDependencyCheckDeleteById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if(entity is not null)
            {
                _context.Entry<TEntity>(entity).Property(e => e.Deleted).CurrentValue = true;
                await _context.SaveChangesAsync();
                return ResultStatus.Success;

            }
            else
            {
                return ResultStatus.NotFound;
            }
        }

        public async Task UpdateEntityAsync(TEntity entity)
        {
            var attach = _context.Attach<TEntity>(entity);
            IEnumerable<EntityEntry> unchangedEntities = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Unchanged);
            foreach(EntityEntry ee in unchangedEntities)
            {
                ee.State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
    }
}
