using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AuditLog
    {
        public int AuditId { get; set; }
        public string AuditUser { get; set; }
        public DateTime AuditDateTime { get; set; }
        public string TableAffected { get; set; }
        public string AttributeAffected { get; set; }
        public int AuditTypeId { get; set; }

        public AuditType AuditType { get; set; }
    }
}
