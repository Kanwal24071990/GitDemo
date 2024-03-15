using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.Disputes
{
    internal class DisputesDAL : BaseDataAccessLayer
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
                    query = @"SELECT TOP 1 CONVERT (Date , invoicedate) AS FromDate, Convert (Date, invoicedate) AS ToDate FROM invoice_tb IV WITH (NOLOCK) INNER JOIN transactionDisputes_tb TD WITH (NOLOCK) ON IV.[transactionId] = TD.[transactionId] AND TD.[isActive] = 1 AND IV.[isActive] = 1 ORDER BY invoicedate DESC;";

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
                    query = @"DECLARE @@Userid AS INT; DECLARE @@FleetAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM user_tb WHERE
                            username=@UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT U.entityId FROM userRelationships_tb U WITH(NOLOCK) INNER JOIN user_tb US WITH(NOLOCK) ON U.userId = US.userId WHERE US.userId = @@UserID AND U.IsActive = 1 AND U.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@FleetAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT U2.entityId As entityDetailId FROM userRelationships_tb U2 WITH(NOLOCK) WHERE U2.userId = @@UserID AND IsActive =1 AND U2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE,invoicedate) AS FromDate,CONVERT(DATE,invoicedate) AS ToDate FROM invoice_tb IV WITH(NOLOCK) INNER JOIN transactionDisputes_tb TD WITH(NOLOCK) ON IV.transactionId = TD.transactionId AND TD.isActive = 1 LEFT JOIN user_tb U WITH(NOLOCK) ON TD.ownedBy = U.userId LEFT JOIN user_tb U1 WITH(NOLOCK) ON TD.userId = U1.userId LEFT JOIN lookup_tb L ON TD.disputeReason = L.lookupCode AND L.parentLookupCode = 46 WHERE IV.isActive = 1 AND receiverEntityDetailId IN (SELECT entityDetailId FROM @@FleetAccessLocations) AND ISNULL(IV.systemType,0) <> 2;";
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
                else if (userType == "DEALER")
                {
                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM user_tb WHERE username=@UserName; WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT UR.entityId FROM userRelationships_tb UR WITH(NOLOCK) INNER JOIN user_tb U WITH(NOLOCK) ON UR.userId = U.userId WHERE U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM userRelationships_tb UR2 WITH(NOLOCK) WHERE UR2.userId = @@UserID AND IsActive =1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE,invoicedate) AS FromDate,CONVERT (DATE,invoicedate) AS ToDate FROM invoice_tb IV WITH(NOLOCK) INNER JOIN transactionDisputes_tb TD WITH(NOLOCK) ON IV.transactionId = TD.transactionId AND TD.isActive = 1 LEFT JOIN user_tb U WITH(NOLOCK) ON TD.ownedBy = U.userId LEFT JOIN user_tb U1 WITH(NOLOCK) ON TD.userId = U1.userId LEFT JOIN lookup_tb L ON TD.disputeReason = L.lookupCode AND L.parentLookupCode = 46 WHERE IV.isActive = 1 AND senderEntityDetailId IN (SELECT entityDetailId FROM @@DealerAccessLocations) AND ISNULL(IV.systemType,0) <> 2;";
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

        internal void GetDataRowCount(string dateType, out int rowCount)
        {
            string query = null;
            rowCount = 0;
            try
            {
                if (dateType == "Program Invoice Date")
                {
                    query = @"select count(*) FROM  invoice_tb iv
                            inner join transactionDisputes_tb TD   on iv.transactionId = td.transactionId and TD.isActive = 1
                            where (disputeStatus in (1,2,3)) AND iv.isActive = 1
                            and iv.arinvoicedate BETWEEN GETDATE()-90 AND GETDATE()";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);

                        }
                    }
                }
                else if (dateType == "Settlement Date")
                {
                    query = @"select count(*) FROM  invoice_tb iv
                            inner join transactionDisputes_tb TD   on iv.transactionId = td.transactionId and TD.isActive = 1
                            where (disputeStatus in (1,2,3)) AND iv.isActive = 1  
                            and iv.invoicedate BETWEEN GETDATE()-90 AND GETDATE()";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);
                        }
                    }
                }
                else if (dateType == "Dispute Date")
                {
                    query = @"select count(*) FROM  invoice_tb iv
                            inner join transactionDisputes_tb TD   on iv.transactionId = td.transactionId and TD.isActive = 1
                            where (disputeStatus in (1,2,3)) AND iv.isActive = 1  
                            aND TD.dateSent BETWEEN GETDATE()-90 AND GETDATE()";


                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);
                        }
                    }
                }
                else if (dateType == "Follow Up By")
                {
                    query = @"select count(*) FROM  invoice_tb iv
                            inner join transactionDisputes_tb TD   on iv.transactionId = td.transactionId and TD.isActive = 1
                            where (disputeStatus in (1,2,3)) AND iv.isActive = 1  
                            aND TD.followupby BETWEEN GETDATE()-90 AND GETDATE()";


                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);
                        }
                    }
                }
                else if (dateType == "Last Updated")
                {
                    query = @"select  count(*) FROM  invoice_tb iv
                            inner join transactionDisputes_tb TD   on iv.transactionId = td.transactionId and TD.isActive = 1
                            where (disputeStatus in (1,2,3)) AND iv.isActive = 1  
                            aND TD.datesent BETWEEN GETDATE()-90 AND GETDATE()";


                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                rowCount = 0;
            }
        }

        internal void GetDataRowCountForStatus(string status, out int rowCount)
        {
            string query = null;
            rowCount = 0;
            string disputeStatusCondition = "";
            string entityID = "";

            if (status.ToLower().Equals("all"))
            {
                disputeStatusCondition = "(disputeStatus = 1 or disputestatus = 2 or disputeStatus = 3)";
            }

            else if (status.ToLower().Equals("disputed"))
            {
                disputeStatusCondition = "disputeStatus = 1";
            }

            else if (status.ToLower().Equals("resolved"))
            {
                disputeStatusCondition = "disputeStatus = 2";
            }

            else if (status.ToLower().Equals("closed"))
            {
                disputeStatusCondition = "disputeStatus = 3";
            }

            try
            {
                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                string userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
                if (userType == "ADMIN")
                {
                    query = @"select Count(*) FROM invoice_tb iv inner join transactionDisputes_tb TD on iv.transactionId = td.transactionId and TD.isActive = 1 where " + disputeStatusCondition + " And iv.isActive = 1 anD TD.dateSent BETWEEN GETDATE()-90 AND GETDATE()";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);

                        }
                    }
                }
                else
                {
                    if (userType == "DEALER")
                    {
                        entityID = "senderbilltoentitydetailid";
                    }
                    else if (userType == "FLEET")
                    {
                        entityID = "receiverbilltoentitydetailid";
                    }

                    query = @"select Count(*) FROM invoice_tb iv inner join transactionDisputes_tb TD on iv.transactionId = td.transactionId inner join userRelationships_tb ur on ur.entityid = iv." + entityID + " inner join user_tb u on u.userid = ur.userid and TD.isActive = 1 where " + disputeStatusCondition + " And iv.isActive = 1 and TD.dateSent BETWEEN GETDATE()-90 AND GETDATE() and " + string.Format("u.userName = '{0}'", userName);

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                query = null;
                rowCount = 0;
            }
        }

        internal void GetDataRowCountForEntity(string status, string[] billToEntityDetailId, bool isDealer, bool isFleet, bool isDealerAndFleet, out int rowCount)
        {
            string query = null;
            rowCount = 0;
            string disputeStatusCondition = string.Empty;
            string entityID = string.Empty;
            string entityCondition = string.Empty;

            try
            {

                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                string userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();

                if (status.ToLower().Equals("all"))
                {
                    disputeStatusCondition = "(disputeStatus = 1 or disputestatus = 2 or disputeStatus = 3)";
                }

                else if (status.ToLower().Equals("disputed"))
                {
                    disputeStatusCondition = "disputeStatus = 1";
                }

                else if (status.ToLower().Equals("resolved"))
                {
                    disputeStatusCondition = "disputeStatus = 2";
                }

                else if (status.ToLower().Equals("closed"))
                {
                    disputeStatusCondition = "disputeStatus = 3";
                }

                if (userType.ToLower().Equals("dealer"))
                {
                    entityID = "senderbilltoentitydetailid";
                }

                else if (userType.ToLower().Equals("fleet"))
                {
                    entityID = "receiverbilltoentitydetailid";
                }

                if (isDealer)
                {
                    entityCondition = string.Format("iv.senderbilltoentitydetailid = {0}", billToEntityDetailId[0]);
                }
                if (isFleet)
                {

                    entityCondition = string.Format("iv.receiverbilltoentitydetailid = {0}", billToEntityDetailId[0]);
                }

                if (isDealerAndFleet)
                {
                    entityCondition = string.Format("iv.senderbilltoentitydetailid = {0} And iv.receiverbilltoentitydetailid = {1}", billToEntityDetailId[0], billToEntityDetailId[1]);
                }

                if (userType.ToLower().Equals("admin"))
                {
                    query = @"select Count(*) FROM invoice_tb iv inner join transactionDisputes_tb TD on iv.transactionId = td.transactionId and TD.isActive = 1 where " + disputeStatusCondition + " AND iv.isActive = 1 AND " + entityCondition + "  AND TD.dateSent BETWEEN GETDATE()-90 AND GETDATE()";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);

                        }
                    }
                }

                else
                {
                    query = @"select distinct Count(*) FROM invoice_tb iv inner join transactionDisputes_tb TD on iv.transactionId = td.transactionId inner join userRelationships_tb ur on ur.entityid = iv." + entityID + " inner join user_tb u on u.userid = ur.userid and TD.isActive = 1 where " + disputeStatusCondition + " And iv.isActive = 1 AND " + entityCondition + " and TD.dateSent BETWEEN GETDATE()-90 AND GETDATE() and " + string.Format("u.userName = '{0}'", userName);

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            rowCount = reader.GetInt32(0);

                        }
                    }
                }

            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                query = null;
                rowCount = 0;
            }
        }

        internal void GetBillToEntityDetailId(string entityType, string invoiceNumber, out string entityDetailId)
        {
            entityDetailId = string.Empty;
            string query = null;
            string targetedColumn = string.Empty;
            try
            {
                if (entityType.ToLower().Equals("dealer"))
                {
                    targetedColumn = "senderBillToEntityDetailId";
                }

                else if (entityType.ToLower().Equals("fleet"))
                {
                    targetedColumn = "receiverBillToEntityDetailId";
                }

                query = @"select distinct " + targetedColumn + " from invoice_tb where " + string.Format("invoiceNumber='{0}'", invoiceNumber);
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        entityDetailId = reader.GetInt32(0).ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                query = null;
            }
        }

        internal void GetGridRowCount(string status, string fromDate, string toDate, out int rowCount)
        {
            string query = null;
            rowCount = 0;
            string disputeStatusCondition = "";
            string entityID = "";
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter("@fromDate", CommonUtils.ConvertDateDBFormat(fromDate)),
                new SqlParameter("@toDate", CommonUtils.ConvertDateDBFormat(toDate))
            };

            if (status.ToLower().Equals("all"))
            {
                disputeStatusCondition = "(disputeStatus = 1 or disputestatus = 2 or disputeStatus = 3)";
            }

            else if (status.ToLower().Equals("disputed"))
            {
                disputeStatusCondition = "disputeStatus = 1";
            }

            else if (status.ToLower().Equals("resolved"))
            {
                disputeStatusCondition = "disputeStatus = 2";
            }

            else if (status.ToLower().Equals("closed"))
            {
                disputeStatusCondition = "disputeStatus = 3";
            }

            try
            {
                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                string userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
                if (userType == "ADMIN")
                {
                    query = @"select Count(*) FROM invoice_tb iv inner join transactionDisputes_tb TD on iv.transactionId = td.transactionId and TD.isActive = 1 where " + disputeStatusCondition + " And iv.isActive = 1 anD iv.invoiceDate BETWEEN @fromDate and @toDate";
                }
                else
                {
                    if (userType == "DEALER")
                    {
                        entityID = "senderbilltoentitydetailid";
                    }
                    else if (userType == "FLEET")
                    {
                        entityID = "receiverbilltoentitydetailid";
                    }
                    query = @"select Count(*) FROM invoice_tb iv inner join transactionDisputes_tb TD on iv.transactionId = td.transactionId inner join userRelationships_tb ur on ur.entityid = iv." + entityID + " " +
                        "inner join user_tb u on u.userid = ur.userid and TD.isActive = 1 where " + disputeStatusCondition + " And iv.isActive = 1 and iv.invoiceDate BETWEEN @fromDate and @toDate and @userName";
                    sp.Add(new SqlParameter("@userName", userName));
                }

                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        rowCount = reader.GetInt32(0);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                query = null;
                rowCount = 0;
            }
        }

        internal string GetDisputeDealerInvoiceNumber(string status, string resolutionDetail, string from, string to)
        {
            string query = null;
            string dealerInvoiceNumber = string.Empty;
            string disputeStatusCondition = string.Empty;
            if (status.ToLower().Equals("all"))
            {
                disputeStatusCondition = "(disputeStatus = 1 or disputestatus = 2 or disputeStatus = 3)";
            }

            else if (status.ToLower().Equals("disputed"))
            {
                disputeStatusCondition = "disputeStatus = 1";
            }

            else if (status.ToLower().Equals("resolved"))
            {
                disputeStatusCondition = "disputeStatus = 2";
            }

            else if (status.ToLower().Equals("closed"))
            {
                disputeStatusCondition = "disputeStatus = 3";
            }

            try
            {
                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                string userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();

                if (userType.Equals("ADMIN"))
                {
                    query = @"select top 1 i.transactionnumber, p.resolutiondetailid,lu.name as resolutiondetailname,t.disputestatus,l.name as disputestatusname 
                            from transactionDisputes_tb t
                            inner join invoice_tb i on t.transactionid=i.transactionid
                            inner join transactionDisputesProcessing_tb p on t.transactionDisputeId=p.transactionDisputeId and p.isActive = 1
                            inner join lookup_tb l on l.lookupcode=t.disputestatus and l.parentlookupcode=45
                            inner join lookup_tb lu on lu.lookupid=p.resolutiondetailid
                            where lu.name=" + string.Format("'{0}'", resolutionDetail) + " and t." + disputeStatusCondition +
                            " and i.invoicedate between " + string.Format("'{0}'", from) + " and " + string.Format("'{0}'", to);
                }

                else
                {
                    if (userType.Equals("DEALER"))
                    {
                        query = @"select i.transactionnumber, p.resolutiondetailid,lu.name as resolutiondetailname,t.disputestatus,l.name as disputestatusname 
                                from transactionDisputes_tb t
                                inner join invoice_tb i on t.transactionid=i.transactionid
                                inner join transactionDisputesProcessing_tb p on t.transactionDisputeId=p.transactionDisputeId and p.isActive = 1
                                inner join lookup_tb l on l.lookupcode=t.disputestatus and l.parentlookupcode=45
                                inner join lookup_tb lu on lu.lookupid=p.resolutiondetailid
                                inner JOIN entityDetails_tb sen  on sen.entityDetailId = senderEntityDetailId and  sen.isActive=1 AND sen.isTerminated=0 
                                inner join userrelationships_tb ur1 on ur1.entityid=i.senderEntityDetailId
                                inner join userrelationships_tb ur2 on ur2.entityid=i.senderBillToEntityDetailId
                                inner join user_tb u on u.userid=ur1.userid
                                where u.username=" + string.Format("'{0}'", userName) +
                                " and lu.name= " + string.Format("'{0}'", resolutionDetail) + " and t." + disputeStatusCondition +
                                " and i.invoicedate between " + string.Format("'{0}'", from) + " and " + string.Format("'{0}'", to) +
                                " order by invoiceid desc";
                    }
                    else if (userType.Equals("FLEET"))
                    {
                        query = @"select i.transactionnumber, p.resolutiondetailid,lu.name as resolutiondetailname,t.disputestatus,l.name as disputestatusname 
                                from transactionDisputes_tb t
                                inner join invoice_tb i on t.transactionid=i.transactionid
                                inner join transactionDisputesProcessing_tb p on t.transactionDisputeId=p.transactionDisputeId and p.isActive = 1
                                inner join lookup_tb l on l.lookupcode=t.disputestatus and l.parentlookupcode=45
                                inner join lookup_tb lu on lu.lookupid=p.resolutiondetailid
                                inner JOIN entityDetails_tb sen  on sen.entityDetailId = receiverEntityDetailId and  sen.isActive=1 AND sen.isTerminated=0 
                                inner join userrelationships_tb ur1 on ur1.entityid=i.receiverEntityDetailId
                                inner join userrelationships_tb ur2 on ur2.entityid=i.receiverBillToEntityDetailId
                                inner join user_tb u on u.userid=ur1.userid
                                where u.username=" + string.Format("'{0}'", userName) +
                                " and lu.name= " + string.Format("'{0}'", resolutionDetail) + " and t." + disputeStatusCondition +
                                " and i.invoicedate between " + string.Format("'{0}'", from) + " and " + string.Format("'{0}'", to) +
                                " order by invoiceid desc";
                    }
                }

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        dealerInvoiceNumber = reader.GetString(0);

                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                query = null;
            }

            return dealerInvoiceNumber;
        }

        internal int GetDBRowCountForCurrencySearch(string currency, string fromDate, string toDate)
        {
            string query = null;

            try
            {
                query = @"select COUNT(*)  FROM  invoice_tb iv  
                    inner join transactionDisputes_tb TD on iv.transactionId = td.transactionId and TD.isActive = 1 
                    where (disputeStatus in (1,2,3)) and iv.currencycode = @currency AND iv.isActive = 1 AND TD.dateSent BETWEEN @fromDate AND @toDate";

                List<SqlParameter> sp = new()
                {
                new SqlParameter("@currency", currency) ,
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate),
                };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);

                    }
                }
                return 0;
            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                query = null;
                return 0;
            }
        }

        internal int GetDBRowCountCurrencySearchForEntity(string currency, string[] billToEntityDetailId, string fromDate, string toDate, bool isDealerDropDown, bool isFleetDropDown, bool isDealerAndFleetDropDown)
        {
            string query = null;
            string entityCondition = string.Empty;

            try
            {
                if (isDealerDropDown)
                {
                    entityCondition = string.Format("iv.senderEntityDetailId = {0}", billToEntityDetailId[0]);
                }
                if (isFleetDropDown)
                {

                    entityCondition = string.Format("iv.receiverEntityDetailId = {0}", billToEntityDetailId[0]);
                }

                if (isDealerAndFleetDropDown)
                {
                    entityCondition = string.Format("iv.senderEntityDetailId = {0} And iv.receiverEntityDetailId = {1}", billToEntityDetailId[0], billToEntityDetailId[1]);
                }

                query = @"select COUNT(*)  FROM  invoice_tb iv  
                        inner join transactionDisputes_tb TD on iv.transactionId = td.transactionId and TD.isActive = 1 
                        where (disputeStatus in (1,2,3)) and iv.currencycode = @currency AND " + entityCondition + " AND iv.isActive = 1 AND TD.dateSent BETWEEN @fromDate AND @toDate";


                List<SqlParameter> sp = new()
                {
                new SqlParameter("@currency", currency) ,
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate),
                };

                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);

                    }
                }
                return 0;
            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                query = null;
                return 0;
            }
        }

        internal string GetDisputedInvoiceNumber()
        {
            string query = null;
            try
            {
                string userType;

                if (applicationContext.ApplicationContext.GetInstance().UserData != null)
                {
                    userType = applicationContext.ApplicationContext.GetInstance().UserData.Type.NameUpperCase;
                }
                else
                {
                    userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                }
                if (userType == "ADMIN")
                {
                    query = @"select top  1 i.invoicenumber from transactionDisputes_tb td
inner join invoice_tb i on td.transactionid=i.transactionid
inner join transactionDisputesProcessing_tb p on td.transactionDisputeId=p.transactionDisputeId and p.isActive = 1
inner join lookup_tb l on l.lookupcode=td.disputestatus and l.parentlookupcode=45
where td.disputeStatus=1 order by i.invoicedate desc";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            return
                                reader.GetString(0);
                        }
                    }
                }
                else if (userType == "FLEET")
                {
                    query = @"DECLARE @@Userid AS INT; DECLARE @@FleetAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid = userid FROM user_tb WHERE
                            username = @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT U.entityId FROM userRelationships_tb U WITH(NOLOCK) INNER JOIN user_tb US WITH(NOLOCK) ON U.userId = US.userId WHERE US.userId = @@UserID AND U.IsActive = 1 AND U.hasHierarchyAccess = 1)
                            UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@FleetAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId<> entityDetailId AND parentEntityDetailId<> 0
                            UNION SELECT U2.entityId As entityDetailId FROM userRelationships_tb U2 WITH(NOLOCK) WHERE U2.userId = @@UserID AND IsActive = 1 AND U2.entityId IS NOT NULL;  select top  1 i.invoicenumber from transactionDisputes_tb td
inner join invoice_tb i on td.transactionid = i.transactionid
inner join transactionDisputesProcessing_tb p on td.transactionDisputeId = p.transactionDisputeId and p.isActive = 1
LEFT JOIN user_tb U WITH(NOLOCK) ON TD.ownedBy = U.userId
LEFT JOIN user_tb U1 WITH(NOLOCK) ON TD.userId = U1.userId
inner join lookup_tb l on l.lookupcode = td.disputestatus and l.parentlookupcode = 45
WHERE i.isActive = 1 and td.disputeStatus=1 AND receiverEntityDetailId IN(SELECT entityDetailId FROM @@FleetAccessLocations) AND ISNULL(i.systemType,0) <> 2 ORDER BY i.invoicedate DESC";

                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", applicationContext.ApplicationContext.GetInstance().UserData.User)
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            return
                                reader.GetString(0);
                        }
                    }
                }

                else if (userType == "DEALER")
                {
                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid = userid FROM user_tb WHERE
                            username = @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT U.entityId FROM userRelationships_tb U WITH(NOLOCK) INNER JOIN user_tb US WITH(NOLOCK) ON U.userId = US.userId WHERE US.userId = @@UserID AND U.IsActive = 1 AND U.hasHierarchyAccess = 1)
                            UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId<> entityDetailId AND parentEntityDetailId<> 0
                            UNION SELECT U2.entityId As entityDetailId FROM userRelationships_tb U2 WITH(NOLOCK) WHERE U2.userId = @@UserID AND IsActive = 1 AND U2.entityId IS NOT NULL; 
select top  1 i.invoicenumber from transactionDisputes_tb td
inner join invoice_tb i on td.transactionid = i.transactionid
inner join transactionDisputesProcessing_tb p on td.transactionDisputeId = p.transactionDisputeId and p.isActive = 1
LEFT JOIN user_tb U WITH(NOLOCK) ON TD.ownedBy = U.userId
LEFT JOIN user_tb U1 WITH(NOLOCK) ON TD.userId = U1.userId
inner join lookup_tb l on l.lookupcode = td.disputestatus and l.parentlookupcode = 45
WHERE i.isActive = 1 and td.disputeStatus=1 AND senderEntityDetailId IN(SELECT entityDetailId FROM @@DealerAccessLocations) AND ISNULL(i.systemType,0) <> 2 ORDER BY i.invoicedate DESC";

                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", applicationContext.ApplicationContext.GetInstance().UserData.User)
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            return
                                reader.GetString(0);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;

            }
            return null;
        }
     
        internal string GetDisputeResolvedInvoiceNumber()
        {
            string query = null;
            try
            {
                string userType;

                if (applicationContext.ApplicationContext.GetInstance().UserData != null)
                {
                    userType = applicationContext.ApplicationContext.GetInstance().UserData.Type.NameUpperCase;
                }
                else
                {
                    userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                }
                if (userType == "ADMIN")
                {
                    query = @"select top  1 i.invoicenumber from transactionDisputes_tb td
                    inner join invoice_tb i on td.transactionid=i.transactionid
                    inner join transactionDisputesProcessing_tb p on td.transactionDisputeId=p.transactionDisputeId and p.isActive = 1
                    inner join lookup_tb l on l.lookupcode=td.disputestatus and l.parentlookupcode=45
                    where td.disputeStatus=2 order by td.datesent desc";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            return
                                reader.GetString(0);
                        }
                    }
                }
                else if (userType == "FLEET")
                {

                    query = @"DECLARE @@Userid AS INT; DECLARE @@FleetAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid = userid FROM user_tb WHERE
                            username = @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT U.entityId FROM userRelationships_tb U WITH(NOLOCK) INNER JOIN user_tb US WITH(NOLOCK) ON U.userId = US.userId WHERE US.userId = @@UserID AND U.IsActive = 1 AND U.hasHierarchyAccess = 1)
                            UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@FleetAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId<> entityDetailId AND parentEntityDetailId<> 0
                            UNION SELECT U2.entityId As entityDetailId FROM userRelationships_tb U2 WITH(NOLOCK) WHERE U2.userId = @@UserID AND IsActive = 1 AND U2.entityId IS NOT NULL;  
                            select top  1 i.invoicenumber from transactionDisputes_tb td
                            inner join invoice_tb i on td.transactionid=i.transactionid
                            inner join transactionDisputesProcessing_tb p on td.transactionDisputeId=p.transactionDisputeId and p.isActive = 1
                            LEFT JOIN user_tb U WITH(NOLOCK) ON TD.ownedBy = U.userId
                            LEFT JOIN user_tb U1 WITH(NOLOCK) ON TD.userId = U1.userId
                            inner join lookup_tb l on l.lookupcode=td.disputestatus and l.parentlookupcode=45
                            where i.isActive = 1 and td.disputeStatus=2  AND receiverEntityDetailId IN(SELECT entityDetailId FROM @@FleetAccessLocations) AND ISNULL(i.systemType,0) <> 2
                            order by td.datesent desc";
                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", applicationContext.ApplicationContext.GetInstance().UserData.User)
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            return
                                reader.GetString(0);
                        }
                    }
                }
                else if (userType == "DEALER")
                {

                    query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid = userid FROM user_tb WHERE
                            username = @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT U.entityId FROM userRelationships_tb U WITH(NOLOCK) INNER JOIN user_tb US WITH(NOLOCK) ON U.userId = US.userId WHERE US.userId = @@UserID AND U.IsActive = 1 AND U.hasHierarchyAccess = 1)
                            UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId<> entityDetailId AND parentEntityDetailId<> 0
                            UNION SELECT U2.entityId As entityDetailId FROM userRelationships_tb U2 WITH(NOLOCK) WHERE U2.userId = @@UserID AND IsActive = 1 AND U2.entityId IS NOT NULL; 
                            select top  1 i.invoicenumber from transactionDisputes_tb td
                            inner join invoice_tb i on td.transactionid=i.transactionid
                            inner join transactionDisputesProcessing_tb p on td.transactionDisputeId=p.transactionDisputeId and p.isActive = 1
                            LEFT JOIN user_tb U WITH(NOLOCK) ON TD.ownedBy = U.userId
                            LEFT JOIN user_tb U1 WITH(NOLOCK) ON TD.userId = U1.userId
                            inner join lookup_tb l on l.lookupcode=td.disputestatus and l.parentlookupcode=45
                            where i.isActive = 1 and td.disputeStatus=2  AND senderEntityDetailId IN(SELECT entityDetailId FROM @@DealerAccessLocations) AND ISNULL(i.systemType,0) <> 2
                            order by td.datesent desc";
                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", applicationContext.ApplicationContext.GetInstance().UserData.User)
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            return
                                reader.GetString(0);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;

            }
            return null;
        }



        internal int GetCountByDateRange(string dateRange, int days = 0)
        {

            string query = null;
            string fromDateTime = null;

            if (dateRange == "Last 7 days")
            {
                fromDateTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                query = @"Select COUNT(*) FROM invoice_tb iv with (nolock) inner join transactionDisputes_tb TD with (nolock) on iv.transactionId = td.transactionId and TD.isActive in( 1)
                where iv.isActive = 1 and (systemType) <> 2 AND TD.dateSent BETWEEN @fromDateTime and GETDATE()";

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
                query = @"Select COUNT(*) FROM invoice_tb iv with (nolock) inner join transactionDisputes_tb TD with (nolock) on iv.transactionId = td.transactionId and TD.isActive in( 1)
                where iv.isActive = 1 and (systemType) <> 2 AND TD.dateSent BETWEEN @fromDateTime and GETDATE()";

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
                query = @"Select COUNT(*) FROM invoice_tb iv with (nolock) inner join transactionDisputes_tb TD with (nolock) on iv.transactionId = td.transactionId and TD.isActive in( 1)
                where iv.isActive = 1 and (systemType) <> 2 AND TD.dateSent BETWEEN @fromDateTime and GETDATE()";

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

                query = @"Select COUNT(*) FROM invoice_tb iv with (nolock) inner join transactionDisputes_tb TD with (nolock) on iv.transactionId = td.transactionId and TD.isActive in( 1)
                where iv.isActive = 1 and (systemType) <> 2 AND TD.dateSent BETWEEN @fromDateTime and GETDATE()";

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

                query = @"Select COUNT(*) FROM invoice_tb iv with (nolock) inner join transactionDisputes_tb TD with (nolock) on iv.transactionId = td.transactionId and TD.isActive in( 1)
                where iv.isActive = 1 and (systemType) <> 2 AND TD.dateSent BETWEEN @fromDateTime and @toDateTime";

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
                query = @"Select COUNT(*) FROM invoice_tb iv with (nolock) inner join transactionDisputes_tb TD with (nolock) on iv.transactionId = td.transactionId and TD.isActive in( 1)
                where iv.isActive = 1 and (systemType) <> 2 AND TD.dateSent BETWEEN @fromDateTime and GETDATE()";

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



        internal void GetDataByProgramInvNumber(string programInvNumber , out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;
            try
            {
                    query = @"SELECT TOP 1 CONVERT (Date , invoicedate) AS FromDate, Convert (Date, invoicedate) AS ToDate FROM invoice_tb IV where IV.invoiceNumber= @programInvNumber ";

                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@programInvNumber", programInvNumber)
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
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                FromDate = null;
                ToDate = null;
            }
        }

    }
}
