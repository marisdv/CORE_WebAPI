﻿using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentAgentLocation
    {
        public int CurrentLocId { get; set; }
        public string CurrentLocLatitude { get; set; }
        public string CurrentLocLongitude { get; set; }
    }
}
