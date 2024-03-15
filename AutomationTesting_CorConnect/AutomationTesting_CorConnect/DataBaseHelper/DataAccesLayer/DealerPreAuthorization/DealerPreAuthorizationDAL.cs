using AutomationTesting_CorConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerPreAuthorization
{
    internal class DealerPreAuthorizationDAL : BaseDataAccessLayer
    {

        internal void GetData(out string dealerCode, out string fleetCode)
        {
            dealerCode = String.Empty;
            fleetCode = String.Empty;

            try
            {
                string query = @"select top 1 *  from (SELECT 
 
              case when dealer.haspreauth=1 then dealer.corcentricCode else null end as [DealerCode], case when fleet.haspreauth=1 then fleet.corcentricCode else null end as [FleetCode]
 
              
 
       FROM relInvoicePreApprovalRules_tb preApp WITH (NOLOCK)
       INNER JOIN relSenderReceiver_tb rel WITH (NOLOCK) on rel.senderReceiverRelId = preApp.senderReceiverRelId
       INNER JOIN entityDetails_tb dealer WITH (NOLOCK) on rel.senderId = dealer.entityDetailId
       INNER JOIN entityAddressRel_tb dealerAdd WITH (NOLOCK) on dealerAdd.entityDetailId = dealer.entityDetailId
       INNER JOIN address_tb adr WITH (NOLOCK) ON dealerAdd.addressId = adr.addressId
       INNER JOIN entityDetails_tb fleet WITH (NOLOCK) on rel.receiverId = fleet.entityDetailId
       LEFT JOIN relPreAuthorization_tb relPreAuth WITH (NOLOCK) ON relPreAuth.senderReceiverRelId = rel.senderReceiverRelId
       where 
       
       relPreAuth.relPreAuthorizationId IS NULL 
       UNION ALL
       SELECT 
 
              dealer.corcentricCode as [DealerCode],
              
              fleet.corcentricCode as [FleetCode]
 
       from relPreAuthorization_tb preAuth WITH (NOLOCK)
 
       INNER JOIN entityDetails_tb dealer WITH (NOLOCK) on preAuth.senderId = dealer.entityDetailId
       INNER JOIN entityAddressRel_tb dealerAdd WITH (NOLOCK) on dealerAdd.entityDetailId = dealer.entityDetailId
       INNER JOIN address_tb adr WITH (NOLOCK) ON dealerAdd.addressId = adr.addressId
       INNER JOIN entityDetails_tb fleet WITH (NOLOCK) on preAuth.receiverId = fleet.entityDetailId
       INNER JOIN lookUp_tb status WITH (NOLOCK) on preAuth.statusId = status.lookUpId
       INNER JOIN user_tb usr WITH (NOLOCK) on usr.userId = preAuth.createdBy
       LEFT JOIN user_tb usrUpdate WITH (NOLOCK) on usrUpdate.userId = preAuth.updatedBy
       LEFT JOIN relSenderReceiver_tb relPreApp WITH (NOLOCK) ON preAuth.senderReceiverRelId = relPreApp.senderReceiverRelId
       LEFT JOIN relInvoicePreApprovalRules_tb preApp WITH (NOLOCK) ON preApp.senderReceiverRelId = relPreApp.senderReceiverRelId
       where 
 
       preAuth.isActive = 1) a order by DealerCode desc ,fleetcode desc";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        dealerCode = reader.GetString(0);
                        fleetCode = reader.GetString(1);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);

                dealerCode = null;
                fleetCode = null;
            }
        }
    }
}
