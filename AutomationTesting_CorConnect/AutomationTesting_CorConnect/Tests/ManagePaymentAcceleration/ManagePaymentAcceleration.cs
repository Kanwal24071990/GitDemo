using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.ManagePaymentAcceleration;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using AutomationTesting_CorConnect.Utils;
using System;
using AutomationTesting_CorConnect.PageObjects.ManagePaymentAcceleration.AccelerationProgram;
using AutomationTesting_CorConnect.Helper;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.ManagePaymentAcceleration
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage Payment Acceleration")]
    internal class ManagePaymentAcceleration : DriverBuilderClass
    {
        ManagePaymentAccelerationPage page;
        AccelerationProgramPage popupPage;
        private string accelerationTermName = CommonUtils.RandomString(4);

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ManagePaymentAcceleration);
            page = new ManagePaymentAccelerationPage(driver);
            popupPage = new AccelerationProgramPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21051" })]
        public void TC_21051(string UserType)
        {
            page.LoadDataOnGrid();
            var acclerationProgram = page.OpenAccelerationProgram();

            bool isMsgDisplayed = acclerationProgram.CreateNewAccelerationProgram(accelerationTermName);
            page.SearchByAccelerationTermName(accelerationTermName);

            Assert.Multiple(() =>
            {
                Assert.IsTrue(isMsgDisplayed);
                Assert.AreEqual(1, page.GetRowCount());
                Assert.AreEqual(accelerationTermName, page.GetFirstRowData(TableHeaders.AccelerationProgramName));
                Assert.AreEqual("10", page.GetFirstRowData(TableHeaders.AccelerationDays));
                Assert.AreEqual("Amount", page.GetFirstRowData(TableHeaders.InvAccelerationFeeValueType));
                Assert.AreEqual("10.00000000", page.GetFirstRowData(TableHeaders.InvAccelerationFeeValue));
                Assert.AreEqual("Both AP and AR", page.GetFirstRowData(TableHeaders.AvailableTo));
                Assert.AreEqual("Active", page.GetFirstRowData(TableHeaders.Status));
                Assert.AreEqual(DateTime.Now.ChangeDateFormat("MM/dd/yyyy"), page.GetFirstRowData(TableHeaders.CreationDate));
                Console.WriteLine($"Acceleration Term Created: [{accelerationTermName}]");
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21053" })]
        public void TC_21053(string UserType)
        {
            page.SearchByAccelerationTermName(accelerationTermName);
            page.ClickHyperLinkOnGrid(FieldNames.AccelerationProgramName);

            Assert.Multiple(() =>
            {
                Assert.IsFalse(popupPage.IsTextBoxEnabled(FieldNames.AccelerationProgramID), GetErrorMessage(ErrorMessages.TextBoxEnabled, FieldNames.AccelerationProgramID));
                Assert.IsFalse(popupPage.IsTextBoxEnabled(FieldNames.AccelerationProgramName), GetErrorMessage(ErrorMessages.TextBoxEnabled, FieldNames.AccelerationProgramName));
                Assert.IsFalse(popupPage.IsTextBoxEnabled(FieldNames.DueDateadjustment_days_), GetErrorMessage(ErrorMessages.TextBoxEnabled, FieldNames.DueDateadjustment_days_));
                Assert.IsTrue(popupPage.IsCheckBoxDisabled("AR Only Disabled"), GetErrorMessage(ErrorMessages.CheckBoxEnabled, FieldNames.AROnly_));
                Assert.IsTrue(popupPage.IsCheckBoxDisabled("AP Only Disabled"), GetErrorMessage(ErrorMessages.CheckBoxEnabled, FieldNames.APOnly_));
                Assert.IsFalse(popupPage.IsDropDownDisabled(FieldNames.AccelerationFeeValueType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.AccelerationFeeValueType));
                Assert.IsTrue(popupPage.IsTextBoxEnabled(FieldNames.AccelerationFeeValue), GetErrorMessage(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationFeeValue));
                Assert.IsTrue(popupPage.IsCheckBoxDisabled(FieldNames.IsActive_), GetErrorMessage(ErrorMessages.CheckBoxEnabled, FieldNames.IsActive_));
                Assert.IsTrue(popupPage.IsElementDisplayed(FieldNames.CreditAccelerationFeeValueType), GetErrorMessage(ErrorMessages.ElementNotPresent, FieldNames.CreditAccelerationFeeValueType));
                Assert.IsTrue(popupPage.IsElementDisplayed(FieldNames.CreditAccelerationFeeValue), GetErrorMessage(ErrorMessages.ElementNotPresent, FieldNames.CreditAccelerationFeeValue));
            });

            popupPage.EnterTextAfterClear(FieldNames.AccelerationFeeValue, "2500");
            popupPage.Click(ButtonsAndMessages.Update);
            popupPage.CheckForText("Record saved successfully.", true);
            popupPage.Click(ButtonsAndMessages.Cancel);
            popupPage.SwitchToMainWindow();
            page.SearchByAccelerationTermName(accelerationTermName);
            Assert.AreEqual(page.GetRowCount(), 1);
            Assert.AreEqual("Amount", page.GetFirstRowData(TableHeaders.InvAccelerationFeeValueType));
            Assert.AreEqual("2500.00000000", page.GetFirstRowData(TableHeaders.InvAccelerationFeeValue));
            Assert.AreEqual("Active", page.GetFirstRowData(TableHeaders.Status));
            Console.WriteLine($"Acceleration Term Updated Successfully: [{accelerationTermName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(3), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21054" })]
        public void TC_21054(string UserType)
        {
            page.SearchByAccelerationTermName(accelerationTermName);
            if (page.IsAnyDataOnGrid() && page.GetRowCountCurrentPage() > 0)
            {
                page.ClickDelete();
                page.AcceptAlertMessage(out string msg);
                string renamedMsg = menu.RenameMenuField(ButtonsAndMessages.ConfirmDeleteAccelerationTerm);
                Assert.AreEqual(renamedMsg, msg.Trim());
                page.SearchByAccelerationTermName(accelerationTermName);
                Assert.AreEqual("InActive", page.GetFirstRowData(TableHeaders.Status));
                Assert.AreEqual(accelerationTermName, page.GetFirstRowData(TableHeaders.AccelerationProgramName));
                Console.WriteLine($"Acceleration Term Deleted Successfully: [{accelerationTermName}]");
            }
            else
            {
                Assert.Fail($"No Payment Acceleration Term [{accelerationTermName}] found to delete.");
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24222" })]
        public void TC_24222(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ManagePaymentAcceleration), page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            page.AreFieldsAvailable(Pages.ManagePaymentAcceleration).ForEach(x => { Assert.Fail(x); });

            var buttons = page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateNewAccelerationTerm);
            Assert.IsTrue(buttons.Count > 0);

            Assert.IsTrue(page.VerifyValueDropDown(FieldNames.TermStatus, "All", "Active", "InActive"), $"{FieldNames.TermStatus} DD: " + ErrorMessages.ListElementsMissing);
            Assert.IsTrue(page.VerifyValueDropDown(FieldNames.TermAvailableto, "Both AP and AR", "AP Only", "AR Only"), $"{FieldNames.TermAvailableto} DD: " + ErrorMessages.ListElementsMissing);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(page.ValidateTableHeadersFromFile());

            page.LoadDataOnGrid();

            if (page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.CreateNewAccelerationTerm));
                errorMsgs.AddRange(page.ValidateTableDetails(true, true));
                string accelerationprgmname = page.GetFirstRowData(TableHeaders.AccelerationProgramName);
                page.FilterTable(TableHeaders.AccelerationProgramName, accelerationprgmname);
                Assert.IsTrue(page.VerifyFilterDataOnGridByHeader(TableHeaders.AccelerationProgramName, accelerationprgmname), ErrorMessages.NoRowAfterFilter);
                page.FilterTable(TableHeaders.AccelerationProgramName, CommonUtils.RandomString(10));
                Assert.IsTrue(page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                page.ClearFilter();
                Assert.IsTrue(page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                page.FilterTable(TableHeaders.AccelerationProgramName, CommonUtils.RandomString(10));
                Assert.IsTrue(page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                page.ResetFilter();
                Assert.IsTrue(page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

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
