using AutomationTesting_CorConnect.PageObjects.ManageDelinquentFee.DelinquentFeeInvoiceSetup;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;


namespace AutomationTesting_CorConnect.PageObjects.ManageDelinquentFee
{
    internal class ManageDelinquentFeePage : Commons
    {
        internal ManageDelinquentFeePage(IWebDriver webDriver) : base(webDriver, Pages.ManageDelinquentFee)
        {
        }

        internal void PopulateGrid()
        {
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();

        }

        internal DelinquentFeeInvoiceSetupPage OpenDelinquentFeeInvoiceSetup()
        {
            gridHelper.ButtonClick(ButtonsAndMessages.AddNew);
            SwitchToPopUp();
            return new DelinquentFeeInvoiceSetupPage(driver);
        }
    }
}
