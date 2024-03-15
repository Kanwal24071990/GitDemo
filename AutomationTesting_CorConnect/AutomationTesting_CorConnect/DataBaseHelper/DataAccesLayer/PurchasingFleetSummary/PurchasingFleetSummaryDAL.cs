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

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PurchasingFleetSummary
{
    internal class PurchasingFleetSummaryDAL : BaseDataAccessLayer
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
                    query = @"SELECT TOP 1 Convert (Date , invoicedate) AS FromDate, Convert (Date , invoicedate) AS ToDate FROM [part_tb] PT INNER JOIN [partCategoryCode_tb] PCC ON (PT.categoryCode1Id = PCC.partCategoryCodeId) RIGHT OUTER JOIN [Invoice_tb] IV INNER JOIN [invoiceSection_tb] ON (IV.invoiceId = invoiceSection_tb.invoiceId) INNER JOIN [invoiceLineDetail_tb] ID ON (invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId) ON (PT.partId = ID.itemId) WHERE(ISNULL(ID.accountingDocumentTypeId, 0) = 0 OR ID.accountingDocumentTypeId = (SELECT lookUpId FROM [lookup_tb] WITH (NOLOCK) WHERE parentLookUpCode = 218 AND lookupcode = 1)) AND ID.rebateType IN(0, 2) AND IV.isActive = 1 ORDER BY invoiceDate DESC;";

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
                    query = @"DECLARE @@Userid as int DECLARE @@FleetAccessLocations table(entityDetailId INT primary key) SELECT @@Userid = userid FROM [user_tb] WHERE username = 'ArgentinaQAFleet'; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId iN(SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK) INNER JOIN [user_tb] WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID  AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT entityDetailId  FROM  RootNumber  WHERE  parentEntityDetailId <> entityDetailId  and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId  FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; Select  top 1 Convert(Date, invoicedate) as FromDate, Convert(Date, invoicedate) as ToDate  FROM [part_tb] as PT INNER JOIN [partCategoryCode_tb] ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId RIGHT OUTER JOIN [Invoice_tb] as IV INNER JOIN  [invoiceSection_tb] ON IV.invoiceId = invoiceSection_tb.invoiceId INNER JOIN [invoiceLineDetail_tb] as ID ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId ON PT.partId = ID.itemId INNER JOIN @@FleetAccessLocations AE on AE.Entitydetailid= iv.receiverEntityDetailId WHERE(ISNULL(ID.accountingDocumentTypeId, 0) = 0 OR ID.accountingDocumentTypeId = (Select lookUpId from [lookup_tb] WITH(NOLOCK) WHERE parentLookUpCode = 218 and lookupcode = 1)) AND ID.rebateType IN (0,2) and iv.isActive = 1 order by invoiceDate desc;";
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

        internal void GetTotalSales(out string totalSales, string caseType, string dealerCode, string fleetCode, string fromDate, string toDate)
        {
            totalSales = string.Empty;

            string query = null;

            try
            {
                switch (caseType)
                {
                    case "Sublet Total":
                        query = @"select CAST(sum (case
                       when ID.lineDetailType in ('U', 'B')
                           then ROUND(ID.quantity * (ID.unitPrice + isnull(ID.corePrice, 0)), 2)
                       else 0 end) as decimal (18,2)) as 'Total sales'
                     FROM invoice_tb IV
                     INNER JOIN invoiceSection_tb WITH (NOLOCK)
                                ON IV.invoiceId = invoiceSection_tb.invoiceId
                     left JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                     LEFT OUTER JOIN part_tb AS PT WITH (NOLOCK)
                     INNER JOIN partCategoryCode_tb WITH (NOLOCK)
                                ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId
                                ON ID.itemId = PT.partId
                    left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                    left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                    where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;
                    case "Shop Supplies Total":
                        query = @"select CAST(sum (case
                       when ID.lineDetailType = 'S'
                           then ROUND(ID.quantity * (ID.unitPrice + isnull(ID.corePrice, 0)), 2)
                       else 0 end) as decimal (18,2)) as 'Total sales'
                     FROM invoice_tb IV
                     INNER JOIN invoiceSection_tb WITH (NOLOCK)
                                ON IV.invoiceId = invoiceSection_tb.invoiceId
                     left JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                     LEFT OUTER JOIN part_tb AS PT WITH (NOLOCK)
                     INNER JOIN partCategoryCode_tb WITH (NOLOCK)
                                ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId
                                ON ID.itemId = PT.partId
                    left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                    left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                    where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;
                    case "Frieght Total":
                        query = @"select CAST(sum (case
                       when ID.lineDetailType = 'F'
                           then ROUND(ID.quantity * (ID.unitPrice + isnull(ID.corePrice, 0)), 2)
                       else 0 end) as decimal (18,2)) as 'Total sales'
                     FROM invoice_tb IV
                     INNER JOIN invoiceSection_tb WITH (NOLOCK)
                                ON IV.invoiceId = invoiceSection_tb.invoiceId
                     left JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                     LEFT OUTER JOIN part_tb AS PT WITH (NOLOCK)
                     INNER JOIN partCategoryCode_tb WITH (NOLOCK)
                                ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId
                                ON ID.itemId = PT.partId
                    left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                    left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                   where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;
                    case "Labor Total":
                        query = @"select CAST(sum (case
                       when ID.lineDetailType = 'L'
                           then ROUND(ID.quantity * (ID.unitPrice + isnull(ID.corePrice, 0)), 2)
                       else 0 end) as decimal (18,2)) as 'Total sales'
                     FROM invoice_tb IV
                     INNER JOIN invoiceSection_tb WITH (NOLOCK)
                                ON IV.invoiceId = invoiceSection_tb.invoiceId
                     left JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                     LEFT OUTER JOIN part_tb AS PT WITH (NOLOCK)
                     INNER JOIN partCategoryCode_tb WITH (NOLOCK)
                                ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId
                                ON ID.itemId = PT.partId
                    left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                    left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                   where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;
                    case "Other Total":
                        query = @"select CAST(sum (case
                        when ID.lineDetailType <> 'L'
                        and (ID.lineDetailType = 'P' and ISNULL(partCategoryCode_tb.categoryDescription, '') not in ('Vendor', 'Proprietary','Engine'))
                        and ID.lineDetailType <> 'F'
                        and ID.lineDetailType <> 'U'
                        and ID.lineDetailType <> 'S'
                        and ID.lineDetailType <> 'B'
                        then ROUND(ID.quantity * (ID.unitPrice + isnull(ID.corePrice, 0)), 2)
                       else 0 end) as decimal (18,2)) as 'Total sales'
                     FROM invoice_tb IV
                     INNER JOIN invoiceSection_tb WITH (NOLOCK)
                                ON IV.invoiceId = invoiceSection_tb.invoiceId
                     left JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                     LEFT OUTER JOIN part_tb AS PT WITH (NOLOCK)
                     INNER JOIN partCategoryCode_tb WITH (NOLOCK)
                                ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId
                                ON ID.itemId = PT.partId
                    left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                    left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                    where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;
                    case "Tax Total":
                        query = @"SELECT CAST(sum(it.Amount+id.fetamount) as decimal (18,2))  as tax FROM invoice_tb iv WITH (NOLOCK)
            INNER JOIN invoiceTax_tb it WITH (NOLOCK) on it.invoiceid = iv.invoiceid
            INNER JOIN invoiceSection_tb WITH (NOLOCK) ON IV.invoiceId = invoiceSection_tb.invoiceId
                     INNER JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                    left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;
                    case "Engine Total":
                        query = @"select cast(sum(case
                               when ID.lineDetailType = 'P' and partCategoryCode_tb.categoryDescription = 'Engine'
                                   Then id.quantity * id.unitPrice
                               else 0 end) as decimal (18,2)) as 'Parts Total sales'
                             FROM invoice_tb IV
                             INNER JOIN invoiceSection_tb WITH (NOLOCK)
                             ON IV.invoiceId = invoiceSection_tb.invoiceId
                             left JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                        ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                             LEFT OUTER JOIN part_tb AS PT WITH (NOLOCK)
                             INNER JOIN partCategoryCode_tb WITH (NOLOCK)
                                        ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId
                                        ON ID.itemId = PT.partId
                            left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                            left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                         where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and  sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;
                    case "Proprietary Total":
                        query = @"select cast(sum(case
                               when ID.lineDetailType = 'P' and partCategoryCode_tb.categoryDescription = 'Proprietary'
                                   Then id.quantity * id.unitPrice
                               else 0 end) as decimal (18,2)) as 'Parts Total sales'
                             FROM invoice_tb IV
                             INNER JOIN invoiceSection_tb WITH (NOLOCK)
                             ON IV.invoiceId = invoiceSection_tb.invoiceId
                             left JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                        ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                             LEFT OUTER JOIN part_tb AS PT WITH (NOLOCK)
                             INNER JOIN partCategoryCode_tb WITH (NOLOCK)
                                        ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId
                                        ON ID.itemId = PT.partId
                            left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                            left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                         where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and  sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;
                    case "Vendor Total":
                        query = @"select cast(sum(case
                               when ID.lineDetailType = 'P' and partCategoryCode_tb.categoryDescription = 'Vendor'
                                   Then id.quantity * id.unitPrice
                               else 0 end) as decimal (18,2)) as 'Parts Total sales'
                             FROM invoice_tb IV
                             INNER JOIN invoiceSection_tb WITH (NOLOCK)
                             ON IV.invoiceId = invoiceSection_tb.invoiceId
                             left JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                        ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                             LEFT OUTER JOIN part_tb AS PT WITH (NOLOCK)
                             INNER JOIN partCategoryCode_tb WITH (NOLOCK)
                                        ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId
                                        ON ID.itemId = PT.partId
                            left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                            left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                         where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and  sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;
                    case "Unrecognized Total":
                        query = @"select cast(sum(case
                               when ID.lineDetailType = 'P' and isnull(id.itemid, 0) = 0
                                   Then id.quantity * id.unitPrice
                               else 0 end) as decimal (18,2)) as 'Unrecognized Total sales'
                             FROM invoice_tb IV
                             INNER JOIN invoiceSection_tb WITH (NOLOCK)
                             ON IV.invoiceId = invoiceSection_tb.invoiceId
                             left JOIN invoiceLineDetail_tb AS ID WITH (NOLOCK)
                                        ON invoiceSection_tb.invoiceSectionId = ID.invoiceSectionId
                             LEFT OUTER JOIN part_tb AS PT WITH (NOLOCK)
                             INNER JOIN partCategoryCode_tb WITH (NOLOCK)
                                        ON PT.categoryCode1Id = partCategoryCode_tb.partCategoryCodeId
                                        ON ID.itemId = PT.partId
                            left JOIN entityDetails_tb res WITH (NOLOCK) on res.entityDetailId = receiverEntityDetailId
                            left JOIN entityDetails_tb sen WITH (NOLOCK) on sen.entityDetailId = senderEntityDetailId
                         where iv.invoicedate between '" + fromDate + "' and '" + toDate + "' and  sen.corcentriccode='" + dealerCode + "' and  res.corcentriccode='" + fleetCode + "';";

                        using (var reader = ExecuteReader(query, false))
                        {
                            if (reader.Read())
                            {
                                totalSales = reader.GetDecimal(0).ToString();

                            }
                        }
                        break;

                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                totalSales = null;
            }
        }

        internal void GetTotalInvoiceCount(out string totalInvCount, string dealerCode,string fromDate, string toDate)
        {
            totalInvCount = string.Empty;

            string query = null;

            try
            {
                query = @"select count(*) from invoice_tb where senderentitydetailid in (select entitydetailid from entitydetails_tb where corcentriccode='"+dealerCode+"') and invoiceDate between '"+fromDate+"' and '"+toDate+"'";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        totalInvCount = reader.GetInt32(0).ToString();

                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                totalInvCount = null;
            }
        }
    }
}
