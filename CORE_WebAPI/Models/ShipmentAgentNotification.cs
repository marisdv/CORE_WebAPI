using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentAgentNotification
    {
        public int NotificationId { get; set; }
        public int AgentId { get; set; }
        public byte? NotificationRead { get; set; }
        public DateTime? DateRead { get; set; }
        public byte? RequestAccepted { get; set; }
        public int? ShipmentId { get; set; }

        public ShipmentAgent Agent { get; set; }
        public Notification Notification { get; set; }
        public Shipment Shipment { get; set; }
    }
}
