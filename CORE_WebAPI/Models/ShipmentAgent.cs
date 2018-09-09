using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentAgent
    {
        public ShipmentAgent()
        {
            Application = new HashSet<Application>();
            Shipment = new HashSet<Shipment>();
            ShipmentAgentLocation = new HashSet<ShipmentAgentLocation>();
            Vehicle = new HashSet<Vehicle>();
        }

        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public string AgentSurname { get; set; }
        public string AgentEmail { get; set; }
        public string AgentNationalId { get; set; }
        public string AgentPassportNo { get; set; }
        public DateTime DateEmployed { get; set; }
        public string AgentCompany { get; set; }
        public string BankAccNo { get; set; }
        public string BankName { get; set; }
        public string BankAccType { get; set; }
        public string BankBranchCode { get; set; }
        public byte? ApplicationAccepted { get; set; }
        public byte? AgentAvailability { get; set; }
        public byte? AgentActive { get; set; }
        public byte? Insurance { get; set; }
        public int CityId { get; set; }
        public int LoginId { get; set; }
        public int? LicenceImageId { get; set; }
        public int? AgentImageId { get; set; }
        public int? CurrentLocId { get; set; }

        public AgentProfileImage AgentImage { get; set; }
        public City City { get; set; }
        public LicenceImage LicenceImage { get; set; }
        public Login Login { get; set; }
        public ICollection<Application> Application { get; set; }
        public ICollection<Shipment> Shipment { get; set; }
        public ICollection<ShipmentAgentLocation> ShipmentAgentLocation { get; set; }
        public ICollection<Vehicle> Vehicle { get; set; }
    }
}
