using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AuditLogGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<AuditLog> auditLogs { get; set; }
    }
}
