using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageType
    {
        public PackageType()
        {
            Package = new HashSet<Package>();
            PackagePrice = new HashSet<PackagePrice>();
            VehiclePacakageLine = new HashSet<VehiclePacakageLine>();
        }

        public int PackageTypeId { get; set; }
        public string PackageTypeDescr { get; set; }

        public ICollection<Package> Package { get; set; }
        public ICollection<PackagePrice> PackagePrice { get; set; }
        public ICollection<VehiclePacakageLine> VehiclePacakageLine { get; set; }
    }
}
