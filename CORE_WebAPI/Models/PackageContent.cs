using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageContent
    {
        public int PackageContentId { get; set; }
        public string PackageContent1 { get; set; }
        public string PackageQrcode { get; set; }
        public int ShipmentId { get; set; }
        public int PackageTypeId { get; set; }

        public PackageType PackageType { get; set; }
        public Shipment Shipment { get; set; }
    }
}
