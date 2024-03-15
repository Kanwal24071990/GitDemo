using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartPriceFileUploadReport
{
    internal class PartPriceFileUploadReportDAL : BaseDataAccessLayer
    {
        internal void GetDateData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                query = @"SELECT TOP 1 CONVERT (Date, importDate) AS FromDate, Convert (Date, importDate) AS ToDate FROM [importDetails_tb] WHERE importTypeId 
                    IN (SELECT lookupid FROM [lookUp_tb] WHERE parentlookupcode=34 AND lookupcode IN (1,2)) ORDER BY importDate DESC";
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                        ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                FromDate = null;
                ToDate = null;
            }
        }
    }
}
