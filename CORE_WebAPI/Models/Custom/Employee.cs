using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Employee
    {
        

        public string GetBasicDetails()
        {
            return this.EmployeeName + ' ' + this.EmployeeSurname;

        }

        public void UpdateChangedFields(Employee employee)
        {
            if (employee.EmployeeName != null)
            {
                this.EmployeeName = employee.EmployeeName;
            }
            if (employee.EmployeeSurname != null)
            {
                this.EmployeeSurname = employee.EmployeeSurname;
            }
            if (employee.EmployeeEmail != null)
            {
                this.EmployeeEmail = employee.EmployeeEmail;
            }
            if (employee.EmployeeNationalId != null)
            {
                this.EmployeeNationalId = employee.EmployeeNationalId;
            }
            if (employee.EmployeePassportNo != null)
            {
                this.EmployeePassportNo = employee.EmployeePassportNo;
            }
            if (employee.EmployeeActive != null)
            {
                this.EmployeeActive = employee.EmployeeActive;
            }
            if (employee.DateEmployed != null && employee.DateEmployed != new DateTime())
            {
                this.DateEmployed = employee.DateEmployed;
            }
        }
    }
}
