using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class FleetInvoiceObject
    {
        public string InvoiceNumber { get; set; }
        public string ARCurrencyCode { get; set; }
        public string InvoiceDate { get; set; }
        public bool IsOnline { get; set; }
    }
}
