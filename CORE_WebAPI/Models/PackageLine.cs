using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageLine
    {
        public int PackageTypeQty { get; set; }
        public int ShipmentId { get; set; }
        public int PackageTypeId { get; set; }

        public PackageType Shipment { get; set; }
        public Shipment ShipmentNavigation { get; set; }
    }
}
