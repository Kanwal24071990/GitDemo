using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.Utils;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.GroupRelationshipsMaintenance
{
    internal class GroupRelationshipsMaintenanceDAL : BaseDataAccessLayer
    {

        internal string DeactivateCurrencyCodeReltionship(string groupName)
        {
            string senderReceiverRelId = null;
            string currencyCode = null;
            string query = @"declare @@Relationshiptypeid int
                declare @@senderReceiverRelId int
 
                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Currency Code'

                select @@senderReceiverRelId=senderReceiverRelId from relSenderReceiver_tb where
                relationshipTypeId = @@Relationshiptypeid and
                receiverId = -1 and 
                isActive = 1 and
                receiverGroupId = (select top 1 groupId from group_tb where name = @groupName)
                order by lastUpdatedate desc;

                select currencyCode from relCurrency_tb where senderReceiverRelId = @@senderReceiverRelId;

                select @@senderReceiverRelId;";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@groupName", groupName)
            };
            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    currencyCode = reader.GetString(0);
                }
                if (reader.NextResult())
                {
                    if (reader.Read())
                    {
                        senderReceiverRelId = reader.GetInt32(0).ToString();
                    }
                }
            }

            if (string.IsNullOrEmpty(senderReceiverRelId))
            {
                throw new Exception("SenderReceiverId is null or empty");
            }

            query = @"update relSenderReceiver_tb set isActive = 0 
                where senderReceiverRelId = @senderReceiverRelId;";
            sp = new()
            {
                new SqlParameter("@senderReceiverRelId", senderReceiverRelId)
            };
            ExecuteNonQuery(query, sp, false);

            return currencyCode;
        }
    }
}
