using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Receiver
    {
        public Receiver()
        {
            Shipment = new HashSet<Shipment>();
        }

        public int ReceiverId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public int? Otp { get; set; }

        public ICollection<Shipment> Shipment { get; set; }
    }
}
