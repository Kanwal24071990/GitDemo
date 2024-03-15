using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerReleaseInvoices
{
    internal class DealerReleaseInvoicesDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                
                    query = @"SELECT top 1 Convert (Date , createDate) as FromDate ,Convert (Date , createDate) as ToDate FROM [transaction_tb] tr WHERE tr.isActive = 1 and tr.validationStatus in (2,9) and tr.requestTypeCode in ('S') and tr.isHistorical = 0 and tr.transactionId in (SELECT transactionId FROM [transactionError_tb] WHERE errorCodeId = 105) ORDER BY createDate DESC";

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

        internal void GetData(string dealerName , string fleetName , out string TransactionNumber , out string FromDate , out string ToDate)
        {          
            FromDate = string.Empty;
            ToDate = string.Empty;
            TransactionNumber = string.Empty;
            string query = null;

            try
            {

                query = @"select top 1 transactionnumber, createDate from transaction_tb where  isHistorical=0 and isactive=1 and validationStatus=9 and  senderCorcentricCode=@dealerName and receiverCorcentricCode=@fleetName";


                List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerName", dealerName),
                new SqlParameter("@fleetName", fleetName),

            };

                using (var reader = ExecuteReader(query,sp, false))
                {
                    if (reader.Read())
                    {
                        ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        FromDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        TransactionNumber = reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                FromDate = null;
                ToDate = null;
                TransactionNumber = null;
            }
        }
    }
}
