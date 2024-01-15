using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class PersonDevice : BaseEntity
    {
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public PersonDevice(int personId, int deviceId, DateTime startDate, DateTime endDate)
        {
            PersonId = personId;
            DeviceId = deviceId;
            StartDate = startDate;
            EndDate = endDate;
        }
        public PersonDevice()
        {

        }
    }
}
