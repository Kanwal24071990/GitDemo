using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ManageTaxExemptions;
using AutomationTesting_CorConnect.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.ManageTaxExemptions
{
    internal class ManageTaxExemptionsUtils
    {
        internal static int GetRecordsCount(string fleetCode, int editTypeId)
        {
            return new ManageTaxExemptionsDAL().GetRecordsCount(fleetCode, editTypeId);
        }

        internal static List<ManageTaxExemptionsObject> GetTaxDocsRecordsList(string fleetCode, int editTypeId, string taxDoc = null, string name = null,
           string address = null, string city = null, string states = null)
        {
            return new ManageTaxExemptionsDAL().GetTaxDocsRecordsList(fleetCode, editTypeId, taxDoc, name, address, city, states);
        }

        internal static List<AuditTrailTable> GetAuditTrails()
        {
            return new ManageTaxExemptionsDAL().GetAuditTrails();
        }
    }
}
