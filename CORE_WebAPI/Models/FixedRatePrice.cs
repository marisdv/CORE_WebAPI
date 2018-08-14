using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class FixedRatePrice
    {
        public int FixedRateId { get; set; }
        public string FixedRateDescr { get; set; }
        public decimal FixedRatePrice1 { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
