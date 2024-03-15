using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.AccountMaintenance;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.InvoiceEntry.CreateNewInvoice;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Utils.InvoiceEntry.CreateNewInvoice
{
    internal class CreateNewInvoiceUtils
    {

        CreateNewInvoiceDAL createNewInvoiceDAL = new CreateNewInvoiceDAL();

        internal int UpdateFields()
        {
            return createNewInvoiceDAL.UpdateFields();
        }

        internal List<string> GetCountries()
        {
            return createNewInvoiceDAL.GetCountries();
        }

        internal List<string> GetStates(string country)
        {
            return createNewInvoiceDAL.GetStates(country);
        }

        internal void GetDealerStateCountry(string dealer,out  string country, out string state)
        {
            createNewInvoiceDAL.GetDealerStateCountry(dealer, out country, out state);
        }

        internal DateTime GetApDueDate(string transactionNumber)
        {
           return createNewInvoiceDAL.GetApDueDate(transactionNumber);
        }
        
    }
}
