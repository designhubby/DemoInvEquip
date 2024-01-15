using InvEquip.Data.Entity;
using InvEquip.Logic.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvEquip.Data.Repository
{
    public interface IGenericRepository<TEntity, TDomain>
        where TEntity : BaseEntity, new()
        where TDomain : BaseDomainModel<TEntity>
    {
        Task<TDomain> AddDomainAsync(TDomain domainModel);
        Task<IEnumerable<TDomain>> GetAllDomainModelsAsync();
        Task<IEnumerable<TDomain>> GetAllDomainModelsWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TDomain> GetDomainModelByIdAsync(int id);
    }
}