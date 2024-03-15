using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ManageDelinquentFee;
using AutomationTesting_CorConnect.PageObjects.ManageDelinquentFee.DelinquentFeeInvoiceSetup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.ManageDelinquentFee
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage Delinquent Fee")]
    internal class ManageDelinquentFee : DriverBuilderClass
    {
        ManageDelinquentFeePage Page;
        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ManageDelinquentFee);
            Page = new ManageDelinquentFeePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22550" })]
        public void TC_22550(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.ManageDelinquentFee, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.ManageDelinquentFee).ForEach(x=>{ Assert.Fail(x); });
                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                Page.PopulateGrid();

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                  
                    string dealerCode = Page.GetFirstRowData(TableHeaders.DealerCode);
                    Page.FilterTable(TableHeaders.DealerCode, dealerCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerCode, dealerCode), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.FleetCode, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.DealerCode, dealerCode);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerCode, dealerCode), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.FleetCode, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCount() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ResetNotWorking);
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

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22717" })]
        public void TC_22717(string UserType)
        {
            string required = " Required Label";
            string label = " Label";
            DelinquentFeeInvoiceSetupPage delinquentFeeSetup = Page.OpenDelinquentFeeInvoiceSetup();

            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.SelectDealer + label), string.Format(ErrorMessages.TextMissing, FieldNames.SelectDealer + label));
            Assert.IsTrue(delinquentFeeSetup.ValidateDropDown(FieldNames.Dealer), string.Format(ErrorMessages.DropDownNotFound, FieldNames.Dealer));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.SelectFleet + label), string.Format(ErrorMessages.TextMissing, FieldNames.SelectFleet + label));
            Assert.IsTrue(delinquentFeeSetup.ValidateDropDown(FieldNames.FleetGroup), string.Format(ErrorMessages.DropDownNotFound, FieldNames.FleetGroup));
            Assert.IsTrue(delinquentFeeSetup.ValidateDropDown(FieldNames.Fleet), string.Format(ErrorMessages.DropDownNotFound, FieldNames.Fleet));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.FeeCalculation + label), string.Format(ErrorMessages.TextMissing, FieldNames.FeeCalculation + label));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.YearlyDelinquentFee), string.Format(ErrorMessages.TextBoxMissing, FieldNames.YearlyDelinquentFee));
            Assert.IsFalse(delinquentFeeSetup.IsElementEnabled(FieldNames.DelinquentFeeperperiod), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.DelinquentFeeperperiod));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.Thresholdtime_Days_), string.Format(ErrorMessages.TextBoxMissing, FieldNames.Thresholdtime_Days_));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.MinimumAmount), string.Format(ErrorMessages.TextBoxMissing, FieldNames.MinimumAmount));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.YearlyDelinquentFee), string.Format(ErrorMessages.TextBoxMissing, FieldNames.YearlyDelinquentFee));
            Assert.IsFalse(delinquentFeeSetup.IsElementEnabled(FieldNames.BillingReference), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.BillingReference));
            Assert.IsTrue(delinquentFeeSetup.CheckForTextByVisibility("Delinquent fee is charged every 1st and 16th of every month"));
            Assert.AreEqual("FINANCE CHARGE", delinquentFeeSetup.GetValue(FieldNames.BillingReference), String.Format(ErrorMessages.TextMissing, FieldNames.BillingReference));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.RulesusedforDelinquentFeeInvoicecreation + label), string.Format(ErrorMessages.TextMissing, FieldNames.RulesusedforDelinquentFeeInvoicecreation + label));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.DefaultRelationshipsused + label), string.Format(ErrorMessages.TextMissing, FieldNames.DefaultRelationshipsused + label));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.DealerRelationships + label), string.Format(ErrorMessages.TextMissing, FieldNames.DealerRelationships + label));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.FleetRelationships + label), string.Format(ErrorMessages.TextMissing, FieldNames.FleetRelationships + label));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.Dealer + " " + FieldNames.CurrencyCode + label), string.Format(ErrorMessages.TextMissing, FieldNames.Dealer + "" + FieldNames.CurrencyCode + label));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.Fleet + " " + FieldNames.CurrencyCode + label), string.Format(ErrorMessages.TextMissing, FieldNames.Fleet + "" + FieldNames.CurrencyCode + label));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.PaymentTerms + label), string.Format(ErrorMessages.TextMissing, FieldNames.PaymentTerms + label));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.MasterBillingstatement + label), string.Format(ErrorMessages.TextMissing, FieldNames.MasterBillingstatement + label));
            Assert.IsTrue(delinquentFeeSetup.IsDatePickerClosed(FieldNames.EffectiveDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EffectiveDate));
            Assert.IsTrue(delinquentFeeSetup.IsButtonEnabled(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Save));
            Assert.IsTrue(delinquentFeeSetup.IsButtonEnabled(ButtonsAndMessages.Cancel), string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Cancel));
            Assert.IsFalse(delinquentFeeSetup.IsButtonEnabled(ButtonsAndMessages.Activate), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Activate));
            Assert.IsFalse(delinquentFeeSetup.IsButtonEnabled(ButtonsAndMessages.Deactivate), string.Format(ErrorMessages.ButtonEnabled, ButtonsAndMessages.Deactivate));

            Assert.AreEqual(string.Empty, delinquentFeeSetup.GetValueOfDropDown(FieldNames.Dealer), string.Format(ErrorMessages.FieldNotEmpty, FieldNames.Dealer));
            Assert.AreEqual(string.Empty, delinquentFeeSetup.GetValueOfDropDown(FieldNames.FleetGroup), string.Format(ErrorMessages.FieldNotEmpty, FieldNames.FleetGroup));
            Assert.AreEqual(string.Empty, delinquentFeeSetup.GetValueOfDropDown(FieldNames.Fleet), string.Format(ErrorMessages.FieldNotEmpty, FieldNames.Fleet));
            Assert.AreEqual(string.Empty, delinquentFeeSetup.GetValue(FieldNames.Thresholdtime_Days_), string.Format(ErrorMessages.FieldNotEmpty, FieldNames.Thresholdtime_Days_));
            Assert.AreEqual(string.Empty, delinquentFeeSetup.GetValue(FieldNames.DelinquentFeeperperiod), string.Format(ErrorMessages.FieldNotEmpty, FieldNames.DelinquentFeeperperiod));
            Assert.AreEqual("0.0000", delinquentFeeSetup.GetValue(FieldNames.YearlyDelinquentFee), string.Format(ErrorMessages.ValueMisMatch, FieldNames.YearlyDelinquentFee));
            Assert.AreEqual("0.00", delinquentFeeSetup.GetValue(FieldNames.MinimumAmount), string.Format(ErrorMessages.ValueMisMatch, FieldNames.MinimumAmount));

            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.Dealer + required));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.Fleet + required));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.YearlyDelinquentFee + required));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.Thresholdtime_Days_ + required));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.MinimumAmount + required));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.BillingReference + required));
            Assert.IsTrue(delinquentFeeSetup.IsElementDisplayed(FieldNames.EffectiveDate + required));

            Task t = Task.Run(() => delinquentFeeSetup.WaitForStalenessOfElement(FieldNames.YearlyDelinquentFee));
            delinquentFeeSetup.SelectValueFirstRow(FieldNames.Fleet);
            t.Wait();
            t.Dispose();
            Task t2 = Task.Run(() => delinquentFeeSetup.WaitForStalenessOfElement(FieldNames.Dealer));
            Assert.IsTrue(delinquentFeeSetup.IsAnyRowsInDropdown(FieldNames.Dealer),"No Records Available In Dealer Dropdown");
            delinquentFeeSetup.SelectValueFirstRow(FieldNames.Dealer);
            t2.Wait();
            t2.Dispose();
            delinquentFeeSetup.EnterTextAfterClear(FieldNames.YearlyDelinquentFee, "2.0000");
            delinquentFeeSetup.EnterTextAfterClear(FieldNames.Thresholdtime_Days_, "2");
            delinquentFeeSetup.SelectDateToday(FieldNames.EffectiveDate);
            delinquentFeeSetup.ButtonClick(ButtonsAndMessages.Save);
            Assert.IsTrue(delinquentFeeSetup.CheckForTextByVisibility(ButtonsAndMessages.RecordAddedSuccessfully, true));
            Assert.IsTrue(delinquentFeeSetup.IsButtonEnabled(ButtonsAndMessages.Activate),string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.Activate));

        }
    }
}
