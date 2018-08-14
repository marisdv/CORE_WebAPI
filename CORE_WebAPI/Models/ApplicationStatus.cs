using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ApplicationStatus
    {
        public ApplicationStatus()
        {
            Application = new HashSet<Application>();
        }

        public int ApplicationStatusId { get; set; }
        public string Description { get; set; }

        public ICollection<Application> Application { get; set; }
    }
}
