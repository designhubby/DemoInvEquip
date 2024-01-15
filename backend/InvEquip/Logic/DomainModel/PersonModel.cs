using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Data.Entity;

namespace InvEquip.Logic.DomainModel
{
    public class PersonModel : BaseDomainModel<Person>
    {
        public int PersonId
        {
            get => _entity.Id;
        }
        public string FirstName
        {
            get => _entity.Fname;
            set => _entity.Fname = value;
        }
        public string LastName
        {
            get => _entity.Lname;
            set => _entity.Lname = value;
        }
        public string RoleName => _entity.Role.RoleName;
        public int RoleId
        {
            get => _entity.RoleId;
            set => _entity.RoleId = value;
        }
        public string DepartmentName => _entity.Role.Department.DepartmentName;
        public int DepartmentId => _entity.Role.Department.Id;
       

        public IList<Device> Devices
        {
            get
            {
                return _entity.Devices.ToList();
            }
        }
        
        public PersonModel(Person entity): base(entity)
        {
            
        }
        public PersonModel(int? personId, string personFirstName, string personLastName, int roleId) : base()
        {
            if(personId != null)
            {
                _entity = new Person()
                {
                    Id = (int)personId,
                    Fname = personFirstName,
                    Lname = personLastName,
                    RoleId = roleId,
                };
            }
            else
            {
                _entity = new Person()
                {
                    Fname = personFirstName,
                    Lname = personLastName,
                    RoleId = roleId,
                };
            }

        }

        public void ChangeFirstName(string firstName)
        {
            _entity.Fname = firstName;
        }
        public void ChangeLastNameTo(string lastName)
        {
            _entity.Lname = lastName;
        }

        public void ChangeRoleTo(int roleId)
        {
            _entity.RoleId = roleId;
        }

        public void ChangeRoleTo(RoleModel roleModel)
        {
            _entity.Role = roleModel._entity;
        }

        public void AddThisPersonToDevice(int deviceId, DateTime startDate, DateTime endDate)
        {
            
            _entity.PersonDevice.Add(new PersonDevice(deviceId, PersonId, startDate, endDate));
        }
        public IList<PersonDeviceModel> ListPersonDevicesModels()
        {
            IList<PersonDeviceModel> personDeviceModels = new List<PersonDeviceModel>();
            IList<PersonDevice> personDevices = _entity.PersonDevice;

            foreach(PersonDevice personDevice in personDevices)
            {
                PersonDeviceModel personDeviceModel = (PersonDeviceModel)Activator.CreateInstance(typeof(PersonDeviceModel),personDevice);
                personDeviceModels.Add(personDeviceModel);
            }
            return personDeviceModels; 
        }
        public IList<DeviceModel> ListActiveDevicesModels()
        {
            IList<DeviceModel> deviceModels = new List<DeviceModel>();
            IEnumerable<PersonDevice> personDevices = _entity.PersonDevice.Where(pd => pd.EndDate == null);
            var ListOfDevices = ReturnPersonDeviceToDevices(personDevices);
            return ListOfDevices;
        }
        public IList<DeviceModel> ListActiveDevicesModelsByType(int deviceType)
        {
            //IList<DeviceModel> deviceModels = new List<DeviceModel>();
            IEnumerable<PersonDevice> personDevices = _entity.PersonDevice.Where(pd => pd.EndDate == null && pd.Device.HwModel.DeviceTypeId == deviceType);
            var ListOfDevices = ReturnPersonDeviceToDevices(personDevices);

            return ListOfDevices;
        }
        public IList<DeviceModel> ReturnPersonDeviceToDevices(IEnumerable<PersonDevice> personDevices)
        {
            IList<DeviceModel> deviceModels = new List<DeviceModel>();
            foreach (PersonDevice personDevice in personDevices)
            {
                Device device = _entity.Devices.FirstOrDefault(d => d.Id == personDevice.DeviceId);
                DeviceModel deviceModel = (DeviceModel)Activator.CreateInstance(typeof(DeviceModel), device);
                deviceModels.Add(deviceModel);
            }
            return deviceModels;
        }

    }
}
