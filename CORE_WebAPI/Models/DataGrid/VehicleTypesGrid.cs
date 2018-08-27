using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class VehicleTypeGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<VehicleType> vehicleTypes { get; set; }
    }
}
