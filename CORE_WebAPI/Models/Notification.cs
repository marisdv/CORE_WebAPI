using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Notification
    {
        public Notification()
        {
            ShipmentAgentNotification = new HashSet<ShipmentAgentNotification>();
        }

        public int NotificationId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime NotificationDateTime { get; set; }

        public ICollection<ShipmentAgentNotification> ShipmentAgentNotification { get; set; }
    }
}
