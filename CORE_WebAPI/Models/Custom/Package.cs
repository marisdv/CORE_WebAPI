using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Package
    {
        public void UpdateChangedFields(Package package)
        {
            if (package.PackageTypeQty != 0)
            {
                this.PackageTypeQty = package.PackageTypeQty;
            }

            if (package.PackageId != 0)
            {
                this.PackageId = package.PackageId;
            }

            if (package.PackageContentId != 0)
            {
                this.PackageContentId = package.PackageContentId;
            }

            if (package.ShipmentId != null)
            {
                this.ShipmentId = package.ShipmentId;
            }

        }
    }
}
