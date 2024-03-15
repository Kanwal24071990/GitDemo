using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class EntityDetails
    {
        public int EntityDetailId { get; set; }
        public string EntityCode { get; set; }
        public string CommunityCode { get; set; }
        public double CreditLimit { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool MasterFlag { get; set; }
        public bool? DDEFlag { get; set; }
        public string CorcentricCode { get; set; }
        public string AccountingCode { get; set; }
        public bool? CorcentricLocation { get; set; }
        public bool? FinanceChargeExempt { get; set; }
        public bool? EnablePaymentPortal { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public bool? IsOverrideTermDescription { get; set; }
        public int OverrideTermDescriptionDays { get; set; }
        public string DealerSubCommunity { get; set; }
        public string FleetSubCommunity { get; set; }
        public string FleetGroup { get; set; }
        public string DealerGroup { get; set; }
        public string DisplayName { get; set; }
        public int LocationTypeId { get; set; }

    }
}
