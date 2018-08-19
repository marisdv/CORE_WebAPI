using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Login = new HashSet<Login>();
        }

        public int UserTypeId { get; set; }
        public string UserTypeDescr { get; set; }

        public ICollection<Login> Login { get; set; }
    }
}
