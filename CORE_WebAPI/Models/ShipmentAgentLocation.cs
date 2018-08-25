using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentAgentLocation
    {
        public ShipmentAgentLocation()
        {
            ShipmentAgent = new HashSet<ShipmentAgent>();
        }

        public int CurrentLocId { get; set; }
        public string CurrentLocLatitude { get; set; }
        public string CurrentLocLongitude { get; set; }

        public ICollection<ShipmentAgent> ShipmentAgent { get; set; }
    }
}
