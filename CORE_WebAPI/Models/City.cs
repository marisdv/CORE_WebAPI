﻿using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class City
    {
        public City()
        {
            ShipmentAgent = new HashSet<ShipmentAgent>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityPostalCode { get; set; }
        public int ProvinceId { get; set; }

        public Province Province { get; set; }
        public ICollection<ShipmentAgent> ShipmentAgent { get; set; }
    }
}
