using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreateNewEntity
{
    internal class CreateNewEntityDAL : BaseDataAccessLayer
    {
        internal EntityDetails GetEntityDetails(string corcentricCode)
        {
            EntityDetails entityDetails = null;
            try
            {
                string spName = "select * from entityDetails_tb where corcentricCode = @corcentricCode;";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@corcentricCode", corcentricCode),
                };

                using (var reader = ExecuteReader(spName, sp, false))
                {
                    if (reader.Read())
                    {
                        entityDetails = new EntityDetails();
                        entityDetails.AccountingCode = reader.GetStringValue("accountingCode");
                        entityDetails.CommunityCode = reader.GetStringValue("communityCode");
                        entityDetails.CorcentricCode = reader.GetStringValue("corcentricCode");
                        entityDetails.CorcentricLocation = reader.GetBooleanValue("corcentricLocation");
                        entityDetails.CreatedDate = reader.GetDateTime(reader.GetReaderIndex("createdDate"));
                        entityDetails.CreditLimit = reader.GetDouble(reader.GetReaderIndex("creditLimit"));
                        entityDetails.DDEFlag = reader.GetBooleanValue("DDEFlag");
                        entityDetails.EntityCode = reader.GetStringValue("entityCode");
                        entityDetails.EntityDetailId = reader.GetInt32(reader.GetReaderIndex("entityDetailId"));
                        entityDetails.FinanceChargeExempt = reader.GetBooleanValue("financeChargeExempt");
                        entityDetails.MasterFlag = reader.GetBooleanValue("masterFlag");
                        entityDetails.EnablePaymentPortal = reader.GetBooleanValue("enablePaymentPortal");
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

        internal EntityDetails GetZipAndState(string corcentricCode)
        {
            EntityDetails entityDetails = null;
           try
            {
                string spName = "SELECT ed.corcentriccode, ED.accountingCode, ad.address1, ad.address2, ad.countryCode, ad.state, ad.zip FROM address_tb AD WITH(NOLOCK) INNER JOIN entityAddressRel_tb RA  WITH(NOLOCK) ON ad.addressId = ra.addressId INNER JOIN entityDetails_tb ED  WITH(NOLOCK) ON ra.entityDetailId = ED.entityDetailId WHERE ad.addressTypeId = 228 AND ed.entityDetailId  in (select entitydetailid from entitydetails_tb where  corcentriccode = @corcentricCode)";
                
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@corcentricCode", corcentricCode),
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
    }
}
