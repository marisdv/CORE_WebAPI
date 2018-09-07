using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class City
    {
        public City()
        {
            DownloadLocation = new HashSet<DownloadLocation>();
            ShipmentAgent = new HashSet<ShipmentAgent>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int ProvinceId { get; set; }
        public byte? CityAvailability { get; set; }

        public Province Province { get; set; }
        public ICollection<DownloadLocation> DownloadLocation { get; set; }
        public ICollection<ShipmentAgent> ShipmentAgent { get; set; }
    }
}
