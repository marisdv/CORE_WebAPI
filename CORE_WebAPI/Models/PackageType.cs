using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageType
    {
        public PackageType()
        {
            Package = new HashSet<Package>();
            VehiclePacakageLine = new HashSet<VehiclePacakageLine>();
        }

        public int PackageTypeId { get; set; }
        public string PackageTypeDescr { get; set; }
        public decimal PackageTypePrice { get; set; }
        public string PackageTypeImage { get; set; }

        public ICollection<Package> Package { get; set; }
        public ICollection<VehiclePacakageLine> VehiclePacakageLine { get; set; }
    }
}
