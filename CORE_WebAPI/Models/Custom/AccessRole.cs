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
            if (accessRole.AccessRoleDescr != null)
            {
                this.AccessRoleDescr = accessRole.AccessRoleDescr;
            }

            //Make sure all areas come through in JSON 
            if (accessRole.AccessRoleArea.Count > 0)
            {
                this.AccessRoleArea.Clear();

                foreach (var area in accessRole.AccessRoleArea)
                {
                    if (area.AccessAreaId != 0 && area.AccessRoleId != 0)
                    {
                        this.AccessRoleArea.Add(area);
                    }
                }
            }
        }
    }
}
