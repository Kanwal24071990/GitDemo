using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.UserGroupSetup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;

namespace AutomationTesting_CorConnect.Tests.UserGroupSetup
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("User Group Setup")]
    internal class UserGroupSetup : DriverBuilderClass
    {
        UserGroupSetupPage Page;


        [SetUp]
        public void Setup()
        {
            menu.OpenSingleGridPage(Pages.UserGroupSetup);
            Page = new UserGroupSetupPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20266" })]
        public void TC_20266(string UserType)
        {
            Page.VerifyEditFields(Pages.UserGroupSetup, ButtonsAndMessages.New);
            Assert.AreEqual(Page.CreateNewUser(out string UserGroup, out string UserGroupDescription).Trim(), "The record has been inserted successfully.\r\nPlease use Close button to exit from insert form."); ;
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20556" })]
        public void TC_20556(string UserType)
        {
            Page.VerifyEditFields(Pages.UserGroupSetup, ButtonsAndMessages.Edit);
            Page.CreateNewUser(out string UserGroup, out string UserGroupDescription);
            Page.FilterTable(TableHeaders.Group, UserGroup);
            Assert.AreEqual(Page.EditUser(), "The record has been updated successfully.\r\nPlease use Close button to exit from update form.");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20557" })]
        public void TC_20557(string UserType)
        {
            Page.CreateNewUser(out string UserGroup, out string UserGroupDescription).Trim();
            Page.FilterTable(TableHeaders.Group, UserGroup);
            var alertMsg = Page.DeleteEditField();

            Assert.AreEqual(alertMsg, "Are you sure you want to delete this record?");
            Page.ClearFilter();
            Page.WaitForMsg(ButtonsAndMessages.Loading);
            Page.FilterTable(TableHeaders.Group, UserGroup);
            Assert.AreEqual(Page.GetRowCount(), 0);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24220" })]
        public void TC_24220(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.UserGroupSetup), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            var errorMsgs = Page.ValidateTableHeadersFromFile();

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                string group = Page.GetFirstRowData(TableHeaders.Group);
                Page.FilterTable(TableHeaders.Group, group);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Group, group), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Group, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.Group, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                string commandbuttons = Page.GetFirstRowData(TableHeaders.Commands);
                string[] actualCommandList = commandbuttons.Split(' ');
                string[] expectedCommandList = { "Edit", "New", "Delete" };
                for (int i = 0; i < expectedCommandList.Length; i++)
                {
                    Assert.AreEqual(expectedCommandList[i], actualCommandList[i], $"Command [{actualCommandList[i]}] not found");
                }

                errorMsgs.AddRange(Page.VerifyEditFields(Pages.UserGroupSetup, ButtonsAndMessages.New));
                errorMsgs.AddRange(Page.VerifyEditFields(Pages.UserGroupSetup, ButtonsAndMessages.Edit));
                Page.CreateNewUser(out string UserGroup, out string UserGroupDescription).Trim();
                Page.FilterTable(TableHeaders.Group, UserGroup);
                var alertMsg = Page.DeleteEditField();
                Assert.AreEqual(alertMsg, "Are you sure you want to delete this record?");
                Page.WaitForStalenessTable();
                Page.ClearFilter();
                Page.FilterTable(TableHeaders.Group, UserGroup);
                int rowCount = Page.GetRowCountCurrentPage();
                Assert.AreEqual(0, rowCount);
                Page.ClearFilter();
                Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));
                errorMsgs.AddRange(Page.ValidateNestedTableDetails(true, true));
                errorMsgs.AddRange(Page.ValidateNestedGridTabs("Functions", "Assign Users to User Group"));
                Page.IsInputVisible(ButtonsAndMessages.SaveSelectedFunctions);
                errorMsgs.AddRange(Page.ValidateNestedGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));

                if (Page.IsNestedTableRowCheckBoxUnChecked())
                {
                    Page.CheckNestedTableRowCheckBoxByIndex(1);
                    Page.ClickInputButton(ButtonsAndMessages.SaveSelectedFunctions);

                    if (Page.IsNestedTableRowCheckBoxUnChecked())
                    {
                        errorMsgs.Add("Save Functionality Did Not Worked");
                    }
                }
                else if (!Page.IsNestedTableRowCheckBoxUnChecked())
                {
                    Page.CheckNestedTableRowCheckBoxByIndex(1);
                    Page.ClickInputButton(ButtonsAndMessages.SaveSelectedFunctions);

                    if (!Page.IsNestedTableRowCheckBoxUnChecked())
                    {
                        errorMsgs.Add("Save Functionality Did Not Worked");
                    }
                }

                string nestedGroup = Page.GetFirstRowDataNestedTable(TableHeaders.FunctionName);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                Page.FilterNestedTable(TableHeaders.FunctionName, nestedGroup);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByNestedHeader(TableHeaders.FunctionName, nestedGroup), ErrorMessages.NoRowAfterFilter);
                Page.FilterNestedTable(TableHeaders.FunctionName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountNestedPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearNestedFilter(1);
                Assert.IsTrue(Page.GetRowCountNestedPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterNestedTable(TableHeaders.FunctionName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountNestedPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetNestedFilter(1);
                Assert.IsTrue(Page.GetRowCountNestedPage() > 0, ErrorMessages.ResetNotWorking);
                Page.ClickNestedGridTab("Assign Users to User Group ");
                Page.IsInputVisible(ButtonsAndMessages.AssignUserstotheusergroup);
                errorMsgs.AddRange(Page.ValidateNestedGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));

                if (Page.IsNestedTableRowCheckBoxUnChecked(2))
                {
                    Page.CheckNestedTableRowCheckBoxByIndex(1, 2);
                    int currentWindowCount = driver.WindowHandles.Count;
                    Page.ClickInputButton(ButtonsAndMessages.AssignUserstotheusergroup);
                    NotificationConfigurationPage notifyPage = new NotificationConfigurationPage(driver);
                    notifyPage.SwitchToPopUpWithWait(currentWindowCount);
                    LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                    Assert.AreEqual(Pages.NotificationConfiguration, notifyPage.GetText(FieldNames.Title), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
                    notifyPage.ButtonClick(ButtonsAndMessages.Submit);
                    notifyPage.CheckForText("Records have been saved successfully.");
                    notifyPage.ClosePopupWindow();
                    if (Page.IsNestedTableRowCheckBoxUnChecked(2))
                    {
                        errorMsgs.Add("Assign Functionality Did Not Worked");
                    }
                }
                else if (!Page.IsNestedTableRowCheckBoxUnChecked(2))
                {
                    Page.CheckNestedTableRowCheckBoxByIndex(1, 2);
                    int currentWindowCount = driver.WindowHandles.Count;
                    Page.ClickInputButton(ButtonsAndMessages.AssignUserstotheusergroup);
                    NotificationConfigurationPage notifyPage = new NotificationConfigurationPage(driver);
                    notifyPage.SwitchToPopUpWithWait(currentWindowCount);
                    LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                    Assert.AreEqual(Pages.NotificationConfiguration, notifyPage.GetText(FieldNames.Title), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
                    notifyPage.ButtonClick(ButtonsAndMessages.Submit);
                    notifyPage.CheckForText("Records have been saved successfully.");
                    notifyPage.ClosePopupWindow();
                    if (!Page.IsNestedTableRowCheckBoxUnChecked(2))
                    {
                        errorMsgs.Add("Assign Functionality Did Not Worked");
                    }
                }

                string nestedGroupAssign;
                try
                {
                    nestedGroupAssign = Page.GetFirstRowDataNestedTable(TableHeaders.UserID, 2);
                }
                catch
                {
                    nestedGroupAssign = Page.GetFirstRowDataNestedTable(TableHeaders.UserID, 2);
                }
                Page.FilterNestedTable(TableHeaders.UserID, nestedGroupAssign);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByNestedHeader(TableHeaders.UserID, nestedGroupAssign, 2), ErrorMessages.NoRowAfterFilter);
                Page.FilterNestedTable(TableHeaders.UserID, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountNestedPage(2) <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearNestedFilter(2);
                Assert.IsTrue(Page.GetRowCountNestedPage(2) > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterNestedTable(TableHeaders.UserID, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountNestedPage(2) <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetNestedFilter(2);
                Assert.IsTrue(Page.GetRowCountNestedPage(2) > 0, ErrorMessages.ResetNotWorking);

                Assert.Multiple(() =>
                {
                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                });
            }
        }
    }
}
