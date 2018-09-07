using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackagePriceGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<PackagePrice> packageTypePrices { get; set; }
    }
}
