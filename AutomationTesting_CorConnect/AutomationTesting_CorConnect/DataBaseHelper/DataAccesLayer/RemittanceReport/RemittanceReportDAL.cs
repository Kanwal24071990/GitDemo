using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.RemittanceReport
{
    internal class RemittanceReportDAL : BaseDataAccessLayer
    {
        internal void GetDateData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                string userType;

                if (TestContext.CurrentContext.Test.Properties["Type"].Count() > 0)
                {
                    userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                }
                else
                {
                    userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                }

                if (userType == "ADMIN")
                {
                    query = @"SELECT TOP 1 CONVERT (Date , RH.pymtDate) AS FromDate, Convert (Date, RH.pymtDate) AS ToDate FROM gpRemittance_tb RH  WITH (NOLOCK) 
                        INNER JOIN gpRemittanceDetails_tb RD  WITH (NOLOCK) on RD.gpRemittanceID = RH.gpRemittanceID INNER JOIN entityDetails_tb E WITH (NOLOCK) on RH.vendorAcctCode =
                        E.accountingCode AND E.entityTypeId = 2 AND E.locationTypeId = 25 AND E.isActive = 1 INNER JOIN invoice_tb I WITH (NOLOCK) on I.lookUpId = RD.invLookupId  
                        AND I.isActive =1 INNER JOIN Transaction_tb T WITH (NOLOCK) ON T.transactionId=I.transactionId INNER JOIN lookup_tb LK with (nolock) on parentlookupcode = 21 
                        AND I.transactionTypeCode = LK.[description] LEFT JOIN invoiceReference_tb IR WITH (NOLOCK) ON IR.invoiceId = I.invoiceId AND LTRIM(RTRIM(ISNULL(IR.referenceValue, ''))) 
                        <> '' AND IR.isActive = 1 AND ISNULL(IR.referenceType, '') = 'VN' ORDER BY RH.pymtDate DESC";
                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }
                else if (userType == "DEALER")
                {
                    query = @"DECLARE @@Userid as int declare @@DealerAccessLocations table (entityDetailId INT primary key ) select @@Userid=userid from [user_tb] where username= @UserName;
                        WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH (NOLOCK) WHERE 
                        entityDetailId iN ( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH (NOLOCK) INNER JOIN [user_tb] WITH (NOLOCK) ON userRelationships_tb.userId 
                        = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1) UNION ALL SELECT 
                        C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH (NOLOCK) ON P.entityDetailId = C.parentEntityDetailId)
                        insert into @@DealerAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId<> entityDetailId and parentEntityDetailId <> 0 UNION SELECT
                        userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH (NOLOCK) WHERE userRelationships_tb.userId = @@UserID and IsActive =1 AND 
                        userRelationships_tb.entityId IS NOT NULL; SELECT top 1 Convert(Date, RH.pymtDate) as FromDate ,Convert(Date, RH.pymtDate) as ToDate FROM [gpRemittance_tb] RH 
                        WITH (NOLOCK) INNER JOIN [gpRemittanceDetails_tb] RD WITH (NOLOCK) on RD.gpRemittanceID = RH.gpRemittanceID INNER JOIN [entityDetails_tb] E WITH (NOLOCK) on 
                        RH.vendorAcctCode =E.accountingCode AND E.entityTypeId = 2 AND E.locationTypeId =25 AND E.isActive =1 INNER JOIN [invoice_tb] I WITH (NOLOCK) on I.lookUpId = 
                        RD.invLookupId AND I.isActive =1 INNER JOIN @@DealerAccessLocations AE ON AE.entityDetailId = I.senderBillToEntityDetailId INNER JOIN [Transaction_tb] T WITH 
                        (NOLOCK) ON T.transactionId=I.transactionId INNER JOIN [lookup_tb] LK WITH (NOLOCK) on parentlookupcode = 21 AND I.transactionTypeCode = LK.[description] 
                        LEFT JOIN [invoiceReference_tb] IR WITH(NOLOCK) on IR.invoiceId = I.invoiceId AND LTRIM(RTRIM(ISNULL(IR.referenceValue,'')))<> '' AND IR.isActive =1 AND 
                        ISNULL(IR.referenceType,'') ='VN' ORDER BY RH.pymtDate DESC";
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
