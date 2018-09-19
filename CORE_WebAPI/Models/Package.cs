using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Package
    {
        public Package()
        {
            BasketLine = new HashSet<BasketLine>();
        }

        public int PackageId { get; set; }
        public int PackageTypeQty { get; set; }
        public int PackageTypeId { get; set; }
        public int PackageContentId { get; set; }
        public int? ShipmentId { get; set; }
        public string PackageTypeImage { get; set; }

        public PackageContent PackageContent { get; set; }
        public PackageType PackageType { get; set; }
        public Shipment Shipment { get; set; }
        public ICollection<BasketLine> BasketLine { get; set; }
    }
}
