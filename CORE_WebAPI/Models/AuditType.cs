using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AuditType
    {
        public AuditType()
        {
            AuditLog = new HashSet<AuditLog>();
        }

        public int AuditTypeId { get; set; }
        public string AuditTypeName { get; set; }

        public ICollection<AuditLog> AuditLog { get; set; }
    }
}
