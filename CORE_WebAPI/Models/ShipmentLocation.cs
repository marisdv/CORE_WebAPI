using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentLocation
    {
        public string Category { get; set; }
        public int ShipmentId { get; set; }
        public int AddressId { get; set; }

        public ShipmentAddress Shipment { get; set; }
        public Shipment ShipmentNavigation { get; set; }
    }
}
