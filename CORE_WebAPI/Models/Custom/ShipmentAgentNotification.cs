using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentAgentNotification
    {
        public void UpdateChangedFields(ShipmentAgentNotification saNotification)
        {
            if (saNotification.NotificationRead != 0)
            {
                this.NotificationRead = saNotification.NotificationRead;
            }
            if (saNotification.DateRead != null)
            {
                this.DateRead = saNotification.DateRead;
            }
            if (saNotification.RequestAccepted != null)
            {
                this.RequestAccepted = saNotification.RequestAccepted;
            }
            if (saNotification.ShipmentId != 0)
            {
                this.ShipmentId = saNotification.ShipmentId;
            }
        }
    }
}
