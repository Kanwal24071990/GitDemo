using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class RelCreditPriceAuditTb
    {
        internal int TransactionTypeId { get; set; }
        internal int ApplicableTo { get; set; }
        internal bool IsActive { get; set; }
        internal bool EnableGMCCalculation { get; set; }
        internal bool EnableRebatesCalculation { get; set; }
    }
}
