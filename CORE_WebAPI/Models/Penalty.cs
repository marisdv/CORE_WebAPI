﻿using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Penalty
    {
        public Penalty()
        {
            PaymentReference = new HashSet<PaymentReference>();
        }

        public int PentaltyId { get; set; }
        public DateTime DateCharged { get; set; }
        public DateTime? DatePaid { get; set; }
        public decimal PenaltyAmount { get; set; }
        public int ShipmentId { get; set; }

        public Shipment Shipment { get; set; }
        public ICollection<PaymentReference> PaymentReference { get; set; }
    }
}
