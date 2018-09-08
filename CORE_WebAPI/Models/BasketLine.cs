using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class BasketLine
    {
        public int SenderId { get; set; }
        public int PackageId { get; set; }

        public Package Package { get; set; }
        public Sender Sender { get; set; }
    }
}
