using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.CreateNewEntity;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.CreateNewEntity
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Create New Entity")]
    internal class CreateNewEntity : DriverBuilderClass
    {
        CreateNewEntityPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.CreateNewEntity);
            Page = new CreateNewEntityPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16434" })]
        public void TC_16434(string UserType)
        {
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Dealer);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Language), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.MasterLocation), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.MasterLocation));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DDEFlag), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DDEFlag));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.VendorPaymentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.VendorPaymentType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FranchiseCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FranchiseCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.SelectedFranchiseCodes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SelectedFranchiseCodes));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                Assert.AreEqual("US", Page.GetValueOfDropDown(FieldNames.Country), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Country));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                Assert.AreEqual("Alabama", Page.GetValueOfDropDown(FieldNames.State), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.State));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Currency), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Currency));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FederalTaxIDNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FederalTaxIDNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DealerType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DealerType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AllowBillingNonTranLocations), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AllowBillingNonTranLocations));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DoNotChargeFee), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DoNotChargeFee));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PreAuthorization), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PreAuthorization));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CorcentricLocation), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CorcentricLocation));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);

            });

            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Language), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.ParentAccountName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.MasterLocation), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.MasterLocation));
                Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.MasterLocation), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.MasterLocation));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DDEFlag), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DDEFlag));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.VendorPaymentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.VendorPaymentType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FranchiseCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FranchiseCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.SelectedFranchiseCodes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SelectedFranchiseCodes));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                Assert.AreEqual("US", Page.GetValueOfDropDown(FieldNames.Country), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Country));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                Assert.AreEqual("Alabama", Page.GetValueOfDropDown(FieldNames.State), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.State));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Currency), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Currency));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FederalTaxIDNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FederalTaxIDNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DealerType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DealerType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AllowBillingNonTranLocations), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AllowBillingNonTranLocations));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DoNotChargeFee), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DoNotChargeFee));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PreAuthorization), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PreAuthorization));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CorcentricLocation), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CorcentricLocation));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16435" })]
        public void TC_16435(string UserType)
        {
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Fleet);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.MasterLocation), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.MasterLocation));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ProgramCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CreditLimit), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CreditLimit));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                Assert.AreEqual("US", Page.GetValueOfDropDown(FieldNames.Country), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Country));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                Assert.AreEqual("Alabama", Page.GetValueOfDropDown(FieldNames.State), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.State));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Currency), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Currency));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DirectBillCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DirectBillCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.InvoiceApproval), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApproval));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FinanceChargeExempt));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);

            });

            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                Assert.IsTrue(Page.IsDropDownDisabled(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.ParentAccountName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.MasterLocation), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.MasterLocation));
                Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.MasterLocation), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.MasterLocation));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ProgramCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CreditLimit), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CreditLimit));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                Assert.AreEqual("US", Page.GetValueOfDropDown(FieldNames.Country), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Country));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                Assert.AreEqual("Alabama", Page.GetValueOfDropDown(FieldNames.State), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.State));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Currency), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Currency));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DirectBillCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DirectBillCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.InvoiceApproval), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApproval));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FinanceChargeExempt));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16436" })]
        public void TC_16436(string UserType)
        {
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Dealer);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Master.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Master.ToString());
                t.Wait();
                t.Dispose();
            }
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Language), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DDEFlag), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DDEFlag));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FranchiseCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FranchiseCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.SelectedFranchiseCodes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SelectedFranchiseCodes));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                Assert.AreEqual("US", Page.GetValueOfDropDown(FieldNames.Country), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Country));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                Assert.AreEqual("Alabama", Page.GetValueOfDropDown(FieldNames.State), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.State));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Currency), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Currency));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.FederalTaxIDNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FederalTaxIDNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PreAuthorization), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PreAuthorization));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);

            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16437" })]
        public void TC_16437(string UserType)
        {
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Fleet);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Master.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Master.ToString());
                t.Wait();
                t.Dispose();
            }
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Language), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ProgramCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CreditLimit), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CreditLimit));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EdiCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EdiCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Collector), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Collector));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CustomerTier), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CustomerTier));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                Assert.AreEqual("US", Page.GetValueOfDropDown(FieldNames.Country), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Country));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                Assert.AreEqual("Alabama", Page.GetValueOfDropDown(FieldNames.State), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.State));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.Currency), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Currency));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.DirectBillCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DirectBillCode));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.InvoiceApproval), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApproval));
                Assert.IsTrue(Page.IsElementDisplayed(FieldNames.PreAuthorization), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PreAuthorization));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16438" })]
        public void TC_16438(string UserType)
        {
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Dealer);
            t.Wait();
            t.Dispose();
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            string corcentricCode = CommonUtils.RandomString(8);
            Page.EnterTextAfterClear(FieldNames.DisplayName, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.LegalName, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.AccountCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.AccountingCode, corcentricCode);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Page.EnterTextAfterClear(FieldNames.Address1, corcentricCode + " Address");
            Page.EnterTextAfterClear(FieldNames.City, "City1");
            if (Page.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (Page.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                Page.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            Page.EnterTextAfterClear(FieldNames.Zip, "55555");
            Page.EnterTextAfterClear(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.SelectValueTableDropDown(FieldNames.Currency, "USD");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);
            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{corcentricCode}]");
            }
            EntityDetails entityDetails = CommonUtils.GetEntityDetails(corcentricCode);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));

            menu.SwitchToMainWindow();
            string masterDealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            menu.OpenPopUpPage(Pages.CreateNewEntity);
            Page = new CreateNewEntityPage(driver);
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Dealer);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            corcentricCode = CommonUtils.RandomString(8);
            Page.EnterTextAfterClear(FieldNames.DisplayName, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.LegalName, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.AccountCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.AccountingCode, corcentricCode);
            Page.SelectValueRowByIndex(FieldNames.ParentAccountName, 2);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Page.EnterTextAfterClear(FieldNames.Address1, corcentricCode + " Address");
            Page.EnterTextAfterClear(FieldNames.City, "City1");
            if (Page.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (Page.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                Page.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            Page.EnterTextAfterClear(FieldNames.Zip, "55555");
            Page.EnterTextAfterClear(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.SelectValueTableDropDown(FieldNames.Currency, "USD");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);
            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{corcentricCode}]");
            }
            entityDetails = CommonUtils.GetEntityDetails(corcentricCode);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16439" })]
        public void TC_16439(string UserType)
        {
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Fleet);
            t.Wait();
            t.Dispose();
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            string corcentricCode = CommonUtils.RandomString(8);
            Page.EnterTextAfterClear(FieldNames.DisplayName, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.LegalName, corcentricCode);
            Page.SelectValueListBoxByScroll(FieldNames.ProgramCode, "PCARD");
            Page.EnterTextAfterClear(FieldNames.AccountCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.CreditLimit, "55555555");
            Page.EnterTextAfterClear(FieldNames.AccountingCode, corcentricCode);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Page.EnterTextAfterClear(FieldNames.Address1, corcentricCode + " Address");
            Page.EnterTextAfterClear(FieldNames.City, "City1");
            if (Page.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (Page.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                Page.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            Page.EnterTextAfterClear(FieldNames.Zip, "55555");
            Page.EnterTextAfterClear(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.SelectValueTableDropDown(FieldNames.Currency, "USD");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);
            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{corcentricCode}]");
            }
            EntityDetails entityDetails = CommonUtils.GetEntityDetails(corcentricCode);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));

            menu.SwitchToMainWindow();
            string masterDealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            menu.OpenPopUpPage(Pages.CreateNewEntity);
            Page = new CreateNewEntityPage(driver);
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Fleet);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            corcentricCode = CommonUtils.RandomString(8);
            Page.EnterTextAfterClear(FieldNames.DisplayName, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.LegalName, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.AccountCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.CreditLimit, "55555555");
            Page.EnterTextAfterClear(FieldNames.AccountingCode, corcentricCode);
            Page.SelectValueRowByIndex(FieldNames.ParentAccountName, 2);
            Page.SelectValueListBoxByScroll(FieldNames.ProgramCode, "PCARD");
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Page.EnterTextAfterClear(FieldNames.Address1, corcentricCode + " Address");
            Page.EnterTextAfterClear(FieldNames.City, "City1");
            if (Page.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (Page.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                Page.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            Page.EnterTextAfterClear(FieldNames.Zip, "55555");
            Page.EnterTextAfterClear(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.SelectValueTableDropDown(FieldNames.Currency, "USD");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);
            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{corcentricCode}]");
            }
            entityDetails = CommonUtils.GetEntityDetails(corcentricCode);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21627" })]
        public void TC_21627(string UserType)
        {
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Fleet);
            t.Wait();
            t.Dispose();
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            string corcentricCode = CommonUtils.RandomString(8);
            Page.EnterTextAfterClear(FieldNames.DisplayName, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.LegalName, corcentricCode);
            Page.SelectValueTableDropDown(FieldNames.Language, "en-US");
            Page.SelectValueListBoxByScroll(FieldNames.ProgramCode, "PCARD");
            Page.EnterTextAfterClear(FieldNames.AccountCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.CreditLimit, "55555555");
            Page.EnterTextAfterClear(FieldNames.AccountingCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.EdiCode, "123");
            Page.EnterTextAfterClear(FieldNames.Collector, "test@corcentric.com");
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Page.EnterTextAfterClear(FieldNames.Address1, corcentricCode + " Address1");
            Page.EnterTextAfterClear(FieldNames.Address2, corcentricCode + " Address2");
            Page.EnterTextAfterClear(FieldNames.City, "City1");
            if (Page.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (Page.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                Page.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            Page.EnterTextAfterClear(FieldNames.Zip, "55555");
            Page.EnterTextAfterClear(FieldNames.County, "ABC");
            Page.EnterTextAfterClear(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.SelectValueTableDropDown(FieldNames.Currency, "USD");
            Page.EnterTextAfterClear(FieldNames.DunNumber, "222222222");
            Page.EnterTextAfterClear(FieldNames.EntityCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.CommunityCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.DirectBillCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.SelectValueRowByIndex(FieldNames.SubCommunity, 2);
            Page.SelectValueTableDropDown(FieldNames.InvoiceApproval, "Disabled");
            Page.ClickElement(FieldNames.PreAuthorization);
            Page.ClickElement(FieldNames.FinanceChargeExempt);
            Page.ButtonClick(ButtonsAndMessages.Save);
            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{corcentricCode}]");
            }
            EntityDetails entityDetails = CommonUtils.GetEntityDetails(corcentricCode);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
            Assert.IsTrue(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "True", entityDetails.FinanceChargeExempt.ToString()));

            menu.SwitchToMainWindow();
            string masterDealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            menu.OpenPopUpPage(Pages.CreateNewEntity);
            Page = new CreateNewEntityPage(driver);
            Page.WaitForElementToBeVisible(FieldNames.EnrollmentType);
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, EntityType.Fleet);
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            corcentricCode = CommonUtils.RandomString(8);
            Page.EnterTextAfterClear(FieldNames.DisplayName, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.LegalName, corcentricCode);
            Page.SelectValueRowByIndex(FieldNames.ParentAccountName, 2);
            Page.SelectValueTableDropDown(FieldNames.Language, "en-US");
            Page.SelectValueListBoxByScroll(FieldNames.ProgramCode, "PCARD");
            Page.EnterTextAfterClear(FieldNames.AccountCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.CreditLimit, "55555555");
            Page.EnterTextAfterClear(FieldNames.AccountingCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.EdiCode, "123");
            Page.EnterTextAfterClear(FieldNames.Collector, "test@corcentric.com");
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Page.EnterTextAfterClear(FieldNames.Address1, corcentricCode + " Address1");
            Page.EnterTextAfterClear(FieldNames.Address2, corcentricCode + " Address2");
            Page.EnterTextAfterClear(FieldNames.City, "City1");
            if (Page.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (Page.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                Page.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            Page.EnterTextAfterClear(FieldNames.Zip, "55555");
            Page.EnterTextAfterClear(FieldNames.County, "ABC");
            Page.EnterTextAfterClear(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.SelectValueTableDropDown(FieldNames.Currency, "USD");
            Page.EnterTextAfterClear(FieldNames.DunNumber, "222222222");
            Page.EnterTextAfterClear(FieldNames.EntityCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.CommunityCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.DirectBillCode, corcentricCode);
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.SelectValueRowByIndex(FieldNames.SubCommunity, 2);
            Page.SelectValueTableDropDown(FieldNames.InvoiceApproval, "Disabled");
            Page.ClickElement(FieldNames.PreAuthorization);
            Page.ClickElement(FieldNames.FinanceChargeExempt);
            Page.ButtonClick(ButtonsAndMessages.Save);
            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{corcentricCode}]");
            }
            entityDetails = CommonUtils.GetEntityDetails(corcentricCode);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
            Assert.IsTrue(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, corcentricCode, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24066" })]
        public void TC_24066(string UserType)
        {
            string zip = "12345";
            string state = "AL";
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "US");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                Page.SelectValueTableDropDown(FieldNames.State, "Alabama");

            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);
            if (!Page.WaitForPopupWindowToClose())
            {
                Page.EnterText(FieldNames.Zip, zip);
                Page.ButtonClick(ButtonsAndMessages.Save);
                if (!Page.WaitForPopupWindowToClose())
                {
                    Assert.Fail($"Some error occurred while creating entity.");
                }
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24065" })]
        public void TC_24065(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "PK")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "PK");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24064" })]
        public void TC_24064(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "NL")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "NL");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24063" })]
        public void TC_24063(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "MX")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "MX");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24062" })]
        public void TC_24062(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "IT")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "IT");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24061" })]
        public void TC_24061(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "GB")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "GB");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");            
            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24060" })]
        public void TC_24060(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "FR")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "FR");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");
            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24059" })]
        public void TC_24059(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "ES")
            {
                Page.SelectValueByScroll(FieldNames.Country, "ES");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");
            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24058" })]
        public void TC_24058(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "DE")
            {
                Page.SelectValueByScroll(FieldNames.Country, "DE");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");
            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24057" })]
        public void TC_24057(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "CA")
            {
                Page.SelectValueByScroll(FieldNames.Country, "CA");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");
            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24056" })]
        public void TC_24056(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "AU")
            {
                Page.SelectValueByScroll(FieldNames.Country, "AU");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");
            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24090" })]
        public void TC_24090(string UserType)
        {
            string zip = "12345";
            string state = "AL";
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(3);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "US");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                Page.SelectValueTableDropDown(FieldNames.State, "Alabama");

            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);
            if (!Page.WaitForPopupWindowToClose())
            {
                Page.EnterText(FieldNames.Zip, zip);
                Page.ButtonClick(ButtonsAndMessages.Save);
                if (!Page.WaitForPopupWindowToClose())
                {
                    Assert.Fail($"Some error occurred while creating entity.");
                }
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24089" })]
        public void TC_24089(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "PK")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "PK");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24088" })]
        public void TC_24088(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "NL")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "NL");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24087" })]
        public void TC_24087(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "MX")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "MX");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24086" })]
        public void TC_24086(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "IT")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "IT");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24085" })]
        public void TC_24085(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "GB")
            {
                Page.SelectValueByScroll(FieldNames.Country, "GB");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


                EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24084" })]
        public void TC_24084(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "FR")
            {
                Page.SelectValueByScroll(FieldNames.Country, "FR");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName); 
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24083" })]
        public void TC_24083(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "ES")
            {
                Page.SelectValueByScroll(FieldNames.Country, "ES");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

                
            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");

        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24082" })]
        public void TC_24082(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "DE")
            {
                Page.SelectValueByScroll(FieldNames.Country, "DE");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24081" })]
        public void TC_24081(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "CA")
            {
                Page.SelectValueByScroll(FieldNames.Country, "CA");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24080" })]
        public void TC_24080(string UserType)
        {
            string zip = string.Empty;
            string state = string.Empty;
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(4);
            string parentAccountName = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);


            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.ParentAccountName, parentAccountName);
            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "AU")
            {
                Page.SelectValueByScroll(FieldNames.Country, "AU");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                Page.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, Page.GetValueOfDropDown(FieldNames.State));
            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueByScroll(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);

            if (!Page.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24078" })]
        public void TC_24078(string UserType)
        {
            string zip = "12345";
            string state = "AL";
            string dealerName = "Automate_Dealer" + CommonUtils.RandomString(3);

            Page.EnterText(FieldNames.DisplayName, dealerName);
            Page.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
            Page.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (Page.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Master.ToString())
            {
                t = Task.Run(() => Page.WaitForStalenessOfElement(FieldNames.DisplayName));
                Page.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Master.ToString());
                t.Wait();
                t.Dispose();
            }

            Page.EnterText(FieldNames.AccountCode, dealerName);
            Page.EnterText(FieldNames.AccountingCode, dealerName);
            Page.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(Page.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            Page.EnterText(FieldNames.Address1, dealerName);
            Page.EnterText(FieldNames.City, dealerName);
            if (Page.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                Page.SelectValueTableDropDown(FieldNames.Country, "US");
            }

            if (Page.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                Page.SelectValueTableDropDown(FieldNames.State, "Alabama");

            }

            if (Page.GetValueOfDropDown(FieldNames.Currency) != "USD")
            {
                Page.SelectValueTableDropDown(FieldNames.Currency, "USD");

            }

            Page.ClearText(FieldNames.Zip);
            Page.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            Page.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            Page.ButtonClick(ButtonsAndMessages.Save);
            if (!Page.WaitForPopupWindowToClose())
            {
                Page.EnterText(FieldNames.Zip, zip);
                Page.ButtonClick(ButtonsAndMessages.Save);
                if (!Page.WaitForPopupWindowToClose())
                {
                    Assert.Fail($"Some error occurred while creating entity.");
                }
            }


            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }
        }
}
