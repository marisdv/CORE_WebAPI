using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class UserType
    {
        public UserType()
        {
            AuditLog = new HashSet<AuditLog>();
            Login = new HashSet<Login>();
        }

        public int UserTypeId { get; set; }
        public string UserTypeDescr { get; set; }

        public ICollection<AuditLog> AuditLog { get; set; }
        public ICollection<Login> Login { get; set; }
    }
}
