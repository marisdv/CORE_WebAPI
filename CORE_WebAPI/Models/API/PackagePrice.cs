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
            if (packagePrice.DateFrom != null)
            {
                this.DateFrom = packagePrice.DateFrom;
            }
            if (packagePrice.DateTo != null)
            {
                this.DateTo = packagePrice.DateTo;
            }
            if (packagePrice.Active != 0)
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
