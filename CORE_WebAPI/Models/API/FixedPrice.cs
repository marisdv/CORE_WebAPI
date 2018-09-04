using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class FixedPrice
    {
        public void UpdateChangedFields(FixedPrice fixedPrice)
        {
            if (fixedPrice.FixedPriceDescr != null)
            {
                this.FixedPriceDescr = login.FixedPriceDescr;
            }
            if (fixedPrice.FixedPrice1 != null)
            {
                this.FixedPrice1 = login.FixedPrice1;
            }
            if (fixedPrice.DateFrom != null)
            {
                this.DateFrom = login.DateFrom;
            }
            if (fixedPrice.DateTo != null)
            {
                this.DateTo = login.DateTo;
            }
        }

    }
}
