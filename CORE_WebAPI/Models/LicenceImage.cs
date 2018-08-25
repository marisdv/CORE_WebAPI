using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class LicenceImage
    {
        public LicenceImage()
        {
            ShipmentAgent = new HashSet<ShipmentAgent>();
        }

        public int LicenceImageId { get; set; }
        public string LicenceImage1 { get; set; }

        public ICollection<ShipmentAgent> ShipmentAgent { get; set; }
    }
}
