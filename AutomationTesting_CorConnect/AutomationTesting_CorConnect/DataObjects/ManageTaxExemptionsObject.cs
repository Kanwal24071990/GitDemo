using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class ManageTaxExemptionsObject
    {
        public string CorcentricCode { get; set; }
        public string FleetCode { get; set; }
        public string LegalName { get; set; }
        public string ParentName { get; set; }
        public string UserName { get; set; }
        public string UploadedDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string LocationType { get; set; }
        public string BillingLocation { get; set; }
        public string MasterLocation { get; set; }
        public string TerminationDate { get; set; }
        public string Exemptions { get; set; }
        public string DisplayFileName { get; set; }
    }
}
