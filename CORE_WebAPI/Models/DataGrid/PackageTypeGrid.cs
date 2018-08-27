using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageTypeGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<PackageType> packageTypes { get; set; }
    }
}
