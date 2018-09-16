using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Employee
    {
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
            if (employee.AccessRoleId != 0)
            {
                this.AccessRoleId = employee.AccessRoleId;
            }
            if (employee.EmployeeImage != null)
            {
                this.EmployeeImage = employee.EmployeeImage;
            }
            if (employee.EmployeePhone != null)
            {
                this.EmployeePhone = employee.EmployeePhone;
            }
            if (employee.EmployeePassword != null)
            {
                this.EmployeePassword = employee.EmployeePassword;
            }
        }
    }
}
