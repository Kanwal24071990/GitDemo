using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetInvoicePreApprovalReport
{
    internal class FleetInvoicePreApprovalReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
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
                    query = @"SELECT top 1 Convert (Date , t.createDate) as FromDate ,Convert (Date , t.createDate) as ToDate FROM [transaction_tb] t WITH (NOLOCK) LEFT JOIN [invoice_tb] i WITH (NOLOCK) ON t.transactionId = i.transactionId INNER JOIN [entityDetails_tb] es WITH (NOLOCK) ON es.entityDetailId = t.senderEntityDetailId INNER JOIN [entityDetails_tb] er WITH (NOLOCK) ON er.entityDetailId = t.receiverEntityDetailId INNER JOIN [lookUp_tb] l WITH (NOLOCK) ON l.parentLookUpCode = 21 AND l.[description] = t.transactionTypeCode INNER JOIN (SELECT a.transactionId, ROW_NUMBER() OVER(PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM [actionComments_tb] a WITH(NOLOCK) WHERE a.actionTypeId = (select lookupid from [lookup_tb] where parentlookupcode = 348 and lookupcode = 1) AND a.isActive = 1) af ON af.transactionId = t.transactionId And af.RowNum = 1 LEFT JOIN (SELECT c.updateDate, c.actionTypeId, c.comments, c.transactionId, ROW_NUMBER() OVER(PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM [actionComments_tb] c WITH (NOLOCK) WHERE c.actionTypeId = (select lookupid from [lookup_tb] where parentlookupcode = 348 and lookupcode = 2) AND c.isActive = 1) ac ON ac.transactionId = t.transactionId And ac.RowNum = 1 LEFT JOIN [actionComments_tb] acr WITH (NOLOCK)ON acr.transactionId = t.transactionId AND acr.actionTypeId = (select lookupid from [lookup_tb] where parentlookupcode = 348 and lookupcode = 5) AND acr.isActive = 1 INNER JOIN (SELECT ac1.transactionId, ac1.userId, ROW_NUMBER() OVER(PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM [actionComments_tb] ac1 WITH(NOLOCK) INNER JOIN [lookUp_tb] l ON ac1.actionTypeId = l.lookUpId AND l.parentLookUpCode = 348 WHERE ac1.isActive = 1 ) ac2 ON ac2.RowNum = 1 AND ac2.transactionId = t.transactionId INNER JOIN [user_tb] u WITH (NOLOCK) ON ac2.userId = u.userId WHERE t.validationStatus IN (13, 14, 15, 1) AND t.isActive = 1 AND requestTypeCode = 'S' AND isHistorical = 0 ORDER BY t.createDate DESC";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }

                else if (userType == "FLEET")
                {
                    query = @"declare @@Userid as int declare @@FleetAccessLocations table ( entityDetailId INT primary key ) select @@Userid=userid from [user_tb] where username=@UserName; WITH RootNumber AS ( SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId iN( SELECT DISTINCT urs.entityId FROM [userRelationships_tb] as urs WITH(NOLOCK) INNER JOIN [user_tb] WITH(NOLOCK) ON urs.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND urs.IsActive = 1 AND urs.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId and parentEntityDetailId <>0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; SELECT top 1 Convert (Date , t.createDate) as FromDate ,Convert (Date , t.createDate) as ToDate FROM [transaction_tb] t WITH (NOLOCK) LEFT JOIN [invoice_tb] i WITH (NOLOCK) ON t.transactionId = i.transactionId INNER JOIN [entityDetails_tb] es WITH (NOLOCK) ON es.entityDetailId = t.senderEntityDetailId INNER JOIN [entityDetails_tb] er WITH (NOLOCK) ON er.entityDetailId = t.receiverEntityDetailId inner join @@FleetAccessLocations d on d.entitydetailid=t.receiverEntityDetailId INNER JOIN [lookUp_tb] l WITH (NOLOCK) ON l.parentLookUpCode = 21 AND l.[description] = t.transactionTypeCode INNER JOIN ( SELECT a.transactionId, ROW_NUMBER() OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM [actionComments_tb] a WITH (NOLOCK) WHERE a.actionTypeId = (select lookupid from [lookup_tb] where parentlookupcode=348 and lookupcode=1) AND a.isActive = 1 ) af ON af.transactionId = t.transactionId And af.RowNum = 1 LEFT JOIN ( SELECT c.updateDate,c.actionTypeId,c.comments,c.transactionId, ROW_NUMBER() OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM [actionComments_tb] c WITH (NOLOCK) WHERE c.actionTypeId = (select lookupid from [lookup_tb] where parentlookupcode=348 and lookupcode=2) AND c.isActive = 1 ) ac ON ac.transactionId = t.transactionId And ac.RowNum = 1 LEFT JOIN [actionComments_tb] acr WITH (NOLOCK) ON acr.transactionId = t.transactionId AND acr.actionTypeId = (select lookupid from [lookup_tb] where parentlookupcode=348 and lookupcode=5) AND acr.isActive = 1 INNER JOIN ( SELECT ac1.transactionId,ac1.userId, ROW_NUMBER() OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM [actionComments_tb] ac1 WITH (NOLOCK) INNER JOIN [lookUp_tb] l ON ac1.actionTypeId = l.lookUpId AND l.parentLookUpCode = 348 WHERE ac1.isActive = 1 ) ac2 ON ac2.RowNum = 1 AND ac2.transactionId = t.transactionId INNER JOIN [user_tb] u WITH (NOLOCK) ON ac2.userId = u.userId WHERE t.validationStatus IN (13, 14, 15, 1) AND t.isActive = 1 AND requestTypeCode = 'S' AND isHistorical = 0 order by t.createDate desc";
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
                LoggingHelper.LogException(ex.Message);
                FromDate = null;
                ToDate = null;
            }
        }
        internal int GetCountByDateRange(string dateRange, int days = 0)
        {

            string query = null;
            string fromDateTime = null;

            if (dateRange == "Last 7 days")
            {
                fromDateTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                query = @"SELECT count(*)
                FROM transaction_tb t WITH (NOLOCK)
                LEFT JOIN invoice_tb i  WITH (NOLOCK) ON t.transactionId = i.transactionId
                INNER JOIN entityDetails_tb es WITH (NOLOCK) ON  es.entityDetailId = t.senderEntityDetailId
                INNER JOIN entityDetails_tb er WITH (NOLOCK) ON  er.entityDetailId = t.receiverEntityDetailId
                INNER JOIN lookUp_tb l WITH (NOLOCK) ON l.parentLookUpCode = 21 AND l.[description] = t.transactionTypeCode
                INNER JOIN
                (SELECT a.transactionId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb a  WITH (NOLOCK)
                WHERE a.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=1) AND a.isActive = 1 ) 
                af ON af.transactionId = t.transactionId And af.RowNum = 1 LEFT JOIN (SELECT  c.updateDate,c.actionTypeId,c.comments,c.transactionId, ROW_NUMBER() OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb c  WITH (NOLOCK) WHERE c.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=2) AND c.isActive = 1) ac ON ac.transactionId = t.transactionId And ac.RowNum = 1 LEFT JOIN actionComments_tb acr WITH (NOLOCK) ON acr.transactionId = t.transactionId AND acr.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=5) AND acr.isActive = 1 INNER JOIN ( SELECT  ac1.transactionId,ac1.userId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM    actionComments_tb ac1 WITH (NOLOCK)
                INNER JOIN lookUp_tb l ON ac1.actionTypeId = l.lookUpId AND l.parentLookUpCode = 348 WHERE ac1.isActive = 1 ) ac2 ON ac2.RowNum = 1 AND ac2.transactionId = t.transactionId INNER JOIN user_tb u WITH (NOLOCK) ON ac2.userId = u.userId WHERE t.validationStatus IN (13, 14, 15, 1)                         AND t.isActive = 1 AND requestTypeCode = 'S' AND isHistorical = 0 
                AND t.createDate BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }


            }

            else if (dateRange == "Last 14 days")
            {
                fromDateTime = DateTime.Now.AddDays(-13).ToString("yyyy-MM-dd");
                query = @"SELECT count(*)
                FROM transaction_tb t WITH (NOLOCK)
                LEFT JOIN invoice_tb i  WITH (NOLOCK) ON t.transactionId = i.transactionId
                INNER JOIN entityDetails_tb es WITH (NOLOCK) ON  es.entityDetailId = t.senderEntityDetailId
                INNER JOIN entityDetails_tb er WITH (NOLOCK) ON  er.entityDetailId = t.receiverEntityDetailId
                INNER JOIN lookUp_tb l WITH (NOLOCK) ON l.parentLookUpCode = 21 AND l.[description] = t.transactionTypeCode
                INNER JOIN
                (SELECT a.transactionId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb a  WITH (NOLOCK)
                WHERE a.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=1) AND a.isActive = 1 ) 
                af ON af.transactionId = t.transactionId And af.RowNum = 1 LEFT JOIN (SELECT  c.updateDate,c.actionTypeId,c.comments,c.transactionId, ROW_NUMBER() OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb c  WITH (NOLOCK) WHERE c.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=2) AND c.isActive = 1) ac ON ac.transactionId = t.transactionId And ac.RowNum = 1 LEFT JOIN actionComments_tb acr WITH (NOLOCK) ON acr.transactionId = t.transactionId AND acr.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=5) AND acr.isActive = 1 INNER JOIN ( SELECT  ac1.transactionId,ac1.userId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM actionComments_tb ac1 WITH (NOLOCK)
                INNER JOIN lookUp_tb l ON ac1.actionTypeId = l.lookUpId AND l.parentLookUpCode = 348 WHERE ac1.isActive = 1 ) ac2 ON ac2.RowNum = 1 AND ac2.transactionId = t.transactionId INNER JOIN user_tb u WITH (NOLOCK) ON ac2.userId = u.userId WHERE t.validationStatus IN (13, 14, 15, 1)                         AND t.isActive = 1 AND requestTypeCode = 'S' AND isHistorical = 0 
                AND t.createDate BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
            else if (dateRange == "Last 185 days")
            {
                fromDateTime = DateTime.Now.AddDays(-184).ToString("yyyy-MM-dd");
                query = @"SELECT count(*)
                FROM transaction_tb t WITH (NOLOCK)
                LEFT JOIN invoice_tb i  WITH (NOLOCK) ON t.transactionId = i.transactionId
                INNER JOIN entityDetails_tb es WITH (NOLOCK) ON  es.entityDetailId = t.senderEntityDetailId
                INNER JOIN entityDetails_tb er WITH (NOLOCK) ON  er.entityDetailId = t.receiverEntityDetailId
                INNER JOIN lookUp_tb l WITH (NOLOCK) ON l.parentLookUpCode = 21 AND l.[description] = t.transactionTypeCode
                INNER JOIN
                (SELECT a.transactionId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb a  WITH (NOLOCK)
                WHERE a.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=1) AND a.isActive = 1 ) 
                af ON af.transactionId = t.transactionId And af.RowNum = 1 LEFT JOIN (SELECT  c.updateDate,c.actionTypeId,c.comments,c.transactionId, ROW_NUMBER() OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb c  WITH (NOLOCK) WHERE c.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=2) AND c.isActive = 1) ac ON ac.transactionId = t.transactionId And ac.RowNum = 1 LEFT JOIN actionComments_tb acr WITH (NOLOCK) ON acr.transactionId = t.transactionId AND acr.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=5) AND acr.isActive = 1 INNER JOIN ( SELECT  ac1.transactionId,ac1.userId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM    actionComments_tb ac1 WITH (NOLOCK)
                INNER JOIN lookUp_tb l ON ac1.actionTypeId = l.lookUpId AND l.parentLookUpCode = 348 WHERE ac1.isActive = 1 ) ac2 ON ac2.RowNum = 1 AND ac2.transactionId = t.transactionId INNER JOIN user_tb u WITH (NOLOCK) ON ac2.userId = u.userId WHERE t.validationStatus IN (13, 14, 15, 1)                         AND t.isActive = 1 AND requestTypeCode = 'S' AND isHistorical = 0 
                AND t.createDate BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
            else if (dateRange == "Current month")
            {
                var today = DateTime.Now;
                fromDateTime = new DateTime(today.Year, today.Month, 1).ToString("yyyy-MM-dd");

                query = @"SELECT count(*)
                FROM transaction_tb t WITH (NOLOCK)
                LEFT JOIN invoice_tb i  WITH (NOLOCK) ON t.transactionId = i.transactionId
                INNER JOIN entityDetails_tb es WITH (NOLOCK) ON  es.entityDetailId = t.senderEntityDetailId
                INNER JOIN entityDetails_tb er WITH (NOLOCK) ON  er.entityDetailId = t.receiverEntityDetailId
                INNER JOIN lookUp_tb l WITH (NOLOCK) ON l.parentLookUpCode = 21 AND l.[description] = t.transactionTypeCode
                INNER JOIN
                (SELECT a.transactionId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb a  WITH (NOLOCK)
                WHERE a.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=1) AND a.isActive = 1 ) 
                af ON af.transactionId = t.transactionId And af.RowNum = 1 LEFT JOIN (SELECT  c.updateDate,c.actionTypeId,c.comments,c.transactionId, ROW_NUMBER() OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb c  WITH (NOLOCK) WHERE c.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=2) AND c.isActive = 1) ac ON ac.transactionId = t.transactionId And ac.RowNum = 1 LEFT JOIN actionComments_tb acr WITH (NOLOCK) ON acr.transactionId = t.transactionId AND acr.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=5) AND acr.isActive = 1 INNER JOIN ( SELECT  ac1.transactionId,ac1.userId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM    actionComments_tb ac1 WITH (NOLOCK)
                INNER JOIN lookUp_tb l ON ac1.actionTypeId = l.lookUpId AND l.parentLookUpCode = 348 WHERE ac1.isActive = 1 ) ac2 ON ac2.RowNum = 1 AND ac2.transactionId = t.transactionId INNER JOIN user_tb u WITH (NOLOCK) ON ac2.userId = u.userId WHERE t.validationStatus IN (13, 14, 15, 1)                         AND t.isActive = 1 AND requestTypeCode = 'S' AND isHistorical = 0 
                AND t.createDate BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }

            else if (dateRange == "Last month")
            {
                var lastmonth = DateTime.Now.AddMonths(-1);
                fromDateTime = new DateTime(lastmonth.Year, lastmonth.Month, 1).ToString("yyyy-MM-dd");
                var toDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1).ToString("yyyy-MM-dd");

                query = @"SELECT count(*)
                FROM transaction_tb t WITH (NOLOCK)
                LEFT JOIN invoice_tb i  WITH (NOLOCK) ON t.transactionId = i.transactionId
                INNER JOIN entityDetails_tb es WITH (NOLOCK) ON  es.entityDetailId = t.senderEntityDetailId
                INNER JOIN entityDetails_tb er WITH (NOLOCK) ON  er.entityDetailId = t.receiverEntityDetailId
                INNER JOIN lookUp_tb l WITH (NOLOCK) ON l.parentLookUpCode = 21 AND l.[description] = t.transactionTypeCode
                INNER JOIN
                (SELECT a.transactionId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb a  WITH (NOLOCK)
                WHERE a.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=1) AND a.isActive = 1 ) 
                af ON af.transactionId = t.transactionId And af.RowNum = 1 LEFT JOIN (SELECT  c.updateDate,c.actionTypeId,c.comments,c.transactionId, ROW_NUMBER() OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb c  WITH (NOLOCK) WHERE c.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=2) AND c.isActive = 1) ac ON ac.transactionId = t.transactionId And ac.RowNum = 1 LEFT JOIN actionComments_tb acr WITH (NOLOCK) ON acr.transactionId = t.transactionId AND acr.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=5) AND acr.isActive = 1 INNER JOIN ( SELECT  ac1.transactionId,ac1.userId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM    actionComments_tb ac1 WITH (NOLOCK)
                INNER JOIN lookUp_tb l ON ac1.actionTypeId = l.lookUpId AND l.parentLookUpCode = 348 WHERE ac1.isActive = 1 ) ac2 ON ac2.RowNum = 1 AND ac2.transactionId = t.transactionId INNER JOIN user_tb u WITH (NOLOCK) ON ac2.userId = u.userId WHERE t.validationStatus IN (13, 14, 15, 1)                         AND t.isActive = 1 AND requestTypeCode = 'S' AND isHistorical = 0 
                AND t.createDate BETWEEN @fromDateTime AND @toDateTime";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                    new SqlParameter("@toDateTime", toDateTime),

                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }

            else if (dateRange == "Customized date")
            {
                fromDateTime = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
                query = @"SELECT count(*)
                FROM transaction_tb t WITH (NOLOCK)
                LEFT JOIN invoice_tb i  WITH (NOLOCK) ON t.transactionId = i.transactionId
                INNER JOIN entityDetails_tb es WITH (NOLOCK) ON  es.entityDetailId = t.senderEntityDetailId
                INNER JOIN entityDetails_tb er WITH (NOLOCK) ON  er.entityDetailId = t.receiverEntityDetailId
                INNER JOIN lookUp_tb l WITH (NOLOCK) ON l.parentLookUpCode = 21 AND l.[description] = t.transactionTypeCode
                INNER JOIN
                (SELECT a.transactionId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb a  WITH (NOLOCK)
                WHERE a.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=1) AND a.isActive = 1 ) 
                af ON af.transactionId = t.transactionId And af.RowNum = 1 LEFT JOIN (SELECT  c.updateDate,c.actionTypeId,c.comments,c.transactionId, ROW_NUMBER() OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum
                FROM actionComments_tb c  WITH (NOLOCK) WHERE c.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=2) AND c.isActive = 1) ac ON ac.transactionId = t.transactionId And ac.RowNum = 1 LEFT JOIN actionComments_tb acr WITH (NOLOCK) ON acr.transactionId = t.transactionId AND acr.actionTypeId = (select lookupid from lookup_tb where parentlookupcode=348 and lookupcode=5) AND acr.isActive = 1 INNER JOIN ( SELECT  ac1.transactionId,ac1.userId, ROW_NUMBER()
                OVER (PARTITION BY transactionId ORDER BY updateDate DESC) AS RowNum FROM    actionComments_tb ac1 WITH (NOLOCK)
                INNER JOIN lookUp_tb l ON ac1.actionTypeId = l.lookUpId AND l.parentLookUpCode = 348 WHERE ac1.isActive = 1 ) ac2 ON ac2.RowNum = 1 AND ac2.transactionId = t.transactionId INNER JOIN user_tb u WITH (NOLOCK) ON ac2.userId = u.userId WHERE t.validationStatus IN (13, 14, 15, 1)                         AND t.isActive = 1 AND requestTypeCode = 'S' AND isHistorical = 0 
                AND t.createDate BETWEEN @fromDateTime AND GETDATE()";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@fromDateTime", fromDateTime),
                };

                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }

            }

            return -1;
        }

    }
}
