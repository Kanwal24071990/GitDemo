using AutomationTesting_CorConnect.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreateInvoiceWatchList
{
    internal class CreateInvoiceWatchListDAL : BaseDataAccessLayer
    {
        internal int GetDealerEntitiesCount()
        {
            int count = 0;
            try
            {
                string query = @"select Count(*) from entityDetails_tb 
                        inner join entityaddressrel_tb on entityDetails_tb.entityDetailId = entityaddressrel_tb.entityDetailId 
                        inner join address_tb on entityaddressrel_tb.addressId = address_tb.addressId where 
                        entityTypeId = 2 AND enrollmentStatusId = 13 AND LocationTypeID = 25";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return count;
            }
        }

        internal int GetFleetEntitiesCount()
        {
            int count = 0;
            try
            {
                string query = @"select Count(*) from entityDetails_tb  
                    inner join entityaddressrel_tb on entityDetails_tb.entityDetailId= entityaddressrel_tb.entityDetailId 
                    inner join address_tb on entityaddressrel_tb.addressId= address_tb.addressId  
                    where entityTypeId = 3 AND enrollmentStatusId = 13 AND LocationTypeID = 25";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return count;
            }
        }

        internal string GetExistingInvoiceDealer()
        {
            string dealerName = string.Empty;
            try
            {
                string query = @"select top 1 displayName from entityDetails_tb e 
                    inner join entityaddressrel_tb on e.entityDetailId = entityaddressrel_tb.entityDetailId 
                    inner join address_tb on entityaddressrel_tb.addressId = address_tb.addressId where 
                    e.entityTypeId = 2 and e.isActive = 1 and e.isTerminated = 0 AND e.enrollmentStatusId = 13 AND e.LocationTypeID = 25 and e.entitydetailid in 
                    (select senderentitydetailid from invoiceWatchList_tb)";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        dealerName = reader.GetString(0);
                    }
                }
                return dealerName;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return dealerName;
            }
        }

        internal string GetExistingInvoiceFleet()
        {
            string dealerName = string.Empty;
            try
            {
                string query = @"select top 1 displayName  from entityDetails_tb e 
                    inner join entityaddressrel_tb on e.entityDetailId = entityaddressrel_tb.entityDetailId 
                    inner join address_tb on entityaddressrel_tb.addressId = address_tb.addressId where 
                    e.entityTypeId = 3 and e.isActive = 1 and e.isTerminated = 0 AND e.enrollmentStatusId = 13 AND e.LocationTypeID = 25 and e.entitydetailid in 
                    (select receiverEntityDetailId from invoiceWatchList_tb)";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        dealerName = reader.GetString(0);
                    }
                }
                return dealerName;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return dealerName;
            }
        }

        internal string GetActiveDealer()
        {
            string dealerName = string.Empty;
            try
            {
                string query = @"select displayName from entityDetails_tb e 
                    inner join entityaddressrel_tb on e.entityDetailId = entityaddressrel_tb.entityDetailId 
                    inner join address_tb on entityaddressrel_tb.addressId = address_tb.addressId where 
                    e.entityTypeId = 2 and e.isActive = 1 and e.isTerminated = 0 AND e.enrollmentStatusId = 13 AND e.LocationTypeID = 25 and e.entitydetailid not in 
                    (select senderentitydetailid from invoiceWatchList_tb)";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        dealerName = reader.GetString(0);
                    }
                }
                return dealerName;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return dealerName;
            }
        }

        internal string GetInActiveDealer()
        {
            string dealerName = string.Empty;
            try
            {
                string query = @"select displayName  from entityDetails_tb e 
                    inner join entityaddressrel_tb on e.entityDetailId = entityaddressrel_tb.entityDetailId 
                    inner join address_tb on entityaddressrel_tb.addressId = address_tb.addressId where 
                    e.entityTypeId = 2 and e.isActive = 0 AND e.enrollmentStatusId = 13 AND e.LocationTypeID = 25 and e.entitydetailid not in 
                    (select senderentitydetailid from invoiceWatchList_tb)";


                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        dealerName = reader.GetString(0);
                    }
                }
                return dealerName;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return dealerName;
            }
        }

        internal string GetTerminatedDealer()
        {
            string dealerName = string.Empty;
            try
            {
                string query = @"select displayName  from entityDetails_tb e 
                    inner join entityaddressrel_tb on e.entityDetailId = entityaddressrel_tb.entityDetailId 
                    inner join address_tb on entityaddressrel_tb.addressId = address_tb.addressId where 
                    e.entityTypeId = 2 and e.isTerminated = 1 AND e.enrollmentStatusId = 13 AND e.LocationTypeID = 25 and e.entitydetailid not in 
                    (select senderentitydetailid from invoiceWatchList_tb)";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        dealerName = reader.GetString(0);
                    }
                }
                return dealerName;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return dealerName;
            }
        }

        internal string GetActiveFleet()
        {
            string flletName = string.Empty;
            try
            {
                string query = @"select displayName  from entityDetails_tb e 
                    inner join entityaddressrel_tb on e.entityDetailId = entityaddressrel_tb.entityDetailId 
                    inner join address_tb on entityaddressrel_tb.addressId = address_tb.addressId where 
                    e.entityTypeId = 3 and e.isActive = 1 and e.isTerminated = 0 AND e.enrollmentStatusId = 13 AND e.LocationTypeID = 25 and e.entitydetailid not in  
                    (select receiverEntityDetailId from invoiceWatchList_tb)";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        flletName = reader.GetString(0);
                    }
                }
                return flletName;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return flletName;
            }
        }

        internal string GetInActiveFleet()
        {
            string flletName = string.Empty;
            try
            {
                string query = @"select displayName  from entityDetails_tb e 
                    inner join entityaddressrel_tb on e.entityDetailId = entityaddressrel_tb.entityDetailId 
                    inner join address_tb on entityaddressrel_tb.addressId = address_tb.addressId where 
                    e.entityTypeId = 3 and e.isActive = 0 AND e.enrollmentStatusId = 13 AND e.LocationTypeID = 25 and e.entitydetailid not in 
                    (select receiverEntityDetailId from invoiceWatchList_tb)";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        flletName = reader.GetString(0);
                    }
                }
                return flletName;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return flletName;
            }
        }

        internal string GetTerminatedFleet()
        {
            string flletName = string.Empty;
            try
            {
                string query = @"select displayName  from entityDetails_tb e 
                    inner join entityaddressrel_tb on e.entityDetailId = entityaddressrel_tb.entityDetailId 
                    inner join address_tb on entityaddressrel_tb.addressId = address_tb.addressId where 
                    e.entityTypeId = 3 and e.isTerminated = 1 AND e.enrollmentStatusId = 13 AND e.LocationTypeID = 25 and e.entitydetailid not in 
                    (select receiverEntityDetailId from invoiceWatchList_tb)";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        flletName = reader.GetString(0);
                    }
                }
                return flletName;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return flletName;
            }
        }

        internal bool ChangeIsActiveValue(string displayName, bool isActive)
        {
            int active = isActive ? 1 : 0;
            try
            {
                string query = @"update entitydetails_tb set isactive = @isActive where entitydetailid = (select entityDetailId from entitydetails_tb where displayName = @displayName)";
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@isActive", active),
                    new SqlParameter("@displayName", displayName),
                };
                if (ExecuteNonQuery(query, sp, false) < 1)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                return false;
            }
        }
    }
}
