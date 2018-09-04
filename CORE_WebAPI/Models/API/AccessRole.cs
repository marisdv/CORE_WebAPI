using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AccessRole
    {
    public void UpdateChangedFields(AccessRole accessRole)
    {
        if (accessRole.AccessRoleName != null)
        {
            this.AccessRoleName = accessRole.AccessRoleName;
        }
        if (accessRole.RoleDescription != null)
        {
            this.RoleDescription = accessRole.RoleDescription;
        }

    }
    }

  
}
