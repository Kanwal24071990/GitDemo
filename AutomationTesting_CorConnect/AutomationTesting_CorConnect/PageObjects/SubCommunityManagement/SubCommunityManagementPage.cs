using AutomationTesting_CorConnect.PageObjects.Sub_communityManagement;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.SubcommunityManagement
{
    internal class SubcommunityManagementPage : Commons
    {
        internal SubcommunityManagementPage(IWebDriver webDriver) : base(webDriver, Pages.Sub_communityManagement)
        {
            
        }
        internal CreateNewSubcommunityPage OpenNewSubcommunityPage()
        {
            ButtonClick(ButtonsAndMessages.CreateNewSub_community);
            SwitchToPopUp();
            return new CreateNewSubcommunityPage(driver);
        }
        internal CreateNewSubcommunityPage UpdateNewSubCommunityPage()
        {
            SwitchToPopUp();
            return new CreateNewSubcommunityPage(driver);
        }


        internal void PopulateGrid(string subCommunityName)
        {
            EnterTextAfterClear(FieldNames.Sub_communityName, subCommunityName);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }
    }
}
