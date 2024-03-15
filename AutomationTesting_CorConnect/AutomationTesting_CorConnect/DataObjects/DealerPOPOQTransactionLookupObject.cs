using System;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class DealerPOPOQTransactionLookupObject
    {
        private bool _historical;
        private decimal _total;
        public string DocumentType { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
        public string Update { get; set; }
        public string Historical
        {
            get
            {
                if (_historical)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }

        }
        public string ReceivedDate { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentDate { get; set; }
        public string RefPOQ { get; set; }
        public string POQDate { get; set; }
        public string DealerCode { get; set; }
        public string Dealer { get; set; }
        public string FleetCode { get; set; }
        public string Fleet { get; set; }
        public decimal Total { set { _total = value; } }
        public string TotalString
        {
            get
            {
                return _total.ToString("0.00");
            }
        }

        public string Currency { get; set; }
        public string description { get; set; }

        public bool HistoricalObject
        {
            set { _historical = value; }
        }
    }
}
