using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class LookupTb
    {
        public int LookupId { get; set; }
        public int ParentLookupCode { get; set; }
        public int LookupCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int AvailableToSender { get; set; }
        public int AvailableToReceiver { get; set; }
        public int LookupType { get; set; }
        public string TokenDescription { get; set; }
    }
}
