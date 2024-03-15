using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerSalesSummaryLocation
{
    internal class DealerSalesSummaryLocationDAL : BaseDataAccessLayer
    {
        internal void GetData(out string from, out string to)
        {
            from = String.Empty;
            to = String.Empty;

            string query = @"Select  top 1 Convert (Date , invoicedate) as FromDate, 
                   Convert (Date , invoicedate) as ToDate  FROM  invoice_tb iv with (nolock)
                   inner join invoiceReport_tb ir on iv.invoiceId = ir.invoiceId
                   where iv.isActive = 1
                   Order by invoicedate desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    from = CommonUtils.ConvertDate(reader.GetDateTime(0).Date);
                    to = CommonUtils.ConvertDate(reader.GetDateTime(1).Date);
                }
            }
        }
    }
}
