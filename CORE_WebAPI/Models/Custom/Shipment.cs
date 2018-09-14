using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Shipment
    {
        public void UpdateChangedFields(Shipment shipment)
        {
            if (shipment.StartLongitude != null)
            {
                this.StartLongitude = shipment.StartLongitude;
            }
            if (shipment.StartLatitude != null)
            {
                this.StartLatitude = shipment.StartLatitude;
            }
            if (shipment.EndLongitude != null)
            {
                this.EndLongitude = shipment.EndLongitude;
            }
            if (shipment.EndLatitude != null)
            {
                this.EndLatitude = shipment.EndLatitude;
            }
            if (shipment.ShipmentDate != null && shipment.ShipmentDate != new DateTime())
            {
                this.ShipmentDate = shipment.ShipmentDate;
            }
            if (shipment.SpecialInstruction != null)
            {
                this.SpecialInstruction = shipment.SpecialInstruction;
            }
            if (shipment.CollectionTime != null && shipment.CollectionTime != new DateTime())
            {
                this.CollectionTime = shipment.CollectionTime;
            }
            if (shipment.ShipmentDistance != null)
            {
                this.ShipmentDistance = shipment.ShipmentDistance;
            }
            if (shipment.DeliveryTime != null && shipment.DeliveryTime != new DateTime())
            {
                this.DeliveryTime = shipment.DeliveryTime;
            }
            if (shipment.Terminated != null)
            {
                this.Terminated = shipment.Terminated;
            }
            if (shipment.Paid != null)
            {
                this.Paid = shipment.Paid;
            }
            if (shipment.AgentId != 0)
            {
                this.AgentId = shipment.AgentId;
            }
            if (shipment.ShipmentStatusId != 0)
            {
                this.ShipmentStatusId = shipment.ShipmentStatusId;
            }
            if (shipment.ReceiverId != 0)
            {
                this.ReceiverId = shipment.ReceiverId;
            }
            if (shipment.SenderId != 0)
            {
                this.SenderId = shipment.SenderId;
            }
        }
    }
}
