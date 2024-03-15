using AutomationTesting_CorConnect.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceEntry.CreateNewInvoice
{
    internal class CreateNewInvoiceDAL : BaseDataAccessLayer
    {

        internal int UpdateFields()
        {
            var query = "update uiFieldsDisplay_tb set isactive = 1 where uifieldsdisplayid = 31; update uiFieldsDisplay_tb set isactive = 1 where uifieldsdisplayid = 32; update uiFieldsDisplay_tb set isactive = 1 where uifieldsdisplayid = 33; update uiFieldsDisplay_tb set isactive = 1 where uifieldsdisplayid = 34; update uiFieldsDisplay_tb set isactive = 1 where uifieldsdisplayid = 38; ";

            return ExecuteNonQuery(query, false);

        }

        internal List<string> GetCountries()
        {
            var query = "Select DISTINCT Country from StateCountry_tb ORDER BY Country";
            var countries = new List<string>();

            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    while (reader.Read())
                    {
                        countries.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return countries;
        }

        internal List<string> GetStates(string country)
        {
            var query = " select * from statecountry_tb where country = @State";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@State", country),
            };

            var States = new List<string>();

            try
            {
                using (var reader = ExecuteReader(query, sp, false))
                {
                    while (reader.Read())
                    {
                        States.Add(reader.GetString(1));
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return States;
        }

        internal void GetDealerStateCountry(string dealer, out string country, out string state)
        {
            string query = "select * from address_tb a where addressId=(select a.addressId from entityAddressRel_tb a inner join entityDetails_tb d on a.entityDetailId=d.entityDetailId where d.displayName=@dealer)";
            country = string.Empty;
            state = string.Empty;
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@dealer", dealer),
            };

            try
            {
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        state = reader["State"].ToString();
                        country = reader["countryCode"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        internal DateTime GetApDueDate(string transactionNumber)
        {
            string query = "select top 1  transactionNumber, apInvoiceDueDate from invoice_tb where transactionNumber = @transactionNumber order by invoiceId desc";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@transactionNumber", transactionNumber),
            };

            try
            {
                using (var reader = ExecuteReader(query, sp, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetDateTime(1);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return DateTime.MinValue;
        }
    }
}