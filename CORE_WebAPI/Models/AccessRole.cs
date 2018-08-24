using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AccessRole
    {
        public AccessRole()
        {
            AccessRoleArea = new HashSet<AccessRoleArea>();
            Employee = new HashSet<Employee>();
        }

        public int AccessRoleId { get; set; }
        public string AccessRoleName { get; set; }
        public string RoleDescription { get; set; }

        public ICollection<AccessRoleArea> AccessRoleArea { get; set; }
        public ICollection<Employee> Employee { get; set; }
    }
}
