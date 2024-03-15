using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceDownload
{
    internal class InvoiceDownloadDAL:BaseDataAccessLayer
    {
        internal string GetExportConfigName()
        {
            string query = "select top 1 exportConfigName from ExportConfig_tb order by exportConfigId desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return string.Empty;
        }
    }
}
