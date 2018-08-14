using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class VehicleMake
    {
        public VehicleMake()
        {
            Vehicle = new HashSet<Vehicle>();
        }

        public int VehicleMakeId { get; set; }
        public string VehicleMakeDescr { get; set; }

        public ICollection<Vehicle> Vehicle { get; set; }
    }
}
