using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Vehicle
    {
        public int VehicleId { get; set; }
        public string LicencePlateNo { get; set; }
        public string Colour { get; set; }
        public string Model { get; set; }
        public byte? VehicleActive { get; set; }
        public DateTime? DateVerified { get; set; }
        public DateTime? DateDeactivated { get; set; }
        public int AgentId { get; set; }
        public int VehicleMakeId { get; set; }
        public int VehicleTypeId { get; set; }
        public int VehicleStatusId { get; set; }
        public string VehicleProofImage { get; set; }

        public ShipmentAgent Agent { get; set; }
        public VehicleMake VehicleMake { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}
