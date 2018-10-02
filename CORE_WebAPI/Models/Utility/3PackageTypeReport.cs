using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Reports
{
    public class PackageTypeReport
    {
        public List<PackageReportLine> Lines { get; set; }
        public int TotalSent { get; set; }
        public string EmpFullName { get; set; }
    }
    public class PackageReportLine
    {
        public string packType { get; set; }
        public int totalSent { get; set; }
    }
}
