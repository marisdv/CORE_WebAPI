using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Basket
    {
        public Basket()
        {
            Package = new HashSet<Package>();
            Shipment = new HashSet<Shipment>();
        }

        public int BasketId { get; set; }
        public DateTime BasketDateTime { get; set; }
        public int SenderId { get; set; }

        public Sender Sender { get; set; }
        public ICollection<Package> Package { get; set; }
        public ICollection<Shipment> Shipment { get; set; }
    }
}
