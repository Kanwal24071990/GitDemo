using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PurchaseOrders
{
    internal class PurchaseOrdersDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {

            string databaseName = applicationContext.ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToUpper() == applicationContext.ApplicationContext.GetInstance().client.ToUpper()).Database.EOPDBName;
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {

                query = @"SELECT top 1 Convert (Date , P.poTimeStamp) as FromDate ,Convert (Date , P.poTimeStamp) as ToDate FROM [EOP_DATABASE].dbo.Poreport WITH (NOLOCK) INNER JOIN 
                        [EOP_DATABASE].dbo.Purchase_Order p WITH (NOLOCK) ON Poreport.POTransID = p.transactionID INNER JOIN [EOP_DATABASE].dbo.partner VendorPartner WITH (NOLOCK) ON 
                        VendorPartner.corpId = p.vendorCorpId INNER JOIN [EOP_DATABASE].dbo.partner MemberPartner WITH (NOLOCK) ON MemberPartner.corpId = p.buyerCorpId order by P.poTimeStamp desc";
                query = query.Replace("[EOP_DATABASE]", databaseName);
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
