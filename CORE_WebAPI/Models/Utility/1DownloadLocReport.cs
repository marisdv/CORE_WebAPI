using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Reports
{
    public class DownloadLocReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ReportLine> Lines { get; set; }
        public int TotalDownloads { get; set; }
        public string EmpFullName { get; set; }
    }
    public class ReportLine
    {
        public string province { get; set; }
        public string city { get; set; }
        public int totalDownloads { get; set; }
    }
}
