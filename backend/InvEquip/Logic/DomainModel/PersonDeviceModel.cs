using InvEquip.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.DomainModel
{
    public class PersonDeviceModel : BaseDomainModel<PersonDevice>
    {
        public int PersonDeviceId => _entity.Id;
        public int PersonId {

            get => _entity.PersonId;
            set => _entity.PersonId = value;
        }
        public string Fname
        {
            get => _entity.Person.Fname;

        }
        public string Lname
        {
            get => _entity.Person.Lname;

        }

        public int DeviceId
        {
            get => _entity.DeviceId;
            set => _entity.DeviceId = value;
        }
        public DateTime? StartDate
        {
            get => _entity.StartDate;
            set => _entity.StartDate = value;
        }
        public DateTime? EndDate
        {
            get => _entity.EndDate;
            set => _entity.EndDate = value;

        }
        


        public string DeviceName => _entity.Device.DeviceName;
        public string HwModelName => _entity.Device.HwModel.HwModelName;
        public string DeviceTypeName => _entity.Device.HwModel.DeviceType.DeviceTypeName;
        public int DeviceTypeId => _entity.Device.HwModel.DeviceTypeId;


        public PersonDeviceModel() : base()
        {
            _entity = new PersonDevice();
        }
        public PersonDeviceModel(PersonDevice personDevice) : base(personDevice)
        {

        }

        public PersonDeviceModel(int personId, int deviceId, DateTime startDate, DateTime? endDate = null) : base()
        {
            _entity = new PersonDevice
            {
                DeviceId = deviceId,
                PersonId = personId,
                StartDate = startDate,
                EndDate = endDate,
            };
        }
        public bool  SetEndDate(DateTime dateTime)
        {
            bool endDateExist = (_entity.EndDate.HasValue);
            if (!endDateExist)
            {
                _entity.EndDate = dateTime;
                return true;
            }
            return false;   
            
        }


    }
}
