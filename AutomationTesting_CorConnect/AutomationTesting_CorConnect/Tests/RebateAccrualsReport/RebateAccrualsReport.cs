using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.RebateAccrualsReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.RebateAccrualsReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.Tests.RebateAccrualsReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Rebate Accruals Report")]
    internal class RebateAccrualsReport : DriverBuilderClass
    {
        RebateAccrualsReportPage popupPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.RebateAccrualsReport);
            popupPage = new RebateAccrualsReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22207" })]
        public void TC_22207(string UserType)
        {
            popupPage.OpenMultiSelectDropDown(FieldNames.Currency);
            popupPage.ClickPageTitle();
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.Currency, TableHeaders.Currency, "USD");
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.Currency);
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.Currency);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.Currency);
            popupPage.SelectAllRowsMultiSelectDropDown(FieldNames.Currency);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.Currency);
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.Currency);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Currency), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Currency));

            popupPage.OpenMultiSelectDropDown(FieldNames.Status);
            popupPage.ClickPageTitle();
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.Status, TableHeaders.Status, "Completed");
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.Status);
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.Status);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.Status);
            popupPage.SelectAllRowsMultiSelectDropDown(FieldNames.Status);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.Status);
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.Status);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.Status), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Status));

            popupPage.OpenMultiSelectDropDown(FieldNames.PayerLocation);
            popupPage.ClickPageTitle();
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.PayerLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PayerLocation));
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.PayerLocation, TableHeaders.City, "New York");
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.PayerLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PayerLocation));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.PayerLocation);
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.PayerLocation);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.PayerLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PayerLocation));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.PayerLocation);
            popupPage.SelectAllRowsMultiSelectDropDown(FieldNames.PayerLocation);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.PayerLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PayerLocation));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.PayerLocation);
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.PayerLocation);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.PayerLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.PayerLocation));

            popupPage.SelectValueTableDropDown(FieldNames.Receiver, menu.RenameMenuField("Fleet on invoice"));
            popupPage.WaitForMsg("Processing...");
            popupPage.OpenMultiSelectDropDown(FieldNames.ReceiverLocation);
            popupPage.ClickPageTitle();
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.ReceiverLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ReceiverLocation));
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.ReceiverLocation, TableHeaders.City, "New York");
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.ReceiverLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ReceiverLocation));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.ReceiverLocation);
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.ReceiverLocation);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.ReceiverLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ReceiverLocation));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.ReceiverLocation);
            popupPage.SelectAllRowsMultiSelectDropDown(FieldNames.ReceiverLocation);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.ReceiverLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ReceiverLocation));
            popupPage.ClearSelectionMultiSelectDropDown(FieldNames.ReceiverLocation);
            popupPage.SelectFirstRowMultiSelectDropDown(FieldNames.ReceiverLocation);
            Assert.IsTrue(popupPage.IsMultiSelectDropDownClosed(FieldNames.ReceiverLocation), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ReceiverLocation));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20347" })]
        public void TC_20347(string UserType)
        {
            List<string> errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.RebateAccrualsReport, popupPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(popupPage.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(popupPage.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(popupPage.IsButtonVisible(ButtonsAndMessages.SaveAsBookmark), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            errorMsgs = popupPage.AreFieldsAvailable();

            popupPage.ClickPageTitle();
            if (popupPage.IsDataExistsInDropdown(FieldNames.LoadBookmark))
            {
                List<string> loadBookmarkHeaders = popupPage.GetHeaderNamesTableDropDown(FieldNames.LoadBookmark);
                Assert.IsTrue(loadBookmarkHeaders.Contains(TableHeaders.Name), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Name, FieldNames.LoadBookmark));
                Assert.IsTrue(loadBookmarkHeaders.Contains(TableHeaders.Description), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Description, FieldNames.LoadBookmark));
            }
            RebateAccrualsReportUtils.GetData(out string contractName);
            if (string.IsNullOrEmpty(contractName))
            {
                Assert.Fail("Contract Name returned empty from DB");
            }
            popupPage.LoadDataOnGrid(contractName);

            if (popupPage.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(popupPage.ValidateGridControls());
                Assert.IsTrue(popupPage.ValidateExportButton());
                Assert.AreEqual(popupPage.ValidateSearch("Rebate Accruals"), ButtonsAndMessages.SearchCompleted);
                Assert.IsTrue(popupPage.ValidatePagination());
            }

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
