using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class VehicleStatus
    {
        public VehicleStatus()
        {
            Vehicle = new HashSet<Vehicle>();
        }

        public int VehicleStatusId { get; set; }
        public string VehicleStatusDescr { get; set; }

        public ICollection<Vehicle> Vehicle { get; set; }
    }
}
