using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ManageDisputes;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.ManageDisputes;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.ManageDisputes
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage Disputes")]
    internal class ManageDisputes : DriverBuilderClass
    {
        ManageDisputesPage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ManageDisputes);
            Page = new ManageDisputesPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22226" })]
        public void TC_22226(string UserType)
        {
            Page.OpenDropDown(FieldNames.DisputeOwner);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DisputeOwner), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DisputeOwner));
            Page.OpenDropDown(FieldNames.DisputeOwner);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DisputeOwner), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DisputeOwner));
            Page.SelectValueFirstRow(FieldNames.DisputeOwner);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DisputeOwner), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DisputeOwner));

            ManageDisputesUtil.GetDisputeOwnerDetails(out string FirstName);

            Page.SearchAndSelectValueAfterOpen(FieldNames.DisputeOwner, FirstName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DisputeOwner), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DisputeOwner));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24765" })]
        public void TC_24765(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.ManageDisputes, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
                
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.ManageDisputes).ForEach(x => { Assert.Fail(x); });
              
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                Page.GridLoad();

                if (Page.IsAnyDataOnGrid())
                {
                    Page.ButtonClick(ButtonsAndMessages.Clear);
                    Assert.AreEqual(Page.GetAlertMessage(), ButtonsAndMessages.ConfirmResetAlert);
                    Page.DismissAlert();

                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                    string progInvNum = Page.GetFirstRowData(TableHeaders.FirstName);
                    Page.FilterTable(TableHeaders.FirstName, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FirstName, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.LastName, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.FirstName, progInvNum);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.FirstName, progInvNum), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.LastName, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                }
                Assert.Multiple(() =>
                {
                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                });
            });
        }



    }
}
