using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageContent
    {
        public PackageContent()
        {
            Package = new HashSet<Package>();
        }

        public int PackageContentId { get; set; }
        public string PackageContent1 { get; set; }
        public string PackageQrcode { get; set; }

        public ICollection<Package> Package { get; set; }
    }
}
