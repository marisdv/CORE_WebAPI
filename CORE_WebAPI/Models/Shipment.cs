using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            PaymentReference = new HashSet<PaymentReference>();
            Penalty = new HashSet<Penalty>();
            ShipmentLocation = new HashSet<ShipmentLocation>();
        }

        public int ShipmentId { get; set; }
        public string StartLongitude { get; set; }
        public string StartLatitude { get; set; }
        public string EndLongitude { get; set; }
        public string EndLatitude { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string SpecialInstruction { get; set; }
        public DateTime? CollectionTime { get; set; }
        public string ShipmentDistance { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public byte? Terminated { get; set; }
        public byte Paid { get; set; }
        public int SignatureId { get; set; }
        public int AgentId { get; set; }
        public int ShipmentStatusId { get; set; }
        public int ReceiverId { get; set; }
        public int BasketId { get; set; }

        public ShipmentAgent Agent { get; set; }
        public Basket Basket { get; set; }
        public Receiver Receiver { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public SignatureImage Signature { get; set; }
        public ICollection<PaymentReference> PaymentReference { get; set; }
        public ICollection<Penalty> Penalty { get; set; }
        public ICollection<ShipmentLocation> ShipmentLocation { get; set; }
    }
}
