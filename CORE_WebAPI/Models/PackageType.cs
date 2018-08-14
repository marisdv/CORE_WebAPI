using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageType
    {
        public PackageType()
        {
            PackageContent = new HashSet<PackageContent>();
            PackageLine = new HashSet<PackageLine>();
            PackageTypePrice = new HashSet<PackageTypePrice>();
            VehiclePacakageLine = new HashSet<VehiclePacakageLine>();
        }

        public int PackageTypeId { get; set; }
        public string PackageTypeDescr { get; set; }

        public ICollection<PackageContent> PackageContent { get; set; }
        public ICollection<PackageLine> PackageLine { get; set; }
        public ICollection<PackageTypePrice> PackageTypePrice { get; set; }
        public ICollection<VehiclePacakageLine> VehiclePacakageLine { get; set; }
    }
}
