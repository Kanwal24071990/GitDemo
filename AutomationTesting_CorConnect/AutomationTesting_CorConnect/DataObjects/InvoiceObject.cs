using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class InvoiceObject
    {
        public int InvoiceId { get; set; }
        public int InvoiceSectionId { get; set; }
        public string InvoiceNumber { get; set; }
        public string TransactionId { get; set; }
        public string InvoiceDate { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string APPaidDate { get; set; }
        public string ARPaidDate { get; set; }
        public string OriginatingDocumentNumber { get; set; }
    }
}
