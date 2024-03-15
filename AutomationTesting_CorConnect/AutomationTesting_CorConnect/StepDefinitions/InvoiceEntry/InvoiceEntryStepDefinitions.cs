using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.InvoiceEntry
{
    [Binding]
    internal class InvoiceEntryStepDefinitions : DriverBuilderClass
    {
        InvoiceEntryPage InvoiceEntryPage;
        CreateNewInvoicePage CreateNewInvoicePage;
        private string partNumber = CommonUtils.RandomString(6);
        private string dealerName = "Automation_Dealer" + CommonUtils.RandomString(4);
        private string fleetName = "Automation_Fleet" + CommonUtils.RandomString(4);
        private string dealerInvNum = CommonUtils.RandomString(6) + CommonUtils.RandomString(4) + CommonUtils.GetTime();


        [StepDefinition(@"invoice should be submitted sucessfully using Dealer ""([^""]*)"" and Fleet ""([^""]*)"" with invoice type ""([^""]*)""")]
        public void InvoiceCreation(string dealer, string fleet, string partType)
        {
            string dealerInvNumber = CommonUtils.RandomString(6);
            List<string> errorMsgs = new List<string>();
            menu = new Menu(driver);
            menu.OpenNextPage(Pages.AccountMaintenance, Pages.InvoiceEntry);
            InvoiceEntryPage = new InvoiceEntryPage(driver);
            CreateNewInvoicePage = InvoiceEntryPage.OpenCreateNewInvoice();
            errorMsgs = CreateNewInvoicePage.CreateNewInvoiceWithLineItems(fleet, dealer, dealerInvNumber, partType);

            Assert.IsTrue(CommonUtils.IsInvoiceValidated(dealerInvNumber));
            decimal availCred = CommonUtils.GetAvailableCreditLimit(fleet);
            Assert.IsTrue(Convert.ToString(availCred).Contains('-'));

            int invSystemType = CommonUtils.GetSystemType(dealerInvNumber);
            string systemType = Convert.ToString(invSystemType);
            if (systemType == "1" || systemType == "2" || systemType == "3")
            {
                Assert.Pass();
            }
            else
            {
                errorMsgs.Add("System Type Does Not Contain Required Value");
            }
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });


        }




    }
}
