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
    }
}
