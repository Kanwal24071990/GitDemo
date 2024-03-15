using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.AccountStatusChangeReport
{
    internal class AccountStatusChangeReportDAL : BaseDataAccessLayer
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
                    query = @"SELECT TOP 1 CONVERT (Date , A.ActionDate) AS FromDate, Convert (Date, A.ActionDate) AS ToDate FROM AuditTrail_tb a WITH (NOLOCK) Left Join 
                        relSuspensionStatus_tb s WITH (NOLOCK) ON (s.suspStatid = a.AuditPKId and a.AuditTable ='relSuspensionStatus_tb') left JOIN entitydetails_tb e WITH (NOLOCK) 
                        ON (e.entitydetailid = a.AuditPKId and a.AuditTable ='entityDetails_tb') left JOIN relSenderReceiver_tb r WITH (NOLOCK) ON (r.senderReceiverRelId = 
                        s.senderReceiverRelId) left JOIN entitydetails_tb f WITH (NOLOCK) ON (f.entitydetailid = r.receiverId) left JOIN entityAddressRel_tb arf WITH (NOLOCK) 
                        ON (arf.entityDetailId = f.entitydetailid) left JOIN address_tb arfa WITH (NOLOCK) ON (arf.addressId = arfa.addressId) left JOIN entitydetails_tb d WITH (NOLOCK) 
                        ON (d.entityDetailId = r.senderId) left JOIN entityAddressRel_tb ard WITH (NOLOCK) ON (ard.entityDetailId = d.entitydetailid) left JOIN address_tb arda 
                        WITH (NOLOCK) ON (ard.addressId = arda.addressId) left JOIN entityAddressRel_tb ar WITH (NOLOCK) ON (ar.entityDetailId = e.entitydetailid) left JOIN 
                        address_tb at WITH (NOLOCK) ON (ar.addressId = at.addressId) WHERE LTRIM(RTRIM(UPPER(a.AuditAppName))) = 'ACCOUNT MAINTENANCE'  and a.AuditTable in 
                        ('entityDetails_tb', 'relSuspensionStatus_tb') and((s.suspstatid is not null and r.senderReceiverRelId is not null and f.entitydetailid is not null 
                        and arf.entityaddressRelid is not null and arfa.addressid is not null) or (s.suspstatid is not null and r.senderReceiverRelId is not null and d.entityDetailId 
                        is not null and ard.entityaddressRelid is not null and arda.addressid is not null) or (e.entitydetailid is not null and  ar.entityaddressRelid is not null 
                        and at.addressid is not null)) order by a.ActionDate desc";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }
                else
                {
                    if (userType == "FLEET")
                    {
                        query = @"DECLARE @@Userid AS INT; DECLARE @@FleetAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE 
                            username=@UserName;  WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) 
                            WHERE entityDetailId IN (SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId WHERE 
                            U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS P 
                            INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@FleetAccessLocations SELECT entityDetailId FROM 
                            RootNumber WHERE parentEntityDetailId <> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId AS entityDetailId FROM [userRelationships_tb] 
                            UR2 WITH(NOLOCK)WHERE UR2.userId = @@UserID and IsActive = 1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE, A.ActionDate) AS FromDate, CONVERT
                            (DATE, A.ActionDate) AS ToDate FROM [AuditTrail_tb] A LEFT JOIN [relSuspensionStatus_tb] S WITH(NOLOCK) ON S.suspStatid = A.AuditPKId AND A.AuditTable = 
                            'relSuspensionStatus_tb' LEFT JOIN [entitydetails_tb] E WITH(NOLOCK) ON E.entitydetailid = A.AuditPKId AND A.AuditTable ='entityDetails_tb' LEFT JOIN 
                            [relSenderReceiver_tb] R WITH(NOLOCK) ON R.senderReceiverRelId = S.senderReceiverRelId LEFT JOIN [entitydetails_tb] F WITH(NOLOCK) ON F.entitydetailid = 
                            R.receiverId LEFT JOIN [entityAddressRel_tb] ARF WITH (NOLOCK) ON ARF.entityDetailId = F.entitydetailid LEFT JOIN [address_tb] ARFA WITH(NOLOCK) ON ARF.addressId = 
                            ARFA.addressId LEFT JOIN [entityAddressRel_tb] AR WITH(NOLOCK) ON AR.entityDetailId = E.entitydetailid LEFT JOIN [address_tb] AT WITH(NOLOCK) ON AR.addressId = 
                            AT.addressId WHERE LTRIM(RTRIM(UPPER(A.AuditAppName))) = 'ACCOUNT MAINTENANCE' AND A.AuditTable IN('entityDetails_tb', 'relSuspensionStatus_tb') AND
                            ((S.suspstatid IS NOT NULL AND R.senderReceiverRelId IS NOT NULL AND F.entitydetailid IS NOT NULL AND ARF.entityaddressRelid IS NOT NULL AND ARFA.addressid IS NOT 
                            NULL AND F.entitydetailid IN(SELECT entitydetailid FROM @@FleetAccessLocations)) OR(E.entitydetailid IS NOT NULL AND AR.entityaddressRelid IS NOT NULL AND 
                            AT.addressid IS NOT NULL AND E.entitydetailid IN (SELECT entitydetailid FROM @@FleetAccessLocations))) ORDER BY A.ActionDate DESC;";
                    }
                    else if (userType == "DEALER")
                    {
                        query = @"DECLARE @@Userid AS INT; DECLARE @@DealerAccessLocations TABLE(entityDetailId INT PRIMARY KEY); SELECT @@Userid=userid FROM [user_tb] WHERE 
                            username=@UserName; WITH RootNumber AS (SELECT entityDetailId, parentEntityDetailId, entityDetailId AS RootID FROM [entityDetails_tb] WITH(NOLOCK) WHERE 
                            entityDetailId IN (SELECT DISTINCT UR.entityId FROM [userRelationships_tb] UR WITH(NOLOCK) INNER JOIN [user_tb] U WITH(NOLOCK) ON UR.userId = U.userId WHERE 
                            U.userId = @@UserID AND UR.IsActive = 1 AND UR.hasHierarchyAccess = 1) UNION ALL SELECT C.entityDetailId, C.parentEntityDetailId, P.RootID FROM RootNumber AS 
                            P INNER JOIN [entityDetails_tb] AS C WITH(NOLOCK) ON P.entityDetailId = C.parentEntityDetailId) INSERT INTO @@DealerAccessLocations SELECT entityDetailId FROM 
                            RootNumber WHERE parentEntityDetailId<> entityDetailId AND parentEntityDetailId <> 0 UNION SELECT UR2.entityId As entityDetailId FROM [userRelationships_tb] UR2 
                            WITH(NOLOCK)WHERE UR2.userId = @@UserID AND IsActive = 1 AND UR2.entityId IS NOT NULL; SELECT TOP 1 CONVERT(DATE, A.ActionDate) AS FromDate, CONVERT(DATE, 
                            A.ActionDate) AS ToDate FROM [AuditTrail_tb] A LEFT JOIN [relSuspensionStatus_tb] S WITH(NOLOCK) ON S.suspStatid = A.AuditPKId AND A.AuditTable = 
                            'relSuspensionStatus_tb' LEFT JOIN [entitydetails_tb] E WITH(NOLOCK) ON E.entitydetailid = A.AuditPKId AND A.AuditTable ='entityDetails_tb' LEFT JOIN 
                            [relSenderReceiver_tb] R WITH(NOLOCK) ON R.senderReceiverRelId = S.senderReceiverRelId LEFT JOIN [entitydetails_tb] D WITH(NOLOCK) ON D.entitydetailid = 
                            R.senderid LEFT JOIN [entityAddressRel_tb] ARD WITH(NOLOCK) ON ARD.entityDetailId = D.entitydetailid LEFT JOIN [address_tb] ARDA WITH(NOLOCK) ON ARD.addressId = 
                            ARDA.addressId LEFT JOIN [entityAddressRel_tb] AR WITH(NOLOCK) ON AR.entityDetailId = E.entitydetailid LEFT JOIN [address_tb] AT WITH(NOLOCK) ON AR.addressId = 
                            AT.addressId  WHERE LTRIM(RTRIM(UPPER(A.AuditAppName))) = 'ACCOUNT MAINTENANCE' AND A.AuditTable IN('entityDetails_tb', 'relSuspensionStatus_tb') AND
                            ((S.suspstatid IS NOT NULL AND R.senderReceiverRelId IS NOT NULL AND D.entitydetailid IS NOT NULL AND ARD.entityaddressRelid IS NOT NULL AND ARDA.addressid IS NOT 
                            NULL AND D.entitydetailid IN (SELECT entitydetailid FROM @@DealerAccessLocations)) OR(E.entitydetailid IS NOT NULL AND AR.entityaddressRelid IS NOT NULL AND 
                            AT.addressid IS NOT NULL AND E.entitydetailid IN(SELECT entitydetailid FROM @@DealerAccessLocations))) ORDER BY A.ActionDate DESC;";
                    }

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
