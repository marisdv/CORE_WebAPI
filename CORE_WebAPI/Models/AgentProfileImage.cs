using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AgentProfileImage
    {
        public int AgentImageId { get; set; }
        public byte[] AgentImage { get; set; }
    }
}
