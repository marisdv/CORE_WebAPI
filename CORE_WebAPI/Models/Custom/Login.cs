using System;
using System.Collections.Generic;
using System.Security;
using System.Security;
using Microsoft.AspNet.Identity;

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


        public void hashPassword()
        {
            PasswordHasher hasher = new PasswordHasher();
            this.Password = hasher.HashPassword(this.Password);
        }

        public bool verifyPassword(string password)
        {
            PasswordHasher hasher = new PasswordHasher();
            var result = hasher.VerifyHashedPassword(this.Password,password);
            if (result == PasswordVerificationResult.Success)
            {
                return true;
            }
            else return false;
        }
    }
}
