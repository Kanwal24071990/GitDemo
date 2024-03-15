using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer
{
    internal class CommonDAL : BaseDataAccessLayer
    {
        internal void ActivateTokenPPV()
        {
            string query = "Update relsenderreceiver_tb set isactive=1 where senderreceiverrelid=327325";

            ExecuteNonQuery(query, false);

        }

        internal void DeactivateTokenPPV()
        {
            string query = "Update relsenderreceiver_tb set isactive=0 where senderreceiverrelid=327325";

            ExecuteNonQuery(query, false);

        }

        internal bool IsInvoiceValidated(string invoiceNum)
        {
            int validationStatus;
            bool isValidated = false;
            string query = "select validationStatus from transaction_tb where transactionnumber= '" + invoiceNum + "'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    validationStatus = reader.GetInt32(0);
                    if (validationStatus == 1)
                    {
                        isValidated = true;
                    }

                }
            }

            return isValidated;
        }

        internal void ActivateFutureDateInMinsToken()
        {
            string query = "update lookup_tb set isactive=1 where parentlookupcode=100 and lookupcode=69";

            ExecuteNonQuery(query, false);

        }

        internal void DeactivateFutureDateInMinsToken()
        {
            string query = "update lookup_tb set isactive=0 where parentlookupcode=100 and lookupcode=69";

            ExecuteNonQuery(query, false);

        }

        internal void SetFutureDateInMinsValue(int value)
        {
            string query = "update lookup_tb set description = '" + value + "' where parentlookupcode=100 and lookupcode=69";

            ExecuteNonQuery(query, false);

        }

        internal void UpdateTransactionDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd");

            string query = "update transaction_tb set transactionDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";

            ExecuteNonQuery(query, false);
        }

        internal void UpdateCreateDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string query = "update transaction_tb set createDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateDisputeDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string query = "update transactiondisputes_tb set datesent= '" + dateFinal + "' where transactionid in (select transactionid from transaction_tb where transactionnumber= '" + invoiceNum + "')";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateReceiveDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string query = "update invoice_tb set receiveDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateAPInvoiceDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd");
            string query = "update invoice_tb set APInvoiceDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateARStatementStartDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd");
            string query = "update invoice_tb set arStatementStartDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateAPStatementStartDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd");
            string query = "update invoice_tb set apStatementStartDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateARStatementDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd");
            string query = "update invoice_tb set arStatementDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateAPStatementDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd");
            string query = "update invoice_tb set apStatementDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateCreateDateInvoiceTbForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string query = "update invoice_tb set createDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateInvoiceDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd");
            string query = "update invoice_tb set invoiceDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateARInvoiceDateForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd");
            string query = "update invoice_tb set arInvoiceDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal void UpdateTransactionDateInvoiceTbForInvoice(string invoiceNum, int day)
        {
            DateTime today = DateTime.Now;
            DateTime date = today.AddDays(day);
            string dateFinal = date.ToString("yyyy-MM-dd");
            string query = "update invoice_tb set transactionDate= '" + dateFinal + "' where transactionnumber= '" + invoiceNum + "'";
            ExecuteNonQuery(query, false);
        }

        internal LookupTb GetLookupTbDetails(string name)
        {
            LookupTb lookupTb = null;
            string query = @"select top 1 * from lookup_tb where name = @name";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@name", name)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    lookupTb = new LookupTb();
                    lookupTb.LookupId = reader.GetInt32(reader.GetReaderIndex("lookUpId"));
                    lookupTb.ParentLookupCode = reader.GetInt32(reader.GetReaderIndex("parentLookUpCode"));
                    lookupTb.LookupCode = reader.GetInt32(reader.GetReaderIndex("lookUpCode"));
                    lookupTb.Name = reader.GetStringValue("name");
                    lookupTb.Description = reader.GetStringValue("description");
                    lookupTb.IsActive = reader.GetBoolean(reader.GetReaderIndex("isActive"));
                    lookupTb.AvailableToReceiver = reader.GetInt16(reader.GetReaderIndex("availableToReceiver"));
                    lookupTb.AvailableToSender = reader.GetInt16(reader.GetReaderIndex("availableToSender"));
                }
            }
            return lookupTb;
        }


        internal InvoiceObject GetInvoiceByTransactionNumber(string transactionNumber)
        {
            InvoiceObject invoicesObject = new InvoiceObject();
            string query = @"select top 1 * from invoice_tb where transactionNumber = @transactionNumber";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@transactionNumber", transactionNumber)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    invoicesObject = new InvoiceObject()
                    {
                        InvoiceId = int.Parse(reader.GetSqlDecimal(0).ToString()),
                        InvoiceNumber = reader.GetStringValue("invoiceNumber").Trim(),
                        APPaidDate = reader.GetDateTimeValue("apPaidDate", "MM/dd/yyyy"),
                        ARPaidDate = reader.GetDateTimeValue("arPaidDate", "MM/dd/yyyy"),
                    };
                }
            }
            return invoicesObject;
        }

        internal InvoiceObject GetInvoiceSectionByInvoiceId(int InvoiceId)
        {
            InvoiceObject invoicesObject = new InvoiceObject();
            string query = @"select top 1 * from invoicesection_tb where invoiceid = @invoiceId";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@invoiceId", InvoiceId)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    invoicesObject = new InvoiceObject()
                    {
                        InvoiceSectionId = int.Parse(reader.GetSqlDecimal(0).ToString()),

                    };
                }
            }
            return invoicesObject;
        }

        internal List<string> GetInvoiceLineDetailsItemIds(int InvoiceSectionId)
        {
            List<string> itemIds = null;
            string query = @"select itemId from invoicelinedetail_tb where invoiceSectionId = @invoiceSectionId";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@invoiceSectionId", InvoiceSectionId)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                itemIds = new List<string>();
                while (reader.Read())
                {
                    {
                        itemIds.Add(reader.GetStringValue("itemId"));
                    };
                }
            }
            return itemIds;
        }

        internal void GetInvoicebyTransactionStatus(int transactionStatus, out string invoiceNum)
        {
            string query = null;
            invoiceNum = null;
            try
            {
                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                if (userType == "ADMIN")
                {
                    query = @"select top 1 transactionNumber from transaction_tb where validationstatus='" + transactionStatus + "' and requestTypeCode='S' and isActive=1 and isHistorical=0 order by 1 desc";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {

                            invoiceNum = reader.GetString(0);
                        }
                    }
                }
                else if (userType == "DEALER")
                {
                    query = @"select top 1 transactionNumber from transaction_tb where validationstatus='" + transactionStatus + "' and requestTypeCode='S' and isActive=1 and isHistorical=0 and (senderEntityDetailId in (select entityid from userRelationships_tb r inner join user_tb u on r.userid=u.userid where username=@UserName) or senderBillToEntityDetailId in (select entityid from userRelationships_tb r inner join user_tb u on r.userid=u.userid where username=@UserName)) order by 1 desc";

                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            invoiceNum = reader.GetString(0);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                invoiceNum = null;

            }
        }

        internal string GetDealerCodeHasNoChild(LocationType locationType)
        {
            var query = @"select top 1 a.corcentricCode from (select t.* from entitydetails_tb AS t WITH (NOLOCK)
                INNER JOIN entityaddressrel_tb entAdd WITH (NOLOCK) ON t.entitydetailid = entAdd.entitydetailid
                INNER JOIN address_tb adr WITH (NOLOCK) ON adr.addressid = entAdd.addressid
                where t.entitytypeid = 2 and t.enrollmentstatusid = 13  AND t.isActive=1  and masterflag=@masterFlag and locationtypeid=@locationTypeId) a
                where (select count(*) from entityDetails_tb where parentEntityDetailId = a.entityDetailId) = 0";
            List<SqlParameter> sp;
            if (locationType.Equals(LocationType.Master)
               || locationType.Equals(LocationType.MasterBilling))
            {
                sp = new()
                {
                    new SqlParameter("@masterFlag", true)
                };
            }
            else
            {
                sp = new()
                {
                    new SqlParameter("@masterFlag", false)
                };
            }

            sp.Add(new SqlParameter("@locationTypeId", locationType.Value));
            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            return null;
        }

        internal string GetFleetCodeHasNoChild(LocationType locationType)
        {
            var query = @"select top 1 a.corcentricCode from (select t.* from entitydetails_tb AS t WITH (NOLOCK)
                INNER JOIN entityaddressrel_tb entAdd WITH (NOLOCK) ON t.entitydetailid = entAdd.entitydetailid
                INNER JOIN address_tb adr WITH (NOLOCK) ON adr.addressid = entAdd.addressid
                where t.entitytypeid = 3 and t.enrollmentstatusid = 13  AND t.isActive=1  and masterflag=@masterFlag and locationtypeid=@locationTypeId) a
                where (select count(*) from entityDetails_tb where parentEntityDetailId = a.entityDetailId) = 0";
            List<SqlParameter> sp;
            if (locationType.Equals(LocationType.Master)
              || locationType.Equals(LocationType.MasterBilling))
            {
                sp = new()
                {
                    new SqlParameter("@masterFlag", true)
                };
            }
            else
            {
                sp = new()
                {
                    new SqlParameter("@masterFlag", false)
                };
            }

            sp.Add(new SqlParameter("@locationTypeId", locationType.Value));

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            return null;
        }

        internal void ActivateTokenGMC()
        {
            string query = "update lookup_tb set isActive = 1 where parentlookupcode = 100 and lookupcode = 194";

            ExecuteNonQuery(query, false);

        }

        internal void DeactivateTokenGMC()
        {
            string query = "update lookup_tb set isActive = 0 where parentlookupcode = 100 and lookupcode = 194";

            ExecuteNonQuery(query, false);

        }

        internal List<string> GetStates(string country)
        {
            List<string> states = null;
            var query = @"select * from statecountry_tb where Country=@country;";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@country", country)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                states = new List<string>();
                while (reader.Read())
                {
                    states.Add(reader.GetStringValue("FullName"));
                }
            }
            return states;
        }

        internal string GetFleetCode()
        {
            string query = string.Empty;
            string userType;
            string userName;
            if (TestContext.CurrentContext.Test.Properties["Type"].Count() > 0)
            {
                userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }
            if (userType.Equals("FLEET"))
            {
                query = @"select top 1 sen.corcentriccode FROM invoice_tb IV inner JOIN entityDetails_tb sen  on sen.entityDetailId =  receiverEntityDetailId and  sen.isActive=1 AND sen.isTerminated=0 inner join userrelationships_tb ur1 on ur1.entityid=iv.receiverBillToEntityDetailId inner join userrelationships_tb ur2 on ur2.entityid=iv.senderBillToEntityDetailId inner join user_tb u on u.userid=ur1.userid where iv.currencyCode='USD'  and " + string.Format("u.userName = '{0}'", userName) + "  order by invoiceid desc";
            }

            else
            {
                query = @"select top 1  res.corcentriccode FROM invoice_tb IV
                        inner JOIN entityDetails_tb res on res.entityDetailId = receiverEntityDetailId
                        where iv.currencyCode='USD' AND res.isActive=1 AND res.isTerminated=0 order by invoiceid desc";
            }

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetPartiallyEnrolledFleetCode()
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
                        where t.entitytypeid = 3 and t.enrollmentstatusid != 13";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetString(0);
                }
            }

            return null;
        }


        internal string GetInactiveFleetCode()
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
						where t.entitytypeid = 3 AND t.isActive=0";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetTerminatedFleetCode()
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
						where t.entitytypeid = 3 AND t.isTerminated=1";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetSuspendedFleetCode()
        {
            var query = @"select top 1 e.corcentriccode from entitydetails_tb e inner join relsenderreceiver_tb rs
                        on e.entitydetailid=rs.receiverId
                        where rs.relationshiptypeid=351 and rs.isActive=1 and rs.entityTypeId=3
                        order by rs.senderReceiverRelId desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetDealerCode()
        {
            string query = string.Empty;
            string userType;
            string userName;
            if (TestContext.CurrentContext.Test.Properties["Type"].Count() > 0)
            {
                userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                userName = TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
            }
            else
            {
                userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                userName = ScenarioContext.Current["UserName"].ToString().ToUpper();
            }
            if (userType.Equals("DEALER"))
            {
                query = @"select top 1 sen.corcentriccode FROM invoice_tb IV inner JOIN entityDetails_tb sen  on sen.entityDetailId =  senderEntityDetailId and  sen.isActive=1 AND sen.isTerminated=0 inner join userrelationships_tb ur1 on ur1.entityid=iv.senderBillToEntityDetailId inner join userrelationships_tb ur2 on ur2.entityid=iv.receiverBillToEntityDetailId inner join user_tb u on u.userid=ur1.userid where iv.currencyCode='USD'  and " + string.Format("u.userName = '{0}'", userName) + "  order by invoiceid desc";
            }
            else
            {
                query = @"select top 1 sen.corcentriccode FROM invoice_tb IV
                        inner JOIN entityDetails_tb sen  on sen.entityDetailId = senderEntityDetailId
                        where iv.currencyCode='USD' AND sen.isActive=1 AND sen.isTerminated=0 order by invoiceid desc";
            }


            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetPartiallyEnrolledDealerCode()
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
                        where t.entitytypeid = 2 and t.enrollmentstatusid != 13";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetInactiveDealerCode()
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
						where t.entitytypeid = 2 AND t.isActive=0";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetTerminatedDealerCode()
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
						where t.entitytypeid = 2 AND t.isTerminated=1";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetSuspendedDealerCode()
        {
            var query = @"select top 1 e.corcentriccode from entitydetails_tb e inner join relsenderreceiver_tb rs
                            on e.entitydetailid=rs.senderid
                            where rs.relationshiptypeid=351 and rs.isActive=1 and rs.entityTypeId=2
                            order by rs.senderReceiverRelId desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetString(0);

                }
            }

            return null;
        }

        internal string GetPartByCategory(string category)
        {
            var query = @"select top 1 partnumber from part_tb p inner join
                          partcategorycode_tb pc on p.categoryCode1Id=pc.partCategoryCodeId
                          inner join price_tb pr on p.partid=pr.partid
                          inner join pricedetail_tb pd on pr.priceid=pd.priceid
                          inner join lookup_tb l on pd.pricelevelid=l.lookupid
                          where categoryTypeId=231 and categoryValue='" + category + "' and l.name='afld' and pr.currencycode='usd' order by p.partid desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }


        internal string GetPartNotInCategory()
        {
            var query = @"select top 1 partnumber from part_tb p inner join
                          partcategorycode_tb pc on p.categoryCode1Id=pc.partCategoryCodeId
                          inner join price_tb pr on p.partid=pr.partid
                          inner join pricedetail_tb pd on pr.priceid=pd.priceid
                          inner join lookup_tb l on pd.pricelevelid=l.lookupid
                          where
                          categoryTypeId=231 and categoryDescription not in ('engine','vendor','Proprietary')
                          and l.name='afld'
                          and pr.currencycode='usd'
                          order by p.partid  desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetDealerEntityCount()
        {
            var query = @"select Count(*) from entityDetails_tb inner join entityaddressrel_tb on entityDetails_tb.entityDetailId= entityaddressrel_tb.entityDetailId inner join address_tb on entityaddressrel_tb.addressId= address_tb.addressId where 
                          entityTypeId=2 AND enrollmentStatusId=13";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetInt32(0).ToString();
                }
            }

            return "";
        }

        internal string GetFleetEntityCount()
        {
            var query = @"select Count(*) from entityDetails_tb inner join entityaddressrel_tb on entityDetails_tb.entityDetailId= entityaddressrel_tb.entityDetailId inner join address_tb on entityaddressrel_tb.addressId= address_tb.addressId where 
                        entityTypeId=3 AND enrollmentStatusId=13";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetInt32(0).ToString();
                }
            }

            return "";
        }

        internal string GetCorcentricLocation()
        {
            var query = @"Select top 1 corcentricCode from entitydetails_tb where corcentricLocation=1 and entityTypeId=2 order by createdDate desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.IsDBNull(0) ? "" : reader.GetString(0);

                }
            }

            return null;
        }

        internal List<string> GetActiveCurrencies()
        {
            List<string> activeCurrencies = new List<string>();
            string query = "select distinct commonabbr from currencycodes_tb";
            using (var reader = ExecuteReader(query, false))
            {

                while (reader.Read())
                {
                    activeCurrencies.Add(reader.GetString(0));

                }

            }
            return activeCurrencies;
        }

        internal void ToggleValidateProgramCode(bool activate)
        {
            var query = @"update lookup_tb set isActive=@activate where parentlookupCode = 100 and lookupCode = 71";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@activate", activate)
            };
            ExecuteNonQuery(query, sp, false);
        }


        /// <summary>
        /// Activate or deactivates EnableProgramCodeAssignmentOnSubCommunity in lookup table
        /// </summary>
        /// <param name="activate"></param>
        internal void ToggleEnablePrgmCodeAsgnOnSubCommunity(bool activate)
        {
            var query = "update lookup_tb set isActive=@activate where parentlookupCode = 100 and lookupCode = 238";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@activate", activate)
            };
            ExecuteNonQuery(query, sp, false);
        }

        internal List<EntityDetails> GetProgramCodeEntityLocations(string userName)
        {
            List<EntityDetails> entityDetailsList = null;
            string query = @"declare @Userid as int
                declare @FleetAccessLocations table (  
                  entityDetailId    INT  primary key,
                  displayName VARCHAR(150),
				  locationTypeId INT
                )  
 
                select @Userid=userid from user_tb where username=@userName;
                WITH RootNumber AS (  
                SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID, displayName, locationTypeId FROM entityDetails_tb  WITH(NOLOCK) 
                WHERE entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId   
                FROM userRelationships_tb WITH(NOLOCK) INNER JOIN user_tb   WITH(NOLOCK) 
                ON userRelationships_tb.userId = user_tb.userId    
                WHERE user_tb.userId = @UserID   
                AND userRelationships_tb.IsActive = 1   
                AND userRelationships_tb.hasHierarchyAccess = 1
                )  
                UNION ALL  
                SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID, C.displayName, C.locationTypeId FROM RootNumber AS P  
                INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId  
                )  
                insert into @FleetAccessLocations  
                SELECT entityDetailId, displayName, locationTypeId FROM RootNumber where parentEntityDetailId <> entityDetailId   and parentEntityDetailId <>0  
                UNION  
				SELECT ur.entityId As entityDetailId, ed.displayName, ed.locationTypeId FROM userRelationships_tb ur WITH(NOLOCK)
				INNER JOIN entityDetails_tb ed WITH(NOLOCK) ON ur.entityId = ed.entityDetailID
				WHERE ur.userId = @UserID  and ur.IsActive =1 AND ur.entityId IS NOT NULL;
                Select entityDetailID, displayName, locationTypeId from @FleetAccessLocations";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@userName", userName)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                entityDetailsList = new List<EntityDetails>();
                while (reader.Read())
                {
                    entityDetailsList.Add(new EntityDetails()
                    {
                        DisplayName = reader.GetStringValue("displayName"),
                        LocationTypeId = reader.GetInt32(reader.GetReaderIndex("locationTypeId"))
                    });
                }
            }
            return entityDetailsList;
        }

        internal List<EntityDetails> GetProgCodeOppEntyLocForLoggedInEntyType(string userName, string entityType, out bool isProgramCodeAssigned)
        {
            List<EntityDetails> oppLocations = null;
            isProgramCodeAssigned = true;
            string query = @"declare @Userid as int
                declare @FleetAccessLocations table (  
                  entityDetailId    INT  primary key   
                )  
 
                select @Userid=userid from user_tb where username=@userName;
                WITH RootNumber AS (  
                SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb  WITH(NOLOCK) 
                WHERE entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId   
                FROM userRelationships_tb WITH(NOLOCK) INNER JOIN user_tb   WITH(NOLOCK) 
                ON userRelationships_tb.userId = user_tb.userId    
                WHERE user_tb.userId = @UserID   
                AND userRelationships_tb.IsActive = 1   
                AND userRelationships_tb.hasHierarchyAccess = 1
                )  
                UNION ALL  
                SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P  
                INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId  
                )  
                insert into @FleetAccessLocations  
                SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId   and parentEntityDetailId <>0  
                UNION  
                SELECT userRelationships_tb.entityId As entityDetailId FROM userRelationships_tb  
                WITH(NOLOCK) WHERE userRelationships_tb.userId = @UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL;
                --Select entityDetailID from @FleetAccessLocations

                Select * into #AssignedLocationsProgramCodes FROM (
                select l.description, E.corcentricCode,elr.lookupID from entitydetails_tb e
                LEFT Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
                LEFT join lookup_tb l on l.lookupId=elr.lookupId and l.parentlookupcode=23
                where E.entityDetailId IN (Select entityDetailID from @FleetAccessLocations) 
                ) AS ProgramCode

                Select count(*) from #AssignedLocationsProgramCodes where description is null
                IF (Select count(*) from #AssignedLocationsProgramCodes WHERE lookupID IN (0)) >= 1	
                Select displayName, locationTypeId from entitydetails_tb ed
                inner join entityAddressRel_tb edr on ed.entityDetailId = edr.entityDetailId 
                where ed.isActive=1 and ed.enrollmentStatusId=13 and ed.entityTypeId=@entityTypeId
                ELSE
                SELECT distinct E.displayname, E.locationTypeId from entityLookUpRelationship_tb Elr 
                INNER JOIN entitydetails_tb E ON Elr.entitydetailId = E.entitydetailID 
                INNER JOIN entityAddressRel_tb edr on E.entityDetailId = edr.entityDetailId
                INNER JOIN lookup_tb LK ON lk.lookupID = Elr.lookupID 
                where E.isActive=1 and E.enrollmentStatusId=13 and E.entityTypeId=@entityTypeId
                AND Elr.lookupId IN (Select lookupId from #AssignedLocationsProgramCodes)
                AND Lk.parentlookupCode = 23
                Drop table #AssignedLocationsProgramCodes";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@userName", userName),
                new SqlParameter("@entityTypeId", entityType == EntityType.Dealer ? 3 : 2)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                oppLocations = new List<EntityDetails>();
                while (reader.Read())
                {
                    isProgramCodeAssigned = reader.GetInt32(reader.GetReaderIndex("nullCount")) > 0 ? false : true;
                }
                reader.NextResult();
                while (reader.Read())
                {
                    var loc = new EntityDetails()
                    {
                        DisplayName = reader.GetStringValue("displayName"),
                        LocationTypeId = reader.GetInt32(reader.GetReaderIndex("locationTypeId"))
                    };
                    oppLocations.Add(loc);
                }
            }
            return oppLocations;
        }

        internal List<EntityDetails> GetProgCdOppEntyLocForLoggedInEntyTypeFlsNeg(string userName, string entityType)
        {
            List<EntityDetails> oppoLocations = null;
            string query = @"declare @Userid as int
                declare @FleetAccessLocations table (  
                  entityDetailId    INT  primary key   
                )  
                select @Userid=userid from user_tb where username=@userName;
                WITH RootNumber AS (  
                SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb  WITH(NOLOCK) 
                WHERE entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId   
                FROM userRelationships_tb WITH(NOLOCK) INNER JOIN user_tb   WITH(NOLOCK) 
                ON userRelationships_tb.userId = user_tb.userId    
                WHERE user_tb.userId = @UserID   
                AND userRelationships_tb.IsActive = 1   
                AND userRelationships_tb.hasHierarchyAccess = 1
                )  
                UNION ALL  
                SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P  
                INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId  
                )  
                insert into @FleetAccessLocations  
                SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId   and parentEntityDetailId <>0  
                UNION  
                SELECT userRelationships_tb.entityId As entityDetailId FROM userRelationships_tb  
                WITH(NOLOCK) WHERE userRelationships_tb.userId = @UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL;
                --Select entityDetailID from @FleetAccessLocations 

                Select * into #AssignedLocationsProgramCodes FROM (
                select l.description, E.corcentricCode,elr.lookupID from entitydetails_tb e
                LEFT Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
                LEFT join lookup_tb l on l.lookupId=elr.lookupId and l.parentlookupcode=23
                where E.entityDetailId IN (Select entityDetailID from @FleetAccessLocations) 
                ) AS ProgramCode 

                --Select * from #AssignedLocationsProgramCodes

                IF (Select count(*) from #AssignedLocationsProgramCodes WHERE lookupID IN (0)) >= 1    
                Select count(*) from entitydetails_tb ed
                inner join entityAddressRel_tb edr on ed.entityDetailId = edr.entityDetailId 
                where ed.isActive=1 and ed.enrollmentStatusId=13 and ed.entityTypeId=3                
                ELSE
                Select * into #MatchingEntities from (
                SELECT distinct E.corcentricCode as displayName, E.entitydetailID, E.locationTypeId from entityLookUpRelationship_tb Elr 
                INNER JOIN entitydetails_tb E ON Elr.entitydetailId = E.entitydetailID 
                INNER JOIN entityAddressRel_tb edr on E.entityDetailId = edr.entityDetailId
                INNER JOIN lookup_tb LK ON lk.lookupID = Elr.lookupID 
                where E.isActive=1 and E.enrollmentStatusId=13 and E.entityTypeId=@entityTypeId
                AND Elr.lookupId IN (Select lookupId from #AssignedLocationsProgramCodes)
                AND Lk.parentlookupCode = 23
                ) AS MatchingEntities
                Select ed.displayName, ed.locationTypeId from entitydetails_tb ed
                inner join entityAddressRel_tb edr on ed.entityDetailId = edr.entityDetailId 
                where ed.isActive=1 and ed.enrollmentStatusId=13 and ed.entityTypeId=@entityTypeId  
                AND ed.entitydetailID NOT IN (Select entitydetailID from #MatchingEntities)
                order by displayname
                Drop table #AssignedLocationsProgramCodes
                Drop table #MatchingEntities";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@userName", userName),
                new SqlParameter("@entityTypeId", entityType == EntityType.Dealer ? 3 : 2)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                oppoLocations = new List<EntityDetails>();
                while (reader.Read())
                {
                    var loc = new EntityDetails()
                    {
                        DisplayName = reader.GetStringValue("displayName"),
                        LocationTypeId = reader.GetInt32(reader.GetReaderIndex("locationTypeId"))
                    };
                    oppoLocations.Add(loc);
                }
            }
            return oppoLocations;
        }

        internal void UpdateFleetCreditLimits(int creditLimit, int availCreditLimit, string fleetCode)
        {
            string query = "Update entitydetails_tb set availableCreditLimit = " + availCreditLimit + ", creditLimit = " + creditLimit + " where corcentricCode = '" + fleetCode + "'";
            ExecuteNonQuery(query, false);
        }

        internal List<int> DeactivateActivatedDataContent()
        {
            List<int> dataContentIds;
            var query = @"select dataContentID from dataContent_tb where isactive = 1";
            using (var reader = ExecuteReader(query, false))
            {
                dataContentIds = new List<int>();
                while (reader.Read())
                {
                    dataContentIds.Add(reader.GetInt32(0));
                }
            }

            query = @"update dataContent_tb set isActive = @activate;";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@activate", false)
            };
            ExecuteNonQuery(query, sp, false);
            return dataContentIds;
        }


        internal void ActivateDeactivatedDataContent(List<int> dataContentIds)
        {
            var query = @"update dataContent_tb set isActive = 1 where dataContentId in (" + string.Join(" , ", dataContentIds.ToArray()) + ")";
            ExecuteNonQuery(query, false);
        }

        internal List<string> GetActiveTransactionStatus()
        {
            List<string> transactionStatusList;
            var query = @"select name from lookup_tb where parentLookUpCode=244 and isactive=1;";
            using (var reader = ExecuteReader(query, false))
            {
                transactionStatusList = new List<string>();
                while (reader.Read())
                {
                    transactionStatusList.Add(reader.GetString(0));
                }
            }
            return transactionStatusList;
        }

        internal void ActivateStrongPassowordToken()
        {
            string query = "update pmConfiguration_tb set configurationValue = 1 where configurationKey = 'EnableStrongPassword'";
            ExecuteNonQuery(query, false);
        }

        internal void DeactivateStrongPassowordToken()
        {
            string query = "update pmConfiguration_tb set configurationValue = 0 where configurationKey = 'EnableStrongPassword'";
            ExecuteNonQuery(query, false);
        }

        internal bool ToggleLookupTbToken(string tokenName, bool doActivate)
        {
            string query = "update lookUp_tb set isActive = @doActivate where name = @tokenName;";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@tokenName", tokenName),
                new SqlParameter("@doActivate", doActivate)
            };
            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal List<EntityDetails> GetActiveLocations(LocationType locationType, UserType userType)
        {
            var query = @"select * from entitydetails_tb where locationTypeId=@locationTypeId and entityTypeId=@entityTypeId and isactive=1";
            List<SqlParameter> sp = new();
            sp.Add(new SqlParameter("@locationTypeId", locationType.Value));
            sp.Add(new SqlParameter("@entityTypeId", userType.Value));
            List<EntityDetails> entityDetailsList = new List<EntityDetails>();
            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader != null)
                {
                    entityDetailsList = new List<EntityDetails>();
                    while (reader.Read())
                    {
                        entityDetailsList.Add(new EntityDetails()
                        {
                            DisplayName = reader.GetStringValue(reader.GetReaderIndex("displayName")),
                            CorcentricCode = reader.GetStringValue(reader.GetReaderIndex("corcentricCode")),
                            EntityDetailId = reader.GetInt32(reader.GetReaderIndex("entityDetailId"))
                        });
                    }
                }
            }
            return entityDetailsList;
        }

        internal List<EntityDetails> GetActiveLocations(LocationType locationType, string userName)
        {
            var query = @"declare @Userid as int
                declare @AccessLocations table(
                    entityDetailId    INT  primary key
                )

                select @Userid = userid from user_tb where username = @userName;
                WITH RootNumber AS(
                SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb  WITH(NOLOCK)
                WHERE entityDetailId iN(SELECT DISTINCT userRelationships_tb.entityId
                FROM userRelationships_tb WITH(NOLOCK) INNER JOIN user_tb   WITH(NOLOCK)
                ON userRelationships_tb.userId = user_tb.userId
                WHERE user_tb.userId = @UserID
                AND userRelationships_tb.IsActive = 1
                AND userRelationships_tb.hasHierarchyAccess = 1)
                UNION ALL
                SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P
                INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId)
                insert into @AccessLocations
                    
                SELECT entityDetailId FROM RootNumber where parentEntityDetailId<> entityDetailId   and parentEntityDetailId<>0
                UNION
                SELECT userRelationships_tb.entityId As entityDetailId FROM userRelationships_tb
                WITH(NOLOCK) WHERE userRelationships_tb.userId = @UserID  and IsActive = 1 AND userRelationships_tb.entityId IS NOT NULL
                select* from entitydetails_tb where entitydetailid in(Select entityDetailID from @AccessLocations) and locationtypeid = @locationTypeId";

            List<SqlParameter> sp = new();
            sp.Add(new SqlParameter("@locationTypeId", locationType.Value));
            sp.Add(new SqlParameter("@userName", userName));
            List<EntityDetails> entityDetailsList = new List<EntityDetails>();

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader != null)
                {
                    entityDetailsList = new List<EntityDetails>();
                    while (reader.Read())
                    {
                        entityDetailsList.Add(new EntityDetails()
                        {
                            DisplayName = reader.GetStringValue(reader.GetReaderIndex("displayName")),
                            CorcentricCode = reader.GetStringValue(reader.GetReaderIndex("corcentricCode")),
                            EntityDetailId = reader.GetInt32(reader.GetReaderIndex("entityDetailId"))
                        });
                    }
                }
            }
            return entityDetailsList;
        }

        internal void RunResubmitDiscrepanciesJob()
        {
            string query = "Exec usp_resubmitDiscrepancies";

            ExecuteNonQuery(query, false);

        }

        internal string GetInvoiceNumberForCurrentTransaction()
        {
            string query = null;

            try
            {
                query = @"select top 1 transactionNumber from invoice_tb order by 1 desc";

                using (var reader = ExecuteReader(query, false))
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

        internal string GetInvoiceNumberForDisputeCreation()
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
                    query = @"select top 1 inv.invoicenumber from invoice_tb inv with (NOLOCK)
                        where inv.transactionId not in (select transactionId from transactionDisputes_tb) and 
                        inv.transactionId not in (select transactionId from transactionHold_tb) and
                        (inv.transactionId not in (select srcTransactionId from transactionMap_tb) and 
                        inv.transactionId not in (select destTransactionId from transactionMap_tb)) and
                        inv.createDate > GETDATE()-720
                        order by inv.invoiceId desc";

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
                    query = @"DECLARE @@Userid AS INT; DECLARE @@FleetAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid = userid FROM user_tb WHERE
                            username = @UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM entityDetails_tb WITH(NOLOCK) WHERE entityDetailId IN(SELECT DISTINCT U.entityId FROM userRelationships_tb U WITH(NOLOCK) INNER JOIN user_tb US WITH(NOLOCK) ON U.userId = US.userId WHERE US.userId = @@UserID AND U.IsActive = 1 AND U.hasHierarchyAccess = 1)
                            UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN entityDetails_tb AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@FleetAccessLocations SELECT entityDetailId FROM RootNumber WHERE parentEntityDetailId<> entityDetailId AND parentEntityDetailId<> 0
                            UNION SELECT U2.entityId As entityDetailId FROM userRelationships_tb U2 WITH(NOLOCK) WHERE U2.userId = @@UserID AND IsActive = 1 AND U2.entityId IS NOT NULL;  					
select top 1 inv.invoicenumber from invoice_tb inv with (NOLOCK) where inv.transactionId not in (select transactionId from transactionDisputes_tb) and 
                        inv.transactionId not in (select transactionId from transactionHold_tb) and
                        (inv.transactionId not in (select srcTransactionId from transactionMap_tb) and 
                        inv.transactionId not in (select destTransactionId from transactionMap_tb)) and
                        inv.createDate > GETDATE()-720 AND receiverEntityDetailId IN(SELECT entityDetailId FROM @@FleetAccessLocations) AND ISNULL(inv.systemType,0) <> 2
                        order by inv.invoiceId desc";
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

                return null;
            }

            return null;
        }


        internal string GetSettledInvoice()
        {
            string query = null;

            try
            {
                query = @"select top 1 invoicenumber from invoice_tb where isActive = 1";

                using (var reader = ExecuteReader(query, false))
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
        internal string GetCustomizedDateDays()
        {
            var query = @"select value from displaypref_user where displaypreftypeid = 4  and u_id in (select U_Id from webcore_uid where userId='testsupport')";


            try
            {
                using (var reader = ExecuteReader(query, true))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return null;

        }

        internal string GetCustomizedDate()
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
                    query = @"select value from displaypref_user where displaypreftypeid = 4  and u_id in (select U_Id from webcore_uid where userId=@UserName)";

                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", applicationContext.ApplicationContext.GetInstance().UserData.User)
                    };

                    using (var reader = ExecuteReader(query, sp,true))
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return null;

        }


        internal int GetSystemType(string invoiceNumber)
        {
            var query = @"select systemType from invoice_tb where transactionNumber = @invNumber";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@invNumber", invoiceNumber)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    return reader.GetInt16(0);
                }
            }
            return -1;
        }
        internal bool GetExportToAccounting(string invoiceNumber)
        {
            var query = @"select exportedToAcccounting from invoice_tb where transactionNumber = @invNumber";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@invNumber", invoiceNumber)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    return reader.GetBoolean(0);
                }
            }
            return false;
        }

        internal decimal GetAvailableCreditLimit(string fleetName)
        {
            var query = @"select availableCreditLimit,* from entityDetails_tb where displayName = @fleetName";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetName", fleetName)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    return reader.GetDecimal(0);
                }
            }
            return -1;
        }

        internal int GetValidationStatus(string invNumber)
        {
            string query = "select validationStatus from transaction_tb where transactionnumber=@invNumber";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@invNumber", invNumber)
            };
            using (var reader = ExecuteReader(query,sp, false))
            {
                while (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }
            return -1;
        }
        internal bool ValidateTransactionError(string invNumber, string errorMsg)
        {
            string query = "select te.description from transaction_tb t inner join transactionError_tb te on t.transactionId = te.transactionId where t.transactionNumber =@invNumber and te.description LIKE @errorMsg +'%'";
            int count=0;
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@invNumber", invNumber),
                new SqlParameter("@errorMsg", errorMsg)
            };
            using (var reader = ExecuteReader(query,sp, false))
            {
                while (reader.Read())
                {
                    count++;
                }

            }
            return count>0;

        }

        internal bool ValidateTransactionErrorToken(string invNumber, string errorMsg)
        {
            string query = "select te.isActive from transaction_tb t inner join transactionError_tb te on t.transactionId = te.transactionId where t.transactionNumber =@invNumber and te.description LIKE @errorMsg +'%' and te.isActive = 1";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@invNumber", invNumber),
                new SqlParameter("@errorMsg", errorMsg)
            };
            using (var reader = ExecuteReader(query, sp, false))
            {
                return true;

            }
            return false;
        }

        internal string GetDiscrepantInvoice(string discrepantState, string dealer, string fleet)
        {
            string query = null;
            List<SqlParameter> sp = new();
            if (discrepantState == "Unit Number")
            {
                query = @"select top 1 T.transactionNumber from transaction_tb T
                        inner join transactionError_tb TE on T.transactionid = TE.transactionid
                        inner join errorcode_tb E on TE.errorcodeid = E.errorcodeid
                        where T.senderbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @dealer) 
                        and T.receiverbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @fleet) and E.errorcodeid = 12 and T.requestTypeCode ='S' and T.isHistorical = 0 and TE.isActive = 1 and T.createDate BETWEEN GETDATE()-59 and GETDATE() order by T.createDate desc";

                sp.Add(new SqlParameter("@dealer", dealer));
                sp.Add(new SqlParameter("@fleet", fleet));

            }
            else if (discrepantState == "Credit Not Available")
            {
                query = @"select top 1 T.transactionNumber from transaction_tb T
                        inner join transactionError_tb TE on T.transactionid = TE.transactionid
                        inner join errorcode_tb E on TE.errorcodeid = E.errorcodeid
                        where T.senderbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @dealer) 
                        and T.receiverbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @fleet) and E.errorcodeid = 44  and T.requestTypeCode ='S' and T.isHistorical = 0 and TE.isActive = 1 and T.createDate BETWEEN GETDATE()-59 and GETDATE() order by T.createDate desc";

                sp.Add(new SqlParameter("@dealer", dealer));
                sp.Add(new SqlParameter("@fleet", fleet));
            }
            else if (discrepantState == "On hold for physical copy")
            {
                query = @"select top 1 T.transactionNumber from transaction_tb T
                        inner join transactionError_tb TE on T.transactionid = TE.transactionid
                        inner join errorcode_tb E on TE.errorcodeid = E.errorcodeid
                        where T.senderbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @dealer) 
                        and T.receiverbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @fleet) and E.errorcodeid = 104  and T.requestTypeCode ='S' and T.isHistorical = 0 and TE.isActive = 1 and T.createDate BETWEEN GETDATE()-59 and GETDATE() order by T.createDate desc";

                sp.Add(new SqlParameter("@dealer", dealer));
                sp.Add(new SqlParameter("@fleet", fleet));
            }
            else if (discrepantState == "Awaiting Fleet Release")
            {
                query = @"select top 1 T.transactionNumber from transaction_tb T
                        inner join transactionError_tb TE on T.transactionid = TE.transactionid
                        inner join errorcode_tb E on TE.errorcodeid = E.errorcodeid
                        where T.senderbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @dealer) 
                        and T.receiverbilltoentitydetailid = (Select entitydetailID from entitydetails_tb where corcentricCode = @fleet) and E.errorcodeid = 1335 and T.requestTypeCode ='S' and T.isHistorical = 0 and TE.isActive = 1 and T.createDate BETWEEN GETDATE()-59 and GETDATE() order by T.createDate desc";

                sp.Add(new SqlParameter("@dealer", dealer));
                sp.Add(new SqlParameter("@fleet", fleet));
            }
            else if (discrepantState == "Dealer Code Invalid")
            {
                query = @"select top 1 T.transactionNumber from transaction_tb T
                        inner join transactionError_tb TE on T.transactionid = TE.transactionid
                        inner join errorcode_tb E on TE.errorcodeid = E.errorcodeid
                        where T.receiverEntityDetailId = (Select entitydetailID from entitydetails_tb where corcentricCode = @fleet) 
						and E.errorcodeid = 1 and T.requestTypeCode ='S' and T.isHistorical = 0 and TE.isActive = 1 and T.createDate BETWEEN GETDATE()-59 and GETDATE() order by T.createDate desc";

                sp.Add(new SqlParameter("@fleet", fleet));
            }
            using (var reader = ExecuteReader(query, sp, false))
            {
                while (reader.Read())
                {
                    return reader.GetString(0);
                }

            }

            return null;
        }

        internal void MoveInvoiceToHistory(string dealerInv)
        {
            string query = "update transaction_tb set isHistorical = 1 where transactionNumber =@dealerInv";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerInv", dealerInv)
            };
            ExecuteNonQuery(query, sp, false);

        }

        internal string GetInvoiceExpirationDate(string invNumber)
        {
            string dateTime;
            string query = "select expirationDate from transaction_tb where transactionnumber= '" + invNumber + "'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    dateTime = reader.IsDBNull(0) ? "" : CommonUtils.ConvertDate(reader.GetDateTime(0));
                    return dateTime;
                }
            }

            return CommonUtils.GetCurrentDate();

        }

        internal string GetInvoiceTransactionDate(string invNumber)
        {
            string dateTime;
            string query = "select transactionDate from transaction_tb where transactionNumber= '" + invNumber + "'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    dateTime = reader.IsDBNull(0) ? "" : CommonUtils.ConvertDate(reader.GetDateTime(0));
                    return dateTime;
                }
            }

            return null;
        }

        internal void UpdateInvoiceValidityDays(int validityDays)
        {
            string query = "update community_tb set invoiceValidityDays = '" + validityDays + "' where id = 1";

            ExecuteNonQuery(query, false);

        }

        internal int GetEntityId(string corCentricCode)
        {
            var query = @"select entityDetailId from entityDetails_tb where corcentricCode = '" + corCentricCode + "'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            return -1;
        }

        internal string GetSubcommunityforEntity(string corCentricCode)
        {
            var query = @"select subCommunityName from subCommunity_tb sc join entityDetails_tb ed on  sc.subCommunityId = ed.subCommunityId where ed.corcentricCode = '"+ corCentricCode + "'";

            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;
            }

            return string.Empty;
        }

        internal string GetDealerCodeforLocationType(string locationType)
        {
            var query = @"select top 1 corcentricCode from entityDetails_tb where locationTypeId IN (select Top 1 lookUpId from lookup_tb where name = '"+ locationType + "') AND subCommunityId = 0 and entityTypeId = 2 order by createdDate desc";

            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;
            }

            return string.Empty;
        }

        internal string GetFleetCodeforLocationType(string locationType)
        {
            var query = @"select top 1 corcentricCode from entityDetails_tb where locationTypeId IN (select Top 1 lookUpId from lookup_tb where name = '" + locationType + "') AND subCommunityId = 0 and entityTypeId = 3 order by createdDate desc";

            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;
            }

            return string.Empty;
        }
        internal bool GetMultilingualtoken()
        {

            bool isActive = false;
            string query = "select isActive from lookUp_tb where parentlookupcode=100 and lookupcode=243";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    isActive = reader.GetBoolean(0);


                }
            }
            return isActive;

        }

        internal int GetCommunityIDOfClient()
        {
            string query = "Select top 1 communityID from community_tb";



            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }
            return -1;
        }

        internal int GetInvoiceTransactionAmount(string invNumber)
        {
            string query = "select transactionAmount from transaction_tb where transactionNumber= '" + invNumber + "'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return Convert.ToInt32(reader.GetDecimal(0));
                }
            }

            return -1;
        }

        internal void UpdateCreditLimitVarianceThreshHoldPct(int percent , int clientid)
            { 
            string query = "update community_tb set creditLimitVarianceThreshHoldPct = '" + percent + "' where communityid = '" + clientid + "'";
            ExecuteNonQuery(query, false);
            }

        internal void ToggleEntityActivationFlag(string corcentricCode, bool activate)
        {
            string query = "update entitydetails_tb set isactive=@activate where corcentriccode= '" + corcentricCode + "'";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@activate", activate)
            };
            ExecuteNonQuery(query, sp, false);
        }

        internal void ToggleEntityTerminationFlag(string corcentricCode, bool activate)
        {
            string query = "update entitydetails_tb set isterminated=@activate where corcentriccode= '" + corcentricCode + "'";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@activate", activate)
            };
            ExecuteNonQuery(query, sp, false);
        }

        internal void ToggleEntitySuspensionRelationship(string corcentricCode, bool activate)
        {
            string query = "update relsenderreceiver_tb set isactive=@activate where receiverid in (select entitydetailid from  entitydetails_tb where corcentriccode='" + corcentricCode + "') and relationshiptypeid in (select lookUpId from lookUp_tb where name='Suspension Status')";
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@activate", activate)
            };
            ExecuteNonQuery(query, sp, false);
        }

        internal TransactionDetails GetAuthTrasactionDetails(string dealerCode, string fleetCode)
        {
            TransactionDetails authDetails = null;
            try
            {
                string spName = "select top 1 transactionNumber,authorizationCode from transaction_tb where validationStatus=1 and requestTypeCode='A' and senderCorcentricCode = @dealerCorcentricCode and receiverCorcentricCode=@fleetCorcentricCode order by transactionid desc";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@dealerCorcentricCode", dealerCode),
                    new SqlParameter("@fleetCorcentricCode", fleetCode),

                };

                using (var reader = ExecuteReader(spName, sp, false))

                {
                    if (reader.Read())
                    {
                        authDetails = new TransactionDetails();
                        authDetails.TransactionNumber = reader.GetStringValue("transactionNumber");
                        authDetails.AuthCode = reader.GetStringValue("authorizationCode");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return authDetails;
        }
    }
}