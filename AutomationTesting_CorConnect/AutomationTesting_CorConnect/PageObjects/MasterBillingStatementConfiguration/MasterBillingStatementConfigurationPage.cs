using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.MasterBillingStatementConfiguration.CreateNewMasterBillingStatementConfiguration;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.MasterBillingStatementConfiguration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.MasterBillingStatementConfiguration
{
    internal class MasterBillingStatementConfigurationPage : Commons
    {
        internal MasterBillingStatementConfigurationPage(IWebDriver driver) : base(driver, Pages.MasterBillingStatementConfiguration) { }

        internal void PopulateGrid(string communityStatementName = null )
        {
            if(communityStatementName != null)
            {
                EnterTextAfterClear(ButtonsAndMessages.CommunityStatementName, communityStatementName);
            }
            
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal CreateNewMasterBillingStatementConfigurationPage OpenCreateNewStatement()
        {
            gridHelper.ButtonClick(ButtonsAndMessages.CreateConfiguration);
            SwitchToPopUp();
            return new CreateNewMasterBillingStatementConfigurationPage(driver);
        }

        internal CreateNewMasterBillingStatementConfigurationPage OpenEditStatement()
        {
            gridHelper.ClickAnchorButton(TableHeaders.Commands, ButtonsAndMessages.Edit);
            SwitchToPopUp();
            return new CreateNewMasterBillingStatementConfigurationPage(driver);
        }

        internal void DeleteStatementConfiguration(string communityStatementName)
        {
            MasterBillingStatementConfigurationUtils.DeleteStatementConfiguration(communityStatementName);
        }
    }
}
