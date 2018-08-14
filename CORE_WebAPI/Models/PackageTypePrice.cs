﻿using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageTypePrice
    {
        public int PackagePriceId { get; set; }
        public decimal PackagePrice { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public byte Active { get; set; }
        public int PackageTypeId { get; set; }

        public PackageType PackageType { get; set; }
    }
}
