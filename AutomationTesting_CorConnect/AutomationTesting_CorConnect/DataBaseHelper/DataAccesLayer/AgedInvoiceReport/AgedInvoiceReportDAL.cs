using AutomationTesting_CorConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.AgedInvoiceReport
{
    internal class AgedInvoiceReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string location)
        {
            location = string.Empty;
            string query = null;
            // dealer user and admin
            try
            {
                query = @"DECLARE @@MasterInvoiceStatementTypeId INT; SELECT @@MasterInvoiceStatementTypeId = lookupid FROM [lookup_tb] WHERE parentlookupcode=119 and lookupcode=3; With #entityDetails as (SELECT  entityDetailId,displayName, corcentricCode, accountingCode, enrollmentStatusId, locationTypeId FROM [entityDetails_tb] WITH(NOLOCK) WHERE isActive = 1 And ISNULL(locationTypeId, 0) <> 0 ),#billingentities as (select entitydetailid, accountingCode,corcentricCode FROM #entityDetails WHERE locationTypeId = 25 and enrollmentStatusId = 13),#agedInvoicesCurrent as (select  case when ISNULL(I.appaidinFull,0)= 0 AND I.appostedtoAccounting = 0 then s.corcentricCode else R.corcentricCode end as SearchCorcentriCode FROM [invoice_tb] I WITH (NOLOCK) INNER JOIN #BillingEntities E on  E.entityDetailId = I.senderBillToEntityDetailId INNER JOIN  #entityDetails S  on  S.entityDetailId = I.senderEntityDetailId INNER JOIN  #entityDetails R  on  R.entityDetailId = I.receiverEntityDetailId LEFT JOIN [statementDetail_tb] STD WITH (NOLOCK) ON STD.invoiceId=I.invoiceId LEFT JOIN [statement_tb] ST WITH (NOLOCK) ON ST.statementId = STD.statementId AND ST.statementType = 'AR' WHERE I.systemType = 0  and I.isActive=1 and  ((ISNULL(I.appaidinFull,0)= 0 AND I.appostedtoAccounting = 0  ) or (ISNULL(I.arpaidinFull,0)= 0 AND I.arpostedtoAccounting = 0  AND ARstatementTypeId <> @@MasterInvoiceStatementTypeId )) Union all select R.corcentricCode as SearchCorcentriCode  FROM [statement_tb] i with (nolock) INNER JOIN #BillingEntities E on  E.entityDetailId = I.entityDetailId INNER JOIN #entityDetails R  on  R.entityDetailId = I.invoiceEntityDetailid WHERE ISNULL(I.paidInFull,0)= 0 AND ISNULL(I.postedtoAccounting,0) = 0 AND I.isActive=1 AND i.statementTypeId = @@MasterInvoiceStatementTypeId AND i.statementStatus=1), FinalList as (select E.corcentricCode as SearchCorcentriCode FROM [gpOpenInvoices_tb] OI with (nolock) INNER JOIN #BillingEntities E on E.accountingCode = OI.accountingCode where  OI.isActive=1 and OI.curTransactionAmt <>0 union all Select SearchCorcentriCode from #agedInvoicesCurrent) SELECT TOP 1 * FROM FinalList;";
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
