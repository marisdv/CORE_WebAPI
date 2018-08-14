using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class SignatureImage
    {
        public int SignatureId { get; set; }
        public byte[] SenderSig { get; set; }
        public byte[] ReceiverSig { get; set; }
    }
}
