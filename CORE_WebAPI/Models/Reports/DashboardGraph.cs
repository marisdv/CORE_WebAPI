using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Reports
{
    public class DashboardGraph
    {
        public List<ShipmentStatusLine> Lines { get; set; }
        public int totalShipments { get; set; }
    }
    public class ShipmentStatusLine
    {
        public string statusDescr { get; set; }
        public int statusCount { get; set; }
    }
}
