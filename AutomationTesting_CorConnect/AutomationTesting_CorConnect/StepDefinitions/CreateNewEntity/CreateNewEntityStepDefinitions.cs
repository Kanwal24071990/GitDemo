using System;
using AutomationTesting_CorConnect.DriverBuilder;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.PageObjects.CreateNewEntity;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using System.Threading;
using AutomationTesting_CorConnect.DMS.RequestObjects;
using AutomationTesting_CorConnect.PageObjects;
using NpgsqlTypes;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils.InvoiceEntry.CreateNewInvoice;

namespace AutomationTesting_CorConnect.StepDefinitions.CreateNewEntity
{
    [Binding]
    [Parallelizable(ParallelScope.Fixtures)]
    internal class CreateNewEntityStepDefinitions : DriverBuilderClass
    {
        CreateNewEntityPage CreateNewEntityPage;
        Menu menu;

        string dealerName = "Automation_Dealer" + CommonUtils.RandomString(4);
        string fleetName = "Automation_Fleet" + CommonUtils.RandomString(4);
        string corcentricCode = CommonUtils.RandomString(8);
        string TaxIDValue;
        string taxClassificationValue;

        [Given(@"""([^""]*)"" Entity is created")]
        public void CreateEntity(string entityType)
        {
            menu.SwitchToMainWindow();
            menu.OpenPopUpPage(Pages.CreateNewEntity);
            CreateNewEntityPage = new CreateNewEntityPage(driver);
            if (entityType == "Dealer")
            {
                CreateNewEntityPage.CreateDealerEntity(dealerName);
                Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
            }

            if (entityType == "Fleet")
            {
                CreateNewEntityPage.CreateFleetEntity(fleetName);
                Console.WriteLine($"Fleet Created with Code: [{fleetName}]");

            }
        }

        [When(@"User select Enrollment Type as ""([^""]*)"" on Create New Entity page")]
        public void SelectEnrollmentType(string enrollmentType)
        {
            CreateNewEntityPage = new CreateNewEntityPage(driver);
            menu = new Menu(driver);
            switch (enrollmentType)
            {
                case "Buyer":
                    CreateNewEntityPage.WaitForElementToBeVisible(FieldNames.EnrollmentType);
                    Task t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                    CreateNewEntityPage.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Fleet));
                    t.Wait();
                    t.Dispose();
                    if (CreateNewEntityPage.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
                    {
                        t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                        CreateNewEntityPage.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                        t.Wait();
                        t.Dispose();
                    }
                    break;
            }

        }

        [Then(@"Valid Fields are displayed on Create New Entity page")]
        public void VerifyValidFields()
        {
            CreateNewEntityPage = new CreateNewEntityPage(driver);
            Assert.Multiple(() =>
                {
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.MasterLocation), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.MasterLocation));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.ProgramCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramCode));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.CreditLimit), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CreditLimit));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                    Assert.AreEqual("US", CreateNewEntityPage.GetValueOfDropDown(FieldNames.Country), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Country));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                    Assert.AreEqual("Alabama", CreateNewEntityPage.GetValueOfDropDown(FieldNames.State), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.State));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.Currency), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Currency));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.TaxIDType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TaxIDType));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.TaxClassification), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TaxIDType));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.DirectBillCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DirectBillCode));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.InvoiceApproval), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApproval));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FinanceChargeExempt));
                    Assert.IsTrue(CreateNewEntityPage.IsElementDisplayed(FieldNames.EnablePaymentPortal), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnablePaymentPortal));
                    Assert.IsFalse(CreateNewEntityPage.IsCheckBoxChecked(FieldNames.EnablePaymentPortal));
                    Assert.IsTrue(CreateNewEntityPage.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                    Assert.IsTrue(CreateNewEntityPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                });

        }

        [When(@"""([^""]*)"" Location is created with default values")]
        public void BuyerCreatedWithDefaultValues(string entityLocation)
        {
            CreateNewEntityPage = new CreateNewEntityPage(driver);
            menu = new Menu(driver);
            switch (entityLocation)
            {
                case "Buyer Billing":
                    Task t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                    CreateNewEntityPage.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Fleet));
                    t.Wait();
                    t.Dispose();
                    t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                    CreateNewEntityPage.ClickElement(FieldNames.MasterLocation);
                    t.Wait();
                    t.Dispose();
                    CreateNewEntityPage.InputMandatoryFieldsFleet(entityLocation, fleetName);
                    CreateNewEntityPage.ButtonClick(ButtonsAndMessages.Save);
                    break;
            }

        }

        [Then(@"""([^""]*)"" Location is created successfully")]
        public void EntityCreatedWithoutEnablePaymentPortalCheckbox(string entityLocation)
        {
            CreateNewEntityPage = new CreateNewEntityPage(driver);
            switch (entityLocation)
            {
                case "Buyer Billing":
                    if (!CreateNewEntityPage.WaitForPopupWindowToClose())
                    {
                        Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{fleetName}]");
                    }
                    EntityDetails entityDetails = CommonUtils.GetEntityDetails(fleetName);
                    Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, fleetName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
                    Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, fleetName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
                    Console.WriteLine($"Fleet Created with Code: [{fleetName}]");
                    break;
            }

        }

        [When(@"""([^""]*)"" Location with Pay Online is created")]
        public void CreateEntityWithPayOnline(string entityLocation)
        {
            CreateNewEntityPage = new CreateNewEntityPage(driver);
            menu = new Menu(driver);
            switch (entityLocation)
            {
                case "Buyer Billing":
                    Task t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                    CreateNewEntityPage.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Fleet));
                    t.Wait();
                    t.Dispose();
                    t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                    CreateNewEntityPage.ClickElement(FieldNames.MasterLocation);
                    t.Wait();
                    t.Dispose();
                    CreateNewEntityPage.InputMandatoryFieldsFleet(entityLocation, fleetName);
                    CreateNewEntityPage.ClickElement(FieldNames.EnablePaymentPortal);
                    Assert.IsTrue(CreateNewEntityPage.IsCheckBoxChecked(FieldNames.EnablePaymentPortal));
                    CreateNewEntityPage.ButtonClick(ButtonsAndMessages.Save);
                    break;
            }
        }

        [Then(@"Pay Online ""([^""]*)"" Location is created successfully")]
        public void ThenPayOnlineLocationIsCreatedSuccessfully(string entityLocation)
        {
            CreateNewEntityPage = new CreateNewEntityPage(driver);
            switch (entityLocation)
            {
                case "Buyer Billing":
                    if (!CreateNewEntityPage.WaitForPopupWindowToClose())
                    {
                        Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{fleetName}]");
                    }
                    EntityDetails entityDetails = CommonUtils.GetEntityDetails(fleetName);
                    Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, fleetName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
                    Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, fleetName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
                    Assert.IsTrue(entityDetails.EnablePaymentPortal, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, fleetName, FieldNames.EnablePaymentPortal) + GetErrorMessage(LoggerMesages.ExpectedValue, "True", entityDetails.EnablePaymentPortal.ToString()));
                    Console.WriteLine($"Fleet Created with Code: [{fleetName}]");
                    break;
            }


        }

        [When(@"""([^""]*)"" Location is created with default values and Tax Information")]
        public void WhenLocationIsCreatedWithDefaultValuesAndTaxInformation(string entityLocation)
        {
            CreateNewEntityPage = new CreateNewEntityPage(driver);
            menu = new Menu(driver);
            switch (entityLocation)
            {
                case "Buyer Billing":
                    Task t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                    CreateNewEntityPage.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Fleet));
                    t.Wait();
                    t.Dispose();
                    t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                    CreateNewEntityPage.ClickElement(FieldNames.MasterLocation);
                    t.Wait();
                    t.Dispose();
                    CreateNewEntityPage.InputMandatoryFieldsFleet(entityLocation, corcentricCode);
                    CreateNewEntityPage.SelectValueTableDropDown(FieldNames.TaxIDType);
                    TaxIDValue = CreateNewEntityPage.GetValueOfDropDown(FieldNames.TaxIDType);
                    CreateNewEntityPage.SelectValueTableDropDown(FieldNames.TaxClassification);
                    taxClassificationValue = CreateNewEntityPage.GetValueOfDropDown(FieldNames.TaxClassification);
                    CreateNewEntityPage.ButtonClick(ButtonsAndMessages.Save);
                    break;
            }
        }

        [Then(@"""([^""]*)"" Location is created with Tax Information")]
        public void ThenLocationIsCreatedWithTaxInformation(string entityLocation)
        {
            Assert.AreEqual(TaxIDValue, AccountMaintenanceUtil.GetTaxIDValue(corcentricCode), GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(taxClassificationValue, AccountMaintenanceUtil.GetTaxClassificationIDValue(corcentricCode), GetErrorMessage(ErrorMessages.IncorrectValue));

        }


    }
}

