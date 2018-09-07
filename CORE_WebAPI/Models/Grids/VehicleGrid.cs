using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class VehicleGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<Vehicle> vehicles { get; set; }
    }
}
