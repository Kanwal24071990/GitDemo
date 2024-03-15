using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.Sub_communityManagement;
using AutomationTesting_CorConnect.PageObjects.SubcommunityManagement;
using AutomationTesting_CorConnect.PageObjects.SubCommunityManagement;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.SubcommunityManagement;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AutomationTesting_CorConnect.StepDefinitions.SubCommunityManagement
{
    [Binding]
    internal class SubCommunityManagementStepDefinitions : DriverBuilderClass
    {

        [Given(@"User creates New Subcommunity:")]
        public void CreateNewSubcommunity(Table table)
        {
            var details = table.CreateInstance<SubcommunityDetails>();
            SubcommunityManagementPage Page;
            Page = new SubcommunityManagementPage(driver);
            CreateNewSubcommunityPage CreateNewSubCommunityPopUp = Page.OpenNewSubcommunityPage();
            string subComCode = details.SubCommunityCode;
            string subComName = details.SubCommunityName;
            //string subComCode = details.SubCommunityCode + subComCodePrefix;
            //string subComName = details.SubCommunityName + subComNamePrefix;
            //sftpHost = details.SFTPHost;
            //sftpLocation = details.SFTPFolder;
            CreateNewSubCommunityPopUp.PopulateSubCommunityFields(details.SubCommunityType, subComName, subComCode, details.SFTPHost, details.SFTPFolder);
            if (CreateNewSubCommunityPopUp.IsElementDisplayed("No Program Code Assignment"))
            {
                Task t = Task.Run(() => CreateNewSubCommunityPopUp.WaitForStalenessOfElement(FieldNames.Sub_communityCode));
                CreateNewSubCommunityPopUp.Click("No Program Code Assignment");
                t.Wait();
                t.Dispose();
            }
            CreateNewSubCommunityPopUp.Click(ButtonsAndMessages.Save);
        }

        [Given(@"Subcommunity ""(.*)"" should be created successfully")]
        public void VerifySubcommunityCreatedSuccessfully(string subCommunityName)
        {
            CreateNewSubcommunityPage CreateNewSubCommunityPopUp;
            CreateNewSubCommunityPopUp = new CreateNewSubcommunityPage(driver);
            CreateNewSubCommunityPopUp.SwitchToPopUp();
            CreateNewSubCommunityPopUp.CheckForText("Record saved successfully.", true);

            CreateNewSubCommunityPopUp.ClosePopupWindow();

            CreateNewSubCommunityPopUp.SwitchToMainWindow();

            SubcommunityManagementPage Page;
            Page = new SubcommunityManagementPage(driver);
            Page.PopulateGrid(subCommunityName);
            //Page.PopulateGrid(subCommunityName + subComNamePrefix);

            Assert.AreEqual(Page.GetRowCount(), 1);
            if (subCommunityName.StartsWith("Fleet"))
            {

                Assert.AreEqual(menu.RenameMenuField(FieldNames.Fleet), Page.GetFirstRowData(TableHeaders.Sub_communityType));
            }
            else
            {
                Assert.AreEqual(menu.RenameMenuField(FieldNames.Dealer), Page.GetFirstRowData(TableHeaders.Sub_communityType));
            }
            Assert.AreEqual(subCommunityName, Page.GetFirstRowData(TableHeaders.Sub_communityName));
            //Assert.AreEqual(subCommunityName + subComNamePrefix, Page.GetFirstRowData(TableHeaders.Sub_communityName));

            if (subCommunityName.StartsWith("FSC"))
            {
                SubcommunityDetails subComDetails = SubcommunityManagementUtil.GetSubCommunityDetails(subCommunityName);
                Assert.AreEqual(subComDetails.SFTPHost, GetErrorMessage(ErrorMessages.IncorrectValue));
                Assert.AreEqual(subComDetails.SFTPFolder, GetErrorMessage(ErrorMessages.IncorrectValue));
                //SubcommunityDetails subComDetails = SubcommunityManagementUtil.GetSubCommunityDetails(subCommunityName + subComNamePrefix);
                //Assert.AreEqual(sftpHost, subComDetails.SFTPHost, GetErrorMessage(ErrorMessages.IncorrectValue));
                //Assert.AreEqual(sftpLocation, subComDetails.SFTPFolder, GetErrorMessage(ErrorMessages.IncorrectValue));
            }

        }
    }
}
