using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ShopSummaryReport
{
    internal class ShopSummaryReportDAL : BaseDataAccessLayer
    {
        internal void GetDateData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                if (userType == "ADMIN")
                {
                    query = @"SELECT TOP 1 CONVERT (Date , invoicedate) AS FromDate, Convert (Date, invoicedate) AS ToDate FROM [invoice_tb] I WITH (NOLOCK) INNER JOIN 
                        [transactionSection_tb] S WITH (NOLOCK) ON (I.[transactionId] = S.[transactionId]) INNER JOIN [transactionLineDetail_tb] L WITH (NOLOCK) ON (L.[transactionSectionId] 
                        = S.[transactionSectionId]) WHERE I.[isActive] = 1 AND(ISNULL(L.[accountingDocumentTypeId], 0) = 0 OR L.[accountingDocumentTypeId] = 
                        (SELECT lookUpId FROM [lookup_tb] WITH(NOLOCK) WHERE parentLookUpCode = 218 and lookupcode = 1))  AND L.isActive = 1 ORDER BY invoicedate DESC";
                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }
                else if (userType == "Fleet")
                {
                    query = @"DECLARE @@Userid as int DECLARE @@FleetAccessLocations table(entityDetailId INT  primary key) select @@Userid = userid FROM [user_tb] where 
                        username= @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb]  WITH(NOLOCK) WHERE 
                        entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK)  INNER JOIN [user_tb] WITH(NOLOCK) ON 
                        userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1) 
                        UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = 
                        C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT entityDetailId FROM  RootNumber  where parentEntityDetailId <> entityDetailId and 
                        parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId  FROM [userRelationships_tb]  WITH(NOLOCK) WHERE userRelationships_tb.userId = 
                        @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; Select top 1 Convert(Date, invoicedate) as FromDate, Convert(Date, invoicedate) as 
                        toDate from [invoice_tb] I (NOLOCK) INNER JOIN [transactionSection_tb]  S (NOLOCK) on I.transactionId =S.transactionId INNER JOIN [transactionLineDetail_tb] L 
                        (NOLOCK) on L.transactionSectionId=S.transactionSectionId inner join @@FleetAccessLocations AE on AE.Entitydetailid=  I.receiverBillToEntityDetailId where 
                        I.isActive =1  and (isnull(L.accountingDocumentTypeId,0) = 0 OR L.accountingDocumentTypeId = (Select lookUpId from [lookup_tb] with (nolock) where parentLookUpCode 
                        = 218 and lookupcode = 1)) AND L.isActive = 1 order by i.invoicedate desc ";
                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
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
