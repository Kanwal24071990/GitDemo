using System.Collections.Generic;
using System.Data.SqlClient;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceDownloadSetup
{
    internal class InvoiceDownloadSetupDAL: BaseDataAccessLayer
    { 
        internal bool GetInvoice(string exportName)
        {
            string query = "select count(*) from ExportConfig_tb where exportConfigName =@exportName";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@exportName", exportName),
            };

            using (var reader=ExecuteReader(query, sp, false))
            {
                if(reader.Read())
                {
                    if(reader.GetInt32(0)==1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
