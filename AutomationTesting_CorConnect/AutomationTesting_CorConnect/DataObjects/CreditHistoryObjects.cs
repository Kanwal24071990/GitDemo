using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class CreditHistoryObjects
    {
        internal string updateDate { get; set; }
        internal decimal creditLimit { get; set; }
        internal string UserName { get; set; }
        internal string firstName { get; set; }
        internal string lastName { get; set; }
        internal string isImpersonated { get; set; }
        internal string impersonatedUserName { get; set; }
    }
}
