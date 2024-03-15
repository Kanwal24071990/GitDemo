using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace AutomationTesting_CorConnect.PageObjects.GroupRelationshipsMaintenance
{
    internal class GroupRelationshipsMaintenancePage : PopUpPage
    {
        internal GroupRelationshipsMaintenancePage(IWebDriver webDriver) : base(webDriver, Pages.GroupRelationshipsMaintenance)
        {
        }

        internal void WaitForLoadingMessageThisPage()
        {
            WaitForElementToBeVisible(GetElement(FieldNames.LoadingMessage));
            WaitForElementToBeInvisibleCustomWait(GetElement(FieldNames.LoadingMessage));
        }

        internal void DeleteFirstRelationShipIfExist(string table, string header, string headerName, string buttonCaption)
        {
            if (IsRelationExist(table, header, headerName, buttonCaption) == true)
            {
                ClickHyperLinkOnGrid("RelationShipTable", "RelationShipTableHeader", "#");
                AcceptAlert(out string msg);
                WaitforStalenessofRelationShipTable();
            }
        }

        internal void WaitforStalenessofRelationShipTable()
        {
            wait.Until(ExpectedConditions.StalenessOf(driver.FindElement(GetElement("RelationShipTable"))));
        }
    }
}
