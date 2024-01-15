using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using AutoMapper;

namespace InvEquip.Logic.DomainModel
{
    public class DepartmentModel : BaseDomainModel<Department>
    {
        public int DepartmentId => _entity.Id;
        public string DepartmentName => _entity.DepartmentName;
        public IList<RoleModel> Roles => this.GetRoleModels();
        
        public DepartmentModel(Department department): base(department)
        {
            _entity = department; //this code shud be unneccessary, since the base is already doing this in the constructor
        }
        public DepartmentModel(string departmentName)
        {
            _entity = new Department()
            {
                DepartmentName = departmentName
            };
        }
        public DepartmentModel(int id, string departmentName)
        {
            _entity = new Department()
            {
                Id = id,
                DepartmentName = departmentName,
            };
        }

        public void ChangeDepartmentNameTo (string name)
        {
            _entity.DepartmentName = name;
        }

        public void DeleteDepartment()
        {
            _entity.Deleted = true;
        }
        public IList<RoleModel> GetRoleModels()
        {
            var roles = _entity.Roles;
            IList<RoleModel> roleModels = new List<RoleModel>();
            foreach(Role role in roles)
            {
                var roleModel = (RoleModel)Activator.CreateInstance(typeof(RoleModel), role);
                roleModels.Add(roleModel);
            }
            return roleModels;
        }
        public bool HasDependencies()
        {
            bool hasRoles = _entity.Roles.Any();
            return hasRoles;
        }
    }
}
