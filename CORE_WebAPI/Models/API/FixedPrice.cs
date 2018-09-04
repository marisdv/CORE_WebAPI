﻿using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class FixedPrice
    {
        public void UpdateChangedFields(FixedPrice fixedPrice)
        {
            if (fixedPrice.FixedPriceDescr != null)
            {
                this.FixedPriceDescr = fixedPrice.FixedPriceDescr;
            }
            if (fixedPrice.FixedPrice1 != null)
            {
                this.FixedPrice1 = fixedPrice.FixedPrice1;
            }
            if (fixedPrice.DateFrom != null)
            {
                this.DateFrom = fixedPrice.DateFrom;
            }
            if (fixedPrice.DateTo != null)
            {
                this.DateTo = fixedPrice.DateTo;
            }
        }

    }
}
