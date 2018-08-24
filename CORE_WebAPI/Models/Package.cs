using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Package
    {
        public int PackageId { get; set; }
        public int PackageTypeQty { get; set; }
        public int PackageTypeId { get; set; }
        public int PackageContentId { get; set; }
        public int BasketId { get; set; }

        public Basket Basket { get; set; }
        public PackageContent PackageContent { get; set; }
        public PackageType PackageType { get; set; }
    }
}
