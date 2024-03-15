using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PCardTransactions
{
    internal class PCardTransactionsDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                    query = @"SELECT TOP 1 CONVERT (Date , IV.[invoiceDate]) AS FromDate, Convert (Date, IV.[invoiceDate]) AS ToDate FROM invoice_tb IV WITH (NOLOCK) WHERE ISNULL(IV.[systemType], 0) = 1 AND IV.[isactive] = 1 ORDER BY IV.[invoiceDate] DESC;";
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
