using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AddressType
    {
        public AddressType()
        {
            ShipmentAddress = new HashSet<ShipmentAddress>();
        }

        public int AddressTypeId { get; set; }
        public string AddressTypeDescr { get; set; }

        public ICollection<ShipmentAddress> ShipmentAddress { get; set; }
    }
}
