using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ASN
{
    internal class ASNDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
           
            string databaseName = applicationContext.ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToUpper() == applicationContext.ApplicationContext.GetInstance().client.ToUpper()).Database.EOPDBName;
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {

                query = @"DECLARE @@sql NVARCHAR(4000) = ' IF object_id(''tempdb..#tempScac'') IS NOT NULL BEGIN drop table #tempScac; END SELECT scacid, scacCode, parentScacCode, scacImage, ROW_NUMBER() OVER (PARTITION BY scacCode ORDER BY scacid) AS RowNum INTO #tempScac FROM [EOP_DATABASE].dbo.scacLookUp_tb WITH(NOLOCK) SELECT top 1 Convert (Date , ASN_HEADER.ReceivedDate) as FromDate ,Convert (Date , ASN_HEADER.ReceivedDate) as ToDate FROM [EOP_DATABASE].dbo.ASN_HEADER WITH (NOLOCK) INNER JOIN [EOP_DATABASE].dbo.ASN_SHIPMENT WITH (NOLOCK) ON ASN_HEADER.HeaderId = ASN_SHIPMENT.HeaderId INNER JOIN [EOP_DATABASE].dbo.ASN_ORDER WITH (NOLOCK) ON ASN_SHIPMENT.ShipId = ASN_ORDER.ShipId LEFT JOIN [EOP_DATABASE].dbo.Partner P_Vendor with (nolock) ON P_Vendor.CorpId=ASN_HEADER.vendorCorpId LEFT JOIN [EOP_DATABASE].dbo.Partner P_Member with (nolock) ON P_Member.CorpId=ASN_HEADER.buyerCorpId LEFT JOIN [EOP_DATABASE].dbo.PartnerBranch PB_Member with (nolock) ON PB_Member.locid=ASN_HEADER.locId and P_Member.CorpId=PB_Member.CorpId and PB_Member.active =1 LEFT JOIN #tempScac scacLookUp ON ASN_HEADER.RoutingIdenCode = scacLookUp.scacCode AND scacLookUp.RowNum = 1 order by ASN_HEADER.ReceivedDate desc ' DECLARE @@Filter VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@Filter VARCHAR(255) OUT', @@Filter OUT;";
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
