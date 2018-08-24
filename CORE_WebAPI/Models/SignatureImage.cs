using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class SignatureImage
    {
        public SignatureImage()
        {
            Shipment = new HashSet<Shipment>();
        }

        public int SignatureId { get; set; }
        public byte[] SenderSig { get; set; }
        public byte[] ReceiverSig { get; set; }

        public ICollection<Shipment> Shipment { get; set; }
    }
}
