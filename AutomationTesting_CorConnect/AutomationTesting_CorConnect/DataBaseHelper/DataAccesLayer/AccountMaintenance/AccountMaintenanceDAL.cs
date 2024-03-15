using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.AccountMaintenance
{
    internal class AccountMaintenanceDAL : BaseDataAccessLayer
    {
        internal RelCreditPriceAuditTb GetTransactionDetails()
        {
            RelCreditPriceAuditTb relCreditPriceAuditTb = new RelCreditPriceAuditTb();
            string query = "select top 1 * from relCreditPriceAudit_tb order by 1 desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    relCreditPriceAuditTb.TransactionTypeId = reader.GetInt32(1);
                    relCreditPriceAuditTb.ApplicableTo = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                    relCreditPriceAuditTb.IsActive = reader.GetBoolean(5);
                    relCreditPriceAuditTb.EnableGMCCalculation = reader.GetBoolean(8);
                    relCreditPriceAuditTb.EnableRebatesCalculation = reader.GetBoolean(9);
                }
            }

            return relCreditPriceAuditTb;
        }

        internal RelPricingTypeTb GetPricingTypeDetails()
        {
            RelPricingTypeTb relPricingTypeTb = new RelPricingTypeTb();
            string query = "select top 1 * from relPricingtype_tb order by lastupdatedate desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {

                    relPricingTypeTb.SenderReceiverRelId = Convert.ToInt32(reader[3]);

                }
            }

            return relPricingTypeTb;
        }

        internal RelCurrencyCodeTb GetCurrencyCodeDetails()
        {
            RelCurrencyCodeTb relCurrencyCodeTb = new RelCurrencyCodeTb();
            string query = "select top 1 * from relcurrency_tb order by relCurrencyId desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {

                    relCurrencyCodeTb.currency = reader.GetString(1);
                    relCurrencyCodeTb.isActive = reader.GetBoolean(3);

                }
            }

            return relCurrencyCodeTb;
        }

        internal int GetLookupID(string value)
        {
            string query = "select * from lookup_tb l where parentLookUpCode=21 and Name=@name";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@name", value),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            return -1;
        }

        internal void DeactivateTokenSeperateARAP()
        {
            string query = "update lookup_tb set isActive = 0 where lookUpId = 162401";

            ExecuteNonQuery(query, false);

        }

        internal void ActivateTokenSeperateARAP()
        {
            string query = "update lookup_tb set isActive = 1 where lookUpId = 162401";

            ExecuteNonQuery(query, false);

        }

        internal int GetRowCountFromTable(string table)
        {
            string query = "select count(*) from " + table;

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            return -1;
        }

        internal int GetInvoiceDeliveryMethodId(string fleetCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                    set @@FleetCorcentricCode = @fleetCode
                   
                select invoiceDeliveryMethodId from relInvoicedelivery_tb where relInvoiceDeliveryId IN (
	            select relInvoiceDeliveryId from relsenderreceiver_tb r inner join relinvoicedelivery_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.receiverid
                where r.entitytypeid=3 and d.corcentriccode=@@FleetCorcentricCode)";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCode", fleetCode),

            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            return -1;
        }

        internal int GetApplicableToID(int parentLookupId, string value)
        {
            string query = "select * from lookup_tb l where parentLookUpCode=@parentLookUpCode and Name=@name";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@parentLookUpCode", parentLookupId),
                new SqlParameter("@name", value),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            return -1;
        }

        internal List<string> GetTransactionTypes()
        {
            List<string> transcationNames = new List<string>();
            string query = "select name from lookup_tb where parentLookUpCode=21 and isActive =1";
            using (var reader = ExecuteReader(query, false))
            {

                while (reader.Read())
                {
                    transcationNames.Add(reader.GetString(0));

                }

            }
            return transcationNames;
        }
        internal decimal GetFieldValues()
        {
            decimal values = 0;
            string query = "Select top 1 relCommunityFeeId,corcentricElectronicFeePCT from relCommunityFee_tb order by 1 desc";
            using (var reader = ExecuteReader(query, false))
            {

                while (reader.Read())
                {
                    values = reader.GetDecimal(1);

                }

            }
            return values;
        }
        internal RelationshipSenderReceiverTable GetRelSenderReceiverByFleetCode(string fleetCode, string relationshipName)
        {
            RelationshipSenderReceiverTable relSenderReceiverTb = new RelationshipSenderReceiverTable();

            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                    declare @@Relationshiptypeid int
                    declare @@RelationshipName nvarchar(100)
                    set @@FleetCorcentricCode = @fleetCode
                    set @@RelationshipName = @relationshipName

                    select @@Relationshiptypeid = lookupid from lookup_tb where parentlookupcode = 32 and name = @@RelationshipName
                    select top 1 * from relsenderreceiver_tb r
                    inner join entityDetails_tb f on r.receiverId = f.entityDetailId
                    where r.entityTypeId = 3 and f.corcentricCode = @@FleetCorcentricCode and r.relationshipTypeId = @@Relationshiptypeid
                    order by r.lastUpdateDate desc";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCode", fleetCode),
                new SqlParameter("@relationshipName", relationshipName)
            };


            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    relSenderReceiverTb.SenderId = reader.GetInt32(1);
                    relSenderReceiverTb.ReceiverId = reader.GetInt32(2);
                    relSenderReceiverTb.RelationshipTypeId = reader.GetInt32(3);
                    relSenderReceiverTb.IsActive = reader.GetBoolean(4);
                }
            }

            return relSenderReceiverTb;
        }

        internal bool GetIsActiveValue(int senderReceiverRelId)
        {
            bool isActive = false;
            string query = "select isActive from relSenderReceiver_tb where senderReceiverRelId = '" + senderReceiverRelId + "'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    isActive = reader.GetBoolean(0);
                }
            }

            return isActive;
        }

        internal bool DeleteResellerRelationshipDealerSide(string dealerCorcentricCode)
        {
            string query = @"declare @@DealerCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@DealerCorcentricCode=@dealerCorcentricCode
 
                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Reseller Invoice Required'
 
                delete from relresellerinvoicerequired_tb from relsenderreceiver_tb r inner join relresellerinvoicerequired_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid
 
                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerCorcentricCode", dealerCorcentricCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteResellerRelationshipFleetSide(string fleetCorcentricCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@FleetCorcentricCode=@fleetCorcentricCode
 
                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Reseller Invoice Required'
 
                delete from relresellerinvoicerequired_tb from relsenderreceiver_tb r inner join relresellerinvoicerequired_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid
 
                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCorcentricCode", fleetCorcentricCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeletePricingTypeRelationshipDealerSide(string dealerCorcentricCode)
        {
            string query = @"declare @@DealerCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@DealerCorcentricCode=@dealerCorcentricCode
 
                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Pricing Type'
 
                delete from relpricingtype_tb from relsenderreceiver_tb r inner join relpricingtype_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1
 
                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerCorcentricCode", dealerCorcentricCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeletePricingTypeRelationshipFleetSide(string fleetCorcentricCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@FleetCorcentricCode=@fleetCorcentricCode
 
                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Pricing Type'
 
                delete from relpricingtype_tb from relsenderreceiver_tb r inner join relpricingtype_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.receiverid
                where r.entitytypeid=3 and d.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1
 
                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb d on d.entitydetailid=r.receiverid
                where r.entitytypeid=3 and d.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCorcentricCode", fleetCorcentricCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteCurrencyCodeRelationshipDealerSide(string dealerCorcentricCode)
        {
            string query = @"declare @@DealerCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@DealerCorcentricCode=@dealerCorcentricCode
 
                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Currency Code'
 
                delete from relcurrency_tb from relsenderreceiver_tb r inner join relcurrency_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1
 
                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerCorcentricCode", dealerCorcentricCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteCurrencyCodeRelationshipFleetSide(string fleetCorcentricCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@FleetCorcentricCode=@fleetCorcentricCode
 
                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Currency Code'
 
                delete from relcurrency_tb from relsenderreceiver_tb r inner join relcurrency_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.receiverid
                where r.entitytypeid=3 and d.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1
 
                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb d on d.entitydetailid=r.receiverid
                where r.entitytypeid=3 and d.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCorcentricCode", fleetCorcentricCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteInvoiceDeliveryRelationship(string fleetCorcentricCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@FleetCorcentricCode=@fleetCorcentricCode
 
                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Invoice Delivery'
 
                delete from relinvoicedelivery_tb from relsenderreceiver_tb r inner join relinvoicedelivery_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.receiverid
                where r.entitytypeid=3 and d.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1
 
                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb d on d.entitydetailid=r.receiverid
                where r.entitytypeid=3 and d.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCorcentricCode", fleetCorcentricCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal RelationshipDetails GetRelationshipDetailsForRelType(string relationshipType)
        {
            RelationshipDetails relDetails = null;
            string query = @"
                Declare @@RelationshiptypeName nvarchar(100)
                declare @@Relationshiptypeid int
                declare @@typeid int
                declare @@Entitydetailid int

                set @@RelationshiptypeName = @relationshipType

                select @@Relationshiptypeid = lookupid, @@typeid = case when(availableToSender = 1 and availableToReceiver = 0) then 2 else 3  end from lookup_tb where parentlookupcode = 32 and name = @@RelationshiptypeName

                -- Account Maintenace Search Deler / Fleet Code value ---
                select top 1 @@Entitydetailid = entitydetailid   from entitydetails_tb  where entitytypeid = @@typeid and enrollmentstatusid = 13  AND isActive = 1

                select corcentriccode as AccountMaintenanceSearchCode  , case when entitytypeid = 2 then 'DealerCode' else 'FleetCode' end as EntityType from entitydetails_tb  where entitydetailid = @@Entitydetailid

                -- Relationship Drop Down Code value  Deler / Fleet Code value ---

                If @@typeid = 3

                select top 1 corcentriccode as dealercode from entitydetails_tb s left join relsenderreceiver_tb r on s.entitydetailid = r.senderid and relationshiptypeid = @@Relationshiptypeid and r.entitytypeid = @@typeid and r.isactive = 1 and r.receiverid = @@Entitydetailid
                where r.senderid is null and s.entitytypeid = 2 and s.enrollmentstatusid = 13  AND s.isActive = 1

                else

                select top 1 corcentriccode as FleetCode from entitydetails_tb f left join relsenderreceiver_tb r on f.entitydetailid = r.receiverid and relationshiptypeid = @@Relationshiptypeid and r.entitytypeid = @@typeid and r.isactive = 1 and r.senderid = @@Entitydetailid
                where r.receiverid is null and f.entitytypeid = 3 and f.enrollmentstatusid = 13  AND f.isActive = 1 ";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@relationshipType", relationshipType)
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                relDetails = new RelationshipDetails();
                while (reader.Read())
                {
                    relDetails.AccountMaintSrchCode = reader.GetString(0);
                    relDetails.AccountMaintSrchEntityType = reader.GetString(1);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        relDetails.EntityCode = reader.GetString(0);
                    }
                }
            }
            return relDetails;
        }

        internal string GetDealerCode(LocationType locationType)
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
                INNER JOIN entityaddressrel_tb entAdd WITH (NOLOCK) ON t.entitydetailid = entAdd.entitydetailid
                INNER JOIN address_tb adr WITH (NOLOCK) ON adr.addressid = entAdd.addressid
                where t.entitytypeid = 2 and t.enrollmentstatusid = 13  AND t.isActive=1 AND t.isTerminated=0 AND countryCode = 'US' and masterflag=@masterFlag and locationtypeid=@locationTypeId order by createDate desc";
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

        internal string GetDealerCodeCorcentric(LocationType locationType)
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
                INNER JOIN entityaddressrel_tb entAdd WITH (NOLOCK) ON t.entitydetailid = entAdd.entitydetailid
                INNER JOIN address_tb adr WITH (NOLOCK) ON adr.addressid = entAdd.addressid
                where t.entitytypeid = 2 and t.enrollmentstatusid = 13  AND t.isActive=1 AND t.isTerminated=0 AND countryCode = 'US' and corcentricLocation = 1 and masterflag=@masterFlag and locationtypeid=@locationTypeId";
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

        internal string GetFleetCode(LocationType locationType)
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
                INNER JOIN entityaddressrel_tb entAdd WITH (NOLOCK) ON t.entitydetailid = entAdd.entitydetailid
                INNER JOIN address_tb adr WITH (NOLOCK) ON adr.addressid = entAdd.addressid
                where t.entitytypeid = 3 and t.enrollmentstatusid = 13  AND t.isActive=1 AND t.isTerminated=0 and masterflag=@masterFlag and locationtypeid=@locationTypeId";
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

        internal string GetFleetCodeEnablePaymentPortal(LocationType locationType)
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
                INNER JOIN entityaddressrel_tb entAdd WITH (NOLOCK) ON t.entitydetailid = entAdd.entitydetailid
                INNER JOIN address_tb adr WITH (NOLOCK) ON adr.addressid = entAdd.addressid
                where t.entitytypeid = 3 and t.enrollmentstatusid = 13  AND t.isActive=1 AND t.isTerminated=0 and masterflag=@masterFlag and locationtypeid=@locationTypeId and enablePaymentPortal = 1";
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

        internal bool DeleteCorePriceAdjustmentRelFleetSide(string fleetCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@FleetCorcentricCode=@fleetCorcentricCode

                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Core Price Adjustment'

                delete from relCorePriceAdjustment_tb from relsenderreceiver_tb r inner join relCorePriceAdjustment_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid

                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCorcentricCode", fleetCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteCorePriceAdjustmentRelDealerSide(string dealerCode)
        {
            string query = @"declare @@DealerCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@DealerCorcentricCode=@dealerCorcentricCode

                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Core Price Adjustment'

                delete from relCorePriceAdjustment_tb from relsenderreceiver_tb r inner join relCorePriceAdjustment_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid

                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerCorcentricCode", dealerCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal RelationshipSenderReceiverTable GetRelSenderReceiverByDealerCode(string dealerCode, string relationshipName)
        {
            RelationshipSenderReceiverTable relSenderReceiverTb = new RelationshipSenderReceiverTable();

            string query = @"declare @@DealerCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                declare @@RelationshipName nvarchar(100)
                set @@DealerCorcentricCode = @dealerCode
                set @@RelationshipName = @relationshipName

                select @@Relationshiptypeid = lookupid from lookup_tb where parentlookupcode = 32 and name = @@RelationshipName
                select top 1 * from relsenderreceiver_tb r
                inner join entityDetails_tb f on r.senderId = f.entityDetailId
                where r.entityTypeId = 2 and f.corcentricCode = @@DealerCorcentricCode and r.relationshipTypeId = @@Relationshiptypeid
                order by r.lastUpdateDate desc";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerCode", dealerCode),
                new SqlParameter("@relationshipName", relationshipName),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    relSenderReceiverTb.SenderId = reader.GetInt32(1);
                    relSenderReceiverTb.ReceiverId = reader.GetInt32(2);
                    relSenderReceiverTb.RelationshipTypeId = reader.GetInt32(3);
                    relSenderReceiverTb.IsActive = reader.GetBoolean(4);
                }
            }
            return relSenderReceiverTb;
        }

        internal bool DeleteStatementDeliveryRelFleetSide(string fleetCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@FleetCorcentricCode=@fleetCorcentricCode

                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Statement Delivery'

                delete from relStatementDelivery_tb from relsenderreceiver_tb r inner join relStatementDelivery_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid

                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCorcentricCode", fleetCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteStatementDeliveryRelDealerSide(string dealerCode)
        {
            string query = @"declare @@DealerCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@DealerCorcentricCode=@dealerCorcentricCode

                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Statement Delivery'

                delete from relStatementDelivery_tb from relsenderreceiver_tb r inner join relStatementDelivery_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid

                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerCorcentricCode", dealerCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeletePaymentTermsRelDealerSide(string dealerCorcentricCode)
        {
            string query = @"declare @@DealerCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@DealerCorcentricCode=@dealerCorcentricCode
 
                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Payment Terms'
 
                delete from relPaymentTerm_tb from relsenderreceiver_tb r inner join relPaymentTerm_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1
 
                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb d on d.entitydetailid=r.senderid
                where r.entitytypeid=2 and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerCorcentricCode", dealerCorcentricCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeletePaymentTermsRelFleetSide(string fleetCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@FleetCorcentricCode=@fleetCorcentricCode

                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Payment Terms'

                delete from relPaymentTerm_tb from relsenderreceiver_tb r inner join relPaymentTerm_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid

                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCorcentricCode", fleetCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeletePaymentMethodRelFleetSide(string fleetCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@FleetCorcentricCode=@fleetCorcentricCode

                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Payment Method'

                delete from relPaymentMethod_tb from relsenderreceiver_tb r inner join relPaymentMethod_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid

                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCorcentricCode", fleetCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteFinancialHandlingRelFleetSide(string fleetCode)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100)
                declare @@Relationshiptypeid int
                set @@FleetCorcentricCode=@fleetCorcentricCode

                select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Financial Handling'

                delete from relFinancialHandling_tb from relsenderreceiver_tb r inner join relFinancialHandling_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid
                inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid

                delete from relsenderreceiver_tb from relsenderreceiver_tb r  inner join entitydetails_tb f on f.entitydetailid=r.receiverid
                where r.entitytypeid=3 and f.corcentriccode=@@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid and r.isactive=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCorcentricCode", fleetCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal EntityDetails GetZipAndState(string firstname)
        {
            EntityDetails entityDetails = null;
            try
            {
                string spName = "select firstname, lastname,title,address1,address2, countryCode, state, zip from entityContacts_tb where entitydetailid in (select entitydetailid from entitydetails_tb where firstname=@firstname)";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@firstname", firstname),
                };

                using (var reader = ExecuteReader(spName, sp, false))

                {
                    if (reader.Read())
                    {
                        entityDetails = new EntityDetails();
                        entityDetails.ZipCode = reader.GetStringValue("zip");
                        entityDetails.State = reader.GetStringValue("state");
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
        internal void UpdateEntityToNonCorcentricLocation(string dealerCode)
        {

            string query = @"update entityDetails_tb set corcentricLocation = 0 where corcentricCode = @corcentricCode";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@corcentricCode", dealerCode),
            };


            ExecuteReader(query, sp, false);

        }

        internal void UpdateEntityToCorcentricLocation(string dealerCode)
        {

            string query = @"update entityDetails_tb set corcentricLocation = 1 where corcentricCode = @corcentricCode";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@corcentricCode", dealerCode),
            };


            ExecuteReader(query, sp, false);

        }

        internal string GetDealerCodePaymentTerms()
        {
            var query = @"select top 1 corcentriccode, * from entitydetails_tb e inner join relsenderreceiver_tb r on e.entitydetailid=r.senderid inner join lookup_tb l on r.relationshiptypeid=l.lookupid where l.parentLookUpCode=32  and lookUpCode=3  and r.isactive=1 order by createdDate ASC";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            return null;
        }

        internal string GetTaxIDValue(string CorCentricCode)
        {
            var query = @"select l.name from lookUp_tb l inner join entityDetails_tb e ON e.taxIDTypeId = l.lookUpId  
                          where e.corcentricCode like '"+CorCentricCode+"' and l.parentlookupcode = 416";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            return null;
        }

        internal string GetTaxClassificationIDValue(string CorCentricCode)
        {
            var query = @"select l.name from lookUp_tb l inner join entityDetails_tb e ON e.taxClassificationId = l.lookUpId  
                          where e.corcentricCode like '" + CorCentricCode + "' and l.parentlookupcode = 417";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            return null;
        }

        internal EntityDetails GetOverrideTermDescriptionDetails(string dealerCode, string fleetCode)
        {
            EntityDetails entityDetails = null;
            try
            {
                string spName = "Select isOverrideTermDescription,overrideTermDescriptionDays from relPaymentTerm_tb PT INNER JOIN relSenderReceiver_tb SR  ON SR.senderReceiverRelID = PT.senderReceiverRelID where SR.senderID = (Select entitydetailID from entitydetails_tb where corcentricCode = @dealerCorcentricCode) AND SR.ReceiverID = (Select entitydetailID from entitydetails_tb where corcentricCode = @fleetCorcentricCode) and SR.isActive = 1 AND SR.relationshipTypeID = 205";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@dealerCorcentricCode", dealerCode),
                    new SqlParameter("@fleetCorcentricCode", fleetCode),

                };

                using (var reader = ExecuteReader(spName, sp, false))

                {
                    if (reader.Read())
                    {
                        entityDetails = new EntityDetails();
                        entityDetails.IsOverrideTermDescription = reader.GetBoolean(0);
                        entityDetails.OverrideTermDescriptionDays = reader.GetInt32(1);
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

        internal bool IsPaymentTermActive(string dealerCorcentricCode)
        {
            string query = @"select pt.isactive from relsenderreceiver_tb rs inner join relpaymentterm_tb pt on rs.senderreceiverrelid = pt.senderreceiverrelid inner join entitydetails_tb ed on rs.senderid = ed.entitydetailid where ed.corcentricCode = '@dealerCorcentricCode'";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerCorcentricCode", dealerCorcentricCode)
            };

            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool ToggleTaxRelationshipFleetSide(string fleetCode, bool doActivate)
        {
            string query = @"declare @@FleetCorcentricCode nvarchar(100) 
            declare @@Relationshiptypeid int 
            declare @@SenderReceiverRelId int 
            declare @@doActivate int 
            set @@FleetCorcentricCode= @fleetCode; 
            set @@doActivate = @doActivate 
            select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Tax' 
            select top 1 @@SenderReceiverRelId=r.senderReceiverRelId from relsenderreceiver_tb r 
            inner join relTax_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid 
            inner join entitydetails_tb d on d.entitydetailid=r.receiverid 
            where r.entitytypeid = 3 and r.senderId = 0 and d.corcentriccode = @@FleetCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid 
            and r.isActive = CASE @@doActivate WHEN 0 THEN 1 ELSE 0 END order by r.lastUpdateDate desc 
            update relsenderreceiver_tb set isactive = @@doActivate where senderreceiverrelid = @@SenderReceiverRelId;";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@fleetCode", fleetCode),
                new SqlParameter("@doActivate", doActivate),
            };
            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool ToggleTaxRelationshipDealerSide(string dealerCode, bool doActivate)
        {
            string query = @"declare @@DealerCorcentricCode nvarchar(100) 
            declare @@Relationshiptypeid int 
            declare @@SenderReceiverRelId int 
            declare @@doActivate int; 
            set @@DealerCorcentricCode=@dealerCode; 
            set @@doActivate = @doActivate; 
            select @@Relationshiptypeid=lookupid from lookup_tb where parentlookupcode=32 and name='Tax' 
            select top 1 @@SenderReceiverRelId=r.senderReceiverRelId from relsenderreceiver_tb r 
            inner join relTax_tb ir on r.senderreceiverrelid=ir.senderreceiverrelid 
            inner join entitydetails_tb d on d.entitydetailid=r.senderid 
            where r.entitytypeid = 2 and r.isActive = CASE @@doActivate WHEN 0 THEN 1 ELSE 0 END and r.receiverId = 0 
            and d.corcentriccode=@@DealerCorcentricCode and r.relationshiptypeid=@@Relationshiptypeid order by lastUpdateDate desc; 
            update relsenderreceiver_tb set isactive = @@doActivate where senderreceiverrelid = @@SenderReceiverRelId;";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealerCode", dealerCode),
                new SqlParameter("@doActivate", doActivate),
            };
            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteTaxRelationship(string corcentricCode)
        {
            string query = @"declare @@corcentricCode nvarchar(100) 
            declare @@Relationshiptypeid int 
            declare @@entityTypeId int 
            set @@corcentricCode = @corcentricCode 
            set @@entityTypeId = (select top 1 entityTypeId from entityDetails_tb where corcentricCode = @@corcentricCode) 
            select @@Relationshiptypeid = lookupid from lookup_tb where parentlookupcode = 32 and name = 'Tax'

            delete from relTaxTypesExempted_tb from relsenderreceiver_tb r inner join relTax_tb ir on r.senderreceiverrelid = ir.senderreceiverrelid 
            inner join entitydetails_tb d on d.entitydetailid = CASE @@entityTypeId WHEN 3 THEN r.receiverid ELSE r.senderId END 
            inner join relTaxTypesExempted_tb re on ir.relTaxId = re.relTaxId 
            where r.entitytypeid = @@entityTypeId and d.corcentricCode = @@corcentricCode and r.relationshiptypeid = @@Relationshiptypeid

            delete from relTax_tb from relsenderreceiver_tb r inner join relTax_tb ir on r.senderreceiverrelid = ir.senderreceiverrelid 
            inner join entitydetails_tb d on d.entitydetailid = CASE @@entityTypeId WHEN 3 THEN r.receiverid ELSE r.senderId END 
            where r.entitytypeid = @@entityTypeId and d.corcentricCode = @@corcentricCode and r.relationshiptypeid = @@Relationshiptypeid

            delete from relsenderreceiver_tb from relsenderreceiver_tb r 
            inner join entitydetails_tb d on d.entitydetailid = CASE @@entityTypeId WHEN 3 THEN r.receiverid ELSE r.senderId END 
            where r.entitytypeid = @@entityTypeId and d.corcentricCode = @@corcentricCode and r.relationshiptypeid = @@Relationshiptypeid and r.isactive = 1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@corcentricCode", corcentricCode)
            };
            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal string GetNumberOfYears(string dealerCode)
        {
            var query = @"select noOfYears from relEnhancedDuplicateCheck_tb re
                inner join relSenderReceiver_tb r on r.senderReceiverRelId = re.senderReceiverRelId
                inner join lookUp_tb l on l.lookUpId = r.relationshipTypeId
                inner join entityDetails_tb ed on ed.entityDetailId = r.senderId
                where l.name = 'Enhanced Duplicate Check' and r.isActive = 1 and r.receiverId = 0
                and ed.corcentricCode = @dealerCode order by r.lastUpdateDate desc;";
            List<SqlParameter> sp = new()
            {
            new SqlParameter("@dealerCode", dealerCode)
            };
            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0).ToString();
                }
            }
            return string.Empty;

        }

        internal bool DeleteEnhancedDuplicateCheckRelationship(string corcentricCode)
        {
            string query = @"declare @@corcentricCode nvarchar(100) 
                            declare @@Relationshiptypeid int 
                            declare @@entityTypeId int 
                            set @@corcentricCode = @corcentricCode 
                            set @@entityTypeId = (select top 1 entityTypeId from entityDetails_tb where corcentricCode = @@corcentricCode) 
                            select @@Relationshiptypeid = lookupid from lookup_tb where parentlookupcode = 32 and name = 'Enhanced Duplicate Check';

                            delete from relEnhancedDuplicateCheck_tb from relsenderreceiver_tb r 
                            inner join relEnhancedDuplicateCheck_tb ir on r.senderreceiverrelid = ir.senderreceiverrelid 
                            inner join entitydetails_tb d on d.entitydetailid = CASE @@entityTypeId WHEN 3 THEN r.receiverid ELSE r.senderId END 
                            where r.entitytypeid = @@entityTypeId and d.corcentricCode = @@corcentricCode and r.relationshiptypeid = @@Relationshiptypeid

                            delete from relsenderreceiver_tb from relsenderreceiver_tb r 
                            inner join entitydetails_tb d on d.entitydetailid = CASE @@entityTypeId WHEN 3 THEN r.receiverid ELSE r.senderId END 
                            where r.entitytypeid = @@entityTypeId and d.corcentricCode = @@corcentricCode and r.relationshiptypeid = @@Relationshiptypeid and r.isactive = 1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@corcentricCode", corcentricCode)
            };
            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool DeleteCorePriceTypeRelationship(string corcentricCode)
        {
            string query = @"declare @@corcentricCode nvarchar(100) 
                declare @@Relationshiptypeid int 
                declare @@entityTypeId int 
                set @@corcentricCode = @corcentricCode 
                set @@entityTypeId = (select top 1 entityTypeId from entityDetails_tb where corcentricCode = @@corcentricCode) 
                select @@Relationshiptypeid = lookupid from lookup_tb where parentlookupcode = 32 and name = 'Core Price Type';

                delete from relCorePriceType_tb from relsenderreceiver_tb r 
                inner join relCorePriceType_tb ir on r.senderreceiverrelid = ir.senderreceiverrelid 
                inner join entitydetails_tb d on d.entitydetailid = CASE @@entityTypeId WHEN 3 THEN r.receiverid ELSE r.senderId END 
                where r.entitytypeid = @@entityTypeId and d.corcentricCode = @@corcentricCode and r.relationshiptypeid = @@Relationshiptypeid

                delete from relsenderreceiver_tb from relsenderreceiver_tb r 
                inner join entitydetails_tb d on d.entitydetailid = CASE @@entityTypeId WHEN 3 THEN r.receiverid ELSE r.senderId END 
                where r.entitytypeid = @@entityTypeId and d.corcentricCode = @@corcentricCode and r.relationshiptypeid = @@Relationshiptypeid and r.isactive = 1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@corcentricCode", corcentricCode)
            };
            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }

        internal bool IsEntityOnlinePay(string corcentricCode)
        {
            var query = "Select enablePaymentPortal from entitydetails_tb where corcentricCode = @corcentricCode";
           
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@corcentricCode", corcentricCode)
            };
            if (ExecuteNonQuery(query, sp, false) < 1)
            {
                return false;
            }
            return true;
        }
    }
}

