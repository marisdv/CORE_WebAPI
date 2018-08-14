using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AccessRoleArea
    {
        public int AccessRoleId { get; set; }
        public int AccessAreaId { get; set; }

        public AccessArea AccessArea { get; set; }
        public AccessRole AccessRole { get; set; }
    }
}
