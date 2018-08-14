using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Application
    {
        public int ApplicationId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int AgentId { get; set; }
        public int ApplicationStatusId { get; set; }
        public DateTime? DateAccepted { get; set; }
        public int? EmployeeId { get; set; }

        public ShipmentAgent Agent { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public Employee Employee { get; set; }
    }
}
