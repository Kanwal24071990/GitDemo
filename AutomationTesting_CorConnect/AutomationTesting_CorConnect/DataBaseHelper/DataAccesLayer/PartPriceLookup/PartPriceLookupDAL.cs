using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartPriceLookup
{
    internal class PartPriceLookupDAL : BaseDataAccessLayer
    {
        internal string GetDealerCode()
        {
            string dealerCode = String.Empty;
            string query = string.Empty;
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
                query = "DECLARE @@databaseName NVARCHAR(100) = (SELECT DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString'); DECLARE @@sql NVARCHAR(4000) = 'DECLARE @@SenderCorcentiCode AS VARCHAR(50); SELECT TOP 1 @@SenderCorcentiCode=corcentriccode FROM ' + @@databaseName + '.[dbo].[entitydetails_tb] WHERE isactive=1 AND enrollmentstatusid=13 AND eligiblefortransaction=1 AND entitytypeid=2 AND locationtypeid <> 24 ORDER BY entitydetailid ASC SELECT TOP 1 * FROM ((SELECT  top 100 t.sendercorcentriccode AS DealerCode FROM ' + @@databaseName + '.[dbo].[Invoice_tb] IV WITH (nolock) INNER JOIN ' + @@databaseName + '.[dbo].[transaction_tb] t WITH (nolock) ON t.transactionid=iv.transactionid INNER JOIN ' + @@databaseName + '.[dbo].[invoiceSection_tb] WITH (nolock) ON IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN ' + @@databaseName + '.[dbo].[invoiceLineDetail_tb] ID WITH (nolock) ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId WHERE ISNULL(id.itemid ,0) <> 0 AND iv.isActive = 1) UNION (SELECT @@SenderCorcentiCode AS DealerCode )) AS PPL WHERE ISNULL(PPL.DealerCode,'''') <>'''''; DECLARE @@Filter VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@Filter VARCHAR(255) OUT', @@Filter OUT;";
            }
            else
            {
                query = "DECLARE @@databaseName NVARCHAR(100) = (SELECT DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString'); DECLARE @@sql NVARCHAR(4000) = ' DECLARE @Userid as int DECLARE @@DealerAccessLocations table(entityDetailId INT  primary key) select @@Userid = userid from ' + @@databaseName + '.[dbo].[user_tb] where username = ''" + GetUserId() + "''; WITH RootNumber AS( SELECT  entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM ' + @@databaseName + '.[dbo].[entityDetails_tb]  WITH(NOLOCK) WHERE  entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM ' + @@databaseName + '.[dbo].[userRelationships_tb] WITH(NOLOCK)  INNER JOIN ' + @@databaseName + '.[dbo].[user_tb]  WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1  AND userRelationships_tb.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN ' + @@databaseName + '.[dbo].[entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) insert into @@DealerAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId<> entityDetailId and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM ' + @@databaseName + '.[dbo].[userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; DECLARE @@SenderCorcentiCode as varchar(50), @@ReceiverCorcentiCode as varchar(50) select top 1 @@SenderCorcentiCode = e.corcentriccode from ' + @@databaseName + '.[dbo].[entitydetails_tb] e inner join @@DealerAccessLocations d on d.entityDetailId = d.entityDetailId where e.isactive = 1 and enrollmentstatusid = 13 and eligiblefortransaction = 1 and e.entitytypeid = 2 and locationtypeid <> 24 order by e.entitydetailid asc select top 1 @@ReceiverCorcentiCode = corcentriccode from ' + @@databaseName + '.[dbo].[entitydetails_tb] where isactive = 1 and enrollmentstatusid = 13 and eligiblefortransaction = 1 and entitytypeid = 3 and locationtypeid <> 24 order by entitydetailid asc select top 1 * From((Select  top 100 t.sendercorcentriccode as DealerCode from ' + @@databaseName + '.[dbo].[Invoice_tb] IV with (nolock) inner join ' + @@databaseName + '.[dbo].[transaction_tb] t with(nolock) on t.transactionid = iv.transactionid INNER JOIN ' + @@databaseName + '.[dbo].[invoiceSection_tb] with(nolock) ON IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN ' + @@databaseName + '.[dbo].[invoiceLineDetail_tb] ID with(nolock)  ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId INNER JOIN  @@DealerAccessLocations AE ON AE.entityDetailId = IV.senderEntityDetailId WHERE isnull(id.itemid, 0) <> 0 and iv.isActive = 1) union(select @@SenderCorcentiCode as DealerCode)) AS PPL WHERE ISNULL(PPL.DealerCode, '''') <> '''''; DECLARE  @@Filter VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@Filter VARCHAR(255) OUT', @@Filter OUT;";
            }

            using (var reader = ExecuteReader(query, true))
            {
                if (reader.Read())
                {
                    dealerCode = reader.GetString(0);
                }
            }

            return dealerCode;
        }

        internal string GetFleetCode()
        {
            string fleetCode = String.Empty;
            string query = string.Empty;
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
                query = "DECLARE @@databaseName NVARCHAR(100) = (SELECT DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString'); DECLARE @@sql NVARCHAR(4000) = 'DECLARE @@ReceiverCorcentiCode AS VARCHAR(50); SELECT TOP 1 @@ReceiverCorcentiCode=corcentriccode FROM ' + @@databaseName + '.[dbo].[entitydetails_tb] WHERE isactive=1 AND enrollmentstatusid=13 AND eligiblefortransaction=1 AND entitytypeid=3 AND locationtypeid <> 24 ORDER BY entitydetailid ASC SELECT TOP 1 * FROM ((SELECT  top 100 t.receivercorcentriccode AS FleetCode FROM ' + @@databaseName + '.[dbo].[Invoice_tb] IV WITH (nolock) INNER JOIN ' + @@databaseName + '.[dbo].[transaction_tb] t WITH (nolock) ON t.transactionid=iv.transactionid INNER JOIN ' + @@databaseName + '.[dbo].[invoiceSection_tb] WITH (nolock) ON IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN ' + @@databaseName + '.[dbo].[invoiceLineDetail_tb] ID WITH (nolock) ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId WHERE ISNULL(id.itemid ,0) <> 0 AND iv.isActive = 1) UNION (SELECT @@ReceiverCorcentiCode AS FleetCode )) AS PPL WHERE ISNULL(PPL.FleetCode,'''') <>'''''; DECLARE @@Filter VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@Filter VARCHAR(255) OUT', @@Filter OUT;";
            }
            else
            {
                query = "DECLARE @@databaseName NVARCHAR(100) = (SELECT DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString'); DECLARE @@sql NVARCHAR(4000) = ' DECLARE @@Userid as int DECLARE @@DealerAccessLocations table(entityDetailId INT primary key ) select @@Userid = userid from ' + @@databaseName + '.[dbo].[user_tb] where username = ''" + GetUserId() + "''; WITH RootNumber AS( SELECT  entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM ' + @@databaseName + '.[dbo].[entityDetails_tb]  WITH(NOLOCK) WHERE  entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM ' + @@databaseName + '.[dbo].[userRelationships_tb] WITH(NOLOCK)  INNER JOIN ' + @@databaseName + '.[dbo].[user_tb]  WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1  AND userRelationships_tb.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN ' + @@databaseName + '.[dbo].[entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) insert into @@DealerAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId<> entityDetailId and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM ' + @@databaseName + '.[dbo].[userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; DECLARE @@SenderCorcentiCode as varchar(50),@@ReceiverCorcentiCode as varchar(50) select top 1 @@SenderCorcentiCode = e.corcentriccode from ' + @@databaseName + '.[dbo].[entitydetails_tb] e inner join @@DealerAccessLocations d on d.entityDetailId = d.entityDetailId where e.isactive = 1 and enrollmentstatusid = 13 and eligiblefortransaction = 1 and e.entitytypeid = 2 and locationtypeid <> 24 order by e.entitydetailid asc select top 1 @@ReceiverCorcentiCode = corcentriccode from ' + @@databaseName + '.[dbo].[entitydetails_tb] where isactive = 1 and enrollmentstatusid = 13 and eligiblefortransaction = 1 and entitytypeid = 3 and locationtypeid <> 24 order by entitydetailid asc select top 1 * From((Select  top 100 t.receivercorcentriccode as FleetCode from ' + @@databaseName + '.[dbo].[Invoice_tb] IV with (nolock) inner join ' + @@databaseName + '.[dbo].[transaction_tb] t with(nolock) on t.transactionid = iv.transactionid INNER JOIN ' + @@databaseName + '.[dbo].[invoiceSection_tb] with(nolock) ON IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN ' + @@databaseName + '.[dbo].[invoiceLineDetail_tb] ID with(nolock)  ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId INNER JOIN  @@DealerAccessLocations AE ON AE.entityDetailId = IV.senderEntityDetailId WHERE   isnull(id.itemid, 0) <> 0 and iv.isActive = 1) union(select @@ReceiverCorcentiCode as FleetCode)) AS PPL WHERE ISNULL(PPL.FleetCode, '''') <> ''''' DECLARE  @@Filter VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@Filter VARCHAR(255) OUT', @@Filter OUT; ";
            }

            using (var reader = ExecuteReader(query, true))
            {
                if (reader.Read())
                {
                    fleetCode = reader.GetString(0);
                }
            }
            return fleetCode;
        }

        internal string GetPartNumber()
        {
            string fleetCode = String.Empty;
            string query = string.Empty;
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
                query = "DECLARE @@databaseName NVARCHAR(100) = (SELECT DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString'); DECLARE @@sql NVARCHAR(4000) = 'DECLARE @@Partnumber AS VARCHAR(50); SELECT TOP 1 @@Partnumber=partnumber FROM ' + @@databaseName + '.[dbo].[part_tb] WHERE isactive=1 ORDER BY partid DESC  SELECT TOP 1 * FROM ((SELECT  top 100 pt.partnumber as PartNumber FROM ' + @@databaseName + '.[dbo].[Invoice_tb] IV WITH (nolock) INNER JOIN ' + @@databaseName + '.[dbo].[transaction_tb] t WITH (nolock) ON t.transactionid=iv.transactionid  INNER JOIN ' + @@databaseName + '.[dbo].[invoiceSection_tb] WITH (nolock) ON IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN ' + @@databaseName + '.[dbo].[invoiceLineDetail_tb] ID WITH (nolock) ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId INNER JOIN ' + @@databaseName + '.[dbo].[part_tb] pt WITH (nolock) ON pt.compressedPartNumber=ID.item WHERE ISNULL(id.itemid ,0) <> 0 AND iv.isActive = 1) UNION (SELECT @@Partnumber AS PartNumber)) AS PPL WHERE ISNULL(PPL.Partnumber,'''') <>'''''; DECLARE @@Filter VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@Filter VARCHAR(255) OUT', @@Filter OUT;";
            }
            else
            {
                query = "DECLARE @@databaseName NVARCHAR(100) = (SELECT DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString'); DECLARE @@sql NVARCHAR(4000) = ' DECLARE @@Userid as int DECLARE @@DealerAccessLocations table(entityDetailId INT primary key) select @@Userid = userid from ' + @@databaseName + '.[dbo].[user_tb] where username = ''" + GetUserId() + "''; WITH RootNumber AS(SELECT  entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM ' + @@databaseName + '.[dbo].[entityDetails_tb]  WITH(NOLOCK) WHERE  entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM ' + @@databaseName + '.[dbo].[userRelationships_tb] WITH(NOLOCK)  INNER JOIN ' + @@databaseName + '.[dbo].[user_tb]  WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1  AND userRelationships_tb.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN ' + @@databaseName + '.[dbo].[entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) insert into @@DealerAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId<> entityDetailId and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM ' + @@databaseName + '.[dbo].[userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; Declare @@SenderCorcentiCode as varchar(50),@@ReceiverCorcentiCode as varchar(50),@@Partnumber as varchar(50) select top 1 @@SenderCorcentiCode = e.corcentriccode from ' + @@databaseName + '.[dbo].[entitydetails_tb] e inner join @DealerAccessLocations d on d.entityDetailId = d.entityDetailId where e.isactive = 1 and enrollmentstatusid = 13 and eligiblefortransaction = 1 and e.entitytypeid = 2 and locationtypeid <> 24 order by e.entitydetailid asc select top 1 @@ReceiverCorcentiCode = corcentriccode from ' + @@databaseName + '.[dbo].[entitydetails_tb] where isactive = 1 and enrollmentstatusid = 13 and eligiblefortransaction = 1 and entitytypeid = 3 and locationtypeid <> 24 order by entitydetailid asc select top 1 @@Partnumber = partnumber from '+ @@databaseName + '.[dbo].[part_tb] where isactive = 1 order by partid desc select top 1 * From((Select  top 100 id.item as PartNumber from ' + @@databaseName + '.[dbo].[Invoice_tb] IV with (nolock)  inner join ' + @@databaseName + '.[dbo].[transaction_tb] t with(nolock) on t.transactionid = iv.transactionid INNER JOIN ' + @@databaseName + '.[dbo].[invoiceSection_tb] with(nolock) ON IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN ' + @@databaseName + '.[dbo].[invoiceLineDetail_tb] ID with(nolock)  ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId INNER JOIN  @@DealerAccessLocations AE ON AE.entityDetailId = IV.senderEntityDetailId WHERE isnull(id.itemid, 0) <> 0 and iv.isActive = 1) union(select @@Partnumber as PartNumber)) AS PPL WHERE ISNULL(PPL.Partnumber, '''') <> '''''; DECLARE  @@Filter VARCHAR(255); EXEC sys.sp_executesql @@sql, N'@@Filter VARCHAR(255) OUT', @@Filter OUT;";
            }

            using (var reader = ExecuteReader(query, true))
            {
                if (reader.Read())
                {
                    fleetCode = reader.GetString(0);
                }
            }
            return fleetCode;
        }
    }
}
