using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class VehicleProofImage
    {
        public VehicleProofImage()
        {
            Vehicle = new HashSet<Vehicle>();
        }

        public int VehicleImageId { get; set; }
        public byte[] VehicleImage { get; set; }

        public ICollection<Vehicle> Vehicle { get; set; }
    }
}
