using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageTypePriceGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<PackageTypePrice> packageTypePrices { get; set; }
    }
}
