using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Sender
    {
        public Sender()
        {
            BasketLine = new HashSet<BasketLine>();
            Shipment = new HashSet<Shipment>();
        }

        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderNationalId { get; set; }
        public string SenderPassportNo { get; set; }
        public string SenderEmail { get; set; }
        public byte SenderActive { get; set; }
        public int LoginId { get; set; }

        public Login Login { get; set; }
        public ICollection<BasketLine> BasketLine { get; set; }
        public ICollection<Shipment> Shipment { get; set; }
    }
}
