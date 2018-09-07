using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ApplicationGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<Application> applications { get; set; }
    }
}
