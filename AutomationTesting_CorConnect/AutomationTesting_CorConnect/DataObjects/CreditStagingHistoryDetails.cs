using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class CreditStagingHistoryDetails
    {
        public int CreditLimit { get; set; }
        public int AvailableCreditLimit { get; set; }
        public int TotalAR { get; set; }
    }
}
