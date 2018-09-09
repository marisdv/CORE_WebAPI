using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Application = new HashSet<Application>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeNationalId { get; set; }
        public string EmployeePassportNo { get; set; }
        public byte? EmployeeActive { get; set; }
        public DateTime DateEmployed { get; set; }
        public int AccessRoleId { get; set; }
        public int LoginId { get; set; }

        public AccessRole AccessRole { get; set; }
        public Login Login { get; set; }
        public ICollection<Application> Application { get; set; }
    }
}
