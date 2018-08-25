using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AgentProfileImage
    {
        public AgentProfileImage()
        {
            ShipmentAgent = new HashSet<ShipmentAgent>();
        }

        public int AgentImageId { get; set; }
        public string AgentImage { get; set; }

        public ICollection<ShipmentAgent> ShipmentAgent { get; set; }
    }
}
