using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.TransactionActivity
{
    internal class TransactionActivityDAL : BaseDataAccessLayer
    {

        internal void GetSellerAndFromDate(out string FromDate, out string SellerName)
        {
            FromDate = string.Empty;
            SellerName = string.Empty;
            string query = null;
            try
            {
                string databaseName = applicationContext.ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToUpper() == applicationContext.ApplicationContext.GetInstance().client.ToUpper()).Database.EOPDBName;
                query = @"select top 1 Convert (Date, inv.invTimeStamp) as FromDate, p.PartnerName as SellerName from [EOP_DATABASE].dbo.invoice as inv left join [EOP_DATABASE].dbo.Partner as p on inv.vendorCorpId = p.CorpId order by inv.invTimeStamp DESC";
                query = query.Replace("[EOP_DATABASE]", databaseName);
                using (var reader = ExecuteReader(query, true))
                {
                    if (reader.Read())
                    {
                        FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                        SellerName = reader.GetString(1);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                FromDate = null;
                SellerName = null;
            }
        }
    }
}
