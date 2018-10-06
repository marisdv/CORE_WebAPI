using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Reports
{
    public class DailyShipmentsReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmpFullName { get; set; }


        //count shipments for each date in a given range
    }
    public class DailyShipmentsReportLine
    {
        public DateTime date { get; set; }
        public int noOfShipments { get; set; }
    }

}
