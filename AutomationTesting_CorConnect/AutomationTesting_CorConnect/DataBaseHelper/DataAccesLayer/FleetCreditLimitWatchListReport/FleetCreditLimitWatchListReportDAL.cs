using AutomationTesting_CorConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetCreditLimitWatchListReport
{
    internal class FleetCreditLimitWatchListReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string location)
        {
            location = string.Empty;
            string query = null;
       
            try
            {
                query = @"With invoices as (select receiverBillToEntityDetailId,transactionAmount,currencyCode,lookUpId, arPostedToAccounting from [invoice_tb] where isActive =1), OpenBalances as (SELECT I.receiverBillToEntityDetailId, SUM(curTransactionAmt) OpenInv, I.currencyCode from invoices I join [gpOpenInvoices_tb] G on G.lookupId =I.lookUpId where(G.lookupId is not null and  G.lookupId <> '') AND G.isActive = 1 and G.isARFlag = 1 and I.arPostedToAccounting = 1 group by I.receiverBillToEntityDetailId,I.currencyCode UNION ALL select I.receiverBillToEntityDetailId, SUM(I.transactionAmount)  OpenInv, I.currencyCode from  invoices I where I.arPostedToAccounting = 0 group by I.receiverBillToEntityDetailId,I.currencyCode),OpenBalancesAndAuthAmt as (select receiverBillToEntityDetailId, 0 OpenAuthorizationAmount,SUM(OpenInv) OpenInvAmt,currencyCode from OpenBalances group by receiverBillToEntityDetailId, currencyCode union all SELECT receiverBillToEntityDetailId, sum(authorizationAmount)OpenAuthorizationAmount, 0 OpenInvs, currencyCode FROM [transaction_tb] T WHERE T.isActive =1 and requestTypeCode in('T', 'A') and T.validationStatus in (1,2,7) group by receiverBillToEntityDetailId, currencyCode),Fleets as (SELECT T.receiverBillToEntityDetailId,SUM(isnull(T.OpenAuthorizationAmount,0)) OpenAuthorizationAmount,SUM(isnull(T.OpenInvAmt, 0))  OpenInvAmt,CurrencyCode FROM OpenBalancesAndAuthAmt T GROUP BY T.receiverBillToEntityDetailId,currencyCode) SELECT top 1 E.corcentriccode FROM [entityDetails_tb] E LEFT OUTER JOIN Fleets T on T.receiverBillToEntityDetailId = E.entityDetailId WHERE E.isActive =1  and entitytypeid=3 and locationtypeid=25 order by E.corcentriccode desc;";
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        location = reader.GetString(0);

                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                location = null;

            }
        }
    }
}
