using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.AccountMaintenance;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using System.Collections.Generic;
using AutomationTesting_CorConnect.DataObjects;
using System.Linq;
using System;
using System.Threading;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Helper;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.AccountMaintenance
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Account Maintenance")]
    internal class AccountMaintenance : DriverBuilderClass
    {
        AccountMaintenancePage Page;
        AccountMaintenanceAspx aspxPage;
        private string masterCode = CommonUtils.GetCorcentricLocation();
        private string termDescription = CommonUtils.RandomString(80);
        private string specialCharacter = "@%*";
        private string negativeInterger = "-1.2133";
        private string UTFCharac = CommonUtils.RandomAlphabets(15) + "ñññjjh¿¿¿" + CommonUtils.RandomString(60);
        private string validInt = "123";

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.AccountMaintenance);
            Page = new AccountMaintenancePage(driver);
            aspxPage = new AccountMaintenanceAspx(driver);

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16378" })]
        public void TC_16378(string UserType)
        {
            string masterFleetCode = AccountMaintenanceUtil.GetFleetCode(LocationType.MasterBilling);
            Page.LoadDataOnGrid(masterFleetCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
                var errorMsgs = aspxPage.VerifyAccountConfigFieldsFleet(LocationType.MasterBilling);
                Assert.Multiple(() =>
                {
                    foreach (string errorMsg in errorMsgs)
                    {
                        Assert.Fail("Master Location: " + errorMsg);
                    }
                });
                Page.ClosePopupWindow();
                string billingFleetCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
                Page.LoadDataOnGrid(billingFleetCode);
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations"));
                errorMsgs = aspxPage.VerifyAccountConfigFieldsFleet(LocationType.Billing);
                Assert.Multiple(() =>
                {
                    foreach (string errorMsg in errorMsgs)
                    {
                        Assert.Fail("Billing Location: " + errorMsg);
                    }
                });
            }

            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16380" })]
        public void TC_16380(string UserType)
        {
            var relDetails = AccountMaintenanceUtil.GetRelationshipDetailsForRelType(FieldNames.CorePriceCheck);
            Page.LoadDataOnGrid(relDetails.AccountMaintSrchCode, relDetails.AccountMaintSrchEntityType.Equals("FleetCode") ? EntityType.Fleet : EntityType.Dealer);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);

                aspxPage.SelectRelationShipType(FieldNames.CorePriceCheck, FieldNames.CorePriceCheckTable, relDetails.EntityCode, 2);
                var transactionTypes = AccountMaintenanceUtil.GetTranscationNames();
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AddNewRelationship), ErrorMessages.AddNewRelationShipMissing);
                    Assert.IsTrue(aspxPage.VerifyValueScrollable("Transaction Type", transactionTypes.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");
                    Assert.IsTrue(aspxPage.VerifyValue("Core Price Check Type", "Check Core Price", "Ignore Cores", "Never Charge Cores"), ErrorMessages.ElementNotPresent + ". Core Price Check Type dropdown.");
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16381" })]
        public void TC_16381(string UserType)
        {
            var relDetails = AccountMaintenanceUtil.GetRelationshipDetailsForRelType(FieldNames.CreditLimitVariance);
            Page.LoadDataOnGrid(relDetails.AccountMaintSrchCode, relDetails.AccountMaintSrchEntityType.Equals("FleetCode") ? EntityType.Fleet : EntityType.Dealer);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AddNewRelationship), ErrorMessages.AddNewRelationShipMissing);
                    aspxPage.SelectRelationShipType(FieldNames.CreditLimitVariance, "Credit Limit Variance Table", relDetails.EntityCode, 2);
                    Assert.IsTrue(aspxPage.IsElementDisplayed("Credit Tolerance Percentage", true), ErrorMessages.ElementNotPresent + ". Credit Tolerance Percentage");
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16383" })]
        public void TC_16383(string UserType)
        {
            var relDetails = AccountMaintenanceUtil.GetRelationshipDetailsForRelType(FieldNames.DataRequirements);
            Page.LoadDataOnGrid(relDetails.AccountMaintSrchCode, relDetails.AccountMaintSrchEntityType.Equals("FleetCode") ? EntityType.Fleet : EntityType.Dealer);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.Name);

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AddNewRelationship), ErrorMessages.AddNewRelationShipMissing);

                    aspxPage.SelectRelationShipType(FieldNames.DataRequirements, FieldNames.RelDataRequirementsTable, relDetails.EntityCode, 2);

                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DataRequirements), ErrorMessages.ElementNotPresent);
                    Assert.IsTrue(aspxPage.VerifyValueScrollable("Applicable to Transaction Type", "All"), ErrorMessages.ElementNotPresent + ". Applicable to Transaction Type dropdown.");
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16382" })]
        public void TC_16382(string UserType)
        {
            RelationshipDetails relDetails = AccountMaintenanceUtil.GetRelationshipDetailsForRelType(FieldNames.CurrencyCode);
            Page.LoadDataOnGrid(relDetails.AccountMaintSrchCode, relDetails.AccountMaintSrchEntityType.Equals("FleetCode") ? EntityType.Fleet : EntityType.Dealer);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AddNewRelationship), ErrorMessages.AddNewRelationShipMissing);

                    aspxPage.SelectRelationShipType(FieldNames.CurrencyCode, FieldNames.RelCurrencyTable, relDetails.EntityCode, 2);

                    Assert.IsTrue(aspxPage.VerifyValueScrollable(FieldNames.Currency, "CAD", "USD"), ErrorMessages.CurrencyCodeMissing);
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.AddButtonMissing);
                    Assert.IsTrue(aspxPage.IsElementDisplayed("Allow multi-currency transaction") ||
                        aspxPage.IsElementDisplayed("Transaction currency must match"), ErrorMessages.CheckBoxMissing);
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16379" })]
        public void TC_16379(string UserType)
        {
            string masterDealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.MasterBilling);
            Page.LoadDataOnGrid(masterDealerCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations"));
                var errorMsgs = aspxPage.VerifyAccountConfigFieldsDealer(LocationType.MasterBilling);
                Assert.Multiple(() =>
                {
                    foreach (string errorMsg in errorMsgs)
                    {
                        Assert.Fail("Master Location: " + errorMsg);
                    }
                });
                Page.ClosePopupWindow();
                string billingDealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
                Page.LoadDataOnGrid(billingDealerCode);
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations"));
                errorMsgs = aspxPage.VerifyAccountConfigFieldsDealer(LocationType.Billing);
                Assert.Multiple(() =>
                {
                    foreach (string errorMsg in errorMsgs)
                    {
                        Assert.Fail("Billing Location: " + errorMsg);
                    }
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20090" })]
        public void TC_20090(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.SelectRelationShipType(FieldNames.CreditPriceAudit);
            aspxPage.SelectTransActionType("All");
            aspxPage.SelectPriceAuditCreditTransactions("Yes");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var DataFromDB = AccountMaintenanceUtil.GetTransactionDetails();
            aspxPage.ClickButtonOnGrid("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.New);
            aspxPage.SelectValueByScroll(FieldNames.TransactionType, "All", ButtonsAndMessages.Edit);
            aspxPage.SelectValueByScroll(FieldNames.PriceAuditCreditTransactions, "Yes", ButtonsAndMessages.Edit);
            aspxPage.InsertEditGrid();
            Assert.IsTrue(aspxPage.CheckForText("Cannot insert duplicate transaction type, relationship already exists", true));
            aspxPage.DeleteFirstRelationShip();

            Assert.AreEqual(DataFromDB.TransactionTypeId, AccountMaintenanceUtil.GetLookupID("All"));
            Assert.AreEqual(DataFromDB.ApplicableTo, 0);
            Assert.AreEqual(DataFromDB.IsActive, true);
            Assert.AreEqual(DataFromDB.EnableGMCCalculation, false);
            Assert.AreEqual(DataFromDB.EnableRebatesCalculation, false);
        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20088" })]
        public void TC_20088(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {

            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.SelectRelationShipType(FieldNames.CreditPriceAudit);
            aspxPage.SelectCreditPriceAudit();
            aspxPage.ClickButtonOnGrid("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit);

            Assert.Multiple(() =>
            {
                Assert.AreEqual("All", aspxPage.GetValueOfDropDown(FieldNames.TransactionType, ButtonsAndMessages.Edit), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.TransactionType));
                Assert.AreEqual("Yes", aspxPage.GetValueOfDropDown(FieldNames.PriceAuditCreditTransactions, ButtonsAndMessages.Edit), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PriceAuditCreditTransactions));
                Assert.AreEqual(aspxPage.VerifyDropDownIsDisabled(FieldNames.ApplicableTo, ButtonsAndMessages.Edit), "true", GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.ApplicableTo));
                Assert.IsTrue(aspxPage.IsCheckBoxDisabled(FieldNames.CalculateGMC_AdditionalGMC, ButtonsAndMessages.Edit), GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.ApplicableTo));
                Assert.IsTrue(aspxPage.IsCheckBoxDisabled(FieldNames.CalculateRebates, ButtonsAndMessages.Edit), GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.ApplicableTo));
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20087" })]
        public void TC_20087(string UserType)
        {
            RelationshipDetails relDetails = AccountMaintenanceUtil.GetRelationshipDetailsForRelType(FieldNames.CreditPriceAudit);
            Page.LoadDataOnGrid(relDetails.AccountMaintSrchCode, relDetails.AccountMaintSrchEntityType.Equals("FleetCode") ? EntityType.Fleet : EntityType.Dealer);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.Name);
                aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
                aspxPage.SelectRelationShipType(FieldNames.CreditPriceAudit);
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(aspxPage.GetValueOfDropDown(FieldNames.TransactionType), "All");
                    Assert.AreEqual(aspxPage.GetValueOfDropDown(FieldNames.PriceAuditCreditTransactions), "Yes");
                });
                var transactionTypes = AccountMaintenanceUtil.GetTranscationNames();
                Assert.IsTrue(aspxPage.VerifyValue(FieldNames.TransactionType, transactionTypes.ToArray()), GetErrorMessage(ErrorMessages.ListElementsMissing));
                Assert.IsTrue(aspxPage.VerifyValue(FieldNames.PriceAuditCreditTransactions, "Yes", "No"), GetErrorMessage(ErrorMessages.ListElementsMissing));
                aspxPage.SelectValueTableDropDown(FieldNames.PriceAuditCreditTransactions, "No");
                Assert.IsTrue(aspxPage.VerifyValue(FieldNames.ApplicableTo, "Line Level", "Transaction Level"), GetErrorMessage(ErrorMessages.ListElementsMissing));
                aspxPage.ValidateDropDown(FieldNames.ApplicableTo);
                aspxPage.IsCheckBoxDisplayed(FieldNames.CalculateGMCAdditionalGMC);
                aspxPage.IsCheckBoxDisplayed(FieldNames.CalculateRebates);
            }

            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20091" })]
        public void TC_20091(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.SelectRelationShipType(FieldNames.CreditPriceAudit);
            aspxPage.SelectTransActionType("Parts");
            aspxPage.SelectPriceAuditCreditTransactions("Yes");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var DataFromDB = AccountMaintenanceUtil.GetTransactionDetails();
            aspxPage.ClickButtonOnGrid("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.New);
            aspxPage.SelectValueByScroll(FieldNames.TransactionType, "Parts", ButtonsAndMessages.Edit);
            aspxPage.SelectValueByScroll(FieldNames.PriceAuditCreditTransactions, "Yes", ButtonsAndMessages.Edit);
            aspxPage.InsertEditGrid();
            Assert.IsTrue(aspxPage.CheckForText("Cannot insert duplicate transaction type, relationship already exists", true));
            aspxPage.DeleteFirstRelationShip();

            Assert.AreEqual(DataFromDB.TransactionTypeId, AccountMaintenanceUtil.GetLookupID("Parts"));
            Assert.AreEqual(DataFromDB.ApplicableTo, 0);
            Assert.AreEqual(DataFromDB.IsActive, true);
            Assert.AreEqual(DataFromDB.EnableGMCCalculation, false);
            Assert.AreEqual(DataFromDB.EnableRebatesCalculation, false);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20092" })]
        public void TC_20092(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.SelectRelationShipType(FieldNames.CreditPriceAudit);
            aspxPage.SelectTransActionType("All");
            aspxPage.SelectPriceAuditCreditTransactions("Yes");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var DataFromDB = AccountMaintenanceUtil.GetTransactionDetails();
            aspxPage.ClickButtonOnGrid("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.New);
            aspxPage.SelectValueByScroll(FieldNames.TransactionType, "Fuel", ButtonsAndMessages.Edit);
            aspxPage.SelectValueByScroll(FieldNames.PriceAuditCreditTransactions, "Yes", ButtonsAndMessages.Edit);
            aspxPage.InsertEditGrid();
            Assert.IsTrue(aspxPage.CheckForText("Cannot insert new record, relationship already exists with “All” transaction type", true));
            aspxPage.DeleteFirstRelationShip();

            Assert.AreEqual(DataFromDB.TransactionTypeId, AccountMaintenanceUtil.GetLookupID("All"));
            Assert.AreEqual(DataFromDB.ApplicableTo, 0);
            Assert.AreEqual(DataFromDB.IsActive, true);
            Assert.AreEqual(DataFromDB.EnableGMCCalculation, false);
            Assert.AreEqual(DataFromDB.EnableRebatesCalculation, false);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20089" })]
        public void TC_20089(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.SelectRelationShipType(FieldNames.CreditPriceAudit);
            aspxPage.SelectTransActionType("All");
            aspxPage.SelectPriceAuditCreditTransactions("Yes");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var DataFromDB = AccountMaintenanceUtil.GetTransactionDetails();
            Assert.AreEqual(DataFromDB.TransactionTypeId, AccountMaintenanceUtil.GetLookupID("All"));
            Assert.AreEqual(DataFromDB.ApplicableTo, 0);
            Assert.AreEqual(DataFromDB.IsActive, true);
            Assert.AreEqual(DataFromDB.EnableGMCCalculation, false);
            Assert.AreEqual(DataFromDB.EnableRebatesCalculation, false);

            aspxPage.ClickButtonOnGrid("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit);
            aspxPage.SelectTransActionType("All", ButtonsAndMessages.Edit);
            aspxPage.SelectPriceAuditCreditTransactions("No", ButtonsAndMessages.Edit);
            aspxPage.SelectApplicableTo("Transaction Level", ButtonsAndMessages.Edit);
            aspxPage.Click(FieldNames.CalculateGMCAdditionalGMC, ButtonsAndMessages.Edit);
            aspxPage.Click(FieldNames.CalculateRebates, ButtonsAndMessages.Edit);
            aspxPage.UpdateEditGrid();
            DataFromDB = AccountMaintenanceUtil.GetTransactionDetails();
            Assert.AreEqual(DataFromDB.TransactionTypeId, AccountMaintenanceUtil.GetLookupID("All"));
            Assert.AreEqual(DataFromDB.ApplicableTo, AccountMaintenanceUtil.GetApplicableToID(353, "Transaction Level"));
            Assert.AreEqual(DataFromDB.IsActive, true);
            Assert.AreEqual(DataFromDB.EnableGMCCalculation, true);
            Assert.AreEqual(DataFromDB.EnableRebatesCalculation, true);

            aspxPage.SelectTransActionType("All", ButtonsAndMessages.Edit);
            aspxPage.SelectPriceAuditCreditTransactions("No", ButtonsAndMessages.Edit);
            aspxPage.SelectApplicableTo("Transaction Level", ButtonsAndMessages.Edit);
            aspxPage.Click(FieldNames.CalculateGMCAdditionalGMC, ButtonsAndMessages.Edit);
            aspxPage.Click(FieldNames.CalculateRebates, ButtonsAndMessages.Edit);
            aspxPage.UpdateEditGrid(false);
            aspxPage.WaitForLoadingMessage();
            DataFromDB = AccountMaintenanceUtil.GetTransactionDetails();
            Assert.AreEqual(DataFromDB.TransactionTypeId, AccountMaintenanceUtil.GetLookupID("All"));
            Assert.AreEqual(DataFromDB.ApplicableTo, AccountMaintenanceUtil.GetApplicableToID(353, "Transaction Level"));
            Assert.AreEqual(DataFromDB.IsActive, true);
            Assert.AreEqual(DataFromDB.EnableGMCCalculation, false);
            Assert.AreEqual(DataFromDB.EnableRebatesCalculation, false);

            aspxPage.SelectTransActionType("All", ButtonsAndMessages.Edit);
            aspxPage.SelectPriceAuditCreditTransactions("No", ButtonsAndMessages.Edit);
            aspxPage.SelectApplicableTo("Line Level", ButtonsAndMessages.Edit);
            aspxPage.UpdateEditGrid(false);
            aspxPage.WaitForLoadingMessage();
            DataFromDB = AccountMaintenanceUtil.GetTransactionDetails();
            Assert.AreEqual(DataFromDB.TransactionTypeId, AccountMaintenanceUtil.GetLookupID("All"));
            Assert.AreEqual(DataFromDB.ApplicableTo, AccountMaintenanceUtil.GetApplicableToID(353, "Line Level"));
            Assert.AreEqual(DataFromDB.IsActive, true);
            Assert.AreEqual(DataFromDB.EnableGMCCalculation, false);
            Assert.AreEqual(DataFromDB.EnableRebatesCalculation, false);
            aspxPage.DeleteFirstRelationShip();

        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20206" })]
        public void TC_20206(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.Name);
                aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, null);
                Assert.Multiple(() =>
                {
                    string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                    Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                        string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                    message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                    Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                        string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                });
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20228" })]
        public void TC_20228(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.Name);
                aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, null);
                Assert.Multiple(() =>
                {
                    string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                    Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                        string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                    message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                    Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                        string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                    aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                    message = aspxPage.GetText(FieldNames.ResellerRequiredFieldErrorMessage);
                    Assert.AreEqual(ButtonsAndMessages.ResellerIsRequired, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerIsRequired, message));
                });
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20229" })]
        public void TC_20229(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, null);
            Assert.Multiple(() =>
            {
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                aspxPage.SelectValueFirstRow(FieldNames.ResellerInvoiceRequired);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.ResellerSameFleetError);
                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
                Assert.AreEqual(ButtonsAndMessages.ResellerSameFleetError, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerSameFleetError, message));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20230" })]
        public void TC_20230(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetBillingLocation, string FleetShopLocation)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, FleetShopLocation, 2);
            Assert.Multiple(() =>
            {
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                aspxPage.SelectValueMultipleColumns(FieldNames.ResellerInvoiceRequired, FleetBillingLocation);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.ResellerSameLocationError);
                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
                Assert.AreEqual(ButtonsAndMessages.ResellerSameLocationError, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerSameLocationError, message));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20095" })]
        public void TC_20095(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            var CorcentricCodeSplit = CorcentricCode.Split(",");
            var LocationTypeSplit = LocationType.Split(",");
            string LocTypeBilling = LocationTypeSplit[0];
            string LocTypeShop = LocationTypeSplit[1];
            string CodeBilling = CorcentricCodeSplit[0];
            string CodeShop = CorcentricCodeSplit[1];
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocTypeBilling, CodeBilling);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.SelectRelationShipType(FieldNames.CreditPriceAudit);
            aspxPage.SelectTransActionType("Parts");
            aspxPage.SelectPriceAuditCreditTransactions("Yes");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var DataFromDB = AccountMaintenanceUtil.GetTransactionDetails();
            Assert.AreEqual(DataFromDB.TransactionTypeId, AccountMaintenanceUtil.GetLookupID("Parts"));
            Assert.AreEqual(DataFromDB.ApplicableTo, 0);
            Assert.AreEqual(DataFromDB.IsActive, true);
            Assert.AreEqual(DataFromDB.EnableGMCCalculation, false);
            Assert.AreEqual(DataFromDB.EnableRebatesCalculation, false);
            aspxPage.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClearText(FieldNames.AccountCode);
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocTypeShop, CodeShop);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CreditPriceAudit);
            aspxPage.SelectTransActionType("Parts");
            aspxPage.SelectPriceAuditCreditTransactions("Yes");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            aspxPage.DeleteFirstRelationShip();
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20231" })]
        public void TC_20231(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, FleetCode, 2);
            Assert.Multiple(() =>
            {
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                aspxPage.SelectValueFirstRow(FieldNames.ResellerInvoiceRequired);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.RecordAddedSuccessfully);
                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
                Assert.AreEqual(ButtonsAndMessages.RecordAddedSuccessfully, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, message));
            });
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20248" })]
        public void TC_20248(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, FleetCode, 2);
            Assert.Multiple(() =>
            {
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                aspxPage.SelectValueFirstRow(FieldNames.ResellerInvoiceRequired);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.RecordAddedSuccessfully);
                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
                Assert.AreEqual(ButtonsAndMessages.RecordAddedSuccessfully, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, message));

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
                aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
                Assert.AreEqual(aspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteAlertMessage);
                aspxPage.AcceptAlert(ButtonsAndMessages.DeleteAlertMessage);
                aspxPage.WaitForLoadingMessage();
                Assert.IsFalse(AccountMaintenanceUtil.GetRelSenderReceiverByDealerCode(corcentricCode, FieldNames.ResellerInvoiceRequired).IsActive, ErrorMessages.DeleteOperationFailed);

                aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            });

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20268" })]
        public void TC_20268(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, FleetCode, 2);

            Assert.Multiple(() =>
            {
                aspxPage.SelectValueFirstRow(FieldNames.ResellerInvoiceRequired);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.RecordAddedSuccessfully);
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
                Assert.AreEqual(ButtonsAndMessages.RecordAddedSuccessfully, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, message));

                Page.ClosePopupWindow();
                Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
                Page.ClickHyperLinkOnGrid(TableHeaders.Name);

                Assert.Catch<OpenQA.Selenium.WebDriverException>(() =>
                {
                    aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, FleetCode, 2);
                }, ErrorMessages.DuplicateRelAllowed);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
                aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
                Assert.AreEqual(aspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteAlertMessage);
                aspxPage.AcceptAlert(ButtonsAndMessages.DeleteAlertMessage);
                aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20247" })]
        public void TC_20247(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            string fleetCode1, fleetCode2;
            fleetCode1 = FleetCode.Split(',')[0];
            fleetCode2 = FleetCode.Split(',')[1];
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.AddResellerRelationship(fleetCode1);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.SearchAndSelectValueAfterOpen(FieldNames.EditResellerId, fleetCode1);
            aspxPage.UpdateEditGrid();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(ButtonsAndMessages.ResellerSameLocationError, aspxPage.GetEditMsg());
                aspxPage.WaitForLoadingMessage();
                aspxPage.SearchAndSelectValueAfterOpen(FieldNames.EditResellerId, fleetCode2);
                aspxPage.UpdateEditGrid();
                Assert.IsTrue(aspxPage.WaitForEditMsgChangeText().Contains(ButtonsAndMessages.RecordUpdatedMessage),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordUpdatedMessage, aspxPage.GetEditMsg()));
                aspxPage.DeleteFirstRelationShip();
                aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20093" })]
        public void TC_20093(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            var CorcentricCodeSplit = CorcentricCode.Split(",");
            var LocationTypeSplit = LocationType.Split(",");
            string LocTypeBilling = LocationTypeSplit[0];
            string LocTypeShop = LocationTypeSplit[1];
            string CodeBilling = CorcentricCodeSplit[0];
            string CodeShop = CorcentricCodeSplit[1];
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocTypeBilling, CodeBilling);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            aspxPage.SelectRelationShipType(FieldNames.CreditPriceAudit);
            aspxPage.SelectTransActionTypeByTableDropDown("All");
            aspxPage.SelectPriceAuditCreditTransactions("Yes");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var DataFromDB = AccountMaintenanceUtil.GetTransactionDetails();
            Assert.AreEqual(DataFromDB.TransactionTypeId, AccountMaintenanceUtil.GetLookupID("All"));
            Assert.AreEqual(DataFromDB.ApplicableTo, 0);
            Assert.AreEqual(DataFromDB.IsActive, true);
            Assert.AreEqual(DataFromDB.EnableGMCCalculation, false);
            Assert.AreEqual(DataFromDB.EnableRebatesCalculation, false);
            aspxPage.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClearText(FieldNames.AccountCode);
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocTypeShop, CodeShop);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            Assert.IsTrue(aspxPage.CheckFirstRelationShipIsDisabled());
            aspxPage.ClickButtonOnGrid("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Credit Price Audit");
            Assert.IsTrue(aspxPage.IsCommandsButtonsNotVisible());

        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20250" })]
        public void TC_20250(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode, string DealerMasterLocation, string DealerBillingLocation)
        {
            string fleetCode1, fleetCode2;
            List<string> errorMsgs = new List<string>();
            fleetCode1 = FleetCode.Split(',')[0];
            fleetCode2 = FleetCode.Split(',')[1];

            // Adding new Reseller Invoice Required relationship for parent Dealer
            Page.LoadDataOnGrid(DealerMasterLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.AddResellerRelationship(fleetCode1);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            string resellerFleet = aspxPage.GetFirstValueFromGrid(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Reseller);
            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(DealerBillingLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            string resellerFleetChild = aspxPage.GetFirstValueFromGrid(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Reseller);
            if (resellerFleet != resellerFleetChild)
            {
                errorMsgs.Add(string.Format(ErrorMessages.DataMisMatch, $"Parent Reseller relatioship val [{resellerFleet}], Child Reseller relatioship val [{resellerFleetChild}]"));
            }

            // Updating Reseller Invoice Required relationship value for parent Dealer
            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(DealerMasterLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.SelectValueMultipleColumns(FieldNames.EditResellerId, fleetCode2);
            aspxPage.UpdateEditGrid();
            aspxPage.CloseEditGrid();
            resellerFleet = aspxPage.GetFirstValueFromGrid(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Reseller);

            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(DealerBillingLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            resellerFleetChild = aspxPage.GetFirstValueFromGrid(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Reseller);
            if (resellerFleet != resellerFleetChild)
            {
                errorMsgs.Add(string.Format(ErrorMessages.DataMisMatch, $"After updating Parent Reseller relatioship val to [{resellerFleet}], Child Reseller relatioship val [{resellerFleetChild}]"));
            }

            // Deleting parent Reseller Invoice Required relationship
            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(DealerMasterLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetCode1);
            aspxPage.DeleteFirstRelationShip();

            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(DealerBillingLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetCode1);
            int rowCount = aspxPage.GetRowCount(FieldNames.RelationShipTable, true);
            if (rowCount > 0)
            {
                errorMsgs.Add(ErrorMessages.GridRowCountMisMatch + $". After deleting parent Reseller Invoice Required relationship expected row count is [0] actual row count [{rowCount}]");
            }

            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20269" })]
        public void TC_20269(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            List<string> errorMsgs = new List<string>();
            string fleetCode1 = FleetCode.Split(',')[0];
            string fleetCode2 = FleetCode.Split(',')[1];
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            List<AuditTrailTable> auditTrailOldList = AccountMaintenanceUtil.GetAuditTrails();
            aspxPage.AddResellerRelationship(fleetCode1);
            List<AuditTrailTable> auditTrailNewList = AccountMaintenanceUtil.GetAuditTrails();

            var tempList = auditTrailNewList.Where(x => x.AuditId > auditTrailOldList.First().AuditId).ToList();
            if (tempList.Where(x => x.AuditAction == "I" && x.AuditStatement == ButtonsAndMessages.RelationshipInsertedStatement).Count() == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.AuditTrailNotEntryNotFound, "Insert Relationship"));
            }

            auditTrailOldList = auditTrailNewList;
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, fleetCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.SelectValueMultipleColumns(FieldNames.EditResellerId, fleetCode2);
            aspxPage.UpdateEditGrid();
            if (!aspxPage.GetEditMsg().Contains(ButtonsAndMessages.RecordUpdatedMessage))
            {
                errorMsgs.Add(string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordUpdatedMessage, aspxPage.GetEditMsg()));
            }

            auditTrailNewList = AccountMaintenanceUtil.GetAuditTrails();
            tempList = auditTrailNewList.Where(x => x.AuditId > auditTrailOldList.First().AuditId).ToList();
            if (tempList.Where(x => x.AuditAction == "U" && x.AuditStatement == ButtonsAndMessages.RelationshipUpdatedStatement).Count() == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.AuditTrailNotEntryNotFound, "Update Relationship"));
            }

            auditTrailOldList = auditTrailNewList;
            aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
            aspxPage.AcceptAlert(out string alertMsg);
            System.Threading.Thread.Sleep(3000);
            auditTrailNewList = AccountMaintenanceUtil.GetAuditTrails();
            tempList = auditTrailNewList.Where(x => x.AuditId > auditTrailOldList.First().AuditId).ToList();
            if (tempList.Where(x => x.AuditAction == "D" && x.AuditStatement == ButtonsAndMessages.RelationshipDeletedStatement).Count() == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.AuditTrailNotEntryNotFound, "Delete Relationship"));
            }

            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20205" })]
        public void TC_20205(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.Name);
                aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, null);
                Assert.Multiple(() =>
                {
                    string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                    Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                        string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                    message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                    Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                        string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                });
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20241" })]
        public void TC_20241(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, null);
            Assert.Multiple(() =>
            {
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                message = aspxPage.GetText(FieldNames.ResellerRequiredFieldErrorMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerIsRequired, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerIsRequired, message));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20242" })]
        public void TC_20242(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, null);
            Assert.Multiple(() =>
            {
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                aspxPage.SelectValueFirstRow(FieldNames.ResellerInvoiceRequired);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.ResellerSameDealerError);
                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
                Assert.AreEqual(ButtonsAndMessages.ResellerSameDealerError, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerSameDealerError, message));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20243" })]
        public void TC_20243(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerMasterLocation, string DealerBillingLocation)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, DealerMasterLocation, 2);
            Assert.Multiple(() =>
            {
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                aspxPage.SelectValueMultipleColumns(FieldNames.ResellerInvoiceRequired, DealerBillingLocation);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.ResellerSameLocationError);
                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
                Assert.AreEqual(ButtonsAndMessages.ResellerSameLocationError, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerSameLocationError, message));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20244" })]
        public void TC_20244(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, DealerCode, 2);
            Assert.Multiple(() =>
            {
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                aspxPage.SelectValueFirstRow(FieldNames.ResellerInvoiceRequired);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.RecordAddedSuccessfully);
                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
                Assert.AreEqual(ButtonsAndMessages.RecordAddedSuccessfully, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, message));
            });
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
            aspxPage.DeleteFirstRelationShip();
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20246" })]
        public void TC_20246(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            string dealerCode1 = DealerCode.Split(',')[0];
            string dealerCode2 = DealerCode.Split(',')[1];
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.AddResellerRelationship(dealerCode1);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.SelectValueMultipleColumns(FieldNames.EditResellerId, dealerCode1);
            aspxPage.UpdateEditGrid();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(ButtonsAndMessages.ResellerSameLocationError, aspxPage.GetEditMsg());
                aspxPage.WaitForLoadingMessage();
                aspxPage.SelectValueMultipleColumns(FieldNames.EditResellerId, dealerCode2);
                aspxPage.UpdateEditGrid();
                Assert.IsTrue(aspxPage.WaitForEditMsgChangeText().Contains(ButtonsAndMessages.RecordUpdatedMessage),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordUpdatedMessage, aspxPage.GetEditMsg()));
                aspxPage.DeleteFirstRelationShip();
                aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20249" })]
        public void TC_20249(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, DealerCode, 2);
            Assert.Multiple(() =>
            {
                string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessageConstant);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessageConstant, message));

                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredMessage);
                Assert.AreEqual(ButtonsAndMessages.ResellerInvoiceRequiredMessage.ToLower(), message.ToLower(),
                    string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.ResellerInvoiceRequiredMessage, message));

                aspxPage.SelectValueFirstRow(FieldNames.ResellerInvoiceRequired);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.RecordAddedSuccessfully);
                message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
                Assert.AreEqual(ButtonsAndMessages.RecordAddedSuccessfully, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, message));

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
                aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
                Assert.AreEqual(aspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteAlertMessage);
                aspxPage.AcceptAlert(ButtonsAndMessages.DeleteAlertMessage);
                aspxPage.WaitForLoadingMessage();
                Assert.IsFalse(AccountMaintenanceUtil.GetRelSenderReceiverByFleetCode(corcentricCode, FieldNames.ResellerInvoiceRequired).IsActive, ErrorMessages.DeleteOperationFailed);

                aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21636" })]
        public void TC_21636(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode, string FleetMasterLocation, string FleetBillingLocation)
        {
            List<string> errorMsgs = new List<string>();
            string dealerCode1 = DealerCode.Split(',')[0];
            string dealerCode2 = DealerCode.Split(',')[1];

            // Adding new Reseller Invoice Required relationship for parent Dealer
            Page.LoadDataOnGrid(FleetMasterLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.AddResellerRelationship(dealerCode1);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            string resellerFleet = aspxPage.GetFirstValueFromGrid(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Reseller);
            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(FleetBillingLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            string resellerFleetChild = aspxPage.GetFirstValueFromGrid(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Reseller);
            if (resellerFleet != resellerFleetChild)
            {
                errorMsgs.Add(string.Format(ErrorMessages.DataMisMatch, $"Parent Reseller relatioship val [{resellerFleet}], Child Reseller relatioship val [{resellerFleetChild}]"));
            }

            // Updating Reseller Invoice Required relationship value for parent Dealer
            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(FleetMasterLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.SelectValueMultipleColumns(FieldNames.EditResellerId, dealerCode2);
            aspxPage.UpdateEditGrid();
            aspxPage.CloseEditGrid();
            resellerFleet = aspxPage.GetFirstValueFromGrid(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Reseller);

            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(FleetBillingLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            resellerFleetChild = aspxPage.GetFirstValueFromGrid(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Reseller);
            if (resellerFleet != resellerFleetChild)
            {
                errorMsgs.Add(string.Format(ErrorMessages.DataMisMatch, $"After updating Parent Reseller relatioship val to [{resellerFleet}], Child Reseller relatioship val [{resellerFleetChild}]"));
            }

            // Deleting parent Reseller Invoice Required relationship
            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(FleetMasterLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerCode1);
            aspxPage.DeleteFirstRelationShip();

            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(FleetBillingLocation, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerCode1);
            int rowCount = aspxPage.GetRowCount(FieldNames.RelationShipTable, true);
            if (rowCount > 0)
            {
                errorMsgs.Add(ErrorMessages.GridRowCountMisMatch + $". After deleting parent Reseller Invoice Required relationship expected row count is [0] actual row count [{rowCount}]");
            }

            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21638" })]
        public void TC_21638(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, DealerCode, 2);

            //Assert.Multiple(() =>
            //{
            aspxPage.SelectValueFirstRow(FieldNames.ResellerInvoiceRequired);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            aspxPage.WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.RecordAddedSuccessfully);
            string message = aspxPage.GetText(FieldNames.ResellerInvoiceRequiredError);
            Assert.AreEqual(ButtonsAndMessages.RecordAddedSuccessfully, message, string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, message));

            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);

            Assert.Catch<OpenQA.Selenium.WebDriverException>(() =>
            {
                aspxPage.SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, DealerCode, 2);
            }, ErrorMessages.DuplicateRelAllowed);

            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
            aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
            Assert.AreEqual(aspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteAlertMessage);
            aspxPage.AcceptAlert(ButtonsAndMessages.DeleteAlertMessage);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            // });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21639" })]
        public void TC_21639(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            List<string> errorMsgs = new List<string>();
            string dealerCode1 = DealerCode.Split(',')[0];
            string dealerCode2 = DealerCode.Split(',')[1];
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            List<AuditTrailTable> auditTrailOldList = AccountMaintenanceUtil.GetAuditTrails();
            aspxPage.AddResellerRelationship(dealerCode1);
            List<AuditTrailTable> auditTrailNewList = AccountMaintenanceUtil.GetAuditTrails();

            var tempList = auditTrailNewList.Where(x => x.AuditId > auditTrailOldList.First().AuditId).ToList();
            if (tempList.Where(x => x.AuditAction == "I" && x.AuditStatement == ButtonsAndMessages.RelationshipInsertedStatement).Count() == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.AuditTrailNotEntryNotFound, "Insert Relationship"));
            }

            auditTrailOldList = auditTrailNewList;
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, dealerCode1);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ResellerInvoiceRequired);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.SelectValueMultipleColumns(FieldNames.EditResellerId, dealerCode2);
            aspxPage.UpdateEditGrid();
            if (!aspxPage.GetEditMsg().Contains(ButtonsAndMessages.RecordUpdatedMessage))
            {
                errorMsgs.Add(string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordUpdatedMessage, aspxPage.GetEditMsg()));
            }

            auditTrailNewList = AccountMaintenanceUtil.GetAuditTrails();
            tempList = auditTrailNewList.Where(x => x.AuditId > auditTrailOldList.First().AuditId).ToList();
            if (tempList.Where(x => x.AuditAction == "U" && x.AuditStatement == ButtonsAndMessages.RelationshipUpdatedStatement).Count() == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.AuditTrailNotEntryNotFound, "Update Relationship"));
            }

            auditTrailOldList = auditTrailNewList;
            aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
            aspxPage.AcceptAlert(out string alertMsg);
            Thread.Sleep(5000);
            auditTrailNewList = AccountMaintenanceUtil.GetAuditTrails();
            tempList = auditTrailNewList.Where(x => x.AuditId > auditTrailOldList.First().AuditId).ToList();
            if (tempList.Where(x => x.AuditAction == "D" && x.AuditStatement == ButtonsAndMessages.RelationshipDeletedStatement).Count() == 0)
            {
                errorMsgs.Add(string.Format(ErrorMessages.AuditTrailNotEntryNotFound, "Delete Relationship"));
            }

            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }
            aspxPage.DeleteResellerRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22218" })]
        public void TC_22218(string UserType)
        {
            Page.OpenDropDown(FieldNames.LocationType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.OpenDropDown(FieldNames.LocationType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.SelectValueFirstRow(FieldNames.LocationType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.LocationType, "Billing");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));

            Page.OpenDropDown(FieldNames.EntityType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.EntityType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.EntityType));
            Page.OpenDropDown(FieldNames.EntityType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.EntityType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.EntityType));
            Page.SelectValueFirstRow(FieldNames.EntityType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.EntityType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.EntityType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.EntityType, Page.RenameMenuField("Dealer"));
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.EntityType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.EntityType));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15924" })]
        public void TC_15924(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", DealerCode);
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.PricingType, "Centralized", "Combined", "Decentralized"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.PrecendenceOrder, "N/A"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.AccountingDocumentType, "All"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            aspxPage.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", DealerCode);
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.PricingType, "Centralized", "Combined", "Decentralized"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.PrecendenceOrder, "N/A"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.AccountingDocumentType, "All", "AR Only"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            var rowCountFromTablePricingType = AccountMaintenanceUtil.GetRowCountFromTable("relPricingType_tb");
            aspxPage.SelectPricingType("Centralized");
            aspxPage.SelectPrecendenceOrder("N/A");
            aspxPage.SelectAccountingDocType("All");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var rowCountFromTablePricingTypeAfter = AccountMaintenanceUtil.GetRowCountFromTable("relPricingType_tb");
            Assert.AreNotEqual(rowCountFromTablePricingType, rowCountFromTablePricingTypeAfter);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15896" })]
        public void TC_15896(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", FleetCode);
            var transactionTypes = AccountMaintenanceUtil.GetTranscationNames();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AddNewRelationship), ErrorMessages.AddNewRelationShipMissing);
                Assert.IsTrue(aspxPage.VerifyValueScrollable("Transaction Type", transactionTypes.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Core Price Adjustment Type", "Audit Down Only", "Audit Up and Down"), ErrorMessages.ElementNotPresent + ". Core Price Adjustment Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Accounting Document Type", new string[0]), "Accounting Document Type dropdown not empty.");
            });
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorCellVisibleWithMessage("Accounting Document Type is required"));

            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            Page.ClosePopupWindow();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", FleetCode);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CorePriceAdjustment);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, corcentricCode);
            Assert.AreEqual(1, aspxPage.GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);

            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            Page.ClosePopupWindow();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", FleetCode);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorCellVisibleWithMessage("Accounting Document Type is required"));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15926" })]
        public void TC_15926(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {

            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", DealerCode);
            aspxPage.SelectPricingType("Centralized");
            aspxPage.SelectPrecendenceOrder("N/A");
            aspxPage.SelectAccountingDocType("All");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var senderReceiverRelId = AccountMaintenanceUtil.GetPricingTypeDetails();
            Assert.IsTrue(AccountMaintenanceUtil.GetIsActiveValue(senderReceiverRelId.SenderReceiverRelId));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PricingType);
            aspxPage.DeleteFirstRelationShip();
            Assert.IsFalse(AccountMaintenanceUtil.GetIsActiveValue(senderReceiverRelId.SenderReceiverRelId));
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15895" })]
        public void TC_15895(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", FleetCode);
            var transactionTypes = AccountMaintenanceUtil.GetTranscationNames();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AddNewRelationship), ErrorMessages.AddNewRelationShipMissing);
                Assert.IsTrue(aspxPage.VerifyValueScrollable("Transaction Type", transactionTypes.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Core Price Adjustment Type", "Audit Down Only", "Audit Up and Down"), ErrorMessages.ElementNotPresent + ". Core Price Adjustment Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Accounting Document Type", new string[0]), "Accounting Document Type dropdown not empty.");
            });

            Page.ClosePopupWindow();
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", FleetCode);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("All", aspxPage.GetValueOfDropDown("Transaction Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Transaction Type dropdown"));
                Assert.AreEqual("Audit Down Only", aspxPage.GetValueOfDropDown("Core Price Adjustment Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Core Price Adjustment Type dropdown"));
                Assert.AreEqual("AP Only", aspxPage.GetValueOfDropDown("Accounting Document Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Accounting Document Type dropdown"));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15897" })]
        public void TC_15897(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", FleetCode);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorCellVisibleWithMessage("Accounting Document Type is required"));

            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            Page.ClosePopupWindow();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", FleetCode);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CorePriceAdjustment);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CorePriceAdjustment);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.WaitForLoadingMessage();
            var transactionTypes = AccountMaintenanceUtil.GetTranscationNames();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(aspxPage.VerifyValueScrollable("Edit_Transaction Type", transactionTypes.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Edit_Core Price Adjustment Type", "Audit Down Only", "Audit Up and Down"), ErrorMessages.ElementNotPresent + ". Core Price Adjustment Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Edit_Accounting Document Type", "AP Only"), ErrorMessages.ElementNotPresent + ". Accounting Document Type dropdown.");
            });
            Thread.Sleep(200);
            aspxPage.SelectValueRowByIndex(FieldNames.EditTransactionType, 2);
            aspxPage.UpdateEditGrid();
            string updateMsg = aspxPage.GetEditMsg();
            Assert.IsTrue(updateMsg.Contains(ButtonsAndMessages.RecordUpdatedMessage), string.Format(ErrorMessages.UpdateOperationFailed, updateMsg));
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15898" })]
        public void TC_15898(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", FleetCode);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));

            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CorePriceAdjustment);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
            aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
            Assert.AreEqual(aspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteAlertMessage);
            aspxPage.AcceptAlert(ButtonsAndMessages.DeleteAlertMessage);
            aspxPage.WaitforStalenessofRelationShipTable();
            Assert.IsFalse(AccountMaintenanceUtil.GetRelSenderReceiverByDealerCode(corcentricCode, FieldNames.CorePriceAdjustment).IsActive, ErrorMessages.DeleteOperationFailed);

            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15925" })]
        public void TC_15925(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", DealerCode);
            aspxPage.SelectPricingType("Centralized");
            aspxPage.SelectPrecendenceOrder("N/A");
            aspxPage.SelectAccountingDocType("All");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PricingType);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PricingType);
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit);
            Thread.Sleep(3000);
            Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.PricingType, ButtonsAndMessages.Edit, "Centralized", "Combined", "Decentralized"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.PrecendenceOrder, ButtonsAndMessages.Edit, "N/A"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.AccountingDocumentType, ButtonsAndMessages.Edit, "All"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            aspxPage.SelectPricingType("Decentralized", ButtonsAndMessages.Edit);
            aspxPage.WaitForMsg(" processing...");
            aspxPage.UpdateEditGrid();
            Assert.AreEqual(ButtonsAndMessages.RecordUpdatedPleaseCloseToExitUpdateForm, aspxPage.GetEditMsg());
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            aspxPage.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PricingType);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PricingType);
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit);
            Thread.Sleep(3000);
            Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.PricingType, ButtonsAndMessages.Edit, "Centralized", "Combined", "Decentralized"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.PrecendenceOrder, ButtonsAndMessages.Edit, "N/A"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.AccountingDocumentType, ButtonsAndMessages.Edit, "All", "AR Only"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            aspxPage.SelectPricingType("Combined", ButtonsAndMessages.Edit);
            aspxPage.WaitForMsg(" processing...");
            aspxPage.SelectAccountingDocType("AR Only", ButtonsAndMessages.Edit);
            aspxPage.UpdateEditGrid();
            Assert.AreEqual(ButtonsAndMessages.RecordUpdatedPleaseCloseToExitUpdateForm, aspxPage.GetEditMsg());
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15899" })]
        public void TC_15899(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", DealerCode);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("All", aspxPage.GetValueOfDropDown("Transaction Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Transaction Type dropdown"));
                Assert.AreEqual("Audit Down Only", aspxPage.GetValueOfDropDown("Core Price Adjustment Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Core Price Adjustment Type dropdown"));
                Assert.AreEqual("All", aspxPage.GetValueOfDropDown("Accounting Document Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Accounting Document Type dropdown"));
            });

            Page.ClosePopupWindow();
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", DealerCode);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("All", aspxPage.GetValueOfDropDown("Transaction Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Transaction Type dropdown"));
                Assert.AreEqual("Audit Down Only", aspxPage.GetValueOfDropDown("Core Price Adjustment Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Core Price Adjustment Type dropdown"));
                Assert.AreEqual("All", aspxPage.GetValueOfDropDown("Accounting Document Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Accounting Document Type dropdown"));
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15900" })]
        public void TC_15900(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", DealerCode);
            var transactionTypes = AccountMaintenanceUtil.GetTranscationNames();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(aspxPage.VerifyValueScrollable("Transaction Type", transactionTypes.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Core Price Adjustment Type", "Audit Down Only", "Audit Up and Down"), ErrorMessages.ElementNotPresent + ". Price Adjustment Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Accounting Document Type", "All"), ErrorMessages.ElementNotPresent + ". Accounting Document Type dropdown.");
            });
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));

            Page.ClosePopupWindow();
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", DealerCode);
            Assert.IsTrue(aspxPage.VerifyValue("Accounting Document Type", "All", "AR Only"), ErrorMessages.ElementNotPresent + ". Accounting Document Type dropdown.");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15923" })]
        public void TC_15923(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", DealerCode);
            Assert.AreEqual("Centralized", aspxPage.GetValueOfDropDown(FieldNames.PricingType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PricingType));
            Assert.AreEqual("N/A", aspxPage.GetValueOfDropDown(FieldNames.PrecendenceOrder), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PrecendenceOrder));
            Assert.AreEqual("All", aspxPage.GetValueOfDropDown(FieldNames.AccountingDocumentType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.AccountingDocumentType));
            var errorMsgs = aspxPage.VerifyPricingtypeFields();
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail("Pricing Type: " + errorMsg);
                }
            });
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            aspxPage.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", DealerCode);
            Assert.AreEqual("Centralized", aspxPage.GetValueOfDropDown(FieldNames.PricingType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PricingType));
            Assert.AreEqual("N/A", aspxPage.GetValueOfDropDown(FieldNames.PrecendenceOrder), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PrecendenceOrder));
            Assert.AreEqual("All", aspxPage.GetValueOfDropDown(FieldNames.AccountingDocumentType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.AccountingDocumentType));

        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15920" })]
        public void TC_15920(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", FleetCode);
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.PricingType, "Centralized", "Combined", "Decentralized"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.PrecendenceOrder, "N/A"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.AccountingDocumentType, new string[0]), "Accounting Document Type dropdown not empty.");
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            aspxPage.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", FleetCode);
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.PricingType, "Centralized", "Combined", "Decentralized"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.PrecendenceOrder, "N/A"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.AccountingDocumentType, "AP Only"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            var rowCountFromTablePricingType = AccountMaintenanceUtil.GetRowCountFromTable("relPricingType_tb");
            aspxPage.SelectPricingType("Centralized");
            aspxPage.SelectPrecendenceOrder("N/A");
            aspxPage.SelectAccountingDocType("AP Only");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var rowCountFromTablePricingTypeAfter = AccountMaintenanceUtil.GetRowCountFromTable("relPricingType_tb");
            Assert.AreNotEqual(rowCountFromTablePricingType, rowCountFromTablePricingTypeAfter);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15922" })]
        public void TC_15922(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {

            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", FleetCode);
            aspxPage.SelectPricingType("Centralized");
            aspxPage.SelectPrecendenceOrder("N/A");
            aspxPage.SelectAccountingDocType("AP Only");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var senderReceiverRelId = AccountMaintenanceUtil.GetPricingTypeDetails();
            Assert.IsTrue(AccountMaintenanceUtil.GetIsActiveValue(senderReceiverRelId.SenderReceiverRelId));
            aspxPage.DeleteFirstRelationShip();
            Assert.IsFalse(AccountMaintenanceUtil.GetIsActiveValue(senderReceiverRelId.SenderReceiverRelId));
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15921" })]
        public void TC_15921(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", FleetCode);
            aspxPage.SelectPricingType("Centralized");
            aspxPage.SelectPrecendenceOrder("N/A");
            aspxPage.SelectAccountingDocType("AP Only");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PricingType);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PricingType);
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit);
            Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.PricingType, ButtonsAndMessages.Edit, "Centralized", "Combined", "Decentralized"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.PrecendenceOrder, ButtonsAndMessages.Edit, "N/A"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.AccountingDocumentType, ButtonsAndMessages.Edit, "AP Only"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            aspxPage.SelectPricingType("Decentralized", ButtonsAndMessages.Edit);
            aspxPage.WaitForMsg(" processing...");
            aspxPage.UpdateEditGrid();
            Assert.AreEqual(ButtonsAndMessages.RecordUpdatedPleaseCloseToExitUpdateForm, aspxPage.GetEditMsg());
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15919" })]
        public void TC_15919(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeletePricingTypeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", FleetCode);
            Assert.AreEqual("Centralized", aspxPage.GetValueOfDropDown(FieldNames.PricingType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PricingType));
            Assert.AreEqual("N/A", aspxPage.GetValueOfDropDown(FieldNames.PrecendenceOrder), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PrecendenceOrder));
            Assert.AreEqual(new string[0], aspxPage.GetValueOfDropDown(FieldNames.AccountingDocumentType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.AccountingDocumentType));
            var errorMsgs = aspxPage.VerifyPricingtypeFields();
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail("Pricing Type: " + errorMsg);
                }
            });
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            aspxPage.ClosePopupWindow();
            Page.SwitchToMainWindow();
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.PricingType, "Pricing Type Table", FleetCode);
            Assert.AreEqual("Centralized", aspxPage.GetValueOfDropDown(FieldNames.PricingType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PricingType));
            Assert.AreEqual("N/A", aspxPage.GetValueOfDropDown(FieldNames.PrecendenceOrder), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PrecendenceOrder));
            Assert.AreEqual("AP Only", aspxPage.GetValueOfDropDown(FieldNames.AccountingDocumentType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.AccountingDocumentType));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15901" })]
        public void TC_15901(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            AccountMaintenanceUtil.DeactivateTokenSeperateARAP();
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", DealerCode);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));

            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CorePriceAdjustment);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CorePriceAdjustment);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.WaitForLoadingMessage();
            var transactionTypes = AccountMaintenanceUtil.GetTranscationNames();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(aspxPage.VerifyValueScrollable("Edit_Transaction Type", transactionTypes.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Edit_Core Price Adjustment Type", "Audit Down Only", "Audit Up and Down"), ErrorMessages.ElementNotPresent + ". Core Price Adjustment Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Edit_Accounting Document Type", "All"), ErrorMessages.ElementNotPresent + ". Accounting Document Type dropdown.");
            });
            Thread.Sleep(200);
            aspxPage.SelectValueRowByIndex(FieldNames.EditTransactionType, 2);
            aspxPage.UpdateEditGrid();
            string updateMsg = aspxPage.GetEditMsg();
            Assert.IsTrue(updateMsg.Contains(ButtonsAndMessages.RecordUpdatedMessage), string.Format(ErrorMessages.UpdateOperationFailed, updateMsg));

            aspxPage.CloseEditGrid();
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CorePriceAdjustment);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.WaitForLoadingMessage();
            Assert.Multiple(() =>
            {
                Assert.IsTrue(aspxPage.VerifyValueScrollable("Edit_Transaction Type", transactionTypes.ToArray()), ErrorMessages.ElementNotPresent + ". Transaction Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Edit_Core Price Adjustment Type", "Audit Down Only", "Audit Up and Down"), ErrorMessages.ElementNotPresent + ". Core Price Adjustment Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue("Edit_Accounting Document Type", "All", "AR Only"), ErrorMessages.ElementNotPresent + ". Accounting Document Type dropdown.");
            });
            Thread.Sleep(200);
            aspxPage.SelectValueRowByIndex(FieldNames.EditTransactionType, 3);
            aspxPage.UpdateEditGrid();
            updateMsg = aspxPage.GetEditMsg();
            Assert.IsTrue(updateMsg.Contains(ButtonsAndMessages.RecordUpdatedMessage), string.Format(ErrorMessages.UpdateOperationFailed, updateMsg));

            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15902" })]
        public void TC_15902(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            AccountMaintenanceUtil.ActivateTokenSeperateARAP();
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CorePriceAdjustment, "Core Price Adjustment Table", DealerCode);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));

            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CorePriceAdjustment);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
            aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
            Assert.AreEqual(aspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteAlertMessage);
            aspxPage.AcceptAlert(ButtonsAndMessages.DeleteAlertMessage);
            aspxPage.WaitForLoadingMessage();
            Assert.IsFalse(AccountMaintenanceUtil.GetRelSenderReceiverByFleetCode(corcentricCode, FieldNames.CorePriceAdjustment).IsActive, ErrorMessages.DeleteOperationFailed);

            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16794" })]
        public void TC_16794(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.InvoiceValidityDays);
            Thread.Sleep(1000);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AddNewRelationship), ErrorMessages.AddNewRelationShipMissing);
            Assert.AreEqual(CorcentricCode, aspxPage.GetText(FieldNames.CurrentEntityName), string.Format(ErrorMessages.IncorrectValue, "Fleet name."));
            Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.Entity), ErrorMessages.FieldNotEmpty + " Dealer dropdown.");
            Assert.IsTrue(aspxPage.VerifyValueScrollable(FieldNames.RelationshipType, FieldNames.InvoiceValidityDays), ErrorMessages.ElementNotPresent + $". {FieldNames.InvoiceValidityDays} relationship.");
            Assert.AreEqual(1, CommonUtils.GetLookupTbDetails(FieldNames.InvoiceValidityDays).AvailableToReceiver, string.Format(ErrorMessages.IncorrectValue, " AvailableToReceiver"));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16704" })]
        public void TC_16704(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.InvoiceValidityDays);
            Thread.Sleep(1000);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AddNewRelationship), ErrorMessages.AddNewRelationShipMissing);
            aspxPage.SelectRelationShipType(FieldNames.InvoiceValidityDays, "Invoice Validity Days Table", DealerCode);
            Thread.Sleep(1000);
            Assert.IsTrue(aspxPage.IsElementDisplayed("Applicable to Transaction Type"), ErrorMessages.ElementNotPresent + ". Applicable to Transaction Type dropdown.");
            Assert.IsTrue(aspxPage.IsElementDisplayed("Applicable to Invoice Type"), ErrorMessages.ElementNotPresent + ". Applicable to Invoice Type dropdown.");
            Assert.IsTrue(aspxPage.IsInputFieldVisible("Validity Days"), ErrorMessages.ElementNotPresent + ". Validity Days textbox.");
            Assert.IsTrue(aspxPage.IsInputFieldVisible("Approver Name"), ErrorMessages.ElementNotPresent + ". Approver Name textbox.");
            Assert.IsTrue(aspxPage.IsInputFieldVisible("Approver Phone Number"), ErrorMessages.ElementNotPresent + ". Approver Phone Number textbox.");
            Assert.IsTrue(aspxPage.IsInputFieldVisible("Approver Email"), ErrorMessages.ElementNotPresent + ". Approver Email textbox.");
            Assert.IsTrue(aspxPage.IsElementDisplayed("Start Date"), ErrorMessages.ElementNotPresent + ". Start Date dropdown.");
            Assert.IsTrue(aspxPage.IsElementDisplayed("End Date"), ErrorMessages.ElementNotPresent + ". End Date dropdown.");
            Assert.IsTrue(aspxPage.IsElementDisplayed("Note"), ErrorMessages.ElementNotPresent + ". Note textarea");
            Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
            Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);

            Assert.IsTrue(aspxPage.IsElementDisplayed("Applicable to Transaction Type Required Label"), string.Format(ErrorMessages.FieldNotMarkedMandatory, "Applicable to Transaction Type"));
            Assert.IsTrue(aspxPage.IsElementDisplayed("Applicable to Invoice Type Required Label"), string.Format(ErrorMessages.FieldNotMarkedMandatory, "Applicable to Invoice Type"));
            Assert.IsTrue(aspxPage.IsElementDisplayed("Validity Days Required Label"), string.Format(ErrorMessages.FieldNotMarkedMandatory, "Validity Days"));
            Assert.IsTrue(aspxPage.IsElementDisplayed("Approver Name Required Label"), string.Format(ErrorMessages.FieldNotMarkedMandatory, "Approver Name"));
            Assert.IsTrue(aspxPage.IsElementDisplayed("Approver Phone Number Required Label"), string.Format(ErrorMessages.FieldNotMarkedMandatory, "Approver Phone Number"));
            Assert.IsTrue(aspxPage.IsElementDisplayed("Approver Email Address Required Label"), string.Format(ErrorMessages.FieldNotMarkedMandatory, "Approver Email Address"));
            Assert.IsTrue(aspxPage.IsElementDisplayed("Start Date Required Label"), string.Format(ErrorMessages.FieldNotMarkedMandatory, "Start Date"));

            Assert.AreEqual("All", aspxPage.GetValueOfDropDown("Applicable to Transaction Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Applicable to Transaction Type"));
            Assert.AreEqual("All", aspxPage.GetValueOfDropDown("Applicable to Invoice Type"), string.Format(ErrorMessages.InvalidDefaultValue, "Applicable to Invoice Type"));
            Assert.AreEqual(string.Empty, aspxPage.GetValue("Approver Name"), string.Format(ErrorMessages.InvalidDefaultValue, "Approver Name"));
            Assert.AreEqual(string.Empty, aspxPage.GetValue("Approver Phone Number"), string.Format(ErrorMessages.InvalidDefaultValue, "Approver Phone Number"));
            Assert.AreEqual(string.Empty, aspxPage.GetValue("Approver Email"), string.Format(ErrorMessages.InvalidDefaultValue, "Approver Email"));
            Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown("Start Date"), string.Format(ErrorMessages.InvalidDefaultValue, "Start Date"));
            Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown("End Date"), string.Format(ErrorMessages.InvalidDefaultValue, "End Date"));
            Assert.AreEqual(string.Empty, aspxPage.GetText("Note"), string.Format(ErrorMessages.InvalidDefaultValue, "Note"));

            var transactionTypes = AccountMaintenanceUtil.GetTranscationNames();
            Assert.IsTrue(aspxPage.VerifyValueScrollable("Applicable to Transaction Type", transactionTypes.ToArray()), ErrorMessages.ElementNotPresent + ". Value in Applicable to Transaction Type dropdown.");
            Assert.IsTrue(aspxPage.VerifyValue("Applicable to Invoice Type", "All", "Credit", "Invoice"), ErrorMessages.ElementNotPresent + ". Value in Applicable to Invoice Type dropdown.");
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22604" })]
        public void TC_22604(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteInvoiceDeliveryRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.InvoiceDelivery, "Invoice Delivery Table", DealerCode);
            var errorMsgs = aspxPage.VerifyInvoiceDeliveryFields();
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail("Invoice Delivery: " + errorMsg);
                }
            });

            Assert.AreEqual("EDI", aspxPage.GetValueOfDropDown(FieldNames.Method), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Method));
            Assert.AreEqual("Centralized", aspxPage.GetValueOfDropDown(FieldNames.Structure), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Structure));
            Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.DealerInvoiceCopy));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.Method, "EDI", "Email", "Netsend", "Website"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Assert.IsTrue(aspxPage.VerifyValue(FieldNames.Structure, "Centralized", "Decentralized"), GetErrorMessage(ErrorMessages.ListElementsMissing));

            var rowCountFromTableInvoiceDelivery = AccountMaintenanceUtil.GetRowCountFromTable("relinvoicedelivery_tb");
            aspxPage.SelectMethod("Netsend");
            aspxPage.Click(FieldNames.DealerInvoiceCopy);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var rowCountFromTableInvoiceDeliveryAfter = AccountMaintenanceUtil.GetRowCountFromTable("relinvoicedelivery_tb");
            Assert.AreNotEqual(rowCountFromTableInvoiceDeliveryAfter, rowCountFromTableInvoiceDelivery);
            aspxPage.DeleteInvoiceDeliveryRelationshipFromDB(corcentricCode, entityType);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22605" })]
        public void TC_22605(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteInvoiceDeliveryRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.InvoiceDelivery, "Invoice Delivery Table", DealerCode);
            aspxPage.SelectMethod("Netsend");
            aspxPage.Click(FieldNames.DealerInvoiceCopy);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var deliveryMethodId = AccountMaintenanceUtil.GetInvoiceDeliveryMethodId(corcentricCode);
            Assert.AreEqual(163525, deliveryMethodId);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.InvoiceDelivery);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.InvoiceDelivery);
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            Thread.Sleep(5000); // After edit finding dropdown EDIT takes time, we need to fix here to avoid manual wait time
            aspxPage.SelectMethod("EDI", ButtonsAndMessages.Edit);
            aspxPage.UpdateEditGrid();
            Assert.AreEqual(ButtonsAndMessages.RecordUpdatedPleaseCloseToExitUpdateForm, aspxPage.GetEditMsg());
            deliveryMethodId = AccountMaintenanceUtil.GetInvoiceDeliveryMethodId(corcentricCode);
            Assert.AreEqual(389, deliveryMethodId);
            aspxPage.DeleteInvoiceDeliveryRelationshipFromDB(corcentricCode, entityType);

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22601" })]
        public void TC_22601(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteCurrencyCodeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CurrencyCode, FieldNames.RelCurrencyTable, DealerCode);
            Assert.AreEqual("AUD", aspxPage.GetValueOfDropDown(FieldNames.Currency), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.Currency));
            Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.AllowMultiCurrencyTransaction));
            Assert.IsTrue(aspxPage.VerifyValueScrollable(FieldNames.Currency, "AUD", "CAD", "DEU", "ESP", "EUR", "FRA", "GBP", "ITA", "MXN", "NLD", "USD"), GetErrorMessage(ErrorMessages.ListElementsMissing));
            Page.ClickFieldLabel(FieldNames.Currency);
            var rowCountFromTableCurrencyCode = AccountMaintenanceUtil.GetRowCountFromTable("relcurrency_tb");
            aspxPage.SelectValueByScroll(FieldNames.Currency, "CAD");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var rowCountFromTableCurrencyCodeAfter = AccountMaintenanceUtil.GetRowCountFromTable("relcurrency_tb");
            Assert.AreNotEqual(rowCountFromTableCurrencyCodeAfter, rowCountFromTableCurrencyCode);
            aspxPage.DeleteCurrencyCodeRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22602" })]
        public void TC_22602(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteCurrencyCodeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CurrencyCode, FieldNames.RelCurrencyTable, DealerCode);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            var tblCurrencyCode = AccountMaintenanceUtil.GetCurrencyCodeDetails();
            Assert.AreEqual("AUD", tblCurrencyCode.currency);
            Assert.IsFalse(tblCurrencyCode.isActive);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CurrencyCode);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CurrencyCode);
            aspxPage.ClickAnchorButton("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            Thread.Sleep(5000); // After edit finding dropdown EDIT takes time, we need to fix here to avoid manual wait time
            aspxPage.SelectCurrency("CAD", ButtonsAndMessages.Edit);
            aspxPage.Click(FieldNames.AllowMultiCurrencyTransaction, ButtonsAndMessages.Edit);
            aspxPage.UpdateEditGrid();
            Assert.AreEqual(ButtonsAndMessages.RecordUpdatedPleaseCloseToExitUpdateForm, aspxPage.GetEditMsg());
            tblCurrencyCode = AccountMaintenanceUtil.GetCurrencyCodeDetails();
            Assert.AreEqual("CAD", tblCurrencyCode.currency);
            Assert.IsTrue(tblCurrencyCode.isActive);
            aspxPage.DeleteCurrencyCodeRelationshipFromDB(corcentricCode, entityType);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22603" })]
        public void TC_22603(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string DealerCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteCurrencyCodeRelationshipFromDB(corcentricCode, entityType);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.CurrencyCode, FieldNames.RelCurrencyTable, DealerCode);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CurrencyCode);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, DealerCode);
            aspxPage.DeleteFirstRelationShip();
            aspxPage.DeleteCurrencyCodeRelationshipFromDB(corcentricCode, entityType);

        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22647" })]
        public void TC_22647(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.StatementDelivery);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.StatementDelivery, "Statement Delivery Table", FleetCode);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Website Download", aspxPage.GetValueOfDropDown(FieldNames.StatementDeliveryMethod), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.StatementDeliveryMethod));
                Assert.AreEqual("Centralized", aspxPage.GetValueOfDropDown(FieldNames.StatementDeliveryStructure), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.StatementDeliveryStructure));
                Assert.IsTrue(aspxPage.VerifyValue(FieldNames.StatementDeliveryStructure, "Centralized", "Decentralized"), ErrorMessages.ElementNotPresent + $". {FieldNames.StatementDeliveryStructure} dropdown.");
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.StatementDeliveryMethod + " Required Label"), string.Format(ErrorMessages.FieldNotMarkedMandatory, FieldNames.StatementDeliveryMethod));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.StatementDeliveryStructure + " Required Label"), string.Format(ErrorMessages.FieldNotMarkedMandatory, FieldNames.StatementDeliveryStructure));
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
            });
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22648" })]
        public void TC_22648(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.StatementDelivery);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.StatementDelivery, "Statement Delivery Table", FleetCode);
            aspxPage.SelectValueTableDropDown(FieldNames.StatementDeliveryMethod, "Website Download");
            aspxPage.SelectValueTableDropDown(FieldNames.StatementDeliveryStructure, "Centralized");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.StatementDelivery);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
            Assert.AreEqual(1, aspxPage.GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.StatementDelivery);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22649" })]
        public void TC_22649(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.StatementDelivery);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.StatementDelivery, "Statement Delivery Table", FleetCode);
            aspxPage.SelectValueTableDropDown(FieldNames.StatementDeliveryMethod, "Website Download");
            aspxPage.SelectValueTableDropDown(FieldNames.StatementDeliveryStructure, "Centralized");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.StatementDelivery);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
            Assert.AreEqual(1, aspxPage.GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.StatementDelivery);
            aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
            aspxPage.WaitForLoadingMessage();
            Thread.Sleep(1000);
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Website Download", aspxPage.GetValueOfDropDown(FieldNames.StatementDeliveryMethod, ButtonsAndMessages.Edit), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.StatementDeliveryMethod + " Edit"));
                Assert.AreEqual("Centralized", aspxPage.GetValueOfDropDown(FieldNames.StatementDeliveryStructure, ButtonsAndMessages.Edit), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.StatementDeliveryStructure + " Edit"));
                Assert.IsTrue(aspxPage.VerifyValueByEdit(FieldNames.StatementDeliveryStructure, ButtonsAndMessages.Edit, "Centralized", "Decentralized"), ErrorMessages.ElementNotPresent + $". {FieldNames.StatementDeliveryStructure} Edit dropdown.");
            });
            aspxPage.SelectValueTableDropDown(FieldNames.StatementDeliveryStructure, "Decentralized", ButtonsAndMessages.Edit);
            aspxPage.UpdateEditGrid();
            string updateMsg = aspxPage.GetEditMsg();
            Assert.IsTrue(updateMsg.Contains(ButtonsAndMessages.RecordUpdatedMessage), string.Format(ErrorMessages.UpdateOperationFailed, updateMsg));
            aspxPage.CloseEditGrid();
            string stmtDelStructureVal = aspxPage.GetFirstValueFromGrid(FieldNames.MainTable, FieldNames.MainTableHeader, "statement Delivery Structure");
            Assert.AreEqual("Decentralized", stmtDelStructureVal, ErrorMessages.RecordNotUpdatedDB);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.CorePriceAdjustment);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22650" })]
        public void TC_22650(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, CorcentricCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.StatementDelivery);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.StatementDelivery, "Statement Delivery Table", FleetCode);
            aspxPage.SelectValueTableDropDown(FieldNames.StatementDeliveryMethod, "Website Download");
            aspxPage.SelectValueTableDropDown(FieldNames.StatementDeliveryStructure, "Centralized");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.StatementDelivery);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
            Assert.AreEqual(1, aspxPage.GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);

            aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
            Assert.AreEqual(aspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteAlertMessage);
            aspxPage.AcceptAlert(ButtonsAndMessages.DeleteAlertMessage);
            aspxPage.WaitForLoadingMessage();
            Assert.IsFalse(AccountMaintenanceUtil.GetRelSenderReceiverByDealerCode(corcentricCode, FieldNames.StatementDelivery).IsActive, ErrorMessages.DeleteOperationFailed);

            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.StatementDelivery);
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22651" })]
        public void TC_22651(string Name, string LegalName, string Parent, string EntityType, string LocationType, string CorcentricCode, string FleetCode, string DealerMasterLocation, string DealerBillingLocation, string DealerShopLocation)
        {
            Page.LoadDataOnGrid(Name, LegalName, Parent, EntityType, LocationType, DealerMasterLocation);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.StatementDelivery);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.SelectRelationShipType(FieldNames.StatementDelivery, "Statement Delivery Table", FleetCode);
            aspxPage.SelectValueTableDropDown(FieldNames.StatementDeliveryMethod, "Website Download");
            aspxPage.SelectValueTableDropDown(FieldNames.StatementDeliveryStructure, "Centralized");
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.StatementDelivery);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
            Assert.AreEqual(1, aspxPage.GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);

            // Verifying relationship on billing location
            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(DealerBillingLocation, LegalName, Parent, EntityType, LocationType, DealerBillingLocation);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.StatementDelivery);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.StatementDelivery);
            Assert.IsTrue(aspxPage.CheckFirstRelationShipIsDisabled(), $"Relationship not Disabled at Billing Location [{DealerBillingLocation}]");
            Assert.IsTrue(aspxPage.IsCommandsButtonsNotVisible());

            // Verifying relationship on shop location
            Page.ClosePopupWindow();
            Page.LoadDataOnGrid(DealerShopLocation, LegalName, Parent, EntityType, LocationType, DealerShopLocation);
            Page.ClickHyperLinkOnGrid(TableHeaders.Name);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.StatementDelivery);
            aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, FleetCode);
            aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.StatementDelivery);
            Assert.IsTrue(aspxPage.CheckFirstRelationShipIsDisabled(), $"Relationship not Disabled at Shop Location [{DealerShopLocation}]");
            Assert.IsTrue(aspxPage.IsCommandsButtonsNotVisible());

            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.StatementDelivery);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16430" })]
        public void TC_16430(string UserType)
        {
            string dealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Shop);
            Page.LoadDataOnGrid(dealerCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations"));
                var errorMsgs = aspxPage.VerifyAccountConfigFieldsDealer(LocationType.Shop);
                Assert.Multiple(() =>
                {
                    foreach (string errorMsg in errorMsgs)
                    {
                        Assert.Fail(errorMsg);
                    }
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16431" })]
        public void TC_16431(string UserType)
        {
            string fleetCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Shop);
            Page.LoadDataOnGrid(fleetCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations"));
                var errorMsgs = aspxPage.VerifyAccountConfigFieldsFleet(LocationType.Shop);
                Assert.Multiple(() =>
                {
                    foreach (string errorMsg in errorMsgs)
                    {
                        Assert.Fail(errorMsg);
                    }
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }


        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16432" })]
        public void TC_16432(string UserType)
        {
            string fleetCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Master);
            Page.LoadDataOnGrid(fleetCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations"));
                var errorMsgs = aspxPage.VerifyAccountConfigFieldsFleet(LocationType.Master);
                Assert.Multiple(() =>
                {
                    foreach (string errorMsg in errorMsgs)
                    {
                        Assert.Fail(errorMsg);
                    }
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16433" })]
        public void TC_16433(string UserType)
        {
            string dealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            Page.LoadDataOnGrid(dealerCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations"));
                var errorMsgs = aspxPage.VerifyAccountConfigFieldsDealer(LocationType.Master);
                Assert.Multiple(() =>
                {
                    foreach (string errorMsg in errorMsgs)
                    {
                        Assert.Fail(errorMsg);
                    }
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21625" })]
        public void TC_21625(string UserType)
        {
            string masterDealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            Page.LoadDataOnGrid(masterDealerCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.FieldDisplayed, FieldNames.FinanceChargeExempt));
                Page.ClosePopupWindow();
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for MasterDealerCode [{masterDealerCode}]");
            }

            string billingDealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            Page.LoadDataOnGrid(billingDealerCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.CorcentricLocation), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.CorcentricLocation));
                Page.ClosePopupWindow();
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingDealerCode}]");
            }

            string shopDealerCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Shop);
            Page.LoadDataOnGrid(shopDealerCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.FieldDisplayed, FieldNames.FinanceChargeExempt));
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.CorcentricLocation), string.Format(ErrorMessages.FieldDisplayed, FieldNames.CorcentricLocation));
                Page.ClosePopupWindow();
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for ShopDealerCode [{shopDealerCode}]");
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21626" })]
        public void TC_21626(string UserType)
        {
            string masterFleetCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Master);
            Page.LoadDataOnGrid(masterFleetCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.FieldDisplayed, FieldNames.FinanceChargeExempt));
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.CorcentricLocation), string.Format(ErrorMessages.FieldDisplayed, FieldNames.CorcentricLocation));
                Page.ClosePopupWindow();
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for MasterFleetCode [{masterFleetCode}]");
            }

            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            Page.LoadDataOnGrid(billingCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.CheckBoxChecked, FieldNames.FinanceChargeExempt));
                Page.ClosePopupWindow();
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingFleetCode [{billingCode}]");
            }

            string shopCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Shop);
            Page.LoadDataOnGrid(shopCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.FieldDisplayed, FieldNames.FinanceChargeExempt));
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.CorcentricLocation), string.Format(ErrorMessages.FieldDisplayed, FieldNames.CorcentricLocation));
                Page.ClosePopupWindow();
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for ShopFleetCode [{shopCode}]");
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22946" })]
        public void TC_22946(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingFleetCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);
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
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22947" })]
        public void TC_22947(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);
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
                Assert.AreEqual(menu.RenameMenuField(EntityType.Dealer), aspxPage.GetValueOfDropDown(FieldNames.EnrollmentType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EnrollmentType));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                Assert.AreEqual("Shop", aspxPage.GetValueOfDropDown(FieldNames.LocationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.LocationType));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DDEFlag), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DDEFlag));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Deactivated), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Deactivated));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Terminated), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Terminated));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TerminatedDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TerminatedDate));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TerminationNotes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TerminationNotes));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.FranchiseCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FranchiseCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SelectedFranchiseCodes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SelectedFranchiseCodes));
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
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PreAuthorization), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PreAuthorization));
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.CorcentricLocation), string.Format(ErrorMessages.FieldDisplayed, FieldNames.CorcentricLocation));
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22948" })]
        public void TC_22948(string UserType)
        {
            string shopCode = CommonUtils.GetFleetCodeHasNoChild(LocationType.Shop);
            Page.LoadDataOnGrid(shopCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for ShopFleetCode [{shopCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);
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
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.InvoiceApproval), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApproval));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PreAuthorization), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PreAuthorization));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SelectionBoxAvailable), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SelectionBoxAvailable));
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.FinanceChargeExempt), string.Format(ErrorMessages.FieldDisplayed, FieldNames.FinanceChargeExempt));
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22949" })]
        public void TC_22949(string UserType)
        {
            string shopCode = CommonUtils.GetDealerCodeHasNoChild(LocationType.Shop);
            Page.LoadDataOnGrid(shopCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for ShopDealerCode [{shopCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);
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
                Assert.AreEqual(menu.RenameMenuField(EntityType.Dealer), aspxPage.GetValueOfDropDown(FieldNames.EnrollmentType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EnrollmentType));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                Assert.AreEqual("Shop", aspxPage.GetValueOfDropDown(FieldNames.LocationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.LocationType));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DDEFlag), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DDEFlag));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Deactivated), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Deactivated));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Terminated), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Terminated));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TerminatedDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TerminatedDate));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TerminationNotes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TerminationNotes));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.FranchiseCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FranchiseCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SelectedFranchiseCodes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SelectedFranchiseCodes));
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
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PreAuthorization), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PreAuthorization));
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.CorcentricLocation), string.Format(ErrorMessages.FieldDisplayed, FieldNames.CorcentricLocation));
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22950" })]
        public void TC_22950(string UserType)
        {
            string masterCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Master);
            Page.LoadDataOnGrid(masterCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for MasterFleetCode [{masterCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);
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
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22951" })]
        public void TC_22951(string UserType)
        {
            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            Page.LoadDataOnGrid(masterCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for MasterDealerCode [{masterCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);
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
                Assert.AreEqual(menu.RenameMenuField(EntityType.Dealer), aspxPage.GetValueOfDropDown(FieldNames.EnrollmentType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EnrollmentType));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                Assert.AreEqual("Shop", aspxPage.GetValueOfDropDown(FieldNames.LocationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.LocationType));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DDEFlag), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DDEFlag));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AccountCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AccountingCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Deactivated), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Deactivated));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Terminated), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Terminated));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TerminatedDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TerminatedDate));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TerminationNotes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TerminationNotes));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.FranchiseCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FranchiseCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SelectedFranchiseCodes), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SelectedFranchiseCodes));
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
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.SubCommunity), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PreAuthorization), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PreAuthorization));
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                Assert.IsFalse(aspxPage.IsElementDisplayed(FieldNames.CorcentricLocation), string.Format(ErrorMessages.FieldDisplayed, FieldNames.CorcentricLocation));
            });
        }

        [Category(TestCategory.Smoke)]
        [Category(TestCategory.Premier)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22116" })]
        public void TC_22116(string UserType)
        {
            string masterCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Master);
            Page.LoadDataOnGrid(masterCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.ClickListElements("TabList", "Account Configuration");
                aspxPage.SwitchIframe();
                Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorPhone), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorPhone));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorEmail), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorEmail));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorCompany), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorCompany));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EnrollmentStatus), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentStatus));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Language), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ParentAccountName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentAccountName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProviderCount), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProviderCount));
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
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                    Assert.IsTrue(aspxPage.IsElementDisplayed("Currency Dropdown"), string.Format(ErrorMessages.FieldNotDisplayed, "Currency"));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DirectBillCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DirectBillCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.InvoiceApproval), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApproval));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                });
                aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
                aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
                aspxPage.ButtonClick(FieldNames.AddNewLocation);
                aspxPage.WaitForElementToBeVisible("New Location Frame");
                aspxPage.SwitchIframe();
                aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorPhone), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorPhone));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorEmail), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorEmail));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.RequestorCompany), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.RequestorCompany));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DisplayName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DisplayName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LegalName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LegalName));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Language), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EnrollmentType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EnrollmentType));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LocationType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LocationType));
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
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EligibleForTranSubmission), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EligibleForTranSubmission));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Address + "1"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "1"));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Address + "2"), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address + "2"));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.County), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.County));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PhoneNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PhoneNumber));
                    Assert.IsTrue(aspxPage.IsElementDisplayed("Currency Dropdown"), string.Format(ErrorMessages.FieldNotDisplayed, "Currency"));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DunNumber), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DunNumber));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EntityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CommunityCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.DirectBillCode), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DirectBillCode));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProgramStartDate), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProgramStartDate));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.InvoiceApproval), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApproval));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Save), string.Format(ErrorMessages.ButtonMissing, ButtonsAndMessages.Save));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16386" })]
        public void TC_16386(string UserType)
        {
            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            string requiredLabel = " Required Label";
            Page.LoadDataOnGrid(masterCode);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.SelectRelationShipType(FieldNames.PaymentTerms, FieldNames.PaymentTermsTable, menu.RenameMenuField("All Fleets"));
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                Assert.Multiple(() =>
                {
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
                    Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.BillingCycleStartDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycleStartDate));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
                    Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
                    Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                    Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                    Assert.AreEqual("Net 10", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
                    Assert.AreEqual("Bi-Weekly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                    Assert.AreEqual("Sunday", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                    Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                    Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                    Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementEndDay + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementEndDay + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.BillingCycleStartDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycleStartDate + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));
                    Assert.IsTrue(aspxPage.VerifyValue(FieldNames.BillingCycle, "Bi-Weekly", "Daily", "Monthly", "Twice Monthly", "Weekly"), ErrorMessages.ElementNotPresent + ". Billing Cycle Type dropdown.");
                    Assert.IsTrue(aspxPage.VerifyValue(FieldNames.StatementEndDay, "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"), ErrorMessages.ElementNotPresent + ". Statement End Day dropdown.");

                    aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.IsTrue(aspxPage.VerifyValue(FieldNames.StatementEndDay, "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"), ErrorMessages.ElementNotPresent + ". Statement End Day dropdown.");

                    aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Monthly");
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.IsTrue(aspxPage.VerifyValueScrollable(FieldNames.StatementEndDay, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29 or end of the Month", "30 or end of the Month", "31 or end of the Month"), ErrorMessages.ElementNotPresent + ". Statement End Day dropdown.");
                    Assert.IsTrue(aspxPage.VerifyValue(FieldNames.StatementType, "One statement per due date", "One statement per statement period"), ErrorMessages.ElementNotPresent + ". Statement Type dropdown.");
                    Assert.IsTrue(aspxPage.VerifyValue(FieldNames.AccelerationType, "By Account", "By Invoice", "None"), ErrorMessages.ElementNotPresent + ". Acceleration Type dropdown.");
                    Assert.IsTrue(aspxPage.VerifyValue(FieldNames.EffectiveDateBasedOn, "Dealer Invoice Date", "Settlement Date"), ErrorMessages.ElementNotPresent + ". Effective Date Based On dropdown.");

                    aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Daily");
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
                    Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
                    Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                    Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                    Assert.AreEqual("Net 10", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
                    Assert.AreEqual("Daily", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                    Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                    Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                    Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));

                    aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Monthly");
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
                    Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
                    Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                    Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                    Assert.AreEqual("Net 10", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
                    Assert.AreEqual("Monthly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                    Assert.AreEqual("1", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                    Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                    Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                    Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));

                    aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Twice Monthly");
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
                    Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
                    Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                    Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                    Assert.AreEqual("Net 10", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
                    Assert.AreEqual("Twice Monthly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                    Assert.AreEqual("15th and end of the Month", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                    Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                    Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                    Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));

                    aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                    Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
                    Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
                    Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                    Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                    Assert.AreEqual("Net 10", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
                    Assert.AreEqual("Weekly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                    Assert.AreEqual("Sunday", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                    Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                    Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                    Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));

                });

            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24108" })]
        public void TC_24108(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "AL";
            string zip = "12345";

            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, dealerName);
            aspxPage.EnterText(FieldNames.City, dealerName);
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                aspxPage.EnterText(FieldNames.Zip, zip);
                aspxPage.ButtonClick(ButtonsAndMessages.Save);
                if (aspxPage.CheckForText("Please enter zip/postal code."))
                {
                    Assert.Fail($"Some error occurred while creating entity.");
                }
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24107" })]
        public void TC_24107(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";

            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, dealerName);
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "PK")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "PK");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24106" })]
        public void TC_24106(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "NL")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "NL");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24105" })]
        public void TC_24105(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "MX")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "MX");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24104" })]
        public void TC_24104(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "IT")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "IT");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24103" })]
        public void TC_24103(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "GB")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "GB");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24102" })]
        public void TC_24102(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "FR")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "FR");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24101" })]
        public void TC_24101(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "ES")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "ES");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24100" })]
        public void TC_24100(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "DE")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "DE");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24099" })]
        public void TC_24099(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "CA")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "CA");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24098" })]
        public void TC_24098(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "AU")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "AU");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24097" })]
        public void TC_24097(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, dealerName);
            aspxPage.EnterText(FieldNames.LegalName, dealerName);

            aspxPage.EnterText(FieldNames.AccountCode, dealerName);
            aspxPage.EnterText(FieldNames.AccountingCode, dealerName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "IM")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "IM");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24054" })]
        public void TC_24054(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "AL";
            string zip = "12345";

            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingFleetCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "Address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                aspxPage.EnterText(FieldNames.Zip, zip);
                aspxPage.ButtonClick(ButtonsAndMessages.Save);
                if (aspxPage.CheckForText("Please enter zip/postal code."))
                {
                    Assert.Fail($"Some error occurred while creating entity.");
                }
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Fleet Created with Code: [{fleetName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24053" })]
        public void TC_24053(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "PK")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "PK");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24052" })]
        public void TC_24052(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "NL")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "NL");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24051" })]
        public void TC_24051(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "MX")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "MX");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24050" })]
        public void TC_24050(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "IT")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "IT");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24049" })]
        public void TC_24049(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "GB")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "GB");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24048" })]
        public void TC_24048(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "FR")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "FR");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24047" })]
        public void TC_24047(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "ES")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "ES");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24046" })]
        public void TC_24046(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "DE")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "DE");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24045" })]
        public void TC_24045(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "CA")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "CA");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24044" })]
        public void TC_24044(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string state = "";
            string zip = "";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "New Locations");
            aspxPage.WaitForButtonToBePresent(FieldNames.AddNewLocation);
            aspxPage.ButtonClick(FieldNames.AddNewLocation);
            aspxPage.WaitForElementToBeVisible("New Location Frame");
            aspxPage.SwitchIframe();
            aspxPage.WaitForElementToBeVisible(FieldNames.RequestorName);

            aspxPage.EnterText(FieldNames.DisplayName, fleetName);
            aspxPage.EnterText(FieldNames.LegalName, fleetName);

            aspxPage.EnterText(FieldNames.AccountCode, fleetName);
            aspxPage.EnterText(FieldNames.AccountingCode, fleetName);
            aspxPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            aspxPage.EnterText(FieldNames.Address1, "address1");
            aspxPage.EnterText(FieldNames.City, "City1");
            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "AU")
            {
                aspxPage.SelectValueByScroll(FieldNames.Country, "AU");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != string.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
                Assert.AreEqual(string.Empty, aspxPage.GetValueOfDropDown(FieldNames.State));
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            aspxPage.EnterTextAfterClear("Program Start Date Input", CommonUtils.GetCurrentDate());
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                Assert.Fail($"Some error occurred while creating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Created with Code: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24131" })]
        public void TC_24131(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string email = CommonUtils.RandomString(4) + "@gmail.com";
            string state = "AL";
            string zip = "24546";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingFleetCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "Contacts");
            aspxPage.SelectValueByScroll(FieldNames.Type, "AR");
            aspxPage.EnterText(FieldNames.FirstName, fleetName);
            aspxPage.EnterText(FieldNames.LastName, fleetName);
            aspxPage.EnterText(FieldNames.Title, "title1");
            aspxPage.EnterText(FieldNames.Phone, "(999)999-9999");
            aspxPage.EnterText(FieldNames.Email, email);
            aspxPage.SelectValueByScroll(FieldNames.Language, "en-US");
            aspxPage.EnterText(FieldNames.Address1, "Address1");
            aspxPage.EnterText(FieldNames.City, "City1");

            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                aspxPage.EnterText(FieldNames.Zip, zip);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);

                if (aspxPage.CheckForText("There was an error. Please contact system administrator for further details.") || aspxPage.CheckForText("Please enter zip / postal code."))
                {
                    Assert.Fail($"Some error occurred while creating entity.");
                }
            }

            EntityDetails zipAndStateDetails = AccountMaintenanceUtil.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Fleet Contact with Title: [{fleetName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24129" })]
        public void TC_24129(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string dealerName = "Automate_DealerShop" + CommonUtils.RandomString(4);
            string email = CommonUtils.RandomString(4) + "@gmail.com";
            string state = "AL";
            string zip = "12345";



            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "Contacts");
            aspxPage.SelectValueByScroll(FieldNames.Type, "AR");
            aspxPage.EnterText(FieldNames.FirstName, dealerName);
            aspxPage.EnterText(FieldNames.LastName, dealerName);
            aspxPage.EnterText(FieldNames.Title, "title1");
            aspxPage.EnterText(FieldNames.Phone, "(999)999-9999");
            aspxPage.EnterText(FieldNames.Email, email);
            aspxPage.SelectValueByScroll(FieldNames.Language, "en-US");
            aspxPage.EnterText(FieldNames.Address1, "Address1");
            aspxPage.EnterText(FieldNames.City, "City1");

            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                aspxPage.EnterText(FieldNames.Zip, zip);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);

                if (aspxPage.CheckForText("There was an error. Please contact system administrator for further details.") || aspxPage.CheckForText("Please enter zip / postal code."))
                {
                    Assert.Fail($"Some error occurred while creating entity.");
                }
            }

            EntityDetails zipAndStateDetails = AccountMaintenanceUtil.GetZipAndState(dealerName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Contact with Title: [{dealerName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24119" })]
        public void TC_24119(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string zip = "";
            string state = "";
            string updatedZip = "12345";
            string UpdatedState = "AL";

            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingDealerCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));

            if (aspxPage.GetValueOfDropDown(FieldNames.Country) == "US")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "MX");
            }

            if (aspxPage.GetValueOfDropDown(FieldNames.State) == String.Empty)
            {
                aspxPage.SelectValueFirstRow(FieldNames.State);
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("There was an error. Please contact system administrator for further details.") || aspxPage.CheckForText("Please enter zip / postal code."))
            {
                Assert.Fail($"Some error occurred while Updating entity.");
            }

            EntityDetails zipAndStateDetails = CommonUtils.GetZipAndState(billingCode);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));

            aspxPage.ClickTab("Account Configuration Tabs", "Information");

            if (aspxPage.GetValueOfDropDown(FieldNames.Country) == "MX")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "US");
            }

            if (aspxPage.GetValueOfDropDown(FieldNames.State) == String.Empty)
            {
                aspxPage.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.ButtonClick(ButtonsAndMessages.Save);

            if (aspxPage.CheckForText("There was an error. Please contact system administrator for further details.") || aspxPage.CheckForText("Please enter zip/postal code."))
            {
                aspxPage.EnterText(FieldNames.Zip, updatedZip);
                aspxPage.ButtonClick(ButtonsAndMessages.Save);

                if (aspxPage.CheckForText("There was an error. Please contact system administrator for further details.") || aspxPage.CheckForText("Please enter zip/postal code."))
                {
                    Assert.Fail($"Some error occurred while Updating entity.");
                }
            }

            EntityDetails updatedZipAndStateDetails = CommonUtils.GetZipAndState(billingCode);
            Assert.AreEqual(updatedZip, updatedZipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(UpdatedState, updatedZipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
            Console.WriteLine($"Dealer Updated with Code: [{billingCode}]");


        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24221" })]
        public void TC_24221(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.AccountMaintenance), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.AccountMaintenance).ForEach(x => { Assert.Fail(x); });
            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.Columns, ButtonsAndMessages.SaveLayout);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid();

            if (Page.IsAnyDataOnGrid())
            {
                string name = Page.GetFirstRowData(TableHeaders.Name);
                Page.FilterTable(TableHeaders.Name, name);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Name, name), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Name, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.Name, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                Page.FilterTable(TableHeaders.Name, name);
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Columns, ButtonsAndMessages.SaveLayout));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
            }
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }

            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23801" })]
        public void TC_23801(string UserType)
        {

            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            string requiredLabel = " Required Label";


            Page.LoadDataOnGrid(masterCode);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
            aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.PaymentTerms);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.SelectRelationShipType(FieldNames.PaymentTerms, FieldNames.PaymentTermsTable, menu.RenameMenuField("All Fleets"));
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                aspxPage.SelectValueByScroll(FieldNames.PaymentTerms, "Variable");
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");



                Assert.AreEqual("Payment Terms", aspxPage.GetText(FieldNames.PaymentTermsLabel));
                Assert.AreEqual("Settlement Based Due Date calculation", aspxPage.GetText(FieldNames.SettlementBasedDueDatecalculationLabel));
                Assert.AreEqual("Term Description", aspxPage.GetText(FieldNames.TermDescriptionLabel));
                Assert.AreEqual("Variable Due Date calculation", aspxPage.GetText(FieldNames.VariableDueDatecalculationLabel));
                Assert.AreEqual("Billing Cycle", aspxPage.GetText(FieldNames.BillingCycleLabel));
                Assert.AreEqual("Statement End Day", aspxPage.GetText(FieldNames.StatementEndDayLabel));
                Assert.AreEqual("Statement Type", aspxPage.GetText(FieldNames.StatementTypeLabel));
                Assert.AreEqual("Acceleration Type", aspxPage.GetText(FieldNames.AccelerationTypeLabel));
                Assert.AreEqual("Acceleration Program", aspxPage.GetText(FieldNames.AccelerationProgramLabel));
                Assert.AreEqual("Effective Date Based On", aspxPage.GetText(FieldNames.EffectiveDateBasedOnLabel));
                Assert.AreEqual("Effective Date", aspxPage.GetText(FieldNames.EffectiveDateLabel));
                Assert.AreEqual("Override Payment Term Description (days from current date)", aspxPage.GetText(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdateLabel));
                Assert.AreEqual("Override If Dealer Invoice Date <", aspxPage.GetText(FieldNames.OverrideIfDealerInvoiceDateLabel));

                Assert.IsTrue(aspxPage.IsElementVisibleOnScreen(FieldNames.SettlementBasedDueDatecalculation, true), ErrorMessages.FieldNotDisplayed);
                Assert.IsTrue(aspxPage.IsElementVisibleOnScreen(FieldNames.TermDescription, true), ErrorMessages.FieldNotDisplayed);
                Assert.IsTrue(aspxPage.IsElementVisibleOnScreen(FieldNames.VariableDueDatecalculation, true), ErrorMessages.FieldNotDisplayed);
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.SettlementBasedDueDatecalculation));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.TermDescription), String.Format(ErrorMessages.InvalidDefaultValue, FieldNames.TermDescription));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.VariableDueDatecalculation), String.Format(ErrorMessages.InvalidDefaultValue, FieldNames.VariableDueDatecalculation));
                Assert.AreEqual("Bi-Weekly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), String.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                Assert.AreEqual("Sunday", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
                Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.EffectiveDate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDate));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementEndDay + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementEndDay + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.BillingCycleStartDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycleStartDate + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));
                Assert.IsTrue(aspxPage.VerifyValue(FieldNames.BillingCycle, "Bi-Weekly", "Daily", "Monthly", "Twice Monthly", "Weekly"), ErrorMessages.ElementNotPresent + ". Billing Cycle Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue(FieldNames.StatementEndDay, "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"), ErrorMessages.ElementNotPresent + ". Statement End Day dropdown.");
                Assert.IsTrue(aspxPage.IsDatePickerClosed("Billing Cycle Start Date DD"), string.Format(FieldNames.BillingCycleStartDate + "Calender Not Displayed"));
                Assert.IsTrue(aspxPage.VerifyValue(FieldNames.StatementType, "One statement per statement period", "One statement per due date"), ErrorMessages.ElementNotPresent + ". Statement Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue(FieldNames.AccelerationType, "By Account", "By Invoice", "None"), ErrorMessages.ElementNotPresent + ". Acceleration Type dropdown.");
                Assert.IsTrue(aspxPage.VerifyValue(FieldNames.EffectiveDateBasedOn, "Dealer Invoice Date", "Settlement Date"), ErrorMessages.ElementNotPresent + ". Effective Date Based On dropdown.");
                Assert.IsTrue(aspxPage.IsDatePickerClosed("Effective Date DD"), string.Format(FieldNames.EffectiveDate + "Calender Not Displayed"));

                aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Daily");
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
                Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
                Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                Assert.AreEqual("Variable", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
                Assert.AreEqual("Daily", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));

                aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Monthly");
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
                Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
                Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                Assert.AreEqual("Variable", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
                Assert.AreEqual("Monthly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                Assert.AreEqual("1", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementEndDay + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementEndDay + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));


                aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Twice Monthly");
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
                Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
                Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                Assert.AreEqual("Variable", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
                Assert.AreEqual("Twice Monthly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                Assert.AreEqual("15th and end of the Month", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementEndDay + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementEndDay + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));

                aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.PaymentTerms), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.PaymentTerms));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.BillingCycle), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.BillingCycle));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementEndDay), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementEndDay));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.StatementType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.StatementType));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.AccelerationType), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.AccelerationType));
                Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                Assert.IsFalse(aspxPage.IsDropDownDisabled(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDateBasedOn));
                Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.EffectiveDate), string.Format(ErrorMessages.TextBoxDisabled, FieldNames.EffectiveDate));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                Assert.AreEqual("Variable", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.PaymentTerms));
                Assert.AreEqual("Weekly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                Assert.AreEqual("Sunday", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                Assert.AreEqual("Dealer Invoice Date", aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.PaymentTerms + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.PaymentTerms + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.BillingCycle + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.BillingCycle + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementEndDay + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementEndDay + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.StatementType + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.StatementType + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDateBasedOn + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDateBasedOn + requiredLabel));
                Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.EffectiveDate + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.EffectiveDate + requiredLabel));



            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23809" })]
        public void TC_23809(string UserType)
        {
            Page.LoadDataOnGrid(masterCode);

            if (Page.IsAnyDataOnGrid())
            {

                string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                string entityType = Page.GetFirstRowData(TableHeaders.EntityType);
                aspxPage.DeleteRelationshipFromDB(corcentricCode, entityType, FieldNames.PaymentTerms);
                var rowCountFromTablerelSenderBefore = AccountMaintenanceUtil.GetRowCountFromTable("relSenderReceiver_tb");
                var rowCountFromTablerelPaymentTermBefore = AccountMaintenanceUtil.GetRowCountFromTable("RelPaymentTerm_tb");
                var rowCountFromTableAuditTrail_tbBefore = AccountMaintenanceUtil.GetRowCountFromTable("AuditTrail_tb");

                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.SelectRelationShipType(FieldNames.PaymentTerms, FieldNames.PaymentTermsTable, menu.RenameMenuField("All Fleets"));
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                aspxPage.SelectValueByScroll(FieldNames.PaymentTerms, "Variable");
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");


                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.SettlementBasedDueDatecalculation));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.TermDescription), String.Format(ErrorMessages.InvalidDefaultValue, FieldNames.TermDescription));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.VariableDueDatecalculation), String.Format(ErrorMessages.InvalidDefaultValue, FieldNames.VariableDueDatecalculation));
                Assert.AreEqual("Bi-Weekly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle), String.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                Assert.AreEqual("Sunday", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                Assert.AreEqual(aspxPage.RenameMenuField("Dealer Invoice Date"), aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.BillingCycleStartDate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.EffectiveDate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDate));

                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));

                aspxPage.Click(FieldNames.SettlementBasedDueDatecalculation);
                Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.SettlementBasedDueDatecalculation));
                aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                aspxPage.SelectValueTableDropDown(FieldNames.StatementEndDay, "Friday");
                aspxPage.EnterTextAfterClear(FieldNames.TermDescription, termDescription);
                aspxPage.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, specialCharacter);
                aspxPage.ClickFieldLabel(FieldNames.VariableDueDatecalculationLabel);
                Assert.IsTrue(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, negativeInterger);
                aspxPage.ClickFieldLabel(FieldNames.VariableDueDatecalculationLabel);
                Assert.IsTrue(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.EnterTextAfterClear(FieldNames.TermDescription, UTFCharac);
                Assert.AreEqual(80, aspxPage.GetValue(FieldNames.TermDescription).Count());
                aspxPage.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, "0");
                aspxPage.ClickFieldLabel(FieldNames.VariableDueDatecalculationLabel);
                Assert.IsFalse(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, validInt);
                aspxPage.ClickFieldLabel(FieldNames.VariableDueDatecalculationLabel);
                Assert.IsFalse(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.Click(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate);
                Assert.IsTrue(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate));
                aspxPage.ClearText(FieldNames.OverrideIfDealerInvoiceDate);
                aspxPage.EnterText(FieldNames.OverrideIfDealerInvoiceDate, negativeInterger);
                aspxPage.ClickFieldLabel(FieldNames.OverrideIfDealerInvoiceDateLabel);
                Assert.IsTrue(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.WaitForAnyElementLocatedBy(FieldNames.OverrideIfDealerInvoiceDate);
                aspxPage.ClearText(FieldNames.OverrideIfDealerInvoiceDate);
                aspxPage.EnterText(FieldNames.OverrideIfDealerInvoiceDate, "1234");
                aspxPage.ClickFieldLabel(FieldNames.OverrideIfDealerInvoiceDateLabel);
                Assert.IsTrue(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-7)));

                aspxPage.WaitForAnyElementLocatedBy(FieldNames.OverrideIfDealerInvoiceDate);
                aspxPage.ClearText(FieldNames.OverrideIfDealerInvoiceDate);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                Assert.IsTrue(aspxPage.CheckForText(aspxPage.RenameMenuField("Override If Dealer Invoice Date < Days From Current Date is required")));

                aspxPage.WaitForAnyElementLocatedBy(FieldNames.OverrideIfDealerInvoiceDate);
                aspxPage.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "123");
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                Assert.IsTrue(aspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));


                var rowCountFromTablerelSenderAfter = AccountMaintenanceUtil.GetRowCountFromTable("relSenderReceiver_tb");
                var rowCountFromTablerelPaymentTermAfter = AccountMaintenanceUtil.GetRowCountFromTable("RelPaymentTerm_tb");
                var rowCountFromTableAuditTrail_tbAfter = AccountMaintenanceUtil.GetRowCountFromTable("AuditTrail_tb");

                Assert.AreNotEqual(rowCountFromTablerelSenderBefore, rowCountFromTablerelSenderAfter);
                Assert.AreNotEqual(rowCountFromTablerelPaymentTermBefore, rowCountFromTablerelPaymentTermAfter);
                Assert.AreNotEqual(rowCountFromTableAuditTrail_tbBefore, rowCountFromTableAuditTrail_tbAfter);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, masterCode);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, aspxPage.RenameMenuField("All Fleets"));

                aspxPage.ClickButtonOnGrid("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Payment Terms");

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

                errorMsgs.AddRange(aspxPage.ValidateTableHeaders("Relationship Grid Table", headers.ToArray()));

                Assert.Multiple(() =>
                {
                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                });

            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23824" })]
        public void TC_23824(string UserType)
        {
            AccountMaintenanceUtil.UpdateEntityToNonCorcentricLocation(masterCode);
            var rowCountFromTableauditTrailBefore = AccountMaintenanceUtil.GetRowCountFromTable("AuditTrail_tb");
            var rowCountFromTablerelPaymentTermBefore = AccountMaintenanceUtil.GetRowCountFromTable("relPaymentTerm_tb");

            Page.LoadDataOnGrid(masterCode);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, masterCode);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, aspxPage.RenameMenuField("All Fleets"));

                aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);

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

                errorMsgs.AddRange(aspxPage.ValidateTableHeaders("Relationship Grid Table", headers.ToArray()));

                aspxPage.ClickAnchorButton("Relationship Grid Table", "Payment Terms Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit);

                aspxPage.WaitForAnyElementLocatedBy(FieldNames.TermDescription, ButtonsAndMessages.Edit);
                aspxPage.EnterTextAfterClear(FieldNames.TermDescription, "Hello World", ButtonsAndMessages.Edit);
                Assert.AreEqual("Variable", aspxPage.GetValueOfDropDown(FieldNames.PaymentTerms, ButtonsAndMessages.Edit), string.Format(ErrorMessages.IncorrectValue, FieldNames.PaymentTerms));
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                aspxPage.ClearText(FieldNames.EffectiveDate, ButtonsAndMessages.Edit);
                aspxPage.EnterText(FieldNames.EffectiveDate, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(1)), ButtonsAndMessages.Edit);
                aspxPage.ClearText(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.Edit);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                aspxPage.UpdateEditGrid(false);
                Assert.IsTrue(aspxPage.CheckForTextByVisibility(aspxPage.RenameMenuField("Override If Dealer Invoice Date < Days From Current Date is required"), true));

                aspxPage.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "123", ButtonsAndMessages.Edit);
                aspxPage.UpdateEditGrid(false);
                aspxPage.WaitForLoadingMessage();
                Assert.IsTrue(aspxPage.CheckForTextByVisibility("Term Description and Variable Due Date Calculation fields can be set only for Corcentric locations.", true));


                aspxPage.SelectValueRowByIndex(FieldNames.PaymentTerms, 3, ButtonsAndMessages.Edit);
                aspxPage.WaitForMsg(" processing...");
                Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate, ButtonsAndMessages.Edit), string.Format(ErrorMessages.CheckBoxUnchecked, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                aspxPage.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "33", ButtonsAndMessages.Edit);
                aspxPage.UpdateEditGrid();
                Assert.AreEqual(ButtonsAndMessages.RecordUpdatedPleaseCloseToExitUpdateForm, aspxPage.GetEditMsg());
                aspxPage.CloseEditGrid();

                var rowCountFromTableauditTrailAfter = AccountMaintenanceUtil.GetRowCountFromTable("AuditTrail_tb");
                var rowCountFromTablerelPaymentTermAfter = AccountMaintenanceUtil.GetRowCountFromTable("relPaymentTerm_tb");

                Assert.AreNotEqual(rowCountFromTableauditTrailBefore, rowCountFromTableauditTrailAfter);
                Assert.AreNotEqual(rowCountFromTablerelPaymentTermBefore, rowCountFromTablerelPaymentTermAfter);

                AccountMaintenanceUtil.UpdateEntityToCorcentricLocation(masterCode);


            }

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, Order(3), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22599" })]
        public void TC_22599(string UserType)
        {

            Page.LoadDataOnGrid(masterCode);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, masterCode);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, aspxPage.RenameMenuField("All Fleets"));

                aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);

                aspxPage.ClickAnchorButton("Relationship Grid Table", "Payment Terms Table Header", TableHeaders.Commands, ButtonsAndMessages.Delete);

                aspxPage.AcceptAlert(out string msg);

                Assert.AreEqual(ButtonsAndMessages.DeleteAlertMessage, msg);

                aspxPage.Click("Delete Second Relation");

                aspxPage.AcceptAlert(out string msg1);

                Assert.AreEqual(ButtonsAndMessages.DeleteAlertMessage, msg1);

                Assert.IsFalse(AccountMaintenanceUtil.IsPaymentTermActive(masterCode));


            }


        }

        [Category(TestCategory.FunctionalTest)]
        [Test, Order(4), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24824" })]
        public void TC_24824(string UserType)
        {

            Page.LoadDataOnGrid(masterCode);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, masterCode); //masterCode
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, aspxPage.RenameMenuField("All Fleets"));

                aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);

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

                errorMsgs.AddRange(aspxPage.ValidateTableHeaders("Relationship Grid Table", headers.ToArray()));

                aspxPage.ClickAnchorButton("Relationship Grid Table", "Payment Terms Table Header", TableHeaders.Commands, ButtonsAndMessages.New);

                aspxPage.WaitForAnyElementLocatedBy(FieldNames.PaymentTerms, ButtonsAndMessages.New);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
                aspxPage.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.PaymentTerms, "Variable", ButtonsAndMessages.New);
                aspxPage.WaitForMsg(" processing...");

                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation, ButtonsAndMessages.New), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.SettlementBasedDueDatecalculation));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.TermDescription, ButtonsAndMessages.New), String.Format(ErrorMessages.InvalidDefaultValue, FieldNames.TermDescription));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.VariableDueDatecalculation, ButtonsAndMessages.New), String.Format(ErrorMessages.InvalidDefaultValue, FieldNames.VariableDueDatecalculation));
                Assert.AreEqual("Bi-Weekly", aspxPage.GetValueOfDropDown(FieldNames.BillingCycle, ButtonsAndMessages.New), String.Format(ErrorMessages.InvalidDefaultValue, FieldNames.BillingCycle));
                Assert.AreEqual("Sunday", aspxPage.GetValueOfDropDown(FieldNames.StatementEndDay, ButtonsAndMessages.New), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementEndDay));
                Assert.AreEqual("One statement per due date", aspxPage.GetValueOfDropDown(FieldNames.StatementType, ButtonsAndMessages.New), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.StatementType));
                Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.AccelerationType, ButtonsAndMessages.New), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.AccelerationType));
                Assert.AreEqual(aspxPage.RenameMenuField("Dealer Invoice Date"), aspxPage.GetValueOfDropDown(FieldNames.EffectiveDateBasedOn, ButtonsAndMessages.New), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDateBasedOn));
                Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.AccelerationProgram, ButtonsAndMessages.New), string.Format(ErrorMessages.DropDownEnabled, FieldNames.AccelerationProgram));
                Assert.AreEqual(string.Empty, aspxPage.GetValue(FieldNames.EffectiveDate, ButtonsAndMessages.New), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.EffectiveDate));
                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate, ButtonsAndMessages.New), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate));
                Assert.IsFalse(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.New), string.Format(ErrorMessages.TextBoxEnabled, FieldNames.OverrideIfDealerInvoiceDate));

                aspxPage.Click(FieldNames.SettlementBasedDueDatecalculation, ButtonsAndMessages.New);
                Assert.IsTrue(aspxPage.IsCheckBoxChecked(FieldNames.SettlementBasedDueDatecalculation, ButtonsAndMessages.New), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.SettlementBasedDueDatecalculation));
                aspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly", ButtonsAndMessages.New);
                aspxPage.WaitForMsg(" processing...");
                aspxPage.EnterTextAfterClear(FieldNames.TermDescription, termDescription, ButtonsAndMessages.New);
                aspxPage.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, specialCharacter, ButtonsAndMessages.New);
                aspxPage.ClickFieldLabel(FieldNames.VariableDueDatecalculationLabel);
                Assert.IsTrue(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, negativeInterger, ButtonsAndMessages.New);
                aspxPage.ClickFieldLabel(FieldNames.PaymentTermsLabel);
                Assert.IsTrue(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                //CON-24859 : [Account Maintenance - Relationships] Inconsistent Term Description Field in Add New Relationship and Edit
                //aspxPage.EnterTextAfterClear(FieldNames.TermDescription, UTFCharac, ButtonsAndMessages.New);
                //aspxPage.ClickFieldLabel(FieldNames.VariableDueDatecalculationLabel);
                //Assert.IsTrue(aspxPage.CheckForTextByVisibility("80 characters allowed only"));
                aspxPage.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, "0", ButtonsAndMessages.New);
                aspxPage.ClickFieldLabel(FieldNames.VariableDueDatecalculationLabel);
                Assert.IsFalse(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.EnterTextAfterClear(FieldNames.VariableDueDatecalculation, validInt, ButtonsAndMessages.New);
                aspxPage.ClickFieldLabel(FieldNames.VariableDueDatecalculationLabel);
                Assert.IsFalse(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.Click(FieldNames.OverridePaymentTermDescriptiondaysfromcurrentdate, ButtonsAndMessages.New);
                Assert.IsTrue(aspxPage.IsTextBoxEnabled(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.New));
                aspxPage.ClearText(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.New);
                aspxPage.EnterText(FieldNames.OverrideIfDealerInvoiceDate, negativeInterger, ButtonsAndMessages.New);
                aspxPage.ClickFieldLabel(FieldNames.OverrideIfDealerInvoiceDateLabel);
                Assert.IsTrue(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.WaitForAnyElementLocatedBy(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.New);
                aspxPage.ClearText(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.New);
                aspxPage.EnterText(FieldNames.OverrideIfDealerInvoiceDate, "1234", ButtonsAndMessages.New);
                aspxPage.ClickFieldLabel(FieldNames.OverrideIfDealerInvoiceDateLabel);
                Assert.IsTrue(aspxPage.CheckForTextByVisibility("Only positive integer upto 3 digit values allowed"));
                aspxPage.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(2)), ButtonsAndMessages.New);

                aspxPage.WaitForAnyElementLocatedBy(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.New);
                aspxPage.ClearText(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.New);
                aspxPage.InsertEditGrid(false);
                Assert.IsTrue(aspxPage.CheckForText(aspxPage.RenameMenuField("Override If Dealer Invoice Date < Days From Current Date is required")));

                aspxPage.WaitForAnyElementLocatedBy(FieldNames.OverrideIfDealerInvoiceDate, ButtonsAndMessages.New);
                aspxPage.EnterTextAfterClear(FieldNames.OverrideIfDealerInvoiceDate, "123", ButtonsAndMessages.New);
                aspxPage.InsertEditGrid();
                Assert.AreEqual(ButtonsAndMessages.RecordInsertedSuccessfully, aspxPage.GetEditMsg());


                aspxPage.ClickButtonOnGrid("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Payment Terms");

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));

                Assert.AreEqual("False", aspxPage.GetValueFromSubGridByIndex(TableHeaders.IsActive, 1));
                Assert.AreEqual("False", aspxPage.GetValueFromSubGridByIndex(TableHeaders.IsActive, 2));
                Assert.AreEqual("True", aspxPage.GetValueFromSubGridByIndex(TableHeaders.IsActive, 3));

                Assert.Multiple(() =>
                {
                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                });

            }

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24637" })]
        public void TC_24637(string UserType)
        {
            string paymentDealer = AccountMaintenanceUtil.GetDealerCodePaymentTerms();
            Page.LoadDataOnGrid(paymentDealer);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Dealer, paymentDealer);
                string paymentFleet = aspxPage.GetFirstValueFromGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet);

                aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);

                EntityDetails getOverridetermDescriptionDetails = AccountMaintenanceUtil.GetOverrideTermDescriptionDetails(paymentDealer, paymentFleet);

                Assert.AreEqual("False", aspxPage.GetFirstValueFromGrid("Relationship Grid Table", "Payment Terms Table Header", TableHeaders.IsOverrideTermDescription));
                Assert.AreEqual("0", aspxPage.GetFirstValueFromGrid("Relationship Grid Table", "Payment Terms Table Header", TableHeaders.OverrideTermDescriptionDays));
                Assert.AreEqual(0, getOverridetermDescriptionDetails.OverrideTermDescriptionDays, GetErrorMessage(ErrorMessages.IncorrectValue));
                Assert.AreEqual(false, getOverridetermDescriptionDetails.IsOverrideTermDescription, GetErrorMessage(ErrorMessages.IncorrectValue));
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22663" })]
        public void TC_22663(string UserType)
        {
            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            string requiredLabel = " Required Label";

            Page.LoadDataOnGrid(masterCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.SelectRelationShipType(FieldNames.CommunityFee, FieldNames.CommunityFeeTable, menu.RenameMenuField("All Fleets"));
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CorcentricElectronicFeePCT), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CorcentricElectronicFeePCT));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CorcentricPaperFeePCT), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CorcentricPaperFeePCT));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CorcentricElectronicCreditFeePCT), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CorcentricElectronicCreditFeePCT));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CorcentricPaperCreditFeePCT), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CorcentricPaperCreditFeePCT));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CommunityFeeAdjustmentRatePCT), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CommunityFeeAdjustmentRatePCT));

                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.CorcentricElectronicFeePCT + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.CorcentricElectronicFeePCT + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.CorcentricPaperFeePCT + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.CorcentricPaperFeePCT + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.CorcentricElectronicCreditFeePCT + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.CorcentricElectronicCreditFeePCT + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.CorcentricPaperCreditFeePCT + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.CorcentricPaperCreditFeePCT + requiredLabel));
                    Assert.IsTrue(aspxPage.IsElementVisible(FieldNames.CommunityFeeAdjustmentRatePCT + requiredLabel), string.Format(ErrorMessages.FieldNotDisabled, FieldNames.CommunityFeeAdjustmentRatePCT + requiredLabel));
                });
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22568" })]
        public void TC_22568(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.AccountMaintenance, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            Page.LoadDataOnGrid(masterCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.Name);

                var aspxPage = new AccountMaintenanceAspx(driver);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ProcessingFee);
                aspxPage.DeleteFirstRelationShipIfExist(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ProcessingFee);

                aspxPage.SelectRelationShipType(FieldNames.ProcessingFee, FieldNames.ProcessingFeeTable, menu.RenameMenuField("All Fleets"));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);

                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProcessingFeeType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProcessingFeeType));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProcessingFeeValueType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProcessingFeeValueType));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ProcessingFeeValue), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ProcessingFeeValue));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.ForCredit), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ForCredit));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TransactionType), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.TransactionType));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.EffectiveDateProcessingFee), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EffectiveDateProcessingFee));

                Assert.IsFalse(aspxPage.IsCheckBoxChecked(FieldNames.ForCredit));
                Assert.IsTrue(aspxPage.IsInputFieldEmpty(FieldNames.ProcessingFeeValue));
                Assert.IsTrue(aspxPage.IsInputFieldEmpty(FieldNames.EffectiveDateProcessingFee));
                Assert.AreEqual("Electronic", aspxPage.GetValueOfDropDown(FieldNames.ProcessingFeeType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.ProcessingFeeType));
                Assert.AreEqual("Amount", aspxPage.GetValueOfDropDown(FieldNames.ProcessingFeeValueType), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.ProcessingFeeValueType));

                Assert.IsTrue(aspxPage.VerifyValueDropDown(FieldNames.ProcessingFeeType, "Electronic", "Paper"), $"{FieldNames.ProcessingFeeType} : " + ErrorMessages.ListElementsMissing);
                Assert.IsTrue(aspxPage.VerifyValueDropDown(FieldNames.ProcessingFeeValueType, "Amount", "Percentage"), $"{FieldNames.ProcessingFeeValueType} : " + ErrorMessages.ListElementsMissing);
                Assert.IsTrue(aspxPage.VerifyDataMultiSelectDropDown(FieldNames.TransactionType, "All", "Fixed", "Fuel", "Miscellaneous", "Parts", "Rental", "Service", "Variable"), $"{FieldNames.TransactionType} : " + ErrorMessages.ListElementsMissing);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22570" })]
        public void TC_22570(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.AccountMaintenance, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);

            Page.LoadDataOnGrid(masterCode);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.Name);

                var aspxPage = new AccountMaintenanceAspx(driver);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ProcessingFee);
                aspxPage.DeleteFirstRelationShipIfExist(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ProcessingFee);

                aspxPage.SelectRelationShipType(FieldNames.ProcessingFee, FieldNames.ProcessingFeeTable, menu.RenameMenuField("All Fleets"));

                aspxPage.EnterTextAfterClear(FieldNames.ProcessingFeeValue, "2");

                aspxPage.SelectDateToday(FieldNames.EffectiveDateProcessingFee);

                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                aspxPage.WaitForMsg(ButtonsAndMessages.Loading);

                aspxPage.Refresh();
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ProcessingFee);
                Assert.AreEqual(aspxPage.GetFirstValueFromGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType), FieldNames.ProcessingFee);

            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22664" })]
        public void TC_22664(string UserType)
        {
            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            string requiredLabel = " Required Label";
            string number = "10";
            var rowCountFromTablerelSenderReceiverBefore = AccountMaintenanceUtil.GetRowCountFromTable("relSenderReceiver_tb");
            var rowCountFromTableCommunityFeeBefore = AccountMaintenanceUtil.GetRowCountFromTable("relCommunityFee_tb");
            Page.LoadDataOnGrid(masterCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.DeleteFirstRelationShipIfExist("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Community Fee");
                aspxPage.SelectRelationShipType(FieldNames.CommunityFee, FieldNames.CommunityFeeTable, menu.RenameMenuField("All Fleets"));
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CorcentricElectronicFeePCT), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CorcentricElectronicFeePCT));
                    aspxPage.EnterTextAfterClear(FieldNames.CorcentricElectronicFeePCT, number);
                    aspxPage.EnterTextAfterClear(FieldNames.CorcentricPaperFeePCT, number);
                    aspxPage.EnterTextAfterClear(FieldNames.CorcentricElectronicCreditFeePCT, number);
                    aspxPage.EnterTextAfterClear(FieldNames.CorcentricPaperCreditFeePCT, number);
                    aspxPage.EnterTextAfterClear(FieldNames.CommunityFeeAdjustmentRatePCT, number);

                    aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                });

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CommunityFee);
                string communityFee = aspxPage.GetFirstValueFromGrid("RelationShipTable", "RelationShipTableHeader", FieldNames.Fleet);

                Assert.AreEqual(aspxPage.GetFirstValueFromGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType), FieldNames.CommunityFee);

                var rowCountFromTablerelSenderReceiverAfter = AccountMaintenanceUtil.GetRowCountFromTable("relSenderReceiver_tb");
                var rowCountFromTableCommunityFeeAfter = AccountMaintenanceUtil.GetRowCountFromTable("relCommunityFee_tb");

                Assert.AreNotEqual(rowCountFromTablerelSenderReceiverBefore, rowCountFromTablerelSenderReceiverAfter);
                Assert.AreNotEqual(rowCountFromTableCommunityFeeBefore, rowCountFromTableCommunityFeeAfter);

            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22572" })]
        public void TC_22572(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Pages.AccountMaintenance, Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);

            Page.LoadDataOnGrid(masterCode);

            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(TableHeaders.Name);

                var aspxPage = new AccountMaintenanceAspx(driver);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ProcessingFee);
                string processingFee = aspxPage.GetFirstValueFromGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType);
                if (processingFee != FieldNames.ProcessingFee)
                {
                    aspxPage.SelectRelationShipType(FieldNames.ProcessingFee, FieldNames.ProcessingFeeTable, menu.RenameMenuField("All Fleets"));

                    aspxPage.EnterTextAfterClear(FieldNames.ProcessingFeeValue, "2");

                    aspxPage.SelectDateToday(FieldNames.EffectiveDateProcessingFee);

                    aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                    aspxPage.WaitForMsg(ButtonsAndMessages.Loading);
                }

                aspxPage.Refresh();
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ProcessingFee);
                string processingFeeNew = aspxPage.GetFirstValueFromGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType);

                aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.ProcessingFee);

                List<string> errorMsgs = new List<string>();
                List<string> headers = new List<String>()
                    {
                        TableHeaders.Commands,
                        TableHeaders.FeeType,
                        TableHeaders.FeeValueType,
                        TableHeaders.FeeValue,
                        TableHeaders.Active,
                        TableHeaders.StartDate,
                        TableHeaders.EndDate,
                        TableHeaders.ForCredit,
                        TableHeaders.TransactionType

                    };

                errorMsgs.AddRange(aspxPage.ValidateTableHeaders("Relationship Grid Table", headers.ToArray()));

                Assert.Multiple(() =>
                {
                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                });
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22665" })]
        public void TC_22665(string UserType)
        {
            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);
            string number = "10";

            Page.LoadDataOnGrid(masterCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CommunityFee);

                string communityFee = aspxPage.GetFirstValueFromGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType);
                if (communityFee != FieldNames.CommunityFee)
                {
                    aspxPage.SelectRelationShipType(FieldNames.CommunityFee, FieldNames.CommunityFeeTable, menu.RenameMenuField("All Fleets"));
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.Multiple(() =>
                    {
                        Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.CorcentricElectronicFeePCT), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CorcentricElectronicFeePCT));
                        aspxPage.EnterTextAfterClear(FieldNames.CorcentricElectronicFeePCT, number);
                        aspxPage.EnterTextAfterClear(FieldNames.CorcentricPaperFeePCT, number);
                        aspxPage.EnterTextAfterClear(FieldNames.CorcentricElectronicCreditFeePCT, number);
                        aspxPage.EnterTextAfterClear(FieldNames.CorcentricPaperCreditFeePCT, number);
                        aspxPage.EnterTextAfterClear(FieldNames.CommunityFeeAdjustmentRatePCT, number);

                        aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                    });
                }

                aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CommunityFee);

                List<string> errorMsgs = new List<string>();

                List<string> headers = new List<String>()
                    {
                        TableHeaders.Commands,
                        TableHeaders.CorcentricElectronicFeeType,
                        TableHeaders.CorcentricElectronicFeeValue,
                        TableHeaders.CorcentricPaperFeeType,
                        TableHeaders.CorcentricPaperFeeValue,
                        TableHeaders.CorcentricElectronicCreditFeeType,
                        TableHeaders.CorcentricElectronicCreditFeeValue,
                        TableHeaders.CorcentricPaperCreditFeeType,
                        TableHeaders.CorcentricPaperCreditFeeValue,
                        TableHeaders.CommunityFeeAdjustmentRateType,
                        TableHeaders.CommunityFeeAdjustmentRateValue,

                    };

                errorMsgs.AddRange(aspxPage.ValidateTableHeaders("Relationship Grid Table", headers.ToArray()));

                Page.ClickHyperLinkOnGrid(TableHeaders.Commands);
                aspxPage.WaitForElementToBeClickable("Corcentric Electronic Fee Value Edit Box");

                decimal corcentricEFValueBefore = AccountMaintenanceUtil.GetFieldValues();

                double newNumber;
                string val = aspxPage.GetValue("Corcentric Electronic Fee Value Edit Box");
                double.TryParse(val, out newNumber);

                aspxPage.EnterTextAfterClear("Corcentric Electronic Fee Value Edit Box", (newNumber + 1).ToString());
                aspxPage.UpdateEditGrid();

                decimal corcentricEFValueAfter = AccountMaintenanceUtil.GetFieldValues();

                Assert.AreNotEqual(corcentricEFValueBefore, corcentricEFValueAfter);
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22666" })]
        public void TC_22666(string UserType)
        {
            string masterCode = AccountMaintenanceUtil.GetDealerCode(LocationType.Master);

            Page.LoadDataOnGrid(masterCode);
            if (Page.IsAnyDataOnGrid())
            {
                Page.ClickHyperLinkOnGrid(FieldNames.Name);

                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.CommunityFee);

                aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);

                aspxPage.AcceptAlert(ButtonsAndMessages.OK);
                aspxPage.WaitForLoadingMessage();

                Assert.IsFalse(AccountMaintenanceUtil.GetRelSenderReceiverByDealerCode(masterCode, FieldNames.CommunityFee).IsActive, ErrorMessages.DeleteOperationFailed);

            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "16395" })]
        public void TC_16395(string UserType)
        {
            Page.LoadDataOnGrid("", FieldNames.Fleet);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            if ((Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0))
            {
                Assert.Multiple(() =>
                {
                    AccountMaintenanceUtil.DeleteTaxRelationship(corcentricCode);
                    Page.ClickHyperLinkOnGrid(FieldNames.Name);
                    aspxPage.SelectRelationShipType(FieldNames.Tax, FieldNames.TaxTable, menu.RenameMenuField("All Dealers"));
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.FETTaxExempt, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxIDNumber, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxExemptNumber, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.InvoiceMustHaveDealersVATTaxIDforCADDealer, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.TaxCalculation), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.TaxCalculation));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.TaxType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.TaxType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LineTypeTax), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LineTypeTax));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LinePriceType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LinePriceType));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxRate, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.TaxTypeNotRequired), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.TaxTypeNotRequired));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                });
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24746" })]
        public void TC_24746(string UserType)
        {
            Page.LoadDataOnGrid("", FieldNames.Dealer);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            if ((Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0))
            {
                Assert.Multiple(() =>
                {
                    AccountMaintenanceUtil.DeleteTaxRelationship(corcentricCode);
                    Page.ClickHyperLinkOnGrid(FieldNames.Name);
                    aspxPage.SelectRelationShipType(FieldNames.Tax, FieldNames.TaxTable, menu.RenameMenuField("All Fleets"));
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.FETTaxExempt, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxIDNumber, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxExemptNumber, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.InvoiceMustHaveDealersVATTaxIDforCADDealer, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(!aspxPage.IsDropDownDisabled(FieldNames.TaxCalculation), GetErrorMessage(ErrorMessages.DropDownDisabled, FieldNames.TaxCalculation));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.TaxType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.TaxType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LineTypeTax), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LineTypeTax));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LinePriceType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LinePriceType));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxRate, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(!aspxPage.IsDropDownDisabled(FieldNames.TaxTypeNotRequired), GetErrorMessage(ErrorMessages.DropDownDisabled, FieldNames.TaxTypeNotRequired));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                    Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.TaxCalculation), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.TaxCalculation));
                    aspxPage.OpenDropDown(FieldNames.TaxCalculation);
                    Assert.IsTrue(aspxPage.VerifyValueDropDown(FieldNames.TaxCalculation, "Avalara-AP and AR both", "Avalara-AR only", "Fixed Rate-AR", "None"), $"{FieldNames.TaxCalculation} : " + ErrorMessages.ListElementsMissing);
                    aspxPage.SelectValueTableDropDown(FieldNames.TaxCalculation, "Avalara-AP and AR both");
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.TaxType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.TaxType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LineTypeTax), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LineTypeTax));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LinePriceType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LinePriceType));
                    Assert.IsTrue(!aspxPage.IsElementEnabled(FieldNames.TaxRate, false), ErrorMessages.ElementEnabled);
                    aspxPage.OpenDropDown(FieldNames.TaxCalculation);
                    aspxPage.SelectValueTableDropDown(FieldNames.TaxCalculation, "Avalara-AR only");
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.TaxType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.TaxType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LineTypeTax), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LineTypeTax));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LinePriceType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LinePriceType));
                    Assert.IsTrue(!aspxPage.IsElementEnabled(FieldNames.TaxRate, false), ErrorMessages.ElementEnabled);
                    Task t = Task.Run(() => aspxPage.WaitForStalenessOfElement(FieldNames.TaxType));
                    aspxPage.OpenDropDown(FieldNames.TaxCalculation);
                    aspxPage.SelectValueTableDropDown(FieldNames.TaxCalculation, "Fixed Rate-AR");
                    t.Wait();
                    t.Dispose();
                    Assert.IsTrue(!aspxPage.IsDropDownDisabled(FieldNames.TaxType), GetErrorMessage(ErrorMessages.DropDownDisabled, FieldNames.TaxType));
                    Assert.IsTrue(!aspxPage.IsDropDownDisabled(FieldNames.LineTypeTax), GetErrorMessage(ErrorMessages.DropDownDisabled, FieldNames.LineTypeTax));
                    Assert.IsTrue(!aspxPage.IsDropDownDisabled(FieldNames.LinePriceType), GetErrorMessage(ErrorMessages.DropDownDisabled, FieldNames.LinePriceType));
                    Assert.IsTrue(aspxPage.IsElementEnabled(FieldNames.TaxRate, false), ErrorMessages.ElementNotEnabled);
                    Assert.IsTrue(aspxPage.VerifyValueScrollable(FieldNames.TaxType, "Composite", "County", "Environmental", "GST", "HST", "Local/City", "PST", "QST", "Special", "State", "VAT"), $" {FieldNames.TaxType} : " + ErrorMessages.ListElementsMissing);
                    Assert.IsTrue(aspxPage.VerifyDataMultiSelectDropDown(FieldNames.LineTypeTax, "Adjustment Per Transaction", "All", "Environmental", "Expense", "Fixed", "Freight", "Fuel", "Labor", "Miscellaneous", "Parts"), $" {FieldNames.LineTypeTax} : " + ErrorMessages.ListElementsMissing);
                    aspxPage.ClickFieldLabelWithText("Line Type");
                    if (aspxPage.IsNextPageMultiSelectDropdown(FieldNames.LineTypeTax))
                    {
                        Assert.IsTrue(aspxPage.VerifyDataMultiSelectDropDown(FieldNames.LineTypeTax, "Rental", "Shop Supplies", "Sublet Labor", "Sublet Part", "Tax", "Variable"), $" {FieldNames.LineTypeTax} : " + ErrorMessages.ListElementsMissing);
                    }
                    Assert.IsTrue(aspxPage.VerifyDataMultiSelectDropDown(FieldNames.LinePriceType, "All", "Cores", "FET", "Units"), $" {FieldNames.LinePriceType} : " + ErrorMessages.ListElementsMissing);
                    Assert.IsTrue(aspxPage.VerifyDataMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Composite", "County", "Environmental", "GST", "HST", "Local/City", "PST", "QST", "Special", "State"), $" {FieldNames.TaxTypeNotRequired} : " + ErrorMessages.ListElementsMissing);
                    aspxPage.ClickFieldLabelWithText(FieldNames.TaxTypeNotRequired);
                    if (aspxPage.IsNextPageMultiSelectDropdown(FieldNames.TaxTypeNotRequired))
                    {
                        Assert.IsTrue(aspxPage.VerifyDataMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "VAT"), $" {FieldNames.TaxTypeNotRequired} : " + ErrorMessages.ListElementsMissing);
                    }
                });
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25504" })]
        public void TC_25504(string UserType)
        {
            Page.LoadDataOnGrid("", FieldNames.Dealer);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            List<string> errorMsgs = new List<string>();
            if ((Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0))
            {
                Assert.Multiple(() =>
                {
                    AccountMaintenanceUtil.DeleteTaxRelationship(corcentricCode);
                    Page.ClickHyperLinkOnGrid(FieldNames.Name);
                    aspxPage.SelectRelationShipType(FieldNames.Tax, FieldNames.TaxTable, menu.RenameMenuField("All Fleets"));
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.FETTaxExempt, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxIDNumber, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxExemptNumber, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.InvoiceMustHaveDealersVATTaxIDforCADDealer, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(!aspxPage.IsDropDownDisabled(FieldNames.TaxCalculation), GetErrorMessage(ErrorMessages.DropDownDisabled, FieldNames.TaxCalculation));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.TaxType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.TaxType));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LineTypeTax), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LineTypeTax));
                    Assert.IsTrue(aspxPage.IsDropDownDisabled(FieldNames.LinePriceType), GetErrorMessage(ErrorMessages.DropDownEnabled, FieldNames.LinePriceType));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.TaxRate, false), ErrorMessages.FieldNotDisplayed);
                    Assert.IsTrue(!aspxPage.IsDropDownDisabled(FieldNames.TaxTypeNotRequired), GetErrorMessage(ErrorMessages.DropDownDisabled, FieldNames.TaxTypeNotRequired));
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Add_pascal), ErrorMessages.AddButtonMissing);
                    Assert.IsTrue(aspxPage.IsButtonVisible(ButtonsAndMessages.Cancel), ErrorMessages.CancelButtonMissing);
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "Composite");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "County");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "Environmental");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "GST");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "HST");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "Local/City");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "PST");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "QST");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "Special");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "State");
                    aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Types", "VAT");
                    aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                    Assert.IsTrue(aspxPage.CheckForText("Record has been added successfully", true));
                    aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.Tax);
                    aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, "All Fleets");
                    int rowCount = aspxPage.GetRowCount(FieldNames.RelationShipTable, true);
                    if (rowCount > 0)
                    {
                        errorMsgs.Add(ErrorMessages.GridRowCountMisMatch + $". After deleting Tax relationship expected row count is [0] actual row count [{rowCount}]");
                    }
                });
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25505" })]
        public void TC_25505(string UserType)
        {
            Page.LoadDataOnGrid("", FieldNames.Dealer);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            List<string> errorMsgs = new List<string>();
            try
            {
                if ((Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0))
                {
                    Assert.Multiple(() =>
                    {

                        Page.ClickHyperLinkOnGrid(FieldNames.Name);
                        AccountMaintenanceUtil.DeleteTaxRelationship(corcentricCode);
                        aspxPage.AddTaxRelationshipDealerSide("All Fleets");
                        aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.Tax);
                        aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, "All Fleets");
                        aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.Tax);
                        aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
                        aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Type not Required", "State");
                        aspxPage.SelectValueMultiSelectDropDown(FieldNames.TaxTypeNotRequired, "Tax Type not Required", "VAT");
                        aspxPage.UpdateEditGrid();
                        string updateMsg = aspxPage.GetEditMsg();
                        Assert.IsTrue(aspxPage.WaitForEditMsgChangeText().Contains(ButtonsAndMessages.RecordUpdatedMessage), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordUpdatedMessage, aspxPage.GetEditMsg()));
                    });
                }
            }
            finally
            {
                AccountMaintenanceUtil.DeleteTaxRelationship(corcentricCode);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25585" })]
        public void TC_25585(string UserType)
        {
            Page.LoadDataOnGrid("", FieldNames.Dealer);
            string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
            List<string> errorMsgs = new List<string>();
            try
            {
                if ((Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0))
                {
                    Assert.Multiple(() =>
                    {
                        AccountMaintenanceUtil.DeleteTaxRelationship(corcentricCode);
                        Page.ClickHyperLinkOnGrid(FieldNames.Name);
                        aspxPage.AddTaxRelationshipDealerSide("All Fleets");
                        aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.Tax);
                        aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, "All Fleets");
                        aspxPage.ClickAnchorButton(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, "#", ButtonsAndMessages.Delete);
                        Assert.AreEqual(aspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteAlertMessage);
                        aspxPage.AcceptAlert(ButtonsAndMessages.DeleteAlertMessage);
                        aspxPage.WaitForLoadingMessage();
                        Assert.IsFalse(AccountMaintenanceUtil.GetRelSenderReceiverByDealerCode(corcentricCode, FieldNames.Tax).IsActive, ErrorMessages.DeleteOperationFailed);
                    });
                }
            }
            finally
            {
                AccountMaintenanceUtil.DeleteTaxRelationship(corcentricCode);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22695" })]
        public void TC_22695(string UserType)
        {
            Page.LoadDataOnGrid("", FieldNames.Dealer);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                AccountMaintenanceUtil.DeleteEnhancedDuplicateCheckRelationship(corcentricCode);
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.SelectRelationShipType(FieldNames.EnhancedDuplicateCheck, FieldNames.EnhancedDuplicateCheckTable, menu.RenameMenuField("All Fleets"));
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.MatchOnFleet), GetErrorMessage(ErrorMessages.FieldNotDisabled, FieldNames.MatchOnFleet));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.NumberOfYearsFromPreviousDealerInvoiceDate), GetErrorMessage(ErrorMessages.ElementNotPresent, FieldNames.NumberOfYearsFromPreviousDealerInvoiceDate));
                    Assert.AreEqual("None", aspxPage.GetValueOfDropDown(FieldNames.MatchOnFleet), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.MatchOnFleet));
                    Assert.AreEqual("0", aspxPage.GetValue(FieldNames.NumberOfYearsFromPreviousDealerInvoiceDate), string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.NumberOfYearsFromPreviousDealerInvoiceDate));
                    Assert.IsTrue(aspxPage.VerifyValueDropDown(FieldNames.MatchOnFleet, "None", "Bill To", "Ship To"), $"{FieldNames.MatchOnFleet} DD: " + ErrorMessages.ListElementsMissing);
                });
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22696" })]
        public void TC_22696(string UserType)
        {
            Page.LoadDataOnGrid("", FieldNames.Dealer);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                try
                {
                    AccountMaintenanceUtil.DeleteEnhancedDuplicateCheckRelationship(corcentricCode);
                    Page.ClickHyperLinkOnGrid(FieldNames.Name);
                    aspxPage.SelectRelationShipType(FieldNames.EnhancedDuplicateCheck, FieldNames.EnhancedDuplicateCheckTable, "All Fleets");
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                    Assert.IsTrue(aspxPage.CheckForText(ButtonsAndMessages.RecordAddedSuccessfully, true), $"{FieldNames.EnhancedDuplicateCheck} : " + ButtonsAndMessages.RecordAddingError);
                    aspxPage.Refresh();
                    aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck);
                    Assert.AreEqual(aspxPage.GetFirstValueFromGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType), FieldNames.EnhancedDuplicateCheck, $"{FieldNames.EnhancedDuplicateCheck}: " + ButtonsAndMessages.RelationshipNotFound);

                }

                finally
                {
                    AccountMaintenanceUtil.DeleteEnhancedDuplicateCheckRelationship(corcentricCode);
                }
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22698" })]
        public void TC_22698(string UserType)
        {
            Page.LoadDataOnGrid("", FieldNames.Dealer);
            if (Page.IsAnyDataOnGrid())
            {
                string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                Page.ClickHyperLinkOnGrid(FieldNames.Name);
                aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck);
                if (!aspxPage.IsRelationExist(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck))
                {
                    aspxPage.SelectRelationShipType(FieldNames.EnhancedDuplicateCheck, FieldNames.EnhancedDuplicateCheckTable, "All Fleets");
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                    Assert.IsTrue(aspxPage.CheckForText(ButtonsAndMessages.RecordAddedSuccessfully, true), $"{FieldNames.EnhancedDuplicateCheck} : " + ButtonsAndMessages.RecordAddingError);
                    aspxPage.Refresh();
                    aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck);
                }
                aspxPage.DeleteFirstRelationShipIfExist(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck);
                Assert.IsFalse(AccountMaintenanceUtil.GetRelSenderReceiverByDealerCode(corcentricCode, FieldNames.EnhancedDuplicateCheck).IsActive, ErrorMessages.DeleteOperationFailed);
                Assert.IsFalse(aspxPage.IsRelationExist(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck), $"{FieldNames.EnhancedDuplicateCheck} : " + ErrorMessages.DeleteOperationFailed);

            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22697" })]
        public void TC_22697(string UserType)
        {
            string corcentricCode = string.Empty;
            try
            {
                Page.LoadDataOnGrid("", FieldNames.Dealer);
                if (Page.IsAnyDataOnGrid())
                {
                    corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                    Page.ClickHyperLinkOnGrid(FieldNames.Name);
                    aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck);
                    if (!aspxPage.IsRelationExist(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck))
                    {
                        aspxPage.SelectRelationShipType(FieldNames.EnhancedDuplicateCheck, FieldNames.EnhancedDuplicateCheckTable, "All Fleets");
                        aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");

                        aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
                        Assert.IsTrue(aspxPage.CheckForText(ButtonsAndMessages.RecordAddedSuccessfully, true), $"{FieldNames.EnhancedDuplicateCheck} : " + ButtonsAndMessages.RecordAddingError);
                        aspxPage.Refresh();
                        aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck);
                        aspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.Fleet, "All Fleets");
                    }
                    aspxPage.ClickButtonOnGrid(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.EnhancedDuplicateCheck);
                    aspxPage.ClickAnchorButton(FieldNames.MainTable, FieldNames.MainTableHeader, TableHeaders.Commands, ButtonsAndMessages.Edit, true);
                    aspxPage.WaitForLoadingMessage();
                    Assert.Multiple(() =>
                    {
                        Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.MatchOnFleetEdit), $"{FieldNames.MatchOnFleetEdit} : " + ErrorMessages.ElementNotPresent);
                        Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.NumberOfYearsFromPreviousDealerInvoiceDateEdit), $"{FieldNames.NumberOfYearsFromPreviousDealerInvoiceDateEdit} : " + ErrorMessages.ElementNotPresent);
                        Assert.IsTrue(aspxPage.IsAnchorVisible(ButtonsAndMessages.Update, true), $"{ButtonsAndMessages.Update} : " + ErrorMessages.ButtonNotFoundOnGrid);
                        Assert.IsTrue(aspxPage.IsAnchorVisible(ButtonsAndMessages.Close, true), $"{ButtonsAndMessages.Close} : " + ErrorMessages.ButtonNotFoundOnGrid);
                    });
                    aspxPage.WaitForElementToBeClickable(FieldNames.MatchOnFleetEdit);
                    aspxPage.SelectValueByScroll(FieldNames.MatchOnFleetEdit, "Ship To");
                    aspxPage.EnterTextAfterClear(FieldNames.NumberOfYearsFromPreviousDealerInvoiceDateEdit, "2");
                    aspxPage.UpdateEditGrid();
                    Assert.AreEqual(ButtonsAndMessages.RecordUpdatedPleaseCloseToExitUpdateForm, aspxPage.GetEditMsg(), $"{FieldNames.EnhancedDuplicateCheck} : " + ErrorMessages.RecordNotUpdated);
                    string numberOfYears = AccountMaintenanceUtil.GetNumberOfYears(corcentricCode);
                    Assert.IsFalse(string.IsNullOrEmpty(numberOfYears), "Number of Years returned empty from DB");
                    Assert.AreEqual(numberOfYears, "2", "Numbers of years returned from DB is not the one set while editing.");
                }
                else
                {
                    Assert.Fail(ErrorMessages.NoDataOnGrid);
                }
            }
            finally
            {
                AccountMaintenanceUtil.DeleteEnhancedDuplicateCheckRelationship(corcentricCode);
            }
        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "15907" })]
        public void TC_15907(string UserType)
        {
            string requiredLabel = " Required Label";
            Page.LoadDataOnGrid("", FieldNames.Dealer);
            if (Page.IsAnyDataOnGrid() && Page.GetRowCountCurrentPage() > 0)
            {
                string corcentricCode = Page.GetFirstRowData(TableHeaders.AccountCode);
                AccountMaintenanceUtil.DeleteCorePriceTypeRelationship(corcentricCode);
                CommonUtils.ToggleSeperateARAPCalcsToken(false);
                Page.ClickHyperLinkOnGrid(FieldNames.Name); 
                aspxPage.SelectRelationShipType(FieldNames.CorePriceType, FieldNames.CorePriceTypeTable, menu.RenameMenuField("All Fleets"));
                aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PricingType + requiredLabel), GetErrorMessage(ErrorMessages.FieldNotMarkedMandatory, FieldNames.PricingType));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.PrecendenceOrder + requiredLabel), GetErrorMessage(ErrorMessages.FieldNotMarkedMandatory, FieldNames.PrecendenceOrder));
                    Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.AccountingDocumentType + requiredLabel), GetErrorMessage(ErrorMessages.FieldNotMarkedMandatory, FieldNames.AccountingDocumentType)); 
                    Assert.IsTrue(aspxPage.VerifyValueDropDown(FieldNames.PricingType, FieldNames.Centralized, FieldNames.Combined, FieldNames.Decentralized), $"{FieldNames.PricingType} : " + ErrorMessages.ListElementsMissing);
                    Assert.IsTrue(aspxPage.VerifyValueDropDown(FieldNames.PrecendenceOrder, "N/A"), $"{FieldNames.PrecendenceOrder} : " + ErrorMessages.ListElementsMissing);
                    Assert.AreEqual(FieldNames.Centralized, aspxPage.GetValueOfDropDown(FieldNames.PricingType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PricingType));
                    Assert.AreEqual("N/A", aspxPage.GetValueOfDropDown(FieldNames.PrecendenceOrder), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PrecendenceOrder));
                    Assert.That(aspxPage.GetValueOfDropDown(FieldNames.AccountingDocumentType), Is.Null.Or.Empty, GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.AccountingDocumentType));
                    CommonUtils.ToggleSeperateARAPCalcsToken(true);
                    aspxPage.Refresh();
                    aspxPage.SelectRelationShipType(FieldNames.CorePriceType, FieldNames.CorePriceTypeTable, menu.RenameMenuField("All Fleets"));
                    aspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
                    Assert.AreEqual(FieldNames.Centralized, aspxPage.GetValueOfDropDown(FieldNames.PricingType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PricingType));
                    Assert.AreEqual("N/A", aspxPage.GetValueOfDropDown(FieldNames.PrecendenceOrder), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.PrecendenceOrder));
                    Assert.AreEqual(FieldNames.APOnly, aspxPage.GetValueOfDropDown(FieldNames.AccountingDocumentType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.AccountingDocumentType));
                });
            }
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "26382" })]
        public void TC_26382(string UserType)
        {
            string billingCode = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            string fleetName = "Automate_FleetShop" + CommonUtils.RandomString(4);
            string email = CommonUtils.RandomString(4) + "@gmail.com";
            string state = "AL";
            string zip = "24546";
            Page.LoadDataOnGrid(billingCode);
            if (!Page.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for BillingFleetCode [{billingCode}]");
            }

            Page.ClickHyperLinkOnGrid(FieldNames.Name);
            aspxPage.ClickListElements("TabList", "Account Configuration");
            aspxPage.SwitchIframe();
            Assert.IsTrue(aspxPage.VerifyTableData("Account Configuration Tabs", "Information", "Contacts", "Additional Info", "New Locations", "Notes"));
            aspxPage.ClickTab("Account Configuration Tabs", "Contacts");
            ;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Type), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Type));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.FirstName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.LastName), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Title), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Title));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Phone), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Phone));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Fax), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Fax));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Email), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
                Assert.AreEqual(aspxPage.IsElementDisplayed(FieldNames.Language), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Address1), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address1));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Address2), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Address2));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.City), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.City));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Country), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Country));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.State), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.State));
                Assert.IsTrue(aspxPage.IsElementDisplayed(FieldNames.Zip), string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Zip));

            });

            aspxPage.SelectValueByScroll(FieldNames.Type, "AR");
            aspxPage.EnterText(FieldNames.FirstName, fleetName);
            aspxPage.EnterText(FieldNames.LastName, fleetName);
            aspxPage.EnterText(FieldNames.Title, "title1");
            aspxPage.EnterText(FieldNames.Phone, "(999)999-9999");
            aspxPage.EnterText(FieldNames.Email, email);
            aspxPage.SelectValueByScroll(FieldNames.Language, "en-US");
            aspxPage.EnterText(FieldNames.Address1, "Address1");
            aspxPage.EnterText(FieldNames.City, "City1");

            if (aspxPage.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (aspxPage.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                aspxPage.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }

            aspxPage.ClearText(FieldNames.Zip);
            aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);

            if (aspxPage.CheckForText("Please enter zip/postal code."))
            {
                aspxPage.EnterText(FieldNames.Zip, zip);
                aspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);

                if (aspxPage.CheckForText("There was an error. Please contact system administrator for further details.") || aspxPage.CheckForText("Please enter zip / postal code."))
                {
                    Assert.Fail($"Some error occurred while creating entity.");
                }
            }

            EntityDetails zipAndStateDetails = AccountMaintenanceUtil.GetZipAndState(fleetName);
            Assert.AreEqual(zip, zipAndStateDetails.ZipCode, GetErrorMessage(ErrorMessages.IncorrectValue));
            Assert.AreEqual(state, zipAndStateDetails.State, GetErrorMessage(ErrorMessages.IncorrectValue));
        }
    }
}
