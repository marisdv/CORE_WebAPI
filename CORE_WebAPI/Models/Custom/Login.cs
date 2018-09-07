using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Login
    {
        public void UpdateChangedFields(Login login)
        {
            if (login.PhoneNo != null)
            {
                this.PhoneNo = login.PhoneNo;
            }
            if (login.Password != null)
            {
                this.Password = login.Password;
            }
            if (login.UserTypeId != 0)
            {
                this.UserTypeId = login.UserTypeId;
            }
         
        }
     

    }
}
