using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.DataObjects;

namespace AutomationTesting_CorConnect.Utils.DealerInvoiceTransactionLookup
{
    internal class DealerInvoiceTransactionLookupUtil
    {
        internal static void GetData(out string FromDate, out string ToDate) { 

            new DealerInvoiceTransactionLookupDAL().GetData(out FromDate, out ToDate);
        }

        internal static int GetRowCountTransactionStatus(string transactionStatus)
        { 
            return new DealerInvoiceTransactionLookupDAL().GetRowCountTransactionStatus(transactionStatus);
        }

        internal static InvoiceObject GetInvoiceInfoFromTransactionStatus()
        { 
            return new DealerInvoiceTransactionLookupDAL().GetInvoiceInfoFromTransactionStatus();
        }
        internal static int GetInvoicesCountBySubCommunities(string dealerSubCommunity, string fleetSubCommunity)
        {
            return new DealerInvoiceTransactionLookupDAL().GetInvoicesCountBySubCommunities(dealerSubCommunity, fleetSubCommunity);
        }
        internal static EntityDetails GetEntityGroup(string user)
        {
            return new DealerInvoiceTransactionLookupDAL().GetEntityGroup(user);
        }
        internal static int GetInvoicesCountByGroup(string user, string dealerGroup, string fleetGroup)
        { 
            return new DealerInvoiceTransactionLookupDAL().GetInvoicesCountByGroup(user , dealerGroup, fleetGroup);
        }

        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new DealerInvoiceTransactionLookupDAL().GetCountByDateRange(dateRange, days);
        }

    }
}