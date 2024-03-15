using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPOPOQTransactionLookup
{
    internal class DealerPOPOQTransactionLookupDAL : BaseDataAccessLayer
    {
        internal List<DealerPOPOQTransactionLookupObject> GetData(string fromDate, string toDate)
        {
            var list = new List<DealerPOPOQTransactionLookupObject>();
            string query = @"select top 2 'POQ' as DocumentType, lk.description as TransactionStatus, lk1.name as TransactionType,'' as [Update], isHistorical as Historical, convert(date,t.createDate) as ReceivedDate,
t.transactionNumber as DocumentNumber, t.transactionDate as DocumentDate, t.transactionNumber as RefPOQ#, t.transactionDate as POQDate, 
t.senderCorcentricCode as DealerCode, ed.legalName as Dealer, t.receiverCorcentricCode as FleetCode, ed1.legalName as Fleet, transactionAmount as Total, 
currencyCode as Currenty, te.description from transaction_tb t
inner join userActionStatus_tb ua on t.transactionId = ua.transactionId and t.createDate between @fromDate and @toDate
inner join (select distinct ua.transactionId, max(ua.userActionStatusId) as id from userActionStatus_tb ua
group by ua.transactionId) a on ua.userActionStatusId = a.id
inner join lookUp_tb lk on ua.statusId = lk.lookUpId and parentLookUpCode = 205 and lookUpCode in (23)
left join lookUp_tb lk1 on t.transactionTypeCode = lk1.description and lk1.parentLookUpCode = 55
left join entityDetails_tb ed on t.senderEntityDetailId = ed.entityDetailId
left join entityDetails_tb ed1 on t.receiverEntityDetailId = ed1.entityDetailId
left join transactionError_tb te on t.transactionId = te.transactionId and te.isActive = 1
left join errorCode_tb ec on te.errorCodeId = ec.errorCodeId
where requestTypeCode = 'Q' and t.isActive = 1 and isHistorical = 0 and validationStatus = 1
and a.transactionId is NOT NULL order by a.id desc";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate),

            };

            using (var reader = ExecuteReader(query, sp, false))
            {

                while (reader.Read())
                {

                    list.Add(new DealerPOPOQTransactionLookupObject
                    {

                        DocumentType = reader.GetStringValue(0),
                        TransactionStatus = reader.GetStringValue(1),
                        TransactionType = reader.GetStringValue(2),
                        Update = reader.GetStringValue(3),
                        HistoricalObject = reader.GetBoolean(4),
                        ReceivedDate = CommonUtils.ConvertDate(reader.GetDateTime(5), "M/dd/yyyy"),
                        DocumentNumber = reader.GetStringValue(6),
                        DocumentDate = CommonUtils.ConvertDate(reader.GetDateTime(7), "M/dd/yyyy"),
                        RefPOQ = reader.GetStringValue(8),
                        POQDate = CommonUtils.ConvertDate(reader.GetDateTime(9), "M/dd/yyyy"),
                        DealerCode = reader.GetStringValue(10),
                        Dealer = reader.GetStringValue(11),
                        FleetCode = reader.GetStringValue(12),
                        Fleet = reader.GetStringValue(13),
                        Total = reader.GetDecimal(14),
                        Currency = reader.GetStringValue(15),
                        description = reader.GetStringValue(16),
                    });
                }
            }


            return list;
        }

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
                    query = @"SELECT  top 1 CONVERT (Date , T.createDate) as FromDate ,Convert (Date , T.createDate) as ToDate  FROM [transaction_tb] T WITH (NOLOCK) LEFT JOIN 
                        [transactionReference_tb] TRD WITH(NOLOCK) ON TRD.transactionId = T.transactionId AND TRD.referenceType = 'PQD' AND TRD.isActive = 1 LEFT JOIN 
                        [transactionReference_tb] TRN WITH(NOLOCK) ON TRN.transactionId = T.transactionId AND TRN.referenceType = 'PQN' AND TRN.isActive = 1 LEFT JOIN 
                        [transactionReference_tb] UPD WITH(NOLOCK) ON UPD.transactionId = T.transactionId AND UPD.referenceType = 'UPD' AND UPD.isActive = 1 WHERE T.isActive = 1 AND 
                        T.requestTypeCode IN('R', 'Q', 'O') AND T.validationStatus IN(1, 2, 3, 4, 5, 6, 7, 8, 11, 12) ORDER BY T.createDate DESC";
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
                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE (entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE 
                        username=@UserName; WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) 
                        WHERE entityDetailId IN (SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId 
                        WHERE U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM 
                        RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT 
                        entityDetailId FROM RootNumber WHERE parentEntityDetailId<> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM 
                        [userRelationships_tb] UR2 WITH(NOLOCK)WHERE UR2.userId = @@UserID AND IsActive = 1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 Convert(Date, T.createDate) 
                        AS FromDate, Convert (Date, T.createDate) AS ToDate FROM [transaction_tb] T WITH(NOLOCK) INNER JOIN @@DealerAccessLocations AE ON AE.entityDetailId = 
                        T.senderEntityDetailId LEFT JOIN [transactionReference_tb] TRD WITH (NOLOCK) ON TRD.transactionId = T.transactionId AND TRD.referenceType = 'PQD' AND 
                        TRD.isActive = 1 LEFT JOIN [transactionReference_tb] TRN WITH(NOLOCK) ON TRN.transactionId = T.transactionId AND TRN.referenceType = 'PQN' AND TRN.isActive = 1 
                        LEFT JOIN [transactionReference_tb] UPD WITH(NOLOCK) ON UPD.transactionId = T.transactionId AND UPD.referenceType = 'UPD' AND UPD.isActive = 1 WHERE T.isActive = 1 
                        AND((T.requestTypeCode = 'Q' AND T.validationStatus IN(1, 2, 3, 4, 8)) OR(T.requestTypeCode IN('R', 'O') AND T.validationStatus in (1, 12)));";
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
        internal int GetCountByDateRange(string dateRange, int days = 0)
        {

            string query = null;
            string fromDateTime = null;

            if (dateRange == "Last 7 days")
            {
                fromDateTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                query = @"Select COUNT(*) FROM  transaction_tb T WITH (NOLOCK) LEFT JOIN transactionReference_tb TRD WITH (NOLOCK) ON TRD.transactionId = T.transactionId AND TRD.referenceType = 'PQD' AND TRD.isActive = 1 
                    LEFT JOIN transactionReference_tb TRN WITH (NOLOCK) ON TRN.transactionId = T.transactionId AND TRN.referenceType = 'PQN' AND TRN.isActive = 1 LEFT JOIN transactionReference_tb UPD WITH (NOLOCK)
                    ON UPD.transactionId = T.transactionId AND UPD.referenceType = 'UPD' AND UPD.isActive = 1 WHERE T.isActive =1 AND T.requestTypeCode IN ('R','Q','O') AND T.validationStatus IN (1,2,3,4,5,6,7,8,11,12) 
                    AND T.createdate BETWEEN @fromDateTime AND GETDATE()";
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
                query = @"Select COUNT(*) FROM  transaction_tb T WITH (NOLOCK) LEFT JOIN transactionReference_tb TRD WITH (NOLOCK) ON TRD.transactionId = T.transactionId AND TRD.referenceType = 'PQD' AND TRD.isActive = 1 
                    LEFT JOIN transactionReference_tb TRN WITH (NOLOCK) ON TRN.transactionId = T.transactionId AND TRN.referenceType = 'PQN' AND TRN.isActive = 1 LEFT JOIN transactionReference_tb UPD WITH (NOLOCK)
                    ON UPD.transactionId = T.transactionId AND UPD.referenceType = 'UPD' AND UPD.isActive = 1 WHERE T.isActive =1 AND T.requestTypeCode IN ('R','Q','O') AND T.validationStatus IN (1,2,3,4,5,6,7,8,11,12) 
                    AND T.createdate BETWEEN @fromDateTime AND GETDATE()";

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
                query = @"Select COUNT(*) FROM  transaction_tb T WITH (NOLOCK) LEFT JOIN transactionReference_tb TRD WITH (NOLOCK) ON TRD.transactionId = T.transactionId AND TRD.referenceType = 'PQD' AND TRD.isActive = 1 
                    LEFT JOIN transactionReference_tb TRN WITH (NOLOCK) ON TRN.transactionId = T.transactionId AND TRN.referenceType = 'PQN' AND TRN.isActive = 1 LEFT JOIN transactionReference_tb UPD WITH (NOLOCK)
                    ON UPD.transactionId = T.transactionId AND UPD.referenceType = 'UPD' AND UPD.isActive = 1 WHERE T.isActive =1 AND T.requestTypeCode IN ('R','Q','O') AND T.validationStatus IN (1,2,3,4,5,6,7,8,11,12) 
                    AND T.createdate BETWEEN @fromDateTime AND GETDATE()";

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

                query = @"Select COUNT(*) FROM  transaction_tb T WITH (NOLOCK) LEFT JOIN transactionReference_tb TRD WITH (NOLOCK) ON TRD.transactionId = T.transactionId AND TRD.referenceType = 'PQD' AND TRD.isActive = 1 
                    LEFT JOIN transactionReference_tb TRN WITH (NOLOCK) ON TRN.transactionId = T.transactionId AND TRN.referenceType = 'PQN' AND TRN.isActive = 1 LEFT JOIN transactionReference_tb UPD WITH (NOLOCK)
                    ON UPD.transactionId = T.transactionId AND UPD.referenceType = 'UPD' AND UPD.isActive = 1 WHERE T.isActive =1 AND T.requestTypeCode IN ('R','Q','O') AND T.validationStatus IN (1,2,3,4,5,6,7,8,11,12) 
                    AND T.createdate BETWEEN @fromDateTime AND GETDATE()";

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

                query = @"Select COUNT(*) FROM  transaction_tb T WITH (NOLOCK) LEFT JOIN transactionReference_tb TRD WITH (NOLOCK) ON TRD.transactionId = T.transactionId AND TRD.referenceType = 'PQD' AND TRD.isActive = 1 
                    LEFT JOIN transactionReference_tb TRN WITH (NOLOCK) ON TRN.transactionId = T.transactionId AND TRN.referenceType = 'PQN' AND TRN.isActive = 1 LEFT JOIN transactionReference_tb UPD WITH (NOLOCK)
                    ON UPD.transactionId = T.transactionId AND UPD.referenceType = 'UPD' AND UPD.isActive = 1 WHERE T.isActive =1 AND T.requestTypeCode IN ('R','Q','O') AND T.validationStatus IN (1,2,3,4,5,6,7,8,11,12) 
                    AND T.createdate BETWEEN @fromDateTime AND @toDateTime";

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
                query = @"Select COUNT(*) FROM  transaction_tb T WITH (NOLOCK) LEFT JOIN transactionReference_tb TRD WITH (NOLOCK) ON TRD.transactionId = T.transactionId AND TRD.referenceType = 'PQD' AND TRD.isActive = 1 
                    LEFT JOIN transactionReference_tb TRN WITH (NOLOCK) ON TRN.transactionId = T.transactionId AND TRN.referenceType = 'PQN' AND TRN.isActive = 1 LEFT JOIN transactionReference_tb UPD WITH (NOLOCK)
                    ON UPD.transactionId = T.transactionId AND UPD.referenceType = 'UPD' AND UPD.isActive = 1 WHERE T.isActive =1 AND T.requestTypeCode IN ('R','Q','O') AND T.validationStatus IN (1,2,3,4,5,6,7,8,11,12) 
                    AND T.createdate BETWEEN @fromDateTime AND GETDATE()";

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
