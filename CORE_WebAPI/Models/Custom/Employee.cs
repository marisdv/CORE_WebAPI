using Microsoft.AspNet.Identity;
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

        public void hashPassword()
        {
            PasswordHasher hasher = new PasswordHasher();
            this.EmployeePassword = hasher.HashPassword(this.EmployeePassword);
        }

        public bool verifyPassword(string password)
        {
            PasswordHasher hasher = new PasswordHasher();
            var result = hasher.VerifyHashedPassword(this.EmployeePassword, password);
            if (result == PasswordVerificationResult.Success)
            {
                return true;
            }
            else return false;
        }

        public string getFullName()
        {
            return this.EmployeeName + " " + this.EmployeeSurname;
        }
    }
}
