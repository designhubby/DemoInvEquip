using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using InvEquip.Data.Entity;

namespace InvEquip.Data.Repository.QueryObject
{
    public class DeviceByQueryObject
    {
        public bool _associated { get; set; } = false;
        public int? _deviceTypeId { get; set; }
        public bool _deleted { get; set; } = false;
        public int? _HwModelId { get; set; }
        public string _serviceTag { get; set; }
        public string _assetNumber { get; set; }
        public string _notes { get; set; }
        public int? _contractId { get; set; }


        
        public Expression<Func<Device,bool>> FilterDeviceTypeId()
        {
            return _deviceTypeId is not null ? c => c.HwModel.DeviceType.Id == _deviceTypeId : null;
        }
        public Expression<Func<Device, bool>> FilterDeviceDeleted()
        {
            return d => d.Deleted == _deleted;
        }
        public Expression<Func<Device, bool>> FilterDeviceHwModelId()
        {
            return _HwModelId is not null ? d => d.HwModel.Id == _HwModelId : null;
        }
        public Expression<Func<Device, bool>> FilterDeviceServiceTag()
        {
            return _serviceTag is not null ? d => d.ServiceTag == _serviceTag : null;
        }
        public Expression<Func<Device, bool>> FilterDeviceAssetNumber()
        {
            return _assetNumber is not null ? d => d.AssetNumber == _assetNumber : null;
        }
        public Expression<Func<Device, bool>> FilterDeviceNotes()
        {
            return _notes is not null ? d => d.Notes == _notes : null;
        }
        public Expression<Func<Device, bool>> FilterDeviceContractId()
        {
            return _contractId is not null ? d => d.Contract.Id == _contractId : null;
        }

        public IEnumerable<Expression<Func<Device,bool>>> AsExpression()
        {
            var DeviceTypeId = FilterDeviceTypeId();
            var DeviceDeleted = FilterDeviceDeleted();
            var DeviceHwModelId = FilterDeviceHwModelId();
            var DeviceServiceTag = FilterDeviceServiceTag();
            var DeviceAssetNumber = FilterDeviceAssetNumber();
            var DeviceNotes = FilterDeviceNotes();
            var DeviceContractId = FilterDeviceContractId();
            List<Expression<Func<Device, bool>>> filterExpressions = new() { DeviceTypeId, DeviceDeleted, DeviceHwModelId, DeviceServiceTag, DeviceAssetNumber, DeviceNotes, DeviceContractId };
            return filterExpressions;
        }






    }

}
