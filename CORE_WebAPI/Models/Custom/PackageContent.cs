using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PackageContent
    {
        public void UpdateChangedFields(PackageContent packageContent)
        {
            if (packageContent.PackageContent1 != null)
            {
                this.PackageContent1 = packageContent.PackageContent1;
            }
            if (packageContent.PackageQrcode != null)
            {
                this.PackageQrcode = packageContent.PackageQrcode;
            }
        }
    }
}
