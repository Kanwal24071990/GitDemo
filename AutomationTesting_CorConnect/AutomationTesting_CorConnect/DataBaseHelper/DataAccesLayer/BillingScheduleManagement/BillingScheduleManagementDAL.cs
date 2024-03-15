using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.BillingScheduleManagement
{
    internal class BillingScheduleManagementDAL : BaseDataAccessLayer
    {
        internal void GetFilterData(out string companyName, out string effectiveDate)
        {
            string query = null;
            companyName = null;
            effectiveDate = null;
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
                    query = @"SELECT top 1 ISNULL(receiverDetails.displayName, 'ALL') as Fleet FROM [relsenderreceiver_tb] relSR WITH (NOLOCK)INNER JOIN [relPricing_tb] pricing 
                        WITH (NOLOCK) ON (pricing.senderReceiverRelId = relSR.senderReceiverRelId AND pricing.isActive = 1) LEFT JOIN [entityDetails_tb] senderDetails WITH (NOLOCK) 
                        ON (senderDetails.entityDetailId = relSR.SenderId) LEFT JOIN [entityDetails_tb] receiverDetails WITH (NOLOCK)ON (receiverDetails.entityDetailId = relSR.ReceiverId) 
                        WHERE relSR.isActive = 1 AND((senderDetails.entityDetailId is null) OR(senderDetails.isactive = 1 and ISNULL(senderDetails.locationTypeId, 0) <> 0)) ORDER By 
                        relSR.ReceiverId desc";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            companyName = reader.GetString(0);
                            effectiveDate = null;
                        }
                    }
                }
                else if (userType == "DEALER")
                {
                    query = @"DECLARE @@Userid as int DECLARE @@DealerAccessLocations table ( entityDetailId INT  primary key ) select @@Userid=userid FROM [user_tb] where 
                        username=@UserName; WITH RootNumber AS(SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb]  WITH(NOLOCK) WHERE  
                        entityDetailId iN( SELECT DISTINCT userRelationships_tb.entityId FROM [userRelationships_tb] WITH(NOLOCK) INNER JOIN [user_tb] WITH(NOLOCK) ON 
                        userRelationships_tb.userId = user_tb.userId WHERE user_tb.userId = @@UserID AND userRelationships_tb.IsActive = 1  AND userRelationships_tb.hasHierarchyAccess = 1 ) 
                        UNION ALL  SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM  RootNumber AS P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = 
                        C.parentEntityDetailId ) insert into @@DealerAccessLocations SELECT entityDetailId FROM RootNumber where parentEntityDetailId<> entityDetailId and 
                        parentEntityDetailId <> 0 UNION SELECT userRelationships_tb.entityId As entityDetailId FROM [userRelationships_tb] WITH(NOLOCK) WHERE userRelationships_tb.userId = 
                        @@UserID  and IsActive =1 AND userRelationships_tb.entityId IS NOT NULL; SELECT top 1  ISNULL(receiverDetails.displayName, 'ALL') as Fleet, pricing.startDate as 
                        EffectiveDate FROM [relsenderreceiver_tb] relSR WITH(Nolock) INNER JOIN [relPricing_tb] pricing WITH(Nolock) on pricing.senderReceiverRelId = 
                        relSR.senderReceiverRelId AND pricing.isActive = 1 LEFT JOIN [entityDetails_tb] senderDetails WITH(Nolock) on senderDetails.entityDetailId = relSR.SenderId 
                        LEFT JOIN [entityDetails_tb] receiverDetails with (Nolock) on receiverDetails.entityDetailId = relSR.ReceiverId WHERE relSR.isActive = 1 AND(relSR.SenderId = 0 OR 
                        relSR.SenderId IN(SELECT entityDetailId FROM @@DealerAccessLocations)) ORDER By relSR.ReceiverId desc, EffectiveDate DESC";

                    List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                    };

                    using (var reader = ExecuteReader(query, sp, false))
                    {
                        if (reader.Read())
                        {
                            companyName = reader.GetString(0);
                            effectiveDate = reader.GetDateTimeValue("EffectiveDate");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                companyName = null;
                effectiveDate = null;
            }
        }

        internal void GetFilterAccountCode(out string accountCode)
        {
            string query = null;
            accountCode = null;
            try
            {
               
              query = @"SELECT top 1 ISNULL(receiverDetails.corcentricCode, 'ALL') as AccountCode FROM [relsenderreceiver_tb] relSR WITH (NOLOCK)INNER JOIN [relPricing_tb] pricing 
                        WITH (NOLOCK) ON (pricing.senderReceiverRelId = relSR.senderReceiverRelId AND pricing.isActive = 1) LEFT JOIN [entityDetails_tb] senderDetails WITH (NOLOCK) 
                        ON (senderDetails.entityDetailId = relSR.SenderId) LEFT JOIN [entityDetails_tb] receiverDetails WITH (NOLOCK)ON (receiverDetails.entityDetailId = relSR.ReceiverId) 
                        WHERE relSR.isActive = 1 AND((senderDetails.entityDetailId is null) OR(senderDetails.isactive = 1 and ISNULL(senderDetails.locationTypeId, 0) <> 0)) ORDER By 
                        relSR.ReceiverId desc";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            
                        accountCode = reader.GetString(0);
                        }
                    }
                
                
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                accountCode = null;
                
            }
        }
    }
}
