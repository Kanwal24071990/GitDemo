using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetSalesSummaryBillTo
{
    internal class FleetSalesSummaryBillToDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            var query = @"Select  top 1 Convert (Date , invoicedate) as FromDate ,Convert (Date , invoicedate) as ToDate  FROM  invoice_tb iv with (nolock)
                        inner join invoiceReport_tb ir on iv.invoiceId = ir.invoiceId
                        where iv.isActive = 1
                        Order by invoicedate desc";

            try
            {
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
