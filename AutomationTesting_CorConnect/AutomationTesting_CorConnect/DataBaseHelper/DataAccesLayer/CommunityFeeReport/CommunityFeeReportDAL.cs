using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CommunityFeeReport
{
    internal class CommunityFeeReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                query = @"SELECT TOP 1 CONVERT (Date , invoicedate) as FromDate ,Convert (Date , invoicedate) as ToDate FROM  [invoice_tb] I WITH (NOLOCK)  LEFT JOIN [entitydetails_tb] s on s.entitydetailid=i.senderentitydetailid LEFT JOIN [entitydetails_tb] f on f.entitydetailid=i.receiverentitydetailid WHERE  I.isActive = 1 and I.currencyCode In('USD', 'AUD', 'CAD', 'MXN') ORDER BY I.invoiceDate DESC;";
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

        internal void GetCurrencyCode(out string currencyCode)
        {
            currencyCode = string.Empty;
            
            string query = null;

            try
            {
                query = @"SELECT top 1 I.currencyCode [Currency] FROM  [invoice_tb] I WITH (NOLOCK)  LEFT JOIN [entitydetails_tb] s on s.entitydetailid=i.senderentitydetailid LEFT JOIN [entitydetails_tb] f on f.entitydetailid=i.receiverentitydetailid WHERE  I.isActive = 1 and I.currencyCode In('USD', 'AUD', 'CAD', 'MXN') ORDER BY I.invoiceDate DESC;";
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        currencyCode = reader.GetString(0);
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                currencyCode = null;
                
            }
        }
    }
}
