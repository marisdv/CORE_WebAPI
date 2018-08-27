using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class FixedPrice
    {
        public int FixedPriceId { get; set; }
        public string FixedPriceDescr { get; set; }
        public decimal FixedPrice1 { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
