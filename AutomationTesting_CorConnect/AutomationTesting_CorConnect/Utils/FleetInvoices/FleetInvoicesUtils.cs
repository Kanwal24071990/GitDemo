using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetInvoices;
using AutomationTesting_CorConnect.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.FleetInvoices
{
    internal class FleetInvoicesUtils
    {
        internal static List<FleetInvoiceObject> GetFleetInvoices()
        {
            return new FleetInvoicesDAL().GetFleetInvoices();
        }

        internal static List<FleetInvoiceObject> GetFleetOnlineInvoices()
        {
            return new FleetInvoicesDAL().GetFleetOnlineInvoices();
        }

        internal static void GetData(out string FromDate, out string ToDate)
        {
            new FleetInvoicesDAL().GetData(out FromDate, out ToDate);
        }
        internal static void GetInvoiceDate(string programInvoiceNumber, out string invoiceDate)
        {
            new FleetInvoicesDAL().GetInvoiceDate(programInvoiceNumber, out invoiceDate);
        }
        internal static string GetDisputedInvoice()
        {
           return new FleetInvoicesDAL().GetDisputedInvoice();
        }

        internal static string GetDisputedInvoiceWithMaxNotes()
        {
            return new FleetInvoicesDAL().GetDisputedInvoiceWithMaxNotes();
        }

        internal static List<string> GetResolutionDetails(string actionType)
        {
            return new FleetInvoicesDAL().GetResolutionDetails(actionType);
        }
        internal static List<string> GetActionTypes()
        {
            return new FleetInvoicesDAL().GetActionTypes();
        }
        internal static int GetRowCountTransactionStatus(string transactionStatus)
        {
            return new FleetInvoicesDAL().GetRowCountTransactionStatus(transactionStatus);
        }
        internal static InvoiceObject GetInvoiceInfoFromTransactionStatus()
        { 
            return new FleetInvoicesDAL().GetInvoiceInfoFromTransactionStatus();
        }

        internal static EntityDetails GetInvoicesSubCommunities()
        {
            return new FleetInvoicesDAL().GetInvoicesSubCommunities();
        }
        internal static int GetInvoicesCountBySubCommunities(string dealerSubCommunity, string fleetSubCommunity) 
        {
            return new FleetInvoicesDAL().GetInvoicesCountBySubCommunities(dealerSubCommunity, fleetSubCommunity);
        }
        internal static EntityDetails GetEntityGroup(string user)
        {
            return new FleetInvoicesDAL().GetEntityGroup(user);
        }

        internal static int GetInvoicesCountByGroup(string user, string dealerGroup, string fleetGroup)
        {
            return new FleetInvoicesDAL().GetInvoicesCountByGroup(user,dealerGroup , fleetGroup);
        }
        internal static int GetInvoicesCountByDeliveryStatus()
        {
            return new FleetInvoicesDAL().GetInvoicesCountByDeliveryStatus();
        }

        internal static string GetCurrentInvoice(string dealerName, string fleetName)
        {
            return new FleetInvoicesDAL().GetCurrentInvoice(dealerName, fleetName);
        }

        internal static int GetCountByDateRange(string dateRange, int days)
        {
            return new FleetInvoicesDAL().GetCountByDateRange(dateRange, days);
        }

        internal static string GetBatchGuID(string invoiceNumber)
        {
            return new FleetInvoicesDAL().GetBatchGuID(invoiceNumber);
        }

        internal static string GetInvoiceStatus(string batchID)
        {
            return new FleetInvoicesDAL().GetInvoiceStatus(batchID);
        }

    }
}
