using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Vehicle
    {
        public void UpdateChangedFields(Vehicle vehicle)
        {
            if (vehicle.LicencePlateNo != null)
            {
                this.LicencePlateNo = vehicle.LicencePlateNo;
            }
            if (vehicle.Colour != null)
            {
                this.Colour = vehicle.Colour;
            }
            if (vehicle.Model != null)
            {
                this.Model = vehicle.Model;
            }
            if (vehicle.VehicleActive != null)
            {
                this.VehicleActive = vehicle.VehicleActive;
            }
            if (vehicle.DateVerified != null && vehicle.DateVerified != new DateTime())
            {
                this.DateVerified = vehicle.DateVerified;
            }
            if (vehicle.DateDeactivated != null && vehicle.DateDeactivated != new DateTime())
            {
                this.DateDeactivated = vehicle.DateDeactivated;
            }
            if (vehicle.AgentId != 0)
            {
                this.AgentId = vehicle.AgentId;
            }
            if (vehicle.VehicleMakeId != 0)
            {
                this.VehicleMakeId = vehicle.VehicleMakeId;
            }
            if (vehicle.VehicleTypeId != 0)
            {
                this.VehicleTypeId = vehicle.VehicleTypeId;
            }
            if (vehicle.VehicleStatusId != 0)
            {
                this.VehicleStatusId = vehicle.VehicleStatusId;
            }
            if (vehicle.VehicleImageId != 0)
            {
                this.VehicleImageId = vehicle.VehicleImageId;
            }
        }
    }
}