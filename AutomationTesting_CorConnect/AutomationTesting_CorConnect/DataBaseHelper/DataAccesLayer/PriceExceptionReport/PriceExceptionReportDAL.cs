using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PriceExceptionReport
{
    internal class PriceExceptionReportDAL : BaseDataAccessLayer
    {
        internal void GetDateData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                string client = ApplicationContext.GetInstance().client.ToUpper();
                string userType;

                if (TestContext.CurrentContext.Test.Properties["Type"].Count() > 0)
                {
                    userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                }
                else
                {
                    userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                }

                string viparCondition = @"AND ISNULL(T.origRefTransactionId, '''') <> '''' AND ((ISNULL(LD.origUnitPrice, 0) <> ISNULL(LD.unitPrice, 0)) OR 
                        (ISNULL(LD.origCorePrice, 0) <> ISNULL(LD.corePrice, 0)) OR (ISNULL(LD.origFETAmount, 0) <> ISNULL(LD.FETAmount, 0))) ORDER BY invoicedate DESC";
                string nonViparCondition = @"AND ((LD.NTEP IS NOT NULL AND ((LD.unitPrice <> LD.OrigUnitPrice AND LD.NTEP = LD.unitPrice AND LD.lineDetailStatusId 
                        <> 0) OR (LD.NTEP > LD.unitPrice))) OR (LD.coreNTEP IS NOT NULL AND ((LD.corePrice <> LD.OrigcorePrice AND LD.coreNTEP = LD.corePrice AND LD.lineDetailStatusId <> 0)
                        OR (LD.coreNTEP > LD.corePrice))))";
                if (userType == "ADMIN")
                {
                    string conditions = client.Equals("VIPAR") ? viparCondition : nonViparCondition;
                    query = @"SELECT TOP 1 CONVERT (Date , invoicedate) AS FromDate, Convert (Date, invoicedate) AS ToDate FROM [invoice_tb] I WITH (NOLOCK) 
                        INNER JOIN [transaction_tb] T (NOLOCK) ON T.transactionId = I.transactionId INNER JOIN [transactionSection_tb] TS (NOLOCK) ON I.transactionId =  TS.transactionId 
                        INNER JOIN [transactionLineDetail_tb] LD (NOLOCK) ON TS.transactionSectionId = LD.transactionSectionId WHERE I.isActive=1 AND LD.lineDetailType NOT IN ('C','D') 
                        AND ((ISNULL(LD.accountingDocumentTypeId,0) = 0) OR (LD.accountingDocumentTypeId = (SELECT lookUpId FROM [lookup_tb] WITH (NOLOCK) WHERE parentLookUpCode = 218 
                        AND lookupcode = 2))) AND LD.rebateType IN (0, 1) AND LD.isActive = 1 AND LD.lineDetailType = 'P' AND LD.quantity > 0 AND ((LD.unitPrice > 0) OR (LD.corePrice > 0) 
                        OR (LD.FETAmount > 0)) " + conditions;
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
                    if (client == "VIPAR")
                    {
                        query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE (entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE 
                            username=@UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) 
                            WHERE entityDetailId IN (SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId 
                            WHERE U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber 
                            AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId 
                            FROM RootNumber WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM [userRelationships_tb] 
                            UR2 WITH(NOLOCK) WHERE UR2.userId = @@UserID AND IsActive = 1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE, invoicedate) AS FromDate, 
                            CONVERT(DATE, invoicedate) AS ToDate FROM [invoice_tb] I WITH(NOLOCK) INNER JOIN [transaction_tb] T WITH(NOLOCK) ON T.transactionId = I.transactionId 
                            INNER JOIN [transactionSection_tb] TS WITH(NOLOCK) ON I.transactionId = TS.transactionId INNER JOIN [transactionLineDetail_tb] LD WITH(NOLOCK) ON 
                            TS.transactionSectionId = LD.transactionSectionId  INNER JOIN @@DealerAccessLocations AE ON AE.entityDetailId = I.senderEntityDetailId WHERE I.isActive = 1 
                            AND LD.lineDetailType NOT IN('C', 'D') AND((ISNULL(LD.accountingDocumentTypeId, 0) = 0) OR(LD.accountingDocumentTypeId = (SELECT lookUpId FROM [lookup_tb]
                            WITH(NOLOCK) WHERE parentLookUpCode = 218 AND lookupcode = 2))) AND LD.rebateType IN (0,1) AND LD.isActive = 1  AND LD.lineDetailType = 'P' AND LD.quantity > 0 
                            AND((LD.unitPrice > 0) OR(LD.corePrice > 0) OR(LD.FETAmount > 0)) AND ISNULL(T.origRefTransactionId, '') <> '' AND((ISNULL(LD.origUnitPrice, 0) <> 
                            ISNULL(LD.unitPrice, 0)) OR(ISNULL(LD.origCorePrice, 0) <> ISNULL(LD.corePrice, 0)) OR(ISNULL(LD.origFETAmount, 0) <> ISNULL(LD.FETAmount, 0)));";
                    }
                    else
                    {
                        query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE (entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE username=
                            @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId IN 
                            (SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId WHERE U.userId = @@UserID 
                            AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN 
                            [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId FROM RootNumber 
                            WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM [userRelationships_tb] UR2 WITH(NOLOCK) 
                            WHERE UR2.userId = @@UserID AND IsActive = 1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE, invoicedate) AS FromDate, CONVERT(DATE, invoicedate) 
                            AS ToDate FROM [invoice_tb] I WITH(NOLOCK) INNER JOIN [transaction_tb] T (NOLOCK) ON T.transactionId = I.transactionId INNER JOIN [transactionSection_tb] TS 
                            (NOLOCK) ON I.transactionId = TS.transactionId INNER JOIN [transactionLineDetail_tb] LD (NOLOCK) ON TS.transactionSectionId = LD.transactionSectionId INNER JOIN 
                            @@DealerAccessLocations AE ON AE.entityDetailId = I.senderEntityDetailId WHERE I.isActive = 1 AND LD.lineDetailType NOT IN('C', 'D') AND ((ISNULL(LD.accountingDocumentTypeId, 0) = 0) 
                            OR(LD.accountingDocumentTypeId = (SELECT lookUpId FROM [lookup_tb] WITH(NOLOCK) WHERE parentLookUpCode = 218 AND lookupcode = 2))) AND LD.rebateType IN(0, 1) AND 
                            LD.isActive = 1 AND LD.lineDetailType = 'P' AND LD.quantity > 0 AND((LD.unitPrice > 0) OR(LD.corePrice > 0) OR(LD.FETAmount > 0)) AND((LD.NTEP IS NOT NULL AND
                            ((LD.unitPrice <> LD.OrigUnitPrice AND LD.NTEP = LD.unitPrice AND LD.lineDetailStatusId <> 0) or(LD.NTEP > LD.unitPrice))) OR(LD.coreNTEP IS NOT NULL AND 
                            ((LD.corePrice <> LD.OrigcorePrice AND LD.coreNTEP = LD.corePrice AND LD.lineDetailStatusId <> 0) or(LD.coreNTEP > LD.corePrice)))); ";
                    }
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
