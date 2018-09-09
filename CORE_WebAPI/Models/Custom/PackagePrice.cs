using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackagePrice
    {
        public void UpdateChangedFields(PackagePrice packagePrice)
        {
            if (packagePrice.PackagePrice1 != 0)
            {
                this.PackagePrice1 = packagePrice.PackagePrice1;
            }
            if (packagePrice.DateFrom != null && packagePrice.DateFrom != new DateTime())
            {
                this.DateFrom = packagePrice.DateFrom;
            }
            if (packagePrice.DateTo != null && packagePrice.DateTo != new DateTime())
            {
                this.DateTo = packagePrice.DateTo;
            }
            if (packagePrice.Active != null)
            {
                this.Active = packagePrice.Active;
            }
            if (packagePrice.PackageTypeId != 0)
            {
                this.PackageTypeId = packagePrice.PackageTypeId;
            }

        }

    }
}
