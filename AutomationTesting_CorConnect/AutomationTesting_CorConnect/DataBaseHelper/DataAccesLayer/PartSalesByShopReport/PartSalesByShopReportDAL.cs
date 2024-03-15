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

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PartSalesByShopReport
{
    internal class PartSalesByShopReportDAL : BaseDataAccessLayer
    {
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
                    query = @"SELECT top 1 CONVERT (Date , invoicedate) AS FromDate ,Convert (Date , invoicedate) AS ToDate FROM [invoice_tb]  I INNER JOIN [entitydetails_tb]  AE ON I.receiverEntityDetailId = AE.entityDetailId and AE.isActive = 1  And ISNULL(locationTypeId,0) <> 0 INNER JOIN [entityAddressRel_tb] EA ON AE.entityDetailId = EA.entityDetailId INNER JOIN [address_tb] A ON EA.addressId = A.addressId INNER JOIN [invoiceSection_tb] SI ON I.invoiceId = SI.invoiceId INNER JOIN [invoiceLineDetail_tb] ID ON ID.invoiceSectionId = SI.invoiceSectionId WHERE  I.isActive=1 AND  A.addressTypeId = (SELECT lookupid FROM [lookup_tb] lookup_tb where parentlookupcode=35 and lookupcode=1) AND ID.lineDetailType = 'P' AND ISNULL(ID.accountingDocumentTypeId, 0) = 0 OR ID.accountingDocumentTypeId = (Select lookUpId FROM [lookup_tb] with (nolock) where parentLookUpCode = 218 and lookupcode = 1)  AND ID.rebateType IN (0,2) ORDER BY invoicedate DESC";
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
                    query = @"DECLARE @@Userid as int DECLARE @@FleetAccessLocations table (entityDetailId INT  primary key ) SELECT @@Userid = userid FROM [user_tb] where
                            username=@UserName; WITH RootNumber AS ( SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE  entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK)  INNER JOIN [user_tb] WITH(NOLOCK) ON userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1 AND userRelationships_tb.hasHierarchyAccess = 1 ) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId ) insert into @@FleetAccessLocations SELECT entityDetailId FROM  RootNumber where parentEntityDetailId <> entityDetailId   and parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId  FROM [userRelationships_tb]  WITH(NOLOCK) WHERE userRelationships_tb.userId = @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; SELECT top 1 Convert (Date , invoicedate) as FromDate ,Convert (Date , invoicedate) as ToDate FROM  [invoice_tb]  I INNER JOIN [entitydetails_tb]  AE on I.receiverEntityDetailId = AE.entityDetailId and AE.isActive = 1  And ISNULL(locationTypeId,0) <> 0 INNER JOIN [entityAddressRel_tb] EA on AE.entityDetailId = EA.entityDetailId INNER JOIN [address_tb] A on EA.addressId = A.addressId INNER JOIN [invoiceSection_tb] SI on I.invoiceId = SI.invoiceId INNER JOIN [invoiceLineDetail_tb] ID on ID.invoiceSectionId = SI.invoiceSectionId  WHERE  I.isActive=1 AND A.addressTypeId = (select lookupid from [lookup_tb] where parentlookupcode=35 and lookupcode=1) AND ID.lineDetailType = 'P' AND ISNULL(ID.accountingDocumentTypeId,0) = 0 OR ID.accountingDocumentTypeId = (Select lookUpId FROM [lookup_tb] WITH(NOLOCK) where parentLookUpCode = 218 and lookupcode = 1) AND ID.rebateType IN(0, 2) and i.receiverEntityDetailId in (Select entityDetailId from @@FleetAccessLocations) order by invoicedate DESC";
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
