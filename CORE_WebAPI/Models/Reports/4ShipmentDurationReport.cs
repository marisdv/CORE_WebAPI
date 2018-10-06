using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Reports
{
    public class ShipmentDurationReport
    {
        public string EmpFullName { get; set; }
        public List<DurationReportLine> Lines { get; set; }
        
    }
    public class DurationReportLine
    {
        public string agentName { get; set; } //getFullName
        public string avgDuration { get; set; }//data type?
        //these are used in the calculation but not displayed
        public int noOfShipments { get; set; }
        public TimeSpan? totalDuration { get; set; }
    }


}
