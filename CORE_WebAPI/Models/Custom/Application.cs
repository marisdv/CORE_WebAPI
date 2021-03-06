﻿using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Application
    {
        public void UpdateChangedFields(Application application)
        {
            if (application.ApplicationDate != null && application.ApplicationDate != new DateTime())
            {
                this.ApplicationDate = application.ApplicationDate;
            }

            if (application.AgentId != 0)
            {
                this.AgentId = application.AgentId;
            }

            if (application.ApplicationStatusId != 0)
            {
                this.ApplicationStatusId = application.ApplicationStatusId;
            }

            if (application.DateAccepted != null && application.DateAccepted != new DateTime())
            {
                this.DateAccepted = application.DateAccepted;
            }

            if (application.EmployeeId != 0)
            {
                this.EmployeeId = application.EmployeeId;
            }
        }
    }
}
