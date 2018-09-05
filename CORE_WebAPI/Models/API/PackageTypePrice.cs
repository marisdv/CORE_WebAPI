using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageTypePrice
    {
        public void UpdateChangedFields(PackageTypePrice packageTypePrice)
        {
            if (packageTypePrice.PackagePrice != null)
            {
                this.PackagePrice = packageTypePrice.PackagePrice;
            }
            if (packageTypePrice.DateFrom != null)
            {
                this.DateFrom = packageTypePrice.DateFrom;
            }
            if (packageTypePrice.DateTo != null)
            {
                this.DateTo = packageTypePrice.DateTo;
            }
            if (packageTypePrice.Active != 0)
            {
                this.Active = packageTypePrice.Active;
            }
            if (packageTypePrice.PackageTypeId != 0)
            {
                this.PackageTypeId = packageTypePrice.PackageTypeId;
            }

        }

    }
}
