using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class AuditTrailTable
    {
        public int AuditId { get; set; }
        public int UserId { get; set; }
        public string AuditAppName { get; set; }
        public string AuditTable { get; set; }
        public string AuditStatement { get; set; }
        public DateTime ActionDate { get; set; }
        public string AuditAction { get; set; }
        public string AuditOriginalValue { get; set; }
        public string AuditNewValue { get; set; }
    }
}
