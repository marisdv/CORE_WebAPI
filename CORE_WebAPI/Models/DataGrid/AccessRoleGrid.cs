using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AccessRoleGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<AccessRole> accessRoles { get; set; }
    }
}
