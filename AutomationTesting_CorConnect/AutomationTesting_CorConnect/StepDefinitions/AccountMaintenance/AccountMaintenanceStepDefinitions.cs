using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.AccountMaintenance;
using AutomationTesting_CorConnect.PageObjects.CreateNewEntity;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.Utils.InvoiceEntry;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V109.Debugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.AccountMaintenance
{
    [Binding]
    internal class AccountMaintenanceStepDefinitions : DriverBuilderClass
    {
        AccountMaintenanceAspx aspxPage;
        AccountMaintenancePage Page;
        string fleetEntityEnabledPayment = "";
        string TaxIDValue;
        string taxClassificationValue;
        string corcentricCode;

        [StepDefinition(@"Relationship ""([^""]*)"" with ""(.*)"" is created between Dealer ""([^""]*)"" and Fleet ""([^""]*)""")]
        public void CreateRelationShip(string relationshipName, string handlingType, string dealer, string fleet)
        {

            Page = new AccountMaintenancePage(driver);
            aspxPage = new AccountMaintenanceAspx(driver);
            Page.LoadDataOnGrid(fleet, EntityType.Fleet);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);

            switch (relationshipName)
            {
                case "Financial Handling":
                    aspxPage.CreateFinancialHandlingRelation(dealer, handlingType);
                    Console.WriteLine($"Dealer Used for Creating Financial Handling Relation: [{dealer}]");
                    break;
                case "Payment Method":
                    aspxPage.CreatePaymentMethodRelation(dealer, handlingType);
                    Console.WriteLine($"Dealer Used for Creating Payment Method Relation: [{dealer}]");
                    break;
                case "Data Requirements":
                    aspxPage.CreateDataRequirementRelation(dealer, handlingType);
                    Console.WriteLine($"Dealer Used for Creating Financial Handling Relation: [{dealer}]");
                    break;
            }

            aspxPage.ClosePopupWindow();

        }

        [StepDefinition(@"Relationship ""([^""]*)"" with ""([^""]*)"" is updated between Dealer ""([^""]*)"" and Fleet ""([^""]*)""")]
        public void WhenRelationshipWithIsUpdatedBetweenDealerAndFleet(string relationshipName, string handlingType, string dealer, string fleet)
        {
            Page = new AccountMaintenancePage(driver);
            aspxPage = new AccountMaintenanceAspx(driver);
            Page.LoadDataOnGrid(fleet, EntityType.Fleet);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);

            switch (relationshipName)
            {
                case "Payment Method":
                    aspxPage.UpdatePaymentMethodRelation(handlingType);
                    break;
            }

            aspxPage.ClosePopupWindow();

        }

        [Then(@"credit limit should be (.*) for Fleet ""([^""]*)"" on Account Maintenance")]
        public void CreditLimitValidation(int fleetCredit, string fleetName)
        {
            Page = new AccountMaintenancePage(driver);
            Page.LoadDataOnGrid(fleetName, EntityType.Fleet);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage = new AccountMaintenanceAspx(driver);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.AreEqual(fleetCredit, aspxPage.GetValue(FieldNames.CreditLimit), "Fleet Credit Mismatch");
        }

        [When(@"User is on ""([^""]*)"" tab for Enrollment Type ""([^""]*)"" and (.*)")]
        public void SwitchToTab(string tabName, string enrollmentType, string locationType)
        {

            Page = new AccountMaintenancePage(driver);
            aspxPage = new AccountMaintenanceAspx(driver);
            switch (locationType.Trim())
            {
                case "Master Billing":
                    string masterbillingFleetCode = AccountMaintenanceUtil.GetFleetCode(LocationType.MasterBilling);
                    enrollmentType = masterbillingFleetCode;
                    Page.LoadDataOnGrid(enrollmentType);
                    if (Page.IsAnyDataOnGrid())
                    {
                        corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                        Page.ClickHyperLinkOnGrid(FieldNames.Name);
                        aspxPage.ClickListElements("TabList", tabName);
                        aspxPage.WaitForIframe();
                        aspxPage.SwitchIframe();
                        Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
                    }
                    break;

                case "Billing":
                    string billingFleetCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
                    enrollmentType = billingFleetCode;
                    Page.LoadDataOnGrid(enrollmentType);
                    if (Page.IsAnyDataOnGrid())
                    {
                        corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                        Page.ClickHyperLinkOnGrid(FieldNames.Name);
                        aspxPage.ClickListElements("TabList", tabName);
                        aspxPage.WaitForIframe();
                        aspxPage.SwitchIframe();
                        Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
                    }
                    break;


                case "Master":
                    string masterFleetCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Master);
                    enrollmentType = masterFleetCode;
                    Page.LoadDataOnGrid(enrollmentType);
                    corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                    Page.ClickHyperLinkOnGrid(FieldNames.Name);
                    aspxPage.ClickListElements("TabList", tabName);
                    aspxPage.WaitForIframe();
                    aspxPage.SwitchIframe();
                    Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
                    break;

                case "Shop":
                    string ShopCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Shop);
                    enrollmentType = ShopCode;
                    Page.LoadDataOnGrid(enrollmentType);
                    corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                    Page.ClickHyperLinkOnGrid(FieldNames.Name);
                    aspxPage.ClickListElements("TabList", tabName);
                    aspxPage.WaitForIframe();
                    aspxPage.SwitchIframe();
                    Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
                    break;

                case "Parent Shop":
                    string ParentShopCode = AccountMaintenanceUtil.GetFleetCode(LocationType.ParentShop);
                    enrollmentType = ParentShopCode;
                    Page.LoadDataOnGrid(enrollmentType);
                    corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                    Page.ClickHyperLinkOnGrid(FieldNames.Name);
                    aspxPage.ClickListElements("TabList", tabName);
                    aspxPage.WaitForIframe();
                    aspxPage.SwitchIframe();
                    Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
                    break;
            }
        }


        [Then(@"Valid fields with default values should displayed for (.*) on ""([^""]*)"" tab")]
        public void ValidFieldsDisplay(string locationType, string tabName)
        {
            switch (locationType)
            {
                case "Master Billing":

                    var errorMsgs = aspxPage.VerifyAccountConfigFieldsFleet(LocationType.MasterBilling);
                    Assert.Multiple(() =>
                    {
                        foreach (string errorMsg in errorMsgs)
                        {
                            Assert.Fail("Master Billing Location: " + errorMsg);
                        }
                    });
                    break;

                case "Billing":

                    var errorMsgs1 = aspxPage.VerifyAccountConfigFieldsFleet(LocationType.Billing);
                    Assert.Multiple(() =>
                    {
                        foreach (string errorMsg in errorMsgs1)
                        {
                            Assert.Fail("Billing Location: " + errorMsg);
                        }
                    });
                    break;

                case "Master":

                    var errorMsgs2 = aspxPage.VerifyAccountConfigFieldsFleet(LocationType.Master);
                    Assert.Multiple(() =>
                    {
                        foreach (string errorMsg in errorMsgs2)
                        {
                            Assert.Fail("Master Location: " + errorMsg);
                        }
                    });
                    break;

                case "Shop":

                    var errorMsgs3 = aspxPage.VerifyAccountConfigFieldsFleet(LocationType.Shop, tabName);
                    Assert.Multiple(() =>
                    {
                        foreach (string errorMsg in errorMsgs3)
                        {
                            Assert.Fail("Shop Location: " + errorMsg);
                        }
                    });
                    break;

                case "Parent Shop":

                    var errorMsgs4 = aspxPage.VerifyAccountConfigFieldsFleet(LocationType.ParentShop, tabName);
                    Assert.Multiple(() =>
                    {
                        foreach (string errorMsg in errorMsgs4)
                        {
                            Assert.Fail("Parent Shop Location: " + errorMsg);
                        }
                    });
                    break;
            }
        }

        [When(@"""([^""]*)"" Location is updated to non pay online account")]
        public void WhenLocationIsUpdatedToNonPayOnlineAccount(string enrollmentType)
        {
            Page = new AccountMaintenancePage(driver);
            switch (enrollmentType)
            {
                case "Buyer Billing":
                    fleetEntityEnabledPayment = AccountMaintenanceUtil.GetFleetCodeEnabledForPaymentPortal(LocationType.Billing);
                    Page.LoadDataOnGrid(fleetEntityEnabledPayment);

                    Page.ClickHyperLinkOnGrid(TableHeaders.Name);
                    aspxPage = new AccountMaintenanceAspx(driver);
                    aspxPage.ClickListElements("TabList", "Account Configuration");
                    aspxPage.SwitchIframe();

                    if (aspxPage.IsCheckBoxChecked(FieldNames.EnablePaymentPortal))
                    {
                        aspxPage.ClickElement(FieldNames.EnablePaymentPortal);
                        Assert.IsTrue(aspxPage.IsCheckBoxUnchecked(FieldNames.EnablePaymentPortal));
                        aspxPage.ButtonClick(ButtonsAndMessages.Save);
                    }
                    break;
            }
        }

        [Then(@"""([^""]*)"" Location is updated to non pay online successfully")]
        public void ThenLocationIsUpdatedToNonPayOnlineSuccessfully(string enrollmentType)
        {
            Page = new AccountMaintenancePage(driver);
            switch (enrollmentType)
            {
                case "Buyer Billing":
                    Assert.IsFalse(AccountMaintenanceUtil.IsEntityOnlinePay(fleetEntityEnabledPayment));
                    Console.WriteLine("Enable Payment Portal checkbox unchecked successfully");
                    break;
            }
        }

        [When(@"User switches to ""([^""]*)"" tab")]
        public void SwitchTab(string tabName)
        {
            aspxPage.ClickTab("Account Configuration Tabs", tabName);
            if (tabName == "New Locations")
            {
                aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
                aspxPage.ButtonClick(FieldNames.AddNewLocation);
                aspxPage.WaitForElementToBeVisible("New Location Frame");
                aspxPage.SwitchIframe();
                aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);
            }
        }


        [Then(@"Valid fields with default values is displayed for Parent Shop on ""([^""]*)"" tab")]

        public void ThenValidFieldsWithDefaultValuesIsDisplayedForShopOnTab(string p0)
        {

            Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorPhone), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorPhone));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorEmail), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorEmail));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorCompany), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorCompany));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Language), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
                    Assert.AreEqual("en-US", aspxPage.GetValueOfDropDown(FieldNames.Language), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Language));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                    Assert.AreEqual(menu.RenameMenuField(FieldNames.Fleet), aspxPage.GetValueOfDropDown(FieldNames.EnrollmentType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EnrollmentType));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                    Assert.AreEqual("Shop", aspxPage.GetValueOfDropDown(FieldNames.LocationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.LocationType));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AnticipatedMonthlySpend), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AnticipatedMonthlySpend));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestedCreditLine), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestedCreditLine));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProgramCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CreditLimit), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CreditLimit));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.NAManager), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.NAManager));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Deactivated), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Deactivated));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Terminated), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Terminated));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TerminatedDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TerminatedDate));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TerminationNotes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TerminationNotes));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.UploadTaxForm), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UploadTaxForm));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxFormName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TaxFormName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CopyFromMaster), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CopyFromMaster));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                    Assert.IsTrue(aspxPage.IsElementDisplayed("Currency Dropdown"), string.Format(ErrorMessages.FieldNotDisplayed, "Currency"));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxInfo), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TaxInfo));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DirectBillCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DirectBillCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.InvoiceApproval), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApproval));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PreAuthorization), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PreAuthorization));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SelectionBoxAvailable), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SelectionBoxAvailable));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                    Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.FieldDisplayed, FieldNames.FinanceChargeExempt));
                    Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.EnablePaymentPortal), string.Format(ErrorMessages.FieldDisplayed, FieldNames.EnablePaymentPortal));
                });
        }


        [When(@"User Updates data for in Tax ID Type and Tax Classification Fields for ""([^""]*)"" and  ""([^""]*)""")]
        public void WhenUserUpdatesDataForInTaxIDTypeAndTaxClassificationFieldsForAnd(string enrollmentType, string locationType)
        {
            if (locationType == "Master Billing" || locationType == "Master")
            {
                aspxPage.EnterTextAfterClear(FieldNames.FleetCount, "10");
            }
            if (aspxPage.IsCheckBoxDisplayed(FieldNames.NoProgramCodeAssignment))
            {
                aspxPage.Click(FieldNames.NoProgramCodeAssignment);
            }

            aspxPage.SelectValueTableDropDown(FieldNames.TaxIDType);
            TaxIDValue = aspxPage.GetValueOfDropDown(FieldNames.TaxIDType);
            aspxPage.SelectValueTableDropDown(FieldNames.TaxClassification);
            taxClassificationValue = aspxPage.GetValueOfDropDown(FieldNames.TaxClassification);
            aspxPage.ButtonClick(ButtonsAndMessages.Save);
        }

        [Then(@"Data should be saved in Tax ID Type and Tax Classification Fields")]
        public void ThenDataShouldBeSavedInTaxIDTypeAndTaxClassificationFields()
        {
            Assert.AreEqual(TaxIDValue, AccountMaintenanceUtil.GetTaxIDValue(corcentricCode), GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(taxClassificationValue, AccountMaintenanceUtil.GetTaxClassificationIDValue(corcentricCode), GetErrorMessage(ErrorMessages.IncorrectValue));
        }




    }
}



