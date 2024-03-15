using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class TransactionDetails
    {
        public int CreditAmount { get; set; }
        public string ActionType { get; set; }
        public int TransactionAmount { get; set; }
        public string BYR { get; set; }
        public string SUP { get; set; }
        public string TransactionNumber { get; set; }
        public string AuthCode { get; set; }
    }
}
