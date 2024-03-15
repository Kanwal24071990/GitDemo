using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.VendorSummary
{
    internal class VendorSummaryDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {

            string databaseName = applicationContext.ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToUpper() == applicationContext.ApplicationContext.GetInstance().client.ToUpper()).Database.EOPDBName;
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {

                query = @"DECLARE @@sql NVARCHAR(4000) = ' SELECT top 1 Convert (Date , transactions.invTimeStamp) as FromDate ,Convert (Date , transactions.invTimeStamp) as ToDate from ProntoEOP.dbo.invoice AS transactions WITH (NOLOCK) INNER JOIN ProntoEOP.dbo.invreport ON transactions.transactionID = invreport.InvTransID order by transactions.invTimeStamp desc' DECLARE @@FromDate VARCHAR(255), @@ToDate VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@FromDate VARCHAR(255) OUT, @@ToDate VARCHAR(255) OUT', @@FromDate OUT, @@ToDate OUT;";
                query = query.Replace("[EOP_DATABASE]", databaseName);
                using (var reader = ExecuteReader(query, true))
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
