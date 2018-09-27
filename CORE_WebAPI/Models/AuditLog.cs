using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class AuditLog
    {
        public int AuditId { get; set; }
        public string AuditUserName { get; set; }
        public int UserTypeId { get; set; }
        public string ItemAffected { get; set; }
        public int AuditTypeId { get; set; }
        public decimal? TxAmount { get; set; }
        public DateTime AuditDateTime { get; set; }

        public AuditType AuditType { get; set; }
        public UserType UserType { get; set; }
    }
}
