using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class VehicleProofImage
    {
        public int VehicleImageId { get; set; }
        public byte[] VehicleImage { get; set; }
    }
}
