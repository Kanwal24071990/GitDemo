using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.GroupRelationshipsMaintenance;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.GroupRelationshipsMaintenance;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.GroupRelationshipsMaintenance
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Group Relationships Maintenance")]
    internal class GroupRelationshipsMaintenance : DriverBuilderClass
    {
        GroupRelationshipsMaintenancePage Page;
        string fleetGroup = null;
        string dealerGroup = null;
        string corcentricLocation = CommonUtils.GetCorcentricLocation();

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.GroupRelationshipsMaintenance);
            Page = new GroupRelationshipsMaintenancePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Category(TestCategory.Premier)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22114" })]
        public void TC_22114(string UserType)
        {
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), ErrorMessages.ClearButtonNotFound);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), ErrorMessages.SearchButtonNotFound);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.EntityType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.EntityType));
            Assert.AreEqual(Page.RenameMenuField(EntityType.Dealer), Page.GetValueOfDropDown(FieldNames.EntityType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.EntityType));
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.GroupName), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.GroupName));
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.ContainsLocation), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.ContainsLocation));
            List<string> headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.ContainsLocation);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(headerNames.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.Country), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Country, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.EntityCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, FieldNames.ContainsLocation));
            });
            Page.ButtonClick(ButtonsAndMessages.AddNewRelationship);
            Page.WaitForElementToBeVisible("Relationship Table");
            Page.SelectValueTableDropDown("Rel Entity Type", Page.RenameMenuField(EntityType.Dealer));
            Page.WaitForElementToBeVisible("Add New Group Relationship Table");
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.DealerGroup));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerLocation), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.DealerLocation));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.FleetLocation));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetLocation), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.FleetLocation));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.RelationshipType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.RelationshipType));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Display), GetErrorMessage(ErrorMessages.ButtonMissing, ButtonsAndMessages.Display));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Close), ErrorMessages.CloseButtonMissing);
            });
            Page.SelectValueFirstRow(FieldNames.DealerLocation);
            Page.WaitForLoadingIcon();
            Page.SelectValueRowByIndex(FieldNames.FleetGroup, 2);
            Page.WaitForLoadingIcon();
            Page.SelectValueTableDropDown(FieldNames.RelationshipType, FieldNames.CurrencyCode);
            Page.WaitForLoadingIcon();
            Page.ButtonClick(ButtonsAndMessages.Display);
            Page.WaitForLoadingIcon();
            if (Page.IsTextVisible(ButtonsAndMessages.RelationshipAlreadyExists))
            {
                string currencyCode = GroupRelationshipsMaintenanceUtils.DeactivateCurrencyCodeReltionship(Page.GetValueOfDropDown(FieldNames.FleetGroup));
                Page.ButtonClick(ButtonsAndMessages.Display);
                Page.WaitForLoadingIcon();
                Page.WaitForElementToBeVisible(FieldNames.RelCurrencyTable);
                Page.SelectValueByScroll(FieldNames.Currency, currencyCode);
            }
            else
            {
                Page.WaitForElementToBeVisible(FieldNames.RelCurrencyTable);
            }
            Page.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(Page.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23814" })]
        public void TC_23814(string UserType)
        {
            string requiredLabel = " Required Label";
            Page.ButtonClick(ButtonsAndMessages.AddNewRelationship);
            Page.WaitForElementToBeVisible("Relationship Table");
            Page.SelectValueTableDropDown("Rel Entity Type", Page.RenameMenuField(EntityType.Dealer));
            Page.WaitForElementToBeVisible("Add New Group Relationship Table");
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.DealerGroup));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerLocation), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.DealerLocation));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.FleetLocation));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetLocation), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.FleetLocation));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.RelationshipType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.RelationshipType));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Display), GetErrorMessage(ErrorMessages.ButtonMissing, ButtonsAndMessages.Display));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Close), ErrorMessages.CloseButtonMissing);
            });

            Page.SelectValueRowByIndex(FieldNames.DealerGroup, 2);
            Page.WaitForLoadingIcon();
            Page.SelectValueFirstRow(FieldNames.FleetLocation);
            Page.WaitForLoadingIcon();
            Page.SelectValueTableDropDown(FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.WaitForLoadingIcon();
            Page.ButtonClick(ButtonsAndMessages.Display);
            Page.WaitForLoadingIcon();
            Page.WaitForElementToBeVisible("Rel Payterms Table");
            Assert.IsFalse(Page.IsCheckBoxDisplayed(FieldNames.SettlementBasedDueDatecalculation));
            Assert.IsFalse(Page.IsElementDisplayed(FieldNames.TermDescription));
            Assert.IsFalse(Page.IsElementDisplayed(FieldNames.VariableDueDatecalculation));
            Page.SelectValueByScroll(FieldNames.PaymentTerms, "Variable");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsTrue(Page.IsCheckBoxDisplayed(FieldNames.SettlementBasedDueDatecalculation));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.TermDescription));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.VariableDueDatecalculation));
            Assert.AreEqual("", Page.GetValue(FieldNames.TermDescription));
            Assert.AreEqual("", Page.GetValue(FieldNames.VariableDueDatecalculation));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation));
            Assert.AreEqual("Bi-Weekly", Page.GetValueOfDropDown(FieldNames.BillingCycle));
            Assert.AreEqual("Sunday", Page.GetValueOfDropDown(FieldNames.StatementEndDay));
            Assert.AreEqual("", Page.GetValueOfDropDown(FieldNames.BillingCycleStartDate));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType));
            Assert.AreEqual("--- Select ---", Page.GetValueOfDropDown(FieldNames.AccelerationProgram));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn));
            Assert.AreEqual("", Page.GetValue(FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.StatementEndDay + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementEndDay + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.BillingCycleStartDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycleStartDate + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));
            Assert.IsTrue(Page.VerifyValue(FieldNames.BillingCycle, "Bi-Weekly", "Daily", "Monthly", "Twice Monthly", "Weekly"), ErrorMessages.ElementNotPresent + ". Billing Cycle Type dropdown.");
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.BillingCycleStartDate), string.Format(FieldNames.BillingCycleStartDate + "Calender Not Displayed"));
            Assert.IsTrue(Page.VerifyValue(FieldNames.StatementType, "One statement per due date", "One statement per statement period"), ErrorMessages.ElementNotPresent + ". Statement Type dropdown.");
            Assert.IsTrue(Page.VerifyValue(FieldNames.AccelerationType, "By Account", "By Invoice", "None"), ErrorMessages.ElementNotPresent + ". Acceleration Type dropdown.");
            Assert.IsTrue(Page.VerifyValue(FieldNames.EffectiveDateBasedOn, "Dealer Invoice Date", "Settlement Date"), ErrorMessages.ElementNotPresent + ". Effective Date Based On dropdown.");
            Assert.IsTrue(Page.IsDatePickerClosed("Effective Date DD"), string.Format(FieldNames.EffectiveDate + "Calender Not Displayed"));
            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Daily");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
            Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.AreEqual("Daily", Page.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));

            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Monthly");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
            Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.AreEqual("Monthly", Page.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
            Assert.AreEqual("1", Page.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));

            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Twice Monthly");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
            Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.AreEqual("Twice Monthly", Page.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
            Assert.AreEqual("15th and end of the Month", Page.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));

            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
            Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.AreEqual("Weekly", Page.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
            Assert.AreEqual("Sunday", Page.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23523" })]
        public void TC_23523(string UserType)
        {
            string requiredLabel = " Required Label";
            Page.ButtonClick(ButtonsAndMessages.AddNewRelationship);
            Page.WaitForElementToBeVisible("Relationship Table");
            Page.SelectValueTableDropDown("Rel Entity Type", Page.RenameMenuField(EntityType.Dealer));
            Page.WaitForElementToBeVisible("Add New Group Relationship Table");
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerGroup), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.DealerGroup));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DealerLocation), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.DealerLocation));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetGroup), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.FleetLocation));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetLocation), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.FleetLocation));
                Assert.IsTrue(Page.IsDropDownClosed(FieldNames.RelationshipType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.RelationshipType));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Display), GetErrorMessage(ErrorMessages.ButtonMissing, ButtonsAndMessages.Display));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Close), ErrorMessages.CloseButtonMissing);
            });

            Page.SelectValueRowByIndex(FieldNames.DealerGroup, 2);
            Page.WaitForLoadingIcon();
            Page.SelectValueFirstRow(FieldNames.FleetLocation);
            Page.WaitForLoadingIcon();
            Page.SelectValueTableDropDown(FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.WaitForLoadingIcon();
            Page.ButtonClick(ButtonsAndMessages.Display);
            Page.WaitForLoadingIcon();
            Page.WaitForElementToBeVisible("Rel Payterms Table");
            Assert.AreEqual("Net 10", Page.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
            Assert.AreEqual("Bi-Weekly", Page.GetValueOfDropDown(FieldNames.BillingCycle));
            Assert.AreEqual("Sunday", Page.GetValueOfDropDown(FieldNames.StatementEndDay));
            Assert.AreEqual("", Page.GetValueOfDropDown(FieldNames.BillingCycleStartDate));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType));
            Assert.AreEqual("--- Select ---", Page.GetValueOfDropDown(FieldNames.AccelerationProgram));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn));
            Assert.AreEqual("", Page.GetValue(FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.StatementEndDay + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementEndDay + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.BillingCycleStartDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycleStartDate + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));
            Assert.IsTrue(Page.VerifyValue(FieldNames.BillingCycle, "Bi-Weekly", "Daily", "Monthly", "Twice Monthly", "Weekly"), ErrorMessages.ElementNotPresent + ". Billing Cycle Type dropdown.");
            Assert.IsTrue(Page.VerifyValue(FieldNames.StatementEndDay, "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"), ErrorMessages.ElementNotPresent + ". Statement End Day dropdown.");
            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsTrue(Page.VerifyValue(FieldNames.StatementEndDay, "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"), ErrorMessages.ElementNotPresent + ". Statement End Day dropdown.");
            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Monthly");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsTrue(Page.VerifyValueScrollable(FieldNames.StatementEndDay, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29 or end of the Month", "30 or end of the Month", "31 or end of the Month"), ErrorMessages.ElementNotPresent + ". Statement End Day dropdown.");
            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Twice Monthly");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.AreEqual("15th and end of the Month", Page.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
            Assert.IsTrue(Page.IsDatePickerClosed(FieldNames.BillingCycleStartDate), string.Format(FieldNames.BillingCycleStartDate + "Calender Not Displayed"));
            Assert.IsTrue(Page.VerifyValue(FieldNames.StatementType, "One statement per due date", "One statement per statement period"), ErrorMessages.ElementNotPresent + ". Statement Type dropdown.");
            Assert.IsTrue(Page.VerifyValue(FieldNames.AccelerationType, "By Account", "By Invoice", "None"), ErrorMessages.ElementNotPresent + ". Acceleration Type dropdown.");
            Assert.IsTrue(Page.VerifyValue(FieldNames.EffectiveDateBasedOn, "Dealer Invoice Date", "Settlement Date"), ErrorMessages.ElementNotPresent + ". Effective Date Based On dropdown.");
            Assert.IsTrue(Page.IsDatePickerClosed("Effective Date DD"), string.Format(FieldNames.EffectiveDate + "Calender Not Displayed"));
            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Daily");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
            Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.AreEqual("Net 10", Page.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
            Assert.AreEqual("Daily", Page.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
            Assert.IsTrue(Page.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));

            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Monthly");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
            Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.AreEqual("Net 10", Page.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
            Assert.AreEqual("Monthly", Page.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
            Assert.AreEqual("1", Page.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));

            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Twice Monthly");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
            Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.AreEqual("Net 10", Page.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
            Assert.AreEqual("Twice Monthly", Page.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
            Assert.AreEqual("15th and end of the Month", Page.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));

            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
            Page.WaitForElementToHaveFocus("FleetLocationInput");
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
            Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
            Assert.IsTrue(Page.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Assert.AreEqual("Net 10", Page.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
            Assert.AreEqual("Weekly", Page.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
            Assert.AreEqual("Sunday", Page.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
            Assert.AreEqual("Dealer Invoice Date", Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23816" })]
        public void TC_23816(string UserType)
        {
            string requiredLabel = " Required Label";
            string specialCharacter = "@%*";
            string negativeInterger = "-1.2133";
            string UTFCharac = CommonUtils.RandomAlphabets(10) + "ñññjjh¿¿¿" + CommonUtils.RandomString(60);
            string validInt = "123";
            
            Page.ButtonClick(ButtonsAndMessages.AddNewRelationship);
            Page.WaitForElementToBeVisible("Relationship Table");
            Page.SelectValueTableDropDown("Rel Entity Type", Page.RenameMenuField(EntityType.Dealer));
            Page.WaitForElementToBeVisible("Add New Group Relationship Table");
            Page.SearchAndSelectValue(FieldNames.DealerLocation, corcentricLocation);
            Page.WaitForLoadingIcon();
            Page.SelectValueRowByIndex(FieldNames.FleetGroup, 2);
            Page.WaitForLoadingIcon();
            Page.SelectValueTableDropDown(FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.WaitForLoadingIcon();
            Page.ButtonClick(ButtonsAndMessages.Display);
            Page.WaitForLoadingIcon();
            if (Page.IsTextVisible(ButtonsAndMessages.RelationshipAlreadyExists))
            {
                fleetGroup = Page.GetValueOfDropDown(FieldNames.FleetGroup);
                Page.ButtonClick(ButtonsAndMessages.Search);
                Page.WaitForLoadingIcon();
                Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
                Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, corcentricLocation);
                Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetGroup);
                Page.DeleteFirstRelationShipIfExist(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
                Page.ButtonClick(ButtonsAndMessages.AddNewRelationship);
                Page.WaitForElementToBeVisible("Relationship Table");
                Page.SelectValueTableDropDown("Rel Entity Type", Page.RenameMenuField(EntityType.Dealer));
                Page.WaitForElementToBeVisible("Add New Group Relationship Table");
                Page.SearchAndSelectValue(FieldNames.DealerLocation, corcentricLocation);
                Page.WaitForLoadingIcon();
                Page.SelectValueRowByIndex(FieldNames.FleetGroup, 2);
                Page.WaitForLoadingIcon();
                Page.SelectValueTableDropDown(FieldNames.RelationshipType, FieldNames.PaymentTerms);
                Page.WaitForLoadingIcon();
                Page.ButtonClick(ButtonsAndMessages.Display);
                Page.WaitForLoadingIcon();
                Page.WaitForElementToBeVisible("Rel Payterms Table");
            }
            else
            {
                Page.WaitForElementToBeVisible("Rel Payterms Table");
                fleetGroup = Page.GetValueOfDropDown(FieldNames.FleetGroup);
            }

            Page.SelectValueByScroll(FieldNames.PaymentTerms, "Variable");
            Page.WaitForElementToHaveFocus("DealerLocationInput");
            Assert.IsTrue(Page.IsCheckBoxDisplayed(FieldNames.SettlementBasedDueDatecalculation));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.TermDescription));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.VariableDueDatecalculation));
            Assert.AreEqual("", Page.GetValue(FieldNames.TermDescription));
            Assert.AreEqual("", Page.GetValue(FieldNames.VariableDueDatecalculation));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation));
            Assert.AreEqual("One statement per due date", Page.GetValueOfDropDown(FieldNames.StatementType));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType));
            Assert.AreEqual(Page.RenameMenuField("Dealer Invoice Date"), Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn));
            Assert.AreEqual("", Page.GetValue(FieldNames.EffectiveDate));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Page.Click(FieldNames.SettlementBasedDueDatecalculation);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation));
            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
            Page.WaitForElementToHaveFocus("DealerLocationInput");
            Page.SelectValueTableDropDown(FieldNames.StatementEndDay, "Friday");
            Page.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-7)));
            Page.EnterTextAfterClear(FieldNames.TermDescription, "These are more than eight characters 123 i have to pass in term description so that");
            Assert.AreEqual(80, Page.GetValue(FieldNames.TermDescription).Count());
            Page.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, specialCharacter);
            Page.ClickFieldLabel(FieldNames.PaymentTerms + requiredLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, negativeInterger);
            Page.ClickFieldLabel(FieldNames.PaymentTerms + requiredLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.EnterTextAfterClear(FieldNames.TermDescription, UTFCharac);
            Page.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, "0");
            Page.ClickFieldLabel(FieldNames.PaymentTerms + requiredLabel);
            Assert.IsFalse(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, validInt);
            Page.ClickFieldLabel(FieldNames.PaymentTerms + requiredLabel);
            Assert.IsFalse(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.Click(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate);
            Assert.IsTrue(Page.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, negativeInterger);
            Page.ClickFieldLabel(FieldNames.PaymentTerms + requiredLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "1234");
            Page.ClickFieldLabel(FieldNames.PaymentTerms + requiredLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.ClearText(FieldNames.OverrideIfDealerInvoiceDate);
            Page.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(Page.CheckForText("Override If Dealer Invoice Date < Days From Current Date is required"));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "123");
            var rowCountFromTablerelSenderBefore = AccountMaintenanceUtil.GetRowCountFromTable("relSenderReceiver_tb");
            var rowCountFromTablerelPaymentTermBefore = AccountMaintenanceUtil.GetRowCountFromTable("RelPaymentTerm_tb");
            var rowCountFromTableAuditTrail_tbBefore = AccountMaintenanceUtil.GetRowCountFromTable("AuditTrail_tb");
            Page.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(Page.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            var rowCountFromTablerelSenderAfter = AccountMaintenanceUtil.GetRowCountFromTable("relSenderReceiver_tb");
            var rowCountFromTablerelPaymentTermAfter = AccountMaintenanceUtil.GetRowCountFromTable("RelPaymentTerm_tb");
            var rowCountFromTableAuditTrail_tbAfter = AccountMaintenanceUtil.GetRowCountFromTable("AuditTrail_tb");

            Assert.AreEqual(rowCountFromTablerelSenderBefore + 1, rowCountFromTablerelSenderAfter);
            Assert.AreEqual(rowCountFromTablerelPaymentTermBefore + 1, rowCountFromTablerelPaymentTermAfter);
            Assert.AreEqual(rowCountFromTableAuditTrail_tbBefore + 2, rowCountFromTableAuditTrail_tbAfter);

            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForLoadingIcon();
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, corcentricLocation);
            Page.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);

            List<string> errorMsgs = new List<string>();

            List<string> headers = new List<String>()
                    {
                        TableHeaders.Commands,
                        TableHeaders.PaymentTerms,
                        TableHeaders.BillingCycle,
                        TableHeaders.StatementEndDay,
                        TableHeaders.BillingCycleStartDate,
                        TableHeaders.StatementType,
                        TableHeaders.WhatisDiscountable,
                        TableHeaders.DiscountAppliedonLineItemTypes,
                        TableHeaders.RoundingType,
                        TableHeaders.NumberofDigitstobeRound,
                        TableHeaders.AccelerationProgramName,
                        TableHeaders.AccelerationTypeName,
                        TableHeaders.StartBasedOn,
                        TableHeaders.StartDate,
                        TableHeaders.EndDate,
                        TableHeaders.IsActive,
                        TableHeaders.User,
                        TableHeaders.SettlementBasedDueDateCalculation,
                        TableHeaders.TermDescription,
                        TableHeaders.VariableDueDatecalculation,
                        TableHeaders.IsOverrideTermDescription,
                        TableHeaders.OverrideTermDescriptionDays
                    };

            errorMsgs.AddRange(Page.ValidateTableHeaders("Relationship Grid Table", headers.ToArray()));

            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });

            
            
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23825" })]
        public void TC_23825(string UserType)
        {
            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForLoadingIcon();
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, corcentricLocation);
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetGroup);
            Page.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);

            List<string> errorMsgs = new List<string>();

            List<string> headers = new List<String>()
                    {
                        TableHeaders.Commands,
                        TableHeaders.PaymentTerms,
                        TableHeaders.BillingCycle,
                        TableHeaders.StatementEndDay,
                        TableHeaders.BillingCycleStartDate,
                        TableHeaders.StatementType,
                        TableHeaders.WhatisDiscountable,
                        TableHeaders.DiscountAppliedonLineItemTypes,
                        TableHeaders.RoundingType,
                        TableHeaders.NumberofDigitstobeRound,
                        TableHeaders.AccelerationProgramName,
                        TableHeaders.AccelerationTypeName,
                        TableHeaders.StartBasedOn,
                        TableHeaders.StartDate,
                        TableHeaders.EndDate,
                        TableHeaders.IsActive,
                        TableHeaders.User,
                        TableHeaders.SettlementBasedDueDateCalculation,
                        TableHeaders.TermDescription,
                        TableHeaders.VariableDueDatecalculation,
                        TableHeaders.IsOverrideTermDescription,
                        TableHeaders.OverrideTermDescriptionDays
                    };

            errorMsgs.AddRange(Page.ValidateTableHeaders("Relationship Grid Table", headers.ToArray()));

            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
            Page.ClickAnchorButton("Relationship Grid Table", "Payment Terms Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit);
            Page.WaitForAnyElementLocatedBy(FieldNames.TermDescription, ButtonsAndMessages.Edit);
            Page.EnterTextAfterClear(FieldNames.TermDescription, "Hello World", ButtonsAndMessages.Edit);
            Page.ClearText(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.Edit);
            Page.ClickFieldLabel(FieldNames.PaymentTermsLabel);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            Page.UpdateEditGrid(false);
            Assert.IsTrue(Page.CheckForTextByVisibility("Override If Dealer Invoice Date < Days From Current Date is required", true));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "33", ButtonsAndMessages.Edit);
            Page.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)), ButtonsAndMessages.Edit);
            Page.SelectValueRowByIndex(FieldNames.PaymentTerms, 3, ButtonsAndMessages.Edit);
            Page.WaitForMsg(" processing...");
            Page.UpdateEditGrid();
            Console.WriteLine(Page.GetEditMsg());
            Assert.AreEqual(ButtonsAndMessages.RecordUpdatedPleaseCloseToExitUpdateForm, Page.GetEditMsg());
            Page.CloseEditGrid();
            Assert.AreEqual(CommonUtils.GetCurrentDate(), Page.GetFirstValueFromGrid(TableHeaders.EndDate));
            Assert.AreEqual(" ", Page.GetSecondValueFromGrid(TableHeaders.EndDate));
            Console.WriteLine(corcentricLocation);
            Console.WriteLine(fleetGroup);
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(3), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24832" })]
        public void TC_24832(string UserType)
        {
            string specialCharacter = "@%*";
            string negativeInterger = "-1.2133";
            string UTFCharac = CommonUtils.RandomAlphabets(10) + "ñññjjh¿¿¿" + CommonUtils.RandomString(60);
            string validInt = "123";

            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForLoadingIcon();
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, corcentricLocation);
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetGroup);
            Page.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);

            Page.ClickAnchorButton("Relationship Grid Table", "Payment Terms Table Header", TableHeaders.Commands, ButtonsAndMessages.Delete);
            Page.AcceptAlert(out string msg);
            Assert.AreEqual(ButtonsAndMessages.DeleteAlertMessage, msg);
            Page.Click("Delete Second Relation");
            Page.AcceptAlert(out string msg1);
            Assert.AreEqual(ButtonsAndMessages.DeleteAlertMessage, msg1);

            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForLoadingIcon();

            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, corcentricLocation);
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetGroup);
            Page.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.ClickAnchorButton("Relationship Grid Table", "Payment Terms Table Header", TableHeaders.Commands, ButtonsAndMessages.New);
            Page.WaitForAnyElementLocatedBy(FieldNames.PaymentTerms, ButtonsAndMessages.Edit);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.PaymentTerms, "Variable", ButtonsAndMessages.Edit);
            Page.WaitForMsg(" processing...");
            Assert.IsTrue(Page.IsCheckBoxDisplayed(FieldNames.SettlementBasedDueDatecalculation, true, ButtonsAndMessages.Edit));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.TermDescription, ButtonsAndMessages.Edit));
            Assert.IsTrue(Page.IsElementDisplayed(FieldNames.VariableDueDatecalculation, ButtonsAndMessages.Edit));
            Assert.AreEqual("", Page.GetValue(FieldNames.TermDescription, ButtonsAndMessages.Edit));
            Assert.AreEqual("", Page.GetValue(FieldNames.VariableDueDatecalculation, ButtonsAndMessages.Edit));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation, ButtonsAndMessages.Edit));
            Assert.AreEqual("None", Page.GetValueOfDropDown(FieldNames.AccelerationType, ButtonsAndMessages.Edit));
            Assert.AreEqual(Page.RenameMenuField("Dealer Invoice Date"), Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn, ButtonsAndMessages.Edit));
            Assert.AreEqual("", Page.GetValue(FieldNames.EffectiveDate, ButtonsAndMessages.Edit));
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate, ButtonsAndMessages.Edit));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate, false, ButtonsAndMessages.Edit));
            Page.Click(FieldNames.SettlementBasedDueDatecalculation, ButtonsAndMessages.Edit);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation, ButtonsAndMessages.Edit));
            Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly", ButtonsAndMessages.Edit);
            Page.WaitForMsg(" processing...");
            Page.SelectValueTableDropDown(FieldNames.StatementEndDay, "Friday", ButtonsAndMessages.Edit);
            Page.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-7)), ButtonsAndMessages.Edit);
            Page.EnterTextAfterClear(FieldNames.TermDescription, "These are more than eighty characters 123 i have to pass in term description so that", ButtonsAndMessages.Edit);
            Page.ClickFieldLabel(FieldNames.PaymentTermsLabel);
            
            Page.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, specialCharacter, ButtonsAndMessages.Edit);
            Page.ClickFieldLabel(FieldNames.PaymentTermsLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, negativeInterger, ButtonsAndMessages.Edit);
            Page.ClickFieldLabel(FieldNames.PaymentTermsLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.EnterTextAfterClear(FieldNames.TermDescription, UTFCharac, ButtonsAndMessages.Edit);
            Page.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, "0", ButtonsAndMessages.Edit);
            Page.ClickFieldLabel(FieldNames.PaymentTermsLabel);
            Assert.IsFalse(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, validInt, ButtonsAndMessages.Edit);
            Page.ClickFieldLabel(FieldNames.PaymentTermsLabel);
            Assert.IsFalse(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.Click(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate, ButtonsAndMessages.Edit);
            Assert.IsTrue(Page.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.Edit));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, negativeInterger, ButtonsAndMessages.Edit);
            Page.ClickFieldLabel(FieldNames.PaymentTermsLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "1234", ButtonsAndMessages.Edit);
            Page.ClickFieldLabel(FieldNames.PaymentTermsLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.ClearText(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.Edit);
            Page.InsertEditGrid(false);
            Assert.IsTrue(Page.CheckForText("Override If Dealer Invoice Date < Days From Current Date is required"));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "123", ButtonsAndMessages.Edit);
            Page.InsertEditGrid();
            Assert.AreEqual(ButtonsAndMessages.RecordInsertedSuccessfully, Page.GetEditMsg());
            Page.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);

            List<string> errorMsgs = new List<string>();

            List<string> headers = new List<String>()
                    {
                        TableHeaders.Commands,
                        TableHeaders.PaymentTerms,
                        TableHeaders.BillingCycle,
                        TableHeaders.StatementEndDay,
                        TableHeaders.BillingCycleStartDate,
                        TableHeaders.StatementType,
                        TableHeaders.WhatisDiscountable,
                        TableHeaders.DiscountAppliedonLineItemTypes,
                        TableHeaders.RoundingType,
                        TableHeaders.NumberofDigitstobeRound,
                        TableHeaders.AccelerationProgramName,
                        TableHeaders.AccelerationTypeName,
                        TableHeaders.StartBasedOn,
                        TableHeaders.StartDate,
                        TableHeaders.EndDate,
                        TableHeaders.IsActive,
                        TableHeaders.User,
                        TableHeaders.SettlementBasedDueDateCalculation,
                        TableHeaders.TermDescription,
                        TableHeaders.VariableDueDatecalculation,
                        TableHeaders.IsOverrideTermDescription,
                        TableHeaders.OverrideTermDescriptionDays
                    };

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            errorMsgs.AddRange(Page.ValidateTableHeaders("Relationship Grid Table", headers.ToArray()));

            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23522" })]
        public void TC_23522(string UserType)
        {
            string requiredLabel = " Required Label";
            string negativeInterger = "-1.2133";
            string UTFCharac = CommonUtils.RandomAlphabets(10) + "ñññjjh¿¿¿" + CommonUtils.RandomString(60);
            string validInt = "123";

            Page.ButtonClick(ButtonsAndMessages.AddNewRelationship);
            Page.WaitForElementToBeVisible("Relationship Table");
            Page.SelectValueTableDropDown("Rel Entity Type", Page.RenameMenuField(EntityType.Dealer));
            Page.WaitForElementToBeVisible("Add New Group Relationship Table");
            Page.SelectValueRowByIndex(FieldNames.DealerGroup, 3);
            Page.WaitForLoadingIcon();
            Page.SelectValueRowByIndex(FieldNames.FleetGroup, 4);
            Page.WaitForLoadingIcon();
            Page.SelectValueTableDropDown(FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.WaitForLoadingIcon();
            Page.ButtonClick(ButtonsAndMessages.Display);
            Page.WaitForLoadingIcon();

            if (Page.IsTextVisible(ButtonsAndMessages.RelationshipAlreadyExists))
            {
                fleetGroup = Page.GetValueOfDropDown(FieldNames.FleetGroup);
                dealerGroup = Page.GetValueOfDropDown(FieldNames.DealerGroup);
                Page.ButtonClick(ButtonsAndMessages.Search);
                Page.WaitForLoadingIcon();
                Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
                Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerGroup);
                Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetGroup);
                Page.DeleteFirstRelationShipIfExist(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
                Page.ButtonClick(ButtonsAndMessages.AddNewRelationship);
                Page.WaitForElementToBeVisible("Relationship Table");
                Page.SelectValueTableDropDown("Rel Entity Type", Page.RenameMenuField(EntityType.Dealer));
                Page.WaitForElementToBeVisible("Add New Group Relationship Table");
                Page.SelectValueTableDropDown(FieldNames.DealerGroup, dealerGroup);
                Page.WaitForLoadingIcon();
                Page.SelectValueTableDropDown(FieldNames.FleetGroup, fleetGroup);
                Page.WaitForLoadingIcon();
                Page.SelectValueTableDropDown(FieldNames.RelationshipType, FieldNames.PaymentTerms);
                Page.WaitForLoadingIcon();
                Page.ButtonClick(ButtonsAndMessages.Display);
                Page.WaitForLoadingIcon();
                Page.WaitForElementToBeVisible("Rel Payterms Table");
            }
            else
            {
                Page.WaitForElementToBeVisible("Rel Payterms Table");
                fleetGroup = Page.GetValueOfDropDown(FieldNames.FleetGroup);
                dealerGroup = Page.GetValueOfDropDown(FieldNames.DealerGroup);

            }

            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.PaymentTerms),String.Format("Dropdown Disabled of"),FieldNames.PaymentTerms);
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), String.Format("Dropdown Disabled of"), FieldNames.BillingCycle);
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementEndDay), String.Format("Dropdown Disabled of"), FieldNames.StatementEndDay);
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycle), String.Format("Dropdown Disabled of"), FieldNames.BillingCycle);
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.BillingCycleStartDate), String.Format("Dropdown Disabled of"), FieldNames.BillingCycleStartDate);
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.StatementType), String.Format("Dropdown Disabled of"), FieldNames.StatementType);
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.AccelerationType), String.Format("Dropdown Disabled of"), FieldNames.AccelerationType);
            Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.AccelerationProgram), String.Format("Dropdown Disabled of"), FieldNames.AccelerationProgram);
            Assert.IsFalse(Page.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), String.Format("Dropdown Disabled of"), FieldNames.EffectiveDateBasedOn);
            Assert.IsFalse(Page.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
            Assert.IsFalse(Page.IsElementEnabled(FieldNames.OverrideIfDealerInvoiceDate));



            if (Page.GetValueOfDropDown(FieldNames.PaymentTerms) != "Net 10" && Page.GetValueOfDropDown(FieldNames.StatementType) != "One statement per due date" && Page.GetValueOfDropDown(FieldNames.AccelerationType) != "None" && Page.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn) != "Dealer Invoice Date")
            {
                Page.SelectValueByScroll(FieldNames.PaymentTerms, "Net 10");
                Page.WaitForStalenessOfElement(FieldNames.BillingCycleStartDate);
                Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
                Page.WaitForStalenessOfElement(FieldNames.BillingCycleStartDate);
                Page.SelectValueTableDropDown(FieldNames.StatementEndDay, "Friday");
                Page.SelectValueTableDropDown(FieldNames.StatementType, "One statement per due date");
                Page.SelectValueTableDropDown(FieldNames.AccelerationType, "None");
                Page.SelectValueTableDropDown(FieldNames.EffectiveDateBasedOn, "Dealer Invoice Date");
                Page.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-7)));

            }

            else
            {
                Page.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
                Page.WaitForStalenessOfElement(FieldNames.BillingCycleStartDate);
                Page.SelectValueTableDropDown(FieldNames.StatementEndDay, "Friday");
            }

            Page.Click(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate);
            Assert.IsTrue(Page.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, negativeInterger);
            Page.ClickFieldLabel(FieldNames.PaymentTerms + requiredLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "1234");
            Page.ClickFieldLabel(FieldNames.PaymentTerms + requiredLabel);
            Assert.IsTrue(Page.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
            Page.WaitForAnyElementLocatedBy(FieldNames.OverrideIfDealerInvoiceDate);
            Page.ClearText(FieldNames.OverrideIfDealerInvoiceDate);
            Page.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-7)));
            Page.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(Page.CheckForText("Override If Dealer Invoice Date < Days From Current Date is required"));
            Page.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, validInt);
            var rowCountFromTablerelSenderBefore = AccountMaintenanceUtil.GetRowCountFromTable("relSenderReceiver_tb");
            var rowCountFromTablerelPaymentTermBefore = AccountMaintenanceUtil.GetRowCountFromTable("RelPaymentTerm_tb");
            var rowCountFromTableAuditTrail_tbBefore = AccountMaintenanceUtil.GetRowCountFromTable("AuditTrail_tb");
            Page.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(Page.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            var rowCountFromTablerelSenderAfter = AccountMaintenanceUtil.GetRowCountFromTable("relSenderReceiver_tb");
            var rowCountFromTablerelPaymentTermAfter = AccountMaintenanceUtil.GetRowCountFromTable("RelPaymentTerm_tb");
            var rowCountFromTableAuditTrail_tbAfter = AccountMaintenanceUtil.GetRowCountFromTable("AuditTrail_tb");

            Assert.AreEqual(rowCountFromTablerelSenderBefore + 1, rowCountFromTablerelSenderAfter);
            Assert.AreEqual(rowCountFromTablerelPaymentTermBefore + 1, rowCountFromTablerelPaymentTermAfter);
            Assert.AreEqual(rowCountFromTableAuditTrail_tbBefore + 2, rowCountFromTableAuditTrail_tbAfter);

            Page.ButtonClick(ButtonsAndMessages.Search);
            Page.WaitForLoadingIcon();
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerGroup);
            Page.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetGroup);
            Page.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);

            List<string> errorMsgs = new List<string>();

            List<string> headers = new List<String>()
                    {
                        TableHeaders.Commands,
                        TableHeaders.PaymentTerms,
                        TableHeaders.BillingCycle,
                        TableHeaders.StatementEndDay,
                        TableHeaders.BillingCycleStartDate,
                        TableHeaders.StatementType,
                        TableHeaders.WhatisDiscountable,
                        TableHeaders.DiscountAppliedonLineItemTypes,
                        TableHeaders.RoundingType,
                        TableHeaders.NumberofDigitstobeRound,
                        TableHeaders.AccelerationProgramName,
                        TableHeaders.AccelerationTypeName,
                        TableHeaders.StartBasedOn,
                        TableHeaders.StartDate,
                        TableHeaders.EndDate,
                        TableHeaders.IsActive,
                        TableHeaders.User,
                        TableHeaders.SettlementBasedDueDateCalculation,
                        TableHeaders.TermDescription,
                        TableHeaders.VariableDueDatecalculation,
                        TableHeaders.IsOverrideTermDescription,
                        TableHeaders.OverrideTermDescriptionDays
                    };

            errorMsgs.AddRange(Page.ValidateTableHeaders("Relationship Grid Table", headers.ToArray()));

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
