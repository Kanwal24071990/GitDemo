using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.GrossMarginCreditReport
{
    internal class GrossMarginCreditReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                query = @"SELECT top 1 Convert (Date , invoice.APInvoiceDate) as FromDate ,Convert (Date , invoice.APInvoiceDate) as ToDate FROM [invoice_tb] invoice WITH (NOLOCK)INNER JOIN [invoiceSection_tb] section WITH (NOLOCK) ON section.invoiceId = invoice.invoiceId INNER JOIN [invoiceLineDetail_tb] line WITH (NOLOCK) ON line.invoiceSectionId = section.invoiceSectionId INNER JOIN [entityDetails_tb] senderEntity WITH (NOLOCK) ON senderEntity.entityDetailId = invoice.senderEntityDetailId INNER JOIN [entityDetails_tb] receiverEntity WITH (NOLOCK) ON receiverEntity.entityDetailId = invoice.receiverEntityDetailId INNER JOIN [entityDetails_tb] receiverbillToEntity WITH (NOLOCK) ON receiverbillToEntity.entityDetailId = invoice.receiverBillToEntityDetailId WHERE invoice.isActive = 1 AND (ISNULL(line.GMCAmount, 0) <> 0) AND invoice.GMCTotal IS NOT NULL AND invoice.APInvoiceDate IS NOT NULL ORDER BY invoice.APInvoiceDate DESC";
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                        ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                FromDate = null;
                ToDate = null;
            }
        }
    }
}
