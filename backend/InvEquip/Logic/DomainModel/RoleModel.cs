using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Data.Entity;

namespace InvEquip.Logic.DomainModel
{
    public class RoleModel : BaseDomainModel<Role>
    {
        public int RoleId => _entity.Id;
        public string RoleName => _entity.RoleName;
        public int DepartmentId => _entity.DepartmentId;
        public string DepartmentName => _entity.Department.DepartmentName;
        public RoleModel(int roleId, string roleName, int departmentId)
        {
            _entity = new Role()
            {
                Id = roleId,
                RoleName = roleName,
                DepartmentId = departmentId,
            };
        }
        public RoleModel(Role entity):base(entity)
        {
            _entity = entity;
        }

        public void ChangeRoleNameTo(string name)
        {
            _entity.RoleName = name;
        }
        public void ChangeDepartmentTo(int departmentId)
        {
            _entity.DepartmentId = departmentId;
        }

        public void ChangeDepartmentTo(Department departmentEntity)
        {
            _entity.Department = departmentEntity;
        }
        public void ChangeDepartmentTo(DepartmentModel departmentModel)
        {
            _entity.Department = departmentModel._entity;
        }
        public void AddPerson(Person person)
        {

            _entity.People.Add(person);
        }
        public void AddPerson(PersonModel personModel)
        {
            _entity.People.Add(personModel._entity);
        }
    }
}
