using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Data.Entity;

namespace InvEquip.Logic.DomainModel
{
    public class BaseDomainModel<TEntity> where TEntity:BaseEntity
    {
        public TEntity _entity;
        public BaseDomainModel(TEntity entity)
        {
            _entity = entity;
        }
        public BaseDomainModel()
        {

        }
    }
}
