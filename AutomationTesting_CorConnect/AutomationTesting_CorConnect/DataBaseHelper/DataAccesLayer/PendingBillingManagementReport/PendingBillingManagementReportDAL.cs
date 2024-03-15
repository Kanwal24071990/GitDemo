using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.PendingBillingManagementReport
{
    internal class PendingBillingManagementReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string companyName)
        {
            companyName = string.Empty;
            string query = null;
            string code = "";
            string client = ApplicationContext.GetInstance().client.ToUpper();

            if (client == "Vipar" || client == "Alliance")

            {

                code = "d.corcentriccode";

            }

            else
            {

                code = "d.displayname";

            }

            try
            {
                query = @"DECLARE @@invoiceValidityDays int DECLARE @@tempInvoiceValidityDate DATETIME SET NOCOUNT ON SELECT @@invoiceValidityDays = isnull(invoiceValidityDays, 60) FROM [community_tb] where isActive=1 SELECT @@tempInvoiceValidityDate =DATEADD(day,-@@invoiceValidityDays,getdate())SELECT top 1 " + code + " FROM [transaction_tb] T WITH (NOLOCK) LEFT JOIN [entitydetails_tb] d on  d.entitydetailid=t.senderEntityDetailId WHERE ((T.requestTypeCode = 'S' and T.isActive = 1 and T.isHistorical = 0 and T.transactionDate >= @@tempInvoiceValidityDate and  T.validationStatus in (2, 3, 4)) OR(T.requestTypeCode in ('T', 'A') and T.isActive = 1  and T.expirationDate >= getdate() and T.validationStatus in (1,2)) ) ORDER BY d.corcentriccode DESC;";
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        companyName = reader.GetString(0);

                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                companyName = null;
             
            }
        }
    }
}
