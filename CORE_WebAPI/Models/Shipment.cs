using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            PackageContent = new HashSet<PackageContent>();
            PackageLine = new HashSet<PackageLine>();
            PaymentReferenceNavigation = new HashSet<PaymentReference>();
            Penalty = new HashSet<Penalty>();
            ShipmentLocation = new HashSet<ShipmentLocation>();
        }

        public int ShipmentId { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string SpecialInstruction { get; set; }
        public DateTime? CollectionTime { get; set; }
        public string ShipmentDistance { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public byte? Terminated { get; set; }
        public byte Paid { get; set; }
        public string PaymentReference { get; set; }
        public int ReceiverId { get; set; }
        public int ShipmentStatusId { get; set; }
        public int AgentId { get; set; }
        public int SenderId { get; set; }
        public int SignatureId { get; set; }

        public ShipmentAgent Agent { get; set; }
        public Receiver Receiver { get; set; }
        public Sender Sender { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public ICollection<PackageContent> PackageContent { get; set; }
        public ICollection<PackageLine> PackageLine { get; set; }
        public ICollection<PaymentReference> PaymentReferenceNavigation { get; set; }
        public ICollection<Penalty> Penalty { get; set; }
        public ICollection<ShipmentLocation> ShipmentLocation { get; set; }
    }
}
