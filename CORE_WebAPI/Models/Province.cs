using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Province
    {
        public Province()
        {
            City = new HashSet<City>();
        }

        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }

        public ICollection<City> City { get; set; }
    }
}
