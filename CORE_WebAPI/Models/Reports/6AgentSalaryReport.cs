using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Reports
{
    public class AgentSalaryReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EmpFullName { get; set; }
        public List<SalaryReportLine> Lines {get; set;}
        public decimal TotalSalary { get; set; }

    }
    public class SalaryReportLine
    {
        public string agentName { get; set; } //getFullName
        public int noOfShipments { get; set; }
        public decimal agentSalary { get; set; }
        public string bankDetails { get; set; } //get bank details
    }
}
