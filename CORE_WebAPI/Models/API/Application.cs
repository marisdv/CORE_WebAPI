using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Application
    {
        public void UpdateChangedFields(Application application)
        {
            if (application.ApplicationDate != null)
            {
                this.ApplicationDate = application.ApplicationDate;
            }

            if (application.AgentId != null)
            {
                this.AgentId = application.AgentId;
            }

            if (application.ApplicationStatusId != null)
            {
                this.ApplicationStatusId = application.ApplicationStatusId;
            }

            if (application.DateAccepted != null)
            {
                this.DateAccepted = application.DateAccepted;
            }

            if (application.EmployeeId != null)
            {
                this.EmployeeId = application.EmployeeId;
            }
        }
    }
}
