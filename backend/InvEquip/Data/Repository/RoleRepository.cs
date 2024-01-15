using InvEquip.Data.Entity;
using InvEquip.Logic.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvEquip.Data.Repository
{
    public class RoleRepository : GenericRepository<Role, RoleModel>
    {
        public RoleRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {

        }
        public async Task<RoleModel> GetRoleModelByIdWithDepartment(int id)
        {
            Expression<Func<Role, object>> departmentNav = r => r.Department;
            List<Expression<Func<Role, object>>> listDeptNav = new();
            listDeptNav.Add(departmentNav);
            RoleModel result = await GetDomainModelByIdWithIncludesAsync(id, listDeptNav);
            return result;
        }
    }
}
