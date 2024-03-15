using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartPurchasesReport
{
    internal class PartPurchasesReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                if (userType == "ADMIN")
                {
                    query = @"SELECT top 1 Convert (Date , INV.ARinvoiceDate) as FromDate ,Convert (Date , INV.ARinvoiceDate) as ToDate FROM [invoice_tb] INV with (nolock) INNER JOIN [entityDetails_tb] ED WITH (NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId INNER JOIN [entitydetails_tb] s WITH (NOLOCK) on s.entitydetailid=INV.senderEntityDetailId INNER JOIN [entityAddressRel_tb] EAR WITH (NOLOCK) ON ED.entityDetailId = EAR.entityDetailId INNER JOIN [address_tb] AT WITH (NOLOCK) ON AT.addressId = EAR.addressId INNER JOIN [invoiceSection_tb] IST WITH (NOLOCK) ON INV.invoiceId = IST.invoiceId INNER JOIN [invoiceLineDetail_tb] ILD WITH (NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId LEFT JOIN [actionComments_tb] AC WITH (NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1 WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from [lookup_tb] where parentlookupcode=218 and lookupcode=1)) AND ILD.lineDetailType IN ('P') AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' ORDER BY INV.ARinvoiceDate DESC";

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
                    query = @"declare @@Userid as int declare @@FleetAccessLocations table ( entityDetailId INT primary key ) select @@Userid=userid from [user_tb] where username=@UserName; WITH RootNumber AS ( SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK) INNER JOIN [user_tb] WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId and parentEntityDetailId <>0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; select top 1 Convert (Date , INV.ARinvoiceDate) as FromDate ,Convert (Date , INV.ARinvoiceDate) as ToDate FROM [invoice_tb] INV with (nolock) INNER JOIN [entityDetails_tb] ED WITH (NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId inner join [entitydetails_tb] s WITH (NOLOCK) on s.entitydetailid=INV.senderEntityDetailId inner join @@FleetAccessLocations d on d.entitydetailid=inv.receiverEntityDetailId INNER JOIN [entityAddressRel_tb] EAR WITH (NOLOCK) ON ED.entityDetailId = EAR.entityDetailId INNER JOIN [address_tb] AT WITH (NOLOCK) ON AT.addressId = EAR.addressId INNER JOIN [invoiceSection_tb] IST WITH (NOLOCK) ON INV.invoiceId = IST.invoiceId INNER JOIN [invoiceLineDetail_tb] ILD WITH (NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId LEFT JOIN [actionComments_tb] AC WITH (NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1 WHERE (ILD.accountingDocumentTypeId =0 or ILD.accountingDocumentTypeId in (select lookupid from [lookup_tb] where parentlookupcode=218 and lookupcode=1)) AND ILD.lineDetailType IN ('P') AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' Order by INV.ARinvoiceDate desc";
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

        internal string GetDealerCompanyName(string entityType)
        {
            string query = null;
            if (entityType.ToLower() == "admin")
            {
                query = @"SELECT top 1 s.corcentricCode as dealerCode FROM [invoice_tb] INV with (nolock) INNER JOIN [entityDetails_tb] ED WITH (NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId INNER JOIN [entitydetails_tb] s WITH (NOLOCK) on s.entitydetailid=INV.senderEntityDetailId INNER JOIN [entityAddressRel_tb] EAR WITH (NOLOCK) ON ED.entityDetailId = EAR.entityDetailId INNER JOIN [address_tb] AT WITH (NOLOCK) ON AT.addressId = EAR.addressId INNER JOIN [invoiceSection_tb] IST WITH (NOLOCK) ON INV.invoiceId = IST.invoiceId INNER JOIN [invoiceLineDetail_tb] ILD WITH (NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId LEFT JOIN [actionComments_tb] AC WITH (NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1 WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from [lookup_tb] where parentlookupcode=218 and lookupcode=1)) AND ILD.lineDetailType IN ('P') AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' ORDER BY INV.ARinvoiceDate DESC";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }

            else if (entityType.ToLower() == "fleet")
            {
                query = @"declare @@Userid as int declare @@FleetAccessLocations table ( entityDetailId INT primary key ) select @@Userid=userid from [user_tb] where username=@UserName; WITH RootNumber AS ( SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK) INNER JOIN [user_tb] WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId and parentEntityDetailId <>0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; select top 1 s.corcentricCode as dealerCode, Convert (Date , INV.ARinvoiceDate) as FromDate ,Convert (Date , INV.ARinvoiceDate) as ToDate FROM [invoice_tb] INV with (nolock) INNER JOIN [entityDetails_tb] ED WITH (NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId inner join [entitydetails_tb] s WITH (NOLOCK) on s.entitydetailid=INV.senderEntityDetailId inner join @@FleetAccessLocations d on d.entitydetailid=inv.receiverEntityDetailId INNER JOIN [entityAddressRel_tb] EAR WITH (NOLOCK) ON ED.entityDetailId = EAR.entityDetailId INNER JOIN [address_tb] AT WITH (NOLOCK) ON AT.addressId = EAR.addressId INNER JOIN [invoiceSection_tb] IST WITH (NOLOCK) ON INV.invoiceId = IST.invoiceId INNER JOIN [invoiceLineDetail_tb] ILD WITH (NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId LEFT JOIN [actionComments_tb] AC WITH (NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1 WHERE (ILD.accountingDocumentTypeId =0 or ILD.accountingDocumentTypeId in (select lookupid from [lookup_tb] where parentlookupcode=218 and lookupcode=1)) AND ILD.lineDetailType IN ('P') AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' Order by INV.ARinvoiceDate desc";
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
            return string.Empty;
        }

        internal string GetFleetCompanyName(string entityType)
        {
            string query = null;
            if (entityType.ToLower() == "admin")
            {
                query = @"SELECT top 1 ED.corcentricCode as FleetCode FROM [invoice_tb] INV with (nolock) INNER JOIN [entityDetails_tb] ED WITH (NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId INNER JOIN [entitydetails_tb] s WITH (NOLOCK) on s.entitydetailid=INV.senderEntityDetailId INNER JOIN [entityAddressRel_tb] EAR WITH (NOLOCK) ON ED.entityDetailId = EAR.entityDetailId INNER JOIN [address_tb] AT WITH (NOLOCK) ON AT.addressId = EAR.addressId INNER JOIN [invoiceSection_tb] IST WITH (NOLOCK) ON INV.invoiceId = IST.invoiceId INNER JOIN [invoiceLineDetail_tb] ILD WITH (NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId LEFT JOIN [actionComments_tb] AC WITH (NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1 WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from [lookup_tb] where parentlookupcode=218 and lookupcode=1)) AND ILD.lineDetailType IN ('P') AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' ORDER BY INV.ARinvoiceDate DESC";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }

            else if (entityType.ToLower() == "fleet")
            {
                query = @"declare @@Userid as int declare @@FleetAccessLocations table ( entityDetailId INT primary key ) select @@Userid=userid from [user_tb] where username=@UserName; WITH RootNumber AS ( SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK) INNER JOIN [user_tb] WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId <> entityDetailId and parentEntityDetailId <>0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; select top 1 ED.corcentricCode as FleetCode ,Convert (Date , INV.ARinvoiceDate) as FromDate ,Convert (Date , INV.ARinvoiceDate) as ToDate FROM [invoice_tb] INV with (nolock) INNER JOIN [entityDetails_tb] ED WITH (NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId inner join [entitydetails_tb] s WITH (NOLOCK) on s.entitydetailid=INV.senderEntityDetailId inner join @@FleetAccessLocations d on d.entitydetailid=inv.receiverEntityDetailId INNER JOIN [entityAddressRel_tb] EAR WITH (NOLOCK) ON ED.entityDetailId = EAR.entityDetailId INNER JOIN [address_tb] AT WITH (NOLOCK) ON AT.addressId = EAR.addressId INNER JOIN [invoiceSection_tb] IST WITH (NOLOCK) ON INV.invoiceId = IST.invoiceId INNER JOIN [invoiceLineDetail_tb] ILD WITH (NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId LEFT JOIN [actionComments_tb] AC WITH (NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1 WHERE (ILD.accountingDocumentTypeId =0 or ILD.accountingDocumentTypeId in (select lookupid from [lookup_tb] where parentlookupcode=218 and lookupcode=1)) AND ILD.lineDetailType IN ('P') AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' Order by INV.ARinvoiceDate desc";
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
            return string.Empty;
        }
        internal int GetCountByDateRange(string dateRange, int days = 0)
        {

            string query = null;
            string fromDateTime = null;

            if (dateRange == "Last 7 days")
            {
                fromDateTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                query = @"select COUNT(*) FROM invoice_tb INV with (nolock)
                INNER JOIN entityDetails_tb ED WITH(NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId
                inner join entitydetails_tb s WITH(NOLOCK) on s.entitydetailid = INV.senderEntityDetailId
                INNER JOIN entityAddressRel_tb EAR WITH(NOLOCK) ON ED.entityDetailId = EAR.entityDetailId
                INNER JOIN address_tb AT WITH(NOLOCK) ON AT.addressId = EAR.addressId
                INNER JOIN invoiceSection_tb IST WITH(NOLOCK) ON INV.invoiceId = IST.invoiceId
                INNER JOIN invoiceLineDetail_tb ILD WITH(NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId
                LEFT JOIN actionComments_tb AC WITH(NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1
                WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from lookup_tb where parentlookupcode = 218 and lookupcode = 1)) AND ILD.lineDetailType IN('P')
                AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' AND INV.ARinvoicedate BETWEEN @fromDateTime AND GETDATE() AND s.corcentriccode = '18AutoDlr' AND ED.corcentriccode = '18AutoFlt'";

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

            else if (dateRange == "Current Quarter")
            {
                DateTime currentDate = DateTime.Now;
                int currentQuarter = (currentDate.Month - 1) / 3 + 1;
                fromDateTime = new DateTime(currentDate.Year, 3 * currentQuarter - 2, 1).ToString("yyyy-MM-dd");
                var toDateTime = Convert.ToDateTime(fromDateTime).AddMonths(3).AddDays(-1).ToString("yyyy-MM-dd");
                Console.WriteLine("From Date: " + fromDateTime);
                Console.WriteLine("To Date: " + toDateTime);

                query = @"select COUNT(*) FROM invoice_tb INV with (nolock)
                INNER JOIN entityDetails_tb ED WITH(NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId
                inner join entitydetails_tb s WITH(NOLOCK) on s.entitydetailid = INV.senderEntityDetailId
                INNER JOIN entityAddressRel_tb EAR WITH(NOLOCK) ON ED.entityDetailId = EAR.entityDetailId
                INNER JOIN address_tb AT WITH(NOLOCK) ON AT.addressId = EAR.addressId
                INNER JOIN invoiceSection_tb IST WITH(NOLOCK) ON INV.invoiceId = IST.invoiceId
                INNER JOIN invoiceLineDetail_tb ILD WITH(NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId
                LEFT JOIN actionComments_tb AC WITH(NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1
                WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from lookup_tb where parentlookupcode = 218 and lookupcode = 1)) AND ILD.lineDetailType IN('P')
                AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' AND INV.ARinvoicedate BETWEEN @fromDateTime AND @toDateTime AND s.corcentriccode = '18AutoDlr' AND ED.corcentriccode = '18AutoFlt'";

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
            else if (dateRange == "Last 185 days")
            {
                fromDateTime = DateTime.Now.AddDays(-184).ToString("yyyy-MM-dd");
                query = @"select COUNT(*) FROM invoice_tb INV with (nolock)
                INNER JOIN entityDetails_tb ED WITH(NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId
                inner join entitydetails_tb s WITH(NOLOCK) on s.entitydetailid = INV.senderEntityDetailId
                INNER JOIN entityAddressRel_tb EAR WITH(NOLOCK) ON ED.entityDetailId = EAR.entityDetailId
                INNER JOIN address_tb AT WITH(NOLOCK) ON AT.addressId = EAR.addressId
                INNER JOIN invoiceSection_tb IST WITH(NOLOCK) ON INV.invoiceId = IST.invoiceId
                INNER JOIN invoiceLineDetail_tb ILD WITH(NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId
                LEFT JOIN actionComments_tb AC WITH(NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1
                WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from lookup_tb where parentlookupcode = 218 and lookupcode = 1)) AND ILD.lineDetailType IN('P')
                AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' AND INV.ARinvoicedate BETWEEN @fromDateTime AND GETDATE() AND s.corcentriccode = '18AutoDlr' AND ED.corcentriccode = '18AutoFlt'";

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

                query = @"select COUNT(*) FROM invoice_tb INV with (nolock)
                INNER JOIN entityDetails_tb ED WITH(NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId
                inner join entitydetails_tb s WITH(NOLOCK) on s.entitydetailid = INV.senderEntityDetailId
                INNER JOIN entityAddressRel_tb EAR WITH(NOLOCK) ON ED.entityDetailId = EAR.entityDetailId
                INNER JOIN address_tb AT WITH(NOLOCK) ON AT.addressId = EAR.addressId
                INNER JOIN invoiceSection_tb IST WITH(NOLOCK) ON INV.invoiceId = IST.invoiceId
                INNER JOIN invoiceLineDetail_tb ILD WITH(NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId
                LEFT JOIN actionComments_tb AC WITH(NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1
                WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from lookup_tb where parentlookupcode = 218 and lookupcode = 1)) AND ILD.lineDetailType IN('P')
                AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' AND INV.ARinvoicedate between @fromDateTime AND GETDATE() AND s.corcentriccode = '18AutoDlr' AND ED.corcentriccode = '18AutoFlt'";

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

            else if (dateRange == "Last 12 months")
            {
                fromDateTime = DateTime.Now.AddMonths(-12).AddDays(-1).ToString("yyyy-MM-dd");
                Console.WriteLine("From Date: " + fromDateTime);

                query = @"select COUNT(*) FROM invoice_tb INV with (nolock)
                INNER JOIN entityDetails_tb ED WITH(NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId
                inner join entitydetails_tb s WITH(NOLOCK) on s.entitydetailid = INV.senderEntityDetailId
                INNER JOIN entityAddressRel_tb EAR WITH(NOLOCK) ON ED.entityDetailId = EAR.entityDetailId
                INNER JOIN address_tb AT WITH(NOLOCK) ON AT.addressId = EAR.addressId
                INNER JOIN invoiceSection_tb IST WITH(NOLOCK) ON INV.invoiceId = IST.invoiceId
                INNER JOIN invoiceLineDetail_tb ILD WITH(NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId
                LEFT JOIN actionComments_tb AC WITH(NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1
                WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from lookup_tb where parentlookupcode = 218 and lookupcode = 1)) AND ILD.lineDetailType IN('P')
                AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' AND INV.ARinvoicedate BETWEEN @fromDateTime AND GETDATE() AND s.corcentriccode = '18AutoDlr' AND ED.corcentriccode = '18AutoFlt'";

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
            else if (dateRange == "YTD")
            {
                fromDateTime = new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
                Console.WriteLine("From Date: " + fromDateTime);

                query = @"select COUNT(*) FROM invoice_tb INV with (nolock)
                INNER JOIN entityDetails_tb ED WITH(NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId
                inner join entitydetails_tb s WITH(NOLOCK) on s.entitydetailid = INV.senderEntityDetailId
                INNER JOIN entityAddressRel_tb EAR WITH(NOLOCK) ON ED.entityDetailId = EAR.entityDetailId
                INNER JOIN address_tb AT WITH(NOLOCK) ON AT.addressId = EAR.addressId
                INNER JOIN invoiceSection_tb IST WITH(NOLOCK) ON INV.invoiceId = IST.invoiceId
                INNER JOIN invoiceLineDetail_tb ILD WITH(NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId
                LEFT JOIN actionComments_tb AC WITH(NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1
                WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from lookup_tb where parentlookupcode = 218 and lookupcode = 1)) AND ILD.lineDetailType IN('P')
                AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' AND INV.ARinvoicedate BETWEEN @fromDateTime AND GETDATE() AND s.corcentriccode = '18AutoDlr' AND ED.corcentriccode = '18AutoFlt'";
                
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


            else if (dateRange == "Customized date")
            {
                fromDateTime = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
                Console.WriteLine("From Date: " + fromDateTime);

                query = @"select COUNT(*) FROM invoice_tb INV with (nolock)
                INNER JOIN entityDetails_tb ED WITH(NOLOCK) ON INV.receiverEntityDetailId = ED.entityDetailId
                inner join entitydetails_tb s WITH(NOLOCK) on s.entitydetailid = INV.senderEntityDetailId
                INNER JOIN entityAddressRel_tb EAR WITH(NOLOCK) ON ED.entityDetailId = EAR.entityDetailId
                INNER JOIN address_tb AT WITH(NOLOCK) ON AT.addressId = EAR.addressId
                INNER JOIN invoiceSection_tb IST WITH(NOLOCK) ON INV.invoiceId = IST.invoiceId
                INNER JOIN invoiceLineDetail_tb ILD WITH(NOLOCK) ON IST.invoiceSectionId = ILD.invoiceSectionId
                LEFT JOIN actionComments_tb AC WITH(NOLOCK) ON AC.invoiceId = INV.invoiceId AND INV.ApprovalStatus = AC.actionTypeId and ac.isActive = 1
                WHERE(ILD.accountingDocumentTypeId = 0 or ILD.accountingDocumentTypeId in (select lookupid from lookup_tb where parentlookupcode = 218 and lookupcode = 1)) AND ILD.lineDetailType IN('P')
                AND INV.isActive = 1 and INV.ARCurrencyCode = 'USD' AND INV.ARinvoicedate BETWEEN @fromDateTime AND GETDATE() AND s.corcentriccode = '18AutoDlr' AND ED.corcentriccode = '18AutoFlt'";

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
