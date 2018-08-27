using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class FixedPriceGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<FixedPrice> fixedPrices { get; set; }
    }
}
