using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentStatus
    {
        public ShipmentStatus()
        {
            Shipment = new HashSet<Shipment>();
        }

        public int ShipmentStatusId { get; set; }
        public string ShipmentStatusDescr { get; set; }

        public ICollection<Shipment> Shipment { get; set; }
    }
}
