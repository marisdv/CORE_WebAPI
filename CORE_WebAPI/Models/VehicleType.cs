using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicle = new HashSet<Vehicle>();
            VehiclePacakageLine = new HashSet<VehiclePacakageLine>();
        }

        public int VehicleTypeId { get; set; }
        public string VehicleTypeDescr { get; set; }

        public ICollection<Vehicle> Vehicle { get; set; }
        public ICollection<VehiclePacakageLine> VehiclePacakageLine { get; set; }
    }
}
