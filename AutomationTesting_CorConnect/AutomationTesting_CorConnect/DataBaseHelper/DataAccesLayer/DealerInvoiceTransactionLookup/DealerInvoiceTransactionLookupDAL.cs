using AutomationTesting_CorConnect.DataObjects;
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

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerInvoiceTransactionLookup
{
    internal class DealerInvoiceTransactionLookupDAL : BaseDataAccessLayer
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
                    query = @"select top 1 Convert (Date , t.createdate) as FromDate ,Convert (Date , t.createdate) as ToDate from transaction_tb t LEFT JOIN transaction_tb TI
                            WITH (NOLOCK) ON T.transactionId = TI.transactionId AND(TI.requestTypeCode = 'T' or TI.requestTypeCode = 'A') AND TI.validationStatus in
                            (3, 4, 7) LEFT JOIN invoice_tb I WITH (NOLOCK)on I.transactionId = T.transactionId where t.isActive = 1 and TI.transactionId IS NULL and
                            t.validationStatus <> 0 AND T.requestTypeCode NOT IN('I', 'O', 'Q', 'R')
                            and(I.invoiceId IS NULL OR I.systemType = 0)order by t.createDate desc;";
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
                    query = @"declare @@Userid as int declare @@DealerAccessLocations table (entityDetailId INT primary key) select @@Userid=userid from user_tb
                            where username=@UserName; WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK)
                            WHERE entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM userRelationships_tb WITH(NOLOCK) INNER JOIN user_tb WITH(NOLOCK) ON
                            userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1)
                            UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON
                            P.entityDetailId = C.parentEntityDetailId) insert into @@DealerAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId
                            and parentEntityDetailId <>0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM userRelationships_tb WITH(NOLOCK) WHERE userRelationships_tb.userId
                            = @@UserID and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; select top 1 Convert (Date , t.createdate) as FromDate ,Convert (Date , t.createdate)
                            as ToDate from transaction_tb t INNER JOIN @@DealerAccessLocations SE ON SE.entityDetailId = T.senderEntityDetailId LEFT JOIN transaction_tb TI WITH (NOLOCK) ON
                            T.transactionId = TI.transactionId AND (TI.requestTypeCode = 'T' or TI.requestTypeCode = 'A') AND TI.validationStatus in (3,4,7) LEFT JOIN invoice_tb I WITH (NOLOCK)
                            on I.transactionId = T.transactionId where t.isActive =1 and TI.transactionId IS NULL and t.validationStatus <> 0 AND T.requestTypeCode NOT IN ('I','O','Q','R') and
                            (I.invoiceId IS NULL OR I.systemType=0) order by t.createDate desc";
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

        internal int GetRowCountTransactionStatus(string transactionStatus)
        {

            string query = null;
            List<string> rowCount = new List<string>();
            string fromDateTime = null;
            List<SqlParameter> sp = new();

            if (transactionStatus == "Authorized")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where T.requestTypeCode in ('T','A') and T.validationStatus in (1,2)
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));
            }
            else if (transactionStatus == "Authorization Expired")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where T.requestTypeCode in ('T','A') and (T.validationStatus = 6)
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));
            }
            else if (transactionStatus == "Authorization Voided")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                
                query = @"select t.transactionNumber from transaction_tb T
                        where T.requestTypeCode in ('T','A') and (T.validationStatus = 5)
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));
            
            }
            else if (transactionStatus == "Awaiting Fleet Release")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where T.requestTypeCode = 'S' AND T.validationStatus = 13
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Awaiting Reseller Release")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select createDate,t.transactionNumber,* from transaction_tb t
                        where T.requestTypeCode = 'S' AND T.validationStatus = 17
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Fleet Release Rejected")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where T.requestTypeCode = 'S' AND T.validationStatus = 14
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Reseller Release Rejected")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where t.requestTypeCode = 'S' AND t.validationStatus = 18
                        and t.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Paid")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0
                        and T.createDate between @fromDateTime and getdate()
                        and appaidinfull=1
                        order by t.createdate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Disputed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.invoiceNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where IV.systemType=0
                        and td.disputeStatus = 1
                        and T.createDate between @fromDateTime and getdate()";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Awaiting Dealer Release")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where T.requestTypeCode = 'S' AND T.validationStatus = 9
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Draft Invoice")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where T.requestTypeCode = 'S' AND T.validationStatus = 8
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid desc";
                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Fatal Discrepancy")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where T.requestTypeCode = 'S' AND T.validationStatus = 3
                        and T.createDate between @fromDateTime and getdate()
                        order by t.transactionid";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Fixable Discrepancy")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where T.requestTypeCode = 'S' AND T.validationStatus = 2
                        --and requestId not in ('InvForwarding')
                        and T.createDate between  @fromDateTime and getdate()
                        order by t.createDate desc";
                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Reviewable Discrepancy")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t 
                        where T.requestTypeCode = 'S' AND T.validationStatus = 4
                        and requestId not in ('InvForwarding')
                        and T.createDate between  @fromDateTime and getdate()
                        order by t.createDate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Admin Error")
            {
                fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                query = @"select t.transactionNumber from transaction_tb t
                        where T.requestTypeCode='S' and (T.validationStatus = 16)
                        and T.createDate between  @fromDateTime and getdate()
                        order by t.createDate desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where
                        t.createDate between @fromDateTime AND GETDATE()    
                        and IV.systemType=0
						and iv.arPaidInFull is null 
						and (td.disputeStatus not in (1)
						or td.transactionDisputeId is null)
						and iv.apPaidInFull is null 
						and iv.apInvoiceDueDate > GETDATE() 
						and t.validationStatus=1
                        order by invoicenumber asc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));
            }

            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    rowCount.Add(reader.GetString(0));
                }
            }

            int count = rowCount.Count();
            return count;
        }
        internal InvoiceObject GetInvoiceInfoFromTransactionStatus()
        {
            InvoiceObject invoiceObject = null;
            string fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

            try
            {
                string spName = @"select top 1 iv.transactionNumber,iv.purchaseOrderNumber,iv.originatingDocumentNumber,iv.invoiceNumber from invoice_tb iv
                                left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                                left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                                INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                                where
                                t.createDate between @fromDateTime AND GETDATE()    
                                and IV.systemType=0
                                and td.transactionDisputeId is  null and iv.arPaidInFull is null and iv.apPaidInFull is null and iv.apInvoiceDueDate > GETDATE() and t.validationStatus=1
                                order by t.createDate asc";

                List<SqlParameter> sp = new()
            {
                new SqlParameter("@fromDateTime", fromDateTime)
            };

                using (var reader = ExecuteReader(spName,sp, false))

                {
                    if (reader.Read())
                    {
                        invoiceObject = new InvoiceObject();
                        invoiceObject.TransactionNumber = reader.GetStringValue("transactionNumber");
                        invoiceObject.PurchaseOrderNumber = reader.GetStringValue("purchaseOrderNumber");
                        invoiceObject.OriginatingDocumentNumber = reader.GetStringValue("originatingDocumentNumber");
                        invoiceObject.InvoiceNumber = reader.GetStringValue("invoiceNumber");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return invoiceObject;
        }

        internal int GetInvoicesCountBySubCommunities(string dealerSubCommunity, string fleetSubCommunity)
        {
            List<string> rowCount = new List<string>();
            string fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

            var query = @"select DISTINCT(iv.invoiceNumber) from invoice_tb iv
                          INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                            INNER JOIN entityDetails_tb ed1 WITH (NOLOCK) ON ed1.entityDetailId = IV.receiverEntityDetailId
                            INNER JOIN  entityDetails_tb ed2 WITH (NOLOCK) ON ed2.entityDetailId = IV.senderEntityDetailId
                            left join subcommunity_tb s1 wITH (NOLOCK) ON ed1.subcommunityid=s1.subcommunityid and   s1.isactive=1
                            left join subcommunity_tb s2 wITH (NOLOCK) ON ed2.subcommunityid=s2.subcommunityid and   s2.isactive=1
                            where IV.systemType=0 and iv.isPaymentPortal IN(0,1) 
                            and iv.invoicedate between @fromDateTime AND GETDATE() 
                            and iv.transactionTypeCode in ('P','R')
                            and t.validationStatus=1 
                            and iv.apInvoiceDueDate > GETDATE()
                            and iv.arPaidInFull is null and iv.apPaidInFull is null and iv.apInvoiceDueDate > GETDATE() and t.validationStatus=1
                            and s1.subCommunityName=@fleetSubcommunity
                            and s2.subCommunityName=@dealerSubcommunity
                            order by invoicenumber asc";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerSubcommunity", dealerSubCommunity),
                new SqlParameter("@fleetSubcommunity", fleetSubCommunity),
                new SqlParameter("@fromDateTime", fromDateTime),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    rowCount.Add(reader.GetString(0));
                }

            }
            int count = rowCount.Count();
            return count;
        }

        internal EntityDetails GetEntityGroup(string user)
        {
            EntityDetails entityDetails = null;
            try
            {
                var query = @"select top 1 g1.name as Fleetgroup,g2.name as Dealergroup, *from invoice_tb iv
                            left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                            INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                            INNER JOIN entityDetails_tb ed1 WITH (NOLOCK) ON ed1.entityDetailId = IV.receiverEntityDetailId
                            INNER JOIN  entityDetails_tb ed2 WITH (NOLOCK) ON ed2.entityDetailId = IV.senderEntityDetailId
                            left join groupAssignment_tb ga1 wITH (NOLOCK) ON ed1.entityDetailId=ga1.entityDetailId and ga1.isactive=1
                            left join group_tb g1 on ga1.groupid=g1.groupid and g1.isactive=1 
                            left join user_tb u1 on u1.userid=g1.userid and u1.username=@fleetuser 
                            inner join lookup_tb l1 on g1.typeid=l1.lookupid
                            left join groupAssignment_tb ga2 wITH (NOLOCK) ON ed2.entityDetailId=ga2.entityDetailId and ga2.isactive=1
                            left join group_tb g2 on ga2.groupid=g2.groupid and g2.isactive=1 
                            left join user_tb u2 on u2.userid=g2.userid and u2.username=@dealeruser
                            inner join lookup_tb l2 on g2.typeid=l2.lookupid 
                            where 
                            IV.systemType=0
                            and iv.transactionTypeCode in ('P','R')
							and iv.arPaidInFull is null and iv.apPaidInFull is null and iv.apInvoiceDueDate > GETDATE() and t.validationStatus=1
                            and (l1.name='super admin list' or l1.name='user company list')
                            and (l2.name='super admin list' or l2.name='user company list')
							and td.disputeStatus = 1
                            order by iv.invoiceid desc";

                List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetuser", user),
                new SqlParameter("@dealeruser", user),

            };

                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        entityDetails = new EntityDetails();
                        entityDetails.FleetGroup = reader.GetStringValue("Fleetgroup");
                        entityDetails.DealerGroup = reader.GetStringValue("Dealergroup");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return entityDetails;
        }

        internal int GetInvoicesCountByGroup(string user, string dealerGroup, string fleetGroup)
        {
            List<string> rowCount = new List<string>();
            string fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

            var query = @"select g1.name as Fleetgroup,g2.name as Dealergroup,* from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        INNER JOIN entityDetails_tb ed1 WITH (NOLOCK) ON ed1.entityDetailId = IV.receiverEntityDetailId
                        INNER JOIN  entityDetails_tb ed2 WITH (NOLOCK) ON ed2.entityDetailId = IV.senderEntityDetailId
                        left join groupAssignment_tb ga1 wITH (NOLOCK) ON ed1.entityDetailId=ga1.entityDetailId and ga1.isactive=1
                        left join group_tb g1 on ga1.groupid=g1.groupid and g1.isactive=1 
                        left join user_tb u1 on u1.userid=g1.userid and u1.username=@fleetUser 
                        inner join lookup_tb l1 on g1.typeid=l1.lookupid
                        left join groupAssignment_tb ga2 wITH (NOLOCK) ON ed2.entityDetailId=ga2.entityDetailId and ga2.isactive=1
                        left join group_tb g2 on ga2.groupid=g2.groupid and g2.isactive=1 
                        left join user_tb u2 on u2.userid=g2.userid and u2.username=@dealerUser
                        inner join lookup_tb l2 on g2.typeid=l2.lookupid 
                        where
                        t.createDate between @fromDateTime AND GETDATE() 
                        and  IV.systemType=0
						and g1.name = @fleetGroup and g2.name = @dealerGroup
                        and iv.arPaidInFull is null and iv.apPaidInFull is null and iv.apInvoiceDueDate > GETDATE() and t.validationStatus=1
                        and td.disputeStatus = 1
                        and (l1.name='super admin list' or l1.name='user company list')
                        and (l2.name='super admin list' or l2.name='user company list')
                        order by iv.invoiceid desc
";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetUser", user),
                new SqlParameter("@dealerUser", user),
                new SqlParameter("@dealerGroup", dealerGroup),
                new SqlParameter("@fleetGroup", fleetGroup),
                new SqlParameter("@fromDateTime", fromDateTime),
            };


            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    rowCount.Add(reader.GetString(0));
                }

            }

            int count = rowCount.Count();
            return count;
        }


        internal int GetCountByDateRange(string dateRange, int days = 0)
        {

            string query = null;
            string fromDateTime = null;

            if (dateRange == "Last 7 days")
            {
                fromDateTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                query = "select count(*) from transaction_tb where requestTypeCode in ('S','T','A') and CAST(createDate as DATE) between @fromDateTime and GETDATE() and isactive=1 and validationStatus not in (0,7,10,11,12,15)";

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
                query = "select count(*) from transaction_tb where requestTypeCode in ('S','T','A') and CAST(createDate as DATE) between @fromDateTime and GETDATE() and isactive=1 and validationStatus not in (0,7,10,11,12,15)";

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
                query = "select count(*) from transaction_tb where requestTypeCode in ('S','T','A') and CAST(createDate as DATE) between @fromDateTime and GETDATE() and isactive=1 and validationStatus not in (0,7,10,11,12,15)";

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

                query = "select count(*) from transaction_tb where requestTypeCode in ('S','T','A') and CAST(createDate as DATE) between @fromDateTime and GETDATE() and isactive=1 and validationStatus not in (0,7,10,11,12,15)";

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

                query = @"select count(*) from transaction_tb where requestTypeCode in ('S','T','A') and CAST(createDate as DATE) between @fromDateTime and @toDateTime and isactive=1 and validationStatus not in (0,7,10,11,12,15)";

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
                query = "select count(*) from transaction_tb where requestTypeCode in ('S','T','A') and CAST(createDate as DATE) between @fromDateTime and GETDATE() and isactive=1 and validationStatus not in (0,7,10,11,12,15)";

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
