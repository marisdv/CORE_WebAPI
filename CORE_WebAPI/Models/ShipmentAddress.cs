using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentAddress
    {
        public ShipmentAddress()
        {
            ShipmentLocation = new HashSet<ShipmentLocation>();
        }

        public int AddressId { get; set; }
        public string AddressDescription { get; set; }
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public int AddressTypeId { get; set; }
        public int CityId { get; set; }
        public string AddressLatitude { get; set; }
        public string AddressLongitude { get; set; }

        public AddressType AddressType { get; set; }
        public ICollection<ShipmentLocation> ShipmentLocation { get; set; }
    }
}
