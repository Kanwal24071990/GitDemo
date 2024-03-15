using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetInvoices
{
    internal class FleetInvoicesDAL : BaseDataAccessLayer
    {
        internal List<FleetInvoiceObject> GetFleetInvoices()
        {
            List<FleetInvoiceObject> fleetInvoicesObjects = new List<FleetInvoiceObject>();
            string query = @"select distinct inv.InvoiceNumber, inv.ARCurrencyCode, inv.invoiceDate 
				from invoice_tb inv 
				where inv.receiverEntityDetailId in 
					( 
				--Direct relationships
					select entityId from 
					userRelationships_tb ur1 with (NOLOCK) 
					inner join user_tb usr1 with (NOLOCK) on ur1.userId = usr1.userId 
					inner join entityDetails_tb ed1 on ur1.entityId = ed1.entityDetailId and ed1.locationTypeId in (25,27)
					where usr1.userName = N'vmallaflt' and ur1.hasHierarchyAccess = 0
					)
					and inv.invoiceDate > GETDATE()-180
				union
				select distinct inv.InvoiceNumber, inv.ARCurrencyCode, inv.invoiceDate 
				from invoice_tb inv 
				where inv.receiverEntityDetailId in 
					( 
				--Shop with Hierarchy relationship (To pull child shop entityids)
					select entityDetailId from entityDetails_tb where parentEntityDetailId in 
						(select entityId from 
						userRelationships_tb ur2 with (NOLOCK) 
						inner join user_tb usr2 with (NOLOCK) on ur2.userId = usr2.userId 
						inner join entityDetails_tb ed2 on ur2.entityId = ed2.entityDetailId and ed2.locationTypeId in (27)
						where usr2.userName = N'vmallaflt' and ur2.hasHierarchyAccess = 1) and isNull(parentEntityDetailId,0) = 0
					)
					and inv.invoiceDate > GETDATE()-180
				union
				select distinct inv.InvoiceNumber, inv.ARCurrencyCode, inv.invoiceDate 
				from invoice_tb inv 
				inner join 
					(
					--Billing with Hierarchy relationship (Treat it as billto location)
						select entityId from 
						userRelationships_tb ur2 with (NOLOCK) 
						inner join user_tb usr2 with (NOLOCK) on ur2.userId = usr2.userId 
						inner join entityDetails_tb ed2 on ur2.entityId = ed2.entityDetailId and ed2.locationTypeId in (25)
						where usr2.userName = N'vmallaflt' and ur2.hasHierarchyAccess = 1
						union All
					--Master with Hierarchy relationship (Treat it as billto location)
						select entityDetailId from entityDetails_tb where parentEntityDetailId in 
						(select entityId from 
						userRelationships_tb ur2 with (NOLOCK) 
						inner join user_tb usr2 with (NOLOCK) on ur2.userId = usr2.userId 
						inner join entityDetails_tb ed2 on ur2.entityId = ed2.entityDetailId and ed2.locationTypeId in (24)
						where usr2.userName = N'vmallaflt' and ur2.hasHierarchyAccess = 1) and locationTypeId = 25
					) as a
					on inv.receiverBillToEntityDetailId = a.entityId 
					and inv.invoiceDate > GETDATE()-180 order by 1";

            using (var reader = ExecuteReader(query, false))
            {
                while (reader.Read())
                {
                    fleetInvoicesObjects.Add(new FleetInvoiceObject
                    {
                        InvoiceNumber = reader.GetStringValue(0).Trim(),
                        ARCurrencyCode = reader.GetStringValue(1).Trim(),
                        InvoiceDate = CommonUtils.ConvertDate(reader.GetDateTime(2), "M/dd/yyyy"),
                        IsOnline = false
                    });
                }
            }
            return fleetInvoicesObjects;
        }

        internal List<FleetInvoiceObject> GetFleetOnlineInvoices()
        {
            List<FleetInvoiceObject> fleetInvoicesObjects = new List<FleetInvoiceObject>();
            string query = @"select distinct inv.InvoiceNumber, inv.ARCurrencyCode, inv.invoiceDate 
                from invoice_tb inv 
                where inv.receiverEntityDetailId in 
	                ( 
                --Direct relationships
	                select entityId from 
	                userRelationships_tb ur1 with (NOLOCK) 
	                inner join user_tb usr1 with (NOLOCK) on ur1.userId = usr1.userId 
	                inner join entityDetails_tb ed1 on ur1.entityId = ed1.entityDetailId and ed1.locationTypeId in (25,27)
	                where usr1.userName = N'vmallaflt' and ur1.hasHierarchyAccess = 0
	                ) and inv.isPaymentPortal = 1 --uncomment for only online invoices
	                and inv.invoiceDate > GETDATE()-180
                union
                select distinct inv.InvoiceNumber, inv.ARCurrencyCode, inv.invoiceDate 
                from invoice_tb inv 
                where inv.receiverEntityDetailId in 
	                ( 
                --Shop with Hierarchy relationship (To pull child shop entityids)
	                select entityDetailId from entityDetails_tb where parentEntityDetailId in 
		                (select entityId from 
		                userRelationships_tb ur2 with (NOLOCK) 
		                inner join user_tb usr2 with (NOLOCK) on ur2.userId = usr2.userId 
		                inner join entityDetails_tb ed2 on ur2.entityId = ed2.entityDetailId and ed2.locationTypeId in (27)
		                where usr2.userName = N'vmallaflt' and ur2.hasHierarchyAccess = 1) and isNull(parentEntityDetailId,0) = 0
	                ) and inv.isPaymentPortal = 1 --uncomment for only online invoices
	                and inv.invoiceDate > GETDATE()-180
                union
                select distinct inv.InvoiceNumber, inv.ARCurrencyCode, inv.invoiceDate 
                from invoice_tb inv 
                inner join 
	                (
	                --Billing with Hierarchy relationship (Treat it as billto location)
		                select entityId from 
		                userRelationships_tb ur2 with (NOLOCK) 
		                inner join user_tb usr2 with (NOLOCK) on ur2.userId = usr2.userId 
		                inner join entityDetails_tb ed2 on ur2.entityId = ed2.entityDetailId and ed2.locationTypeId in (25)
		                where usr2.userName = N'vmallaflt' and ur2.hasHierarchyAccess = 1
		                union All
	                --Master with Hierarchy relationship (Treat it as billto location)
		                select entityDetailId from entityDetails_tb where parentEntityDetailId in 
		                (select entityId from 
		                userRelationships_tb ur2 with (NOLOCK) 
		                inner join user_tb usr2 with (NOLOCK) on ur2.userId = usr2.userId 
		                inner join entityDetails_tb ed2 on ur2.entityId = ed2.entityDetailId and ed2.locationTypeId in (24)
		                where usr2.userName = N'vmallaflt' and ur2.hasHierarchyAccess = 1) and locationTypeId = 25
	                ) as a
	                on inv.receiverBillToEntityDetailId = a.entityId 
	                and inv.isPaymentPortal = 1 --uncomment for only online invoices
	                and inv.invoiceDate > GETDATE()-180 order by 1";

            using (var reader = ExecuteReader(query, false))
            {
                while (reader.Read())
                {
                    fleetInvoicesObjects.Add(new FleetInvoiceObject
                    {
                        InvoiceNumber = reader.GetStringValue(0).Trim(),
                        ARCurrencyCode = reader.GetStringValue(1).Trim(),
                        InvoiceDate = CommonUtils.ConvertDate(reader.GetDateTime(2), "M/dd/yyyy"),
                        IsOnline = true
                    });
                }
            }
            return fleetInvoicesObjects;
        }

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
                    query = @"SELECT TOP 1 Convert (Date , createdate) AS FromDate, Convert (Date , createdate) as ToDate FROM [Invoice_tb] ORDER BY createDate DESC;";

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
                    query = @"DECLARE @@Userid as int DECLARE @@FleetAccessLocations table(entityDetailId INT  primary key) select @@Userid = userid FROM [user_tb] where
                            username= @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE  entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK)  INNER JOIN [user_tb] WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) insert into @@FleetAccessLocations SELECT   entityDetailId  FROM RootNumber  where parentEntityDetailId <> entityDetailId   and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; select top 1 Convert(Date, invoicedate) as FromDate, Convert(Date, invoicedate) as ToDate FROM [invoice_tb] iv with (nolock) join @@FleetAccessLocations AE ON AE.entityDetailId = IV.receiverEntityDetailId INNER join [lookup_tb] l2 on l2.parentLookUpCode=21 AND l2.description=iv.transactionTypeCode INNER JOIN [entityDetails_tb] ed1 WITH (NOLOCK) ON ed1.entityDetailId = IV.receiverEntityDetailId INNER JOIN [entityDetails_tb] ed2 WITH (NOLOCK) ON ed2.entityDetailId = IV.senderEntityDetailId INNER JOIN(select distinct commonAbbr from [CurrencyCodes_tb] with(nolock)) curr ON  ltrim(rtrim(curr.commonAbbr)) = iv.ARCurrencyCode INNER JOIN [transaction_tb] t WITH(NOLOCK) ON t.transactionId = iv.transactionId where  iv.isActive = 1 ORDER BY Iv.InvoiceDate DESC;";
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

        internal void GetInvoiceDate(string programInvoiceNumber,out string invoiceDate)
        {
            invoiceDate = string.Empty;
            string query = null;
            try
            {
                query = @"select invoiceDate from invoice_tb where invoiceNumber = @programInvoiceNumber";

                List<SqlParameter> sp = new()
            {
                new SqlParameter("@programInvoiceNumber", programInvoiceNumber),

            };
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        invoiceDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                    }
                }                
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                invoiceDate = null;
            }
        }

        internal string GetDisputedInvoice()
        {
            string query = null;
            string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
            if (userType == "DEALER")
            {
                query = @"select top 1 iv.invoiceNumber from invoice_tb iv  inner join transactionDisputes_tb td WITH(NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 and td.disputestatus = 1 inner join entitydetails_tb e on e.entitydetailid = iv.senderentitydetailid inner join userrelationships_tb ur1 on ur1.entityid = iv.senderentitydetailid inner join userrelationships_tb ur2 on ur2.entityid = iv.senderBillToEntityDetailId inner join user_tb u on u.userid = ur1.userid where u.username = @UserName order by invoiceid desc";

                List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }

            }
            else if (userType == "ADMIN")
            {

                query = @"select top 1 iv.transactionNumber from invoice_tb iv
                        inner join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 and td.disputestatus=1
                        order by invoiceid desc";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }

            }
            else if (userType == "FLEET")
            {
                query = @"select top 1 iv.invoiceNumber from invoice_tb iv 
                        inner join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 and td.disputestatus=1 
                        inner join entitydetails_tb e on e.entitydetailid=iv.receiverEntityDetailId
                        inner join userrelationships_tb ur1 on ur1.entityid=iv.receiverEntityDetailId
                        inner join userrelationships_tb ur2 on ur2.entityid=iv.receiverBillToEntityDetailId
                        inner join user_tb u on u.userid=ur1.userid
                        where u.username=@UserName
                        order by invoiceid desc";

                List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
            return null;
        }

        internal string GetDisputedInvoiceWithMaxNotes()
        {
            string query = null;

            try
            {
                query = @"select top 1 iv.transactionNumber from invoice_tb iv
                        inner join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId inner join transactiondisputesprocessing_tb tdp WITH (NOLOCK) on
                        td.transactionDisputeId = tdp.transactionDisputeId where tdp.transactionDisputeId IN (select top 1 tdp.transactionDisputeId from invoice_tb iv
                        inner join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId inner join transactiondisputesprocessing_tb tdp WITH (NOLOCK) on
                        td.transactionDisputeId = tdp.transactionDisputeId where tdp.createdDate between GETDATE()-365 AND GETDATE() group by tdp.transactionDisputeId order by Count(tdp.transactionDisputeId) desc)";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }

            }

            catch (Exception ex) {

                return null;
            }
            
            return null;
        }
        internal List<string> GetResolutionDetails(string actionType)
        {
            List<string> transcationNames = new List<string>();
            string query = null;
            if (actionType == "Resolve Dispute")
            {
                query = "select name from lookup_tb where parentlookupcode = 406";
            }
            else if (actionType == "Close Dispute")
            {
                query = "select name from lookup_tb where parentlookupcode = 407";
            }

            using (var reader = ExecuteReader(query, false))
            {

                while (reader.Read())
                {
                    transcationNames.Add(reader.GetString(0));

                }

            }
            return transcationNames;
        }
        internal List<string> GetActionTypes()
        {
            List<string> transcationNames = new List<string>();
            string query = @"select name , * from lookup_tb where parentlookupcode = 47 and availableToReceiver = 0 and availableToSender = 0";
            using (var reader = ExecuteReader(query, false))
            {

                while (reader.Read())
                {
                    transcationNames.Add(reader.GetString(0));

                }

            }
            return transcationNames;
        }

        internal int GetRowCountTransactionStatus(string transactionStatus)
        {
            string query = null;
            List<string> rowCount = new List<string>();
            string fromDateTime = null;
            List<SqlParameter> sp = new();

            if (transactionStatus == "Current")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                            INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                            where
                            iv.isPaymentPortal IN (1,0) --  1 for OnlinePaymentMethod, 0 for all
                                    and iv.invoicedate between @fromDateTime AND GETDATE()         
                                        and IV.systemType=0
                                        --and iv.transactionTypeCode in ('P','R')
                            and td.transactionDisputeId is  null and iv.arPaidInFull is null and iv.apPaidInFull is null and iv.arInvoiceDueDate > GETDATE() and t.validationStatus=1
                                order by invoicenumber asc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Closed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where
                        iv.invoicedate between @fromDateTime AND GETDATE()
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and td.disputeStatus = 3";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));
            
            }
            else if (transactionStatus == "Current-Disputed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where
                        iv.invoicedate between @fromDateTime AND GETDATE()
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and td.disputeStatus = 1";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Hold")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                            --LEFT JOIN payInvoiceOnline_tb pi WITH (NOLOCK) ON iv.transactionId = pi.transactionId AND pi.isActive = 1
                        where
                        iv.invoicedate between @fromDateTime AND GETDATE() --settlementdate
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and th.holdStatus = 1
                        and iv.arInvoiceDueDate > CAST(GETDATE() AS DATE)";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Hold Released")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                            --LEFT JOIN payInvoiceOnline_tb pi WITH (NOLOCK) ON iv.transactionId = pi.transactionId AND pi.isActive = 1
                        where
			            iv.invoicedate between @fromDateTime AND GETDATE() --settlementdate
                        and IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and  th.holdStatus = 2 and (th.releaseDate > td.dateSent or td.transactionDisputeId is null)
                        and iv.arInvoiceDueDate > CAST(GETDATE() AS DATE) 
                        order by invoiceid desc";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Current-Resolved")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where
                        iv.invoicedate between @fromDateTime AND GETDATE() 
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and td.disputeStatus = 2";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Paid")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where  iv.ARInvoiceDate between  @fromDateTime AND GETDATE() 
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and iv.arPaidInFull = 1  and td.disputeStatus is NULL ";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Paid-Closed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where
                        iv.InvoiceDate between @fromDateTime AND GETDATE() 
                        and  IV.systemType=0 and iv.transactionTypeCode in ('P','R')
			            and iv.arPaidInFull = 1 AND td.disputeStatus = 3";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Paid-Disputed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where  iv.ARInvoiceDate between  @fromDateTime AND GETDATE()
                        and  IV.systemType=0
                       and iv.arPaidInFull = 1 AND td.disputeStatus = 1 ";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Paid-Resolved")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where  iv.ARInvoiceDate between  @fromDateTime AND GETDATE()
                        and  IV.systemType=0
                       and iv.arPaidInFull = 1 AND td.disputeStatus = 1 ";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where
                        iv.invoicedate between @fromDateTime AND GETDATE()
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and iv.arInvoiceDueDate < CAST(GETDATE() AS DATE)";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due-Closed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where
                        iv.invoicedate between @fromDateTime AND GETDATE()
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and iv.arInvoiceDueDate < CAST(GETDATE() AS DATE) AND td.disputeStatus = 3";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due-Disputed")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where
                        iv.invoicedate between @fromDateTime AND GETDATE()
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and  iv.arInvoiceDueDate < CAST(GETDATE() AS DATE) AND td.disputeStatus = 1";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due-Hold")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where 
                        iv.InvoiceDate between @fromDateTime AND GETDATE() 
                        and  IV.systemType=0
		                and th.holdStatus = 1
			            and iv.arInvoiceDueDate < GETDATE() 
			            and iv.arPaidInFull is null";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due-Hold Released")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        where
                        iv.arinvoicedate between @fromDateTime AND GETDATE()  
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
                        and th.holdStatus = 2 and (th.releaseDate > td.dateSent or td.transactionDisputeId is null) and iv.arInvoiceDueDate < CAST(GETDATE() AS DATE)";

                sp.Add(new SqlParameter("@fromDateTime", fromDateTime));

            }
            else if (transactionStatus == "Past due-Resolved")
            {
                fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                query = @"select iv.transactionNumber from invoice_tb iv 
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1 
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId 
                        where iv.invoicedate between @fromDateTime AND GETDATE() --settlementdate 
                        and IV.systemType=0 
                        and iv.transactionTypeCode in ('P','R') 
                        and iv.arInvoiceDueDate < CAST(GETDATE() AS DATE) 
                        AND td.disputeStatus = 2 
                        AND arPostedToAccounting= 0";

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
                string spName = @"select top 1 iv.transactionNumber,iv.purchaseOrderNumber,iv.originatingDocumentNumber,iv.invoiceNumber,* from invoice_tb iv 
                                left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1 
                                left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1 
                                INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId 
                                where
                                iv.invoiceDate between @fromDateTime AND GETDATE() 
                                and  IV.systemType=0 
                                and iv.transactionTypeCode in ('P','R') and td.disputeStatus = 3
								and appaidinfull is null and arpaidinfull is null
								and apinvoiceduedate>getdate() and arinvoiceduedate>getdate()
								order by t.createdate desc";

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
        internal EntityDetails GetInvoicesSubCommunities()
        {
            EntityDetails entityDetails = null;
            try
            {
                string fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                var query = @"select top 1 CONCAT(s1.subCommunityCode, '-', s1.subCommunityName) AS 'Dealer Sub-Community',CONCAT(s2.subCommunityCode, '-', s2.subCommunityName) AS 'Fleet Sub-Community' from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        INNER JOIN entityDetails_tb ed1 WITH (NOLOCK) ON ed1.entityDetailId = IV.receiverEntityDetailId
                        INNER JOIN  entityDetails_tb ed2 WITH (NOLOCK) ON ed2.entityDetailId = IV.senderEntityDetailId
                        left join subcommunity_tb s1 wITH (NOLOCK) ON ed1.subcommunityid=s1.subcommunityid and   s1.isactive=1
                        left join subcommunity_tb s2 wITH (NOLOCK) ON ed2.subcommunityid=s2.subcommunityid and   s2.isactive=1
                        left join groupAssignment_tb ga1 wITH (NOLOCK) ON ed1.entityDetailId=ga1.entityDetailId and ga1.isactive=1
                        left join group_tb g1 on ga1.groupid=g1.groupid and g1.isactive=1 
                        left join groupAssignment_tb ga2 wITH (NOLOCK) ON ed2.entityDetailId=ga2.entityDetailId and ga2.isactive=1
                        where IV.systemType=0 and iv.isPaymentPortal IN(0,1) 
                        and iv.invoicedate between @fromDateTime AND GETDATE() 
                        and iv.transactionTypeCode in ('P','R')
                        and t.validationStatus=1 
                        and s1.subCommunityId=0 and s1.subCommunityType=0
                        and s2.subCommunityId=0 and s2.subCommunityType=0	
                        order by invoicenumber asc";

                List<SqlParameter> sp = new()
            {
                new SqlParameter("@fromDateTime", fromDateTime),
            };

                using (var reader = ExecuteReader(query,sp, false))
                {
                    if (reader.Read())
                    {
                        entityDetails = new EntityDetails();
                        entityDetails.DealerSubCommunity = reader.GetStringValue("Dealer Sub-Community");
                        entityDetails.FleetSubCommunity = reader.GetStringValue("Fleet Sub-Community");
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

        internal int GetInvoicesCountBySubCommunities(string dealerSubCommunity , string fleetSubCommunity)
        {
            List<string> rowCount = new List<string>();
            string fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");

            var query = @"select DISTINCT(iv.invoiceNumber) from invoice_tb iv
                            left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                            left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                            INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                            INNER JOIN entityDetails_tb ed1 WITH (NOLOCK) ON ed1.entityDetailId = IV.receiverEntityDetailId
                            INNER JOIN  entityDetails_tb ed2 WITH (NOLOCK) ON ed2.entityDetailId = IV.senderEntityDetailId
                            left join subcommunity_tb s1 wITH (NOLOCK) ON ed1.subcommunityid=s1.subcommunityid and   s1.isactive=1
                            left join subcommunity_tb s2 wITH (NOLOCK) ON ed2.subcommunityid=s2.subcommunityid and   s2.isactive=1
                            left join groupAssignment_tb ga1 wITH (NOLOCK) ON ed1.entityDetailId=ga1.entityDetailId and ga1.isactive=1
                            left join group_tb g1 on ga1.groupid=g1.groupid and g1.isactive=1 
                            left join groupAssignment_tb ga2 wITH (NOLOCK) ON ed2.entityDetailId=ga2.entityDetailId and ga2.isactive=1
                            where IV.systemType=0 and iv.isPaymentPortal IN(0,1) 
                            and iv.invoicedate between @fromDateTime AND GETDATE() 
                            and iv.transactionTypeCode in ('P','R')
                            and t.validationStatus=1 
							and td.disputeStatus = 2
							and iv.arInvoiceDueDate > GETDATE()
                            and s1.subCommunityName=@fleetSubcommunity
							and s2.subCommunityName=@dealerSubcommunity
                            order by invoicenumber asc";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerSubcommunity", dealerSubCommunity),
                new SqlParameter("@fleetSubcommunity", fleetSubCommunity),
                new SqlParameter("@fromDateTime", fromDateTime),

            };

            using (var reader = ExecuteReader(query,sp,false))
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
                            left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                            INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                            INNER JOIN entityDetails_tb ed1 WITH (NOLOCK) ON ed1.entityDetailId = IV.receiverEntityDetailId
                            INNER JOIN  entityDetails_tb ed2 WITH (NOLOCK) ON ed2.entityDetailId = IV.senderEntityDetailId
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
                            and td.disputeStatus = 2
                            and (l1.name='super admin list' or l1.name='user company list')
                            and (l2.name='super admin list' or l2.name='user company list')
                            order by iv.invoiceid desc";

                List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetuser", user),
                new SqlParameter("@dealeruser", user),

            };

                using (var reader = ExecuteReader(query,sp, false))
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

        internal int GetInvoicesCountByGroup(string user,string dealerGroup , string fleetGroup)
        {
            List<string> rowCount = new List<string>();
            string fromDateTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");

            var query = @"select g1.name as Fleetgroup,* from invoice_tb iv
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
                        iv.invoicedate between @fromDateTime AND GETDATE() 
                        and  IV.systemType=0
                        and iv.transactionTypeCode in ('P','R')
						and g1.name = @fleetGroup and g2.name = @dealerGroup
                        and iv.arPaidInFull is null and iv.apPaidInFull is null and iv.apInvoiceDueDate > GETDATE() and t.validationStatus=1
                        and td.disputeStatus = 2
                        and (l1.name='super admin list' or l1.name='user company list')
                        and (l2.name='super admin list' or l2.name='user company list')
                        order by iv.invoiceid desc";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetUser", user),
                new SqlParameter("@dealerUser", user),
                new SqlParameter("@dealerGroup", dealerGroup),
                new SqlParameter("@fleetGroup", fleetGroup),
                new SqlParameter("@fromDateTime", fromDateTime),

            };


            using (var reader = ExecuteReader(query,sp, false))
            {
                while (reader.Read())
                {
                    rowCount.Add(reader.GetString(0));
                }

            }


            int count = rowCount.Count();
            return count;
        }

        internal int GetInvoicesCountByDeliveryStatus()
        {
            List<string> rowCount = new List<string>();
            string fromDateTime = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            var query = @"select  iv.transactionNumber from invoice_tb iv
                        left join transactionDisputes_tb td WITH (NOLOCK) on iv.transactionId = td.transactionId and td.isActive = 1
                        left join transactionHold_tb th WITH (NOLOCK) on iv.transactionId = th.transactionId and th.isActive = 1
                        INNER JOIN transaction_tb t WITH (NOLOCK) ON t.transactionId = iv.transactionId
                        LEFT JOIN dbo.lookUp_tb dlk ON dlk.lookUpId = dbo.udf_getInvoiceDeliveryStatus(IV.invoiceId)
                        where 
                        iv.isPaymentPortal IN (0,1) --  1 for OnlinePaymentMethod, 0 for all
                        and iv.invoicedate between @fromDateTime AND GETDATE() 
                        and iv.transactionTypeCode in ('P','R')
                        and t.validationStatus=1 
                        and  IV.systemType=0
                        and dlk.name='EDI-accepted'
                        and td.transactionDisputeId is  null and iv.arPaidInFull is null and iv.apPaidInFull is null and iv.arInvoiceDueDate > GETDATE()
                        order by invoicenumber desc";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fromDateTime", fromDateTime),
            };

            using (var reader = ExecuteReader(query,sp, false))
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
                query = @"SELECT COUNT (*)   
                from Invoice_tb Where invoiceDate BETWEEN @fromDateTime AND GETDATE()";

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
                query = @"SELECT COUNT (*)   
                from Invoice_tb Where invoiceDate BETWEEN @fromDateTime AND GETDATE()";

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
                query = @"SELECT COUNT (*)   
                from Invoice_tb Where invoiceDate BETWEEN @fromDateTime AND GETDATE()";

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

                query = @"SELECT COUNT (*)   
                from Invoice_tb Where invoiceDate BETWEEN @fromDateTime AND GETDATE()";

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

                query = @"SELECT COUNT (*)   
                from Invoice_tb Where invoiceDate BETWEEN @fromDateTime AND @toDateTime";

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
                query = @"SELECT COUNT (*)   
                from Invoice_tb Where invoiceDate BETWEEN @fromDateTime AND GETDATE()";

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


        internal string GetCurrentInvoice(string dealerName , string fleetName)
        {
            string query = null;

            try
            {
                query = @"select top 1 transactionNumber from transaction_tb where  
                        senderbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @dealerName)  
                        and receiverbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @fleetName) 
                        and transactionId not in (select referencedTransactionId  from transaction_tb where referencedTransactionId is not null) and referencedTransactionId is null
                        and validationstatus in (1) and requesttypecode = 'S'  and isReversed = 0
                        order by transactionDate desc";

                List<SqlParameter> sp = new()
           
                {                    
                    new SqlParameter("@dealerName", dealerName),
                    new SqlParameter("@fleetName", fleetName),
                };

                using (var reader = ExecuteReader(query,sp,false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }

            }

            catch (Exception ex)
            {

                return null;
            }

            return null;
        }

        internal string GetBatchGuID(string invoiceNumber)
        {
            var query = @"select p.batchGuid from payInvoiceOnline_tb p inner join invoice_tb i ON i.transactionId = p.transactionId
                          where i.invoiceNumber = '"+ invoiceNumber+"'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetInvoiceStatus(string batchID)
        {
            var query = @"select u.name from lookUp_tb u inner join payInvoiceOnline_tb s ON s.paymentStatus = u.lookUpId
                          where s.batchGuid = '" + batchID + "'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

    }
}
