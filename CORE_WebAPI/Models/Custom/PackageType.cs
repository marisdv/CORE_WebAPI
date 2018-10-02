using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageType
    {
        public void UpdateChangedFields(PackageType packageType)
        {
            if (packageType.PackageTypeDescr != null)
            {
                this.PackageTypeDescr = packageType.PackageTypeDescr;
            }
            if (packageType.PackageTypePrice != 0)
            {
                this.PackageTypePrice = packageType.PackageTypePrice;
            }
            if (packageType.PackageTypeImage != null)
            {
                this.PackageTypeImage = packageType.PackageTypeImage;
            }
    }
    }
}
