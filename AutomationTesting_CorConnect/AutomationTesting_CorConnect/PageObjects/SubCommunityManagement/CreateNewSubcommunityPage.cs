using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.PageObjects.SubCommunityManagement;
using AutomationTesting_CorConnect.Resources;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.Sub_communityManagement
{
    internal class CreateNewSubcommunityPage : PopUpPage
    {
        internal CreateNewSubcommunityPage(IWebDriver WebDriver) : base(WebDriver, "Create New Sub-community")
        {

        }

        internal void PopulateSubCommunityFields(string subCommunityType, string subCommunityName, string subCommunityCode, string sftpHost, string sftpFolder)
        {
            if (subCommunityType == "Fleet")
            {
                subCommunityType = RenameMenuField(subCommunityType);
                SearchAndSelectValueWithoutLoadingMsg(FieldNames.SubCommunityType, subCommunityType);
                WaitForAnyElementLocatedBy(FieldNames.SFTPHost);
                EnterText(FieldNames.Sub_communityName, subCommunityName);
                EnterText(FieldNames.Sub_communityCode, subCommunityCode);
                EnterText(FieldNames.SFTPHost, sftpHost);
                EnterText(FieldNames.OpenBalance_SFTP_Folder, sftpFolder);
                EnterText(FieldNames.DraftStatement_SFTP_Folder, "/imports/DraftStatements");
                EnterText(FieldNames.Dunning_SFTPFolder, "/imports/DunningLetters");
                EnterText(FieldNames.Description, "Auto QA Description" + DateTime.Today.ToString());
            }

            else
            {
                subCommunityType = RenameMenuField(subCommunityType);
                if (GetValueOfDropDown(FieldNames.SubCommunityType) != subCommunityType)
                {
                    SearchAndSelectValueWithoutLoadingMsg(FieldNames.SubCommunityType, subCommunityType);
                }

                EnterText(FieldNames.Sub_communityName, subCommunityName);
                EnterText(FieldNames.Sub_communityCode, subCommunityCode);
            }

        }

        internal void UpdateSubCommunityFields(string subCommunityName, string subCommunityCode)
        {
            EnterTextAfterClear(FieldNames.Sub_communityName, subCommunityName);
            EnterTextAfterClear(FieldNames.Sub_communityCode, subCommunityCode);
            Click(ButtonsAndMessages.Update);
        }


        internal void AssignLocationsByIndex(int index)
        {
            SimpleSelectOptionByIndex("Unassigned List", index);
            Click("Assign");
        }

        internal AddRemittanceAddressPage OpenRemittanceAddressPage()
        {
            Click(ButtonsAndMessages.AddNew);
            SwitchToPopUp();
            return new AddRemittanceAddressPage(driver);
        }

    }
}
