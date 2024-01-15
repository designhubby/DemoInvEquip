using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Data.Entity;


namespace InvEquip.Logic.DomainModel
{
    public class VendorModel : BaseDomainModel<Vendor>
    {
        
        public VendorModel(Vendor entity): base(entity)
        {
            _entity = entity;
        }

        public VendorModel(string VendorName)
        {
            _entity = new Vendor()
            {
                VendorName = VendorName,
            };
        }
        public int VendorId => _entity.Id;

        public string VendorName
        {
            get => _entity.VendorName;
            set => _entity.VendorName = value;
        }

        public void ChangeVendorNameTo(string name)
        {
            _entity.VendorName = name;
        }
        public void Delete()
        {
            _entity.Deleted = true;
        }
    }
}
