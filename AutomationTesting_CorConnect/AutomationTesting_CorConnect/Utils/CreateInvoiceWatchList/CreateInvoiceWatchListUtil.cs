using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreateInvoiceWatchList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.CreateInvoiceWatchList
{
    internal class CreateInvoiceWatchListUtil
    {
        internal static string GetExistingInvoiceDealer()
        {
            return new CreateInvoiceWatchListDAL().GetExistingInvoiceDealer();
        }
        internal static string GetExistingInvoiceFleet()
        {
            return new CreateInvoiceWatchListDAL().GetExistingInvoiceFleet();
        }
        internal static string GetActiveDealer()
        {
            return new CreateInvoiceWatchListDAL().GetActiveDealer();
        }
        internal static string GetInActiveDealer()
        {
            return new CreateInvoiceWatchListDAL().GetInActiveDealer();
        }
        internal static string GetTerminatedDealer()
        {
            return new CreateInvoiceWatchListDAL().GetTerminatedDealer();
        }
        internal static string GetActiveFleet()
        {
            return new CreateInvoiceWatchListDAL().GetActiveFleet();
        }
        internal static string GetInActiveFleet()
        {
            return new CreateInvoiceWatchListDAL().GetInActiveFleet();
        }
        internal static string GetTerminatedFleet()
        {
            return new CreateInvoiceWatchListDAL().GetTerminatedFleet();
        }

        internal static int GetDealerEntitiesCount()
        {
            return new CreateInvoiceWatchListDAL().GetDealerEntitiesCount();
        }

        internal static int GetFleetEntitiesCount()
        {
            return new CreateInvoiceWatchListDAL().GetFleetEntitiesCount();
        }

        internal static bool ChangeIsActiveValue(string displayName, bool isActive)
        {
            return new CreateInvoiceWatchListDAL().ChangeIsActiveValue(displayName, isActive);
        }
    }
}
