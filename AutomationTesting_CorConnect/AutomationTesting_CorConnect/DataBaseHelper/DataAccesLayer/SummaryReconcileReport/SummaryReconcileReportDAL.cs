using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;


namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.SummaryReconcileReport
{
    internal class SummaryReconcileReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                query = @"WITH Invoices AS (SELECT I.PostingDate FROM [invoice_tb] I WITH (nolock) LEFT JOIN [invoiceVariablePaymentTerms_tb] Ivpt With (NoLock) ON I.invoiceId=Ivpt.invoiceId  AND Ivpt.primaryPaymentTerm = 1 WHERE I.isActive =1 UNION ALL SELECT  S.postingDate FROM [statement_tb] S (NOLOCK) WHERE S.isActive = 1 AND S.statementTypeId in (SELECT lookupid FROM [lookup_tb] WHERE parentlookupcode = 119 and lookupcode = 3) AND S.statementStatus = 1 ) SELECT top 1 Convert(Date, i.PostingDate) as FromDate ,Convert(Date, i.PostingDate) as ToDate FROM Invoices I ORDER BY PostingDate DESC;";
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
                LoggingHelper.LogException(ex.Message);
                FromDate = null;
                ToDate = null;
            }
        }
    }
}
