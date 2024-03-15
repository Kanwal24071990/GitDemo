using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ManageTaxExemptions
{
    internal class ManageTaxExemptionsDAL : BaseDataAccessLayer
    {
        internal int GetRecordsCount(string fleetCode, int editTypeId, string taxDoc = null, string name = null,
            string address = null, string city = null, string states = null)
        {
            int recordCount = 0;
            string spName = "usp_GetTaxDocs";
            try
            {
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@CustomerCode", GetEntityId(fleetCode)),
                    new SqlParameter("@u_ID", GetUserId()),
                    new SqlParameter("@editType", editTypeId),
                };

                if (taxDoc != null)
                {
                    sp.Add(new SqlParameter("@TaxDoc", taxDoc));
                }
                if (name != null)
                {
                    sp.Add(new SqlParameter("@name", name));
                }
                if (address != null)
                {
                    sp.Add(new SqlParameter("@address", address));
                }
                if (city != null)
                {
                    sp.Add(new SqlParameter("@city", city));
                }
                if (states != null)
                {
                    sp.Add(new SqlParameter("@states", states));
                }

                using (var oReader = ExecuteSP(spName, sp, false))
                {
                    while (oReader.Read())
                    {
                        recordCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                recordCount = -1;
            }
            return recordCount;
        }

        internal List<ManageTaxExemptionsObject> GetTaxDocsRecordsList(string fleetCode, int editTypeId, string taxDoc = null, string name = null,
           string address = null, string city = null, string states = null)
        {
            string spName = "usp_GetTaxDocs";
            List<ManageTaxExemptionsObject> objList = null;
            try
            {
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@CustomerCode", GetEntityId(fleetCode)),
                    new SqlParameter("@u_ID", GetUserId()),
                    new SqlParameter("@editType", editTypeId),
                };

                if (taxDoc != null)
                {
                    sp.Add(new SqlParameter("@TaxDoc", taxDoc));
                }
                if (name != null)
                {
                    sp.Add(new SqlParameter("@name", name));
                }
                if (address != null)
                {
                    sp.Add(new SqlParameter("@address", address));
                }
                if (city != null)
                {
                    sp.Add(new SqlParameter("@city", city));
                }
                if (states != null)
                {
                    sp.Add(new SqlParameter("@states", states));
                }

                using (var oReader = ExecuteSP(spName, sp, false))
                {
                    if (oReader.HasRows)
                    {
                        objList = new List<ManageTaxExemptionsObject>();
                        while (oReader.Read())
                        {
                            objList.Add(new ManageTaxExemptionsObject()
                            {
                                FleetCode = oReader.GetStringValue("corcentricCode"),
                                CorcentricCode = oReader.GetStringValue("corcentricCode"),
                                LegalName = oReader.GetStringValue("legalName"),
                                ParentName = oReader.GetStringValue("ParentLocation"),
                                BillingLocation = oReader.GetStringValue("BillingLocation"),
                                LocationType = oReader.GetStringValue("LocationType"),
                                City = oReader.GetStringValue("City"),
                                State = oReader.GetStringValue("State"),
                                MasterLocation = oReader.GetStringValue("MasterLocation"),
                                UserName = oReader.GetStringValue("UserName"),
                                TerminationDate = oReader.GetDateTimeValue("TerminationDate"),
                                UploadedDate = oReader.GetDateTimeValue("UploadedDate"),
                                Exemptions = oReader.GetStringValue("Exemptions"),
                                DisplayFileName = oReader.GetStringValue("displayFileName")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }
            return objList;
        }
    }
}
