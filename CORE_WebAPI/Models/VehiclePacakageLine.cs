using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class VehiclePacakageLine
    {
        public int Quantity { get; set; }
        public int VehicleTypeId { get; set; }
        public int PackageTypeId { get; set; }

        public PackageType PackageType { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
