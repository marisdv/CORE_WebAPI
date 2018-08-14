using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AccessArea
    {
        public AccessArea()
        {
            AccessRoleArea = new HashSet<AccessRoleArea>();
        }

        public int AccessAreaId { get; set; }
        public string AccessAreaDescr { get; set; }

        public ICollection<AccessRoleArea> AccessRoleArea { get; set; }
    }
}
