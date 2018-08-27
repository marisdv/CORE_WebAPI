using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentAgentGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<ShipmentAgent> shipmentAgents { get; set; }
    }
}
