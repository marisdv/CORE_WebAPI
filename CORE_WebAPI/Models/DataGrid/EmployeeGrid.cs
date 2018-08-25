using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class EmployeeGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<Employee> employees { get; set; }
    }
}
