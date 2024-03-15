using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.AccountMaintenance;
using AutomationTesting_CorConnect.PageObjects.BillingScheduleManagement;
using AutomationTesting_CorConnect.PageObjects.CreateAuthorization;
using AutomationTesting_CorConnect.PageObjects.CreateNewEntity;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.FleetPOPOQTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.PageObjects.MasterBillingStatementConfiguration;
using AutomationTesting_CorConnect.PageObjects.MasterBillingStatementConfiguration.CreateNewMasterBillingStatementConfiguration;
using AutomationTesting_CorConnect.PageObjects.Parts;
using AutomationTesting_CorConnect.PageObjects.POEntry;
using AutomationTesting_CorConnect.PageObjects.POQEntry;
using AutomationTesting_CorConnect.PageObjects.Price;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.Parts;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.OrderedTests
{

    [TestFixture, Timeout(600000)]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Entity Invoice Smoke")]
    internal class EntityInvoiceSmoke : DriverBuilderClass
    {
        private string dealerName = "Automation_Dealer" + CommonUtils.RandomString(4);
        private string fleetName = "Automation_Fleet" + CommonUtils.RandomString(4);
        private string dealerAccountingCode = "dealerAccCode" + CommonUtils.RandomString(4);
        private string communityStatementName = "communityStatement" + CommonUtils.RandomString(4);
        private string partNumber = CommonUtils.RandomString(6);
        private string dealerInvNum = CommonUtils.RandomString(6) + CommonUtils.RandomString(4) + CommonUtils.GetTime();
        private string transactionInvNum = CommonUtils.RandomString(6) + CommonUtils.RandomString(6) + CommonUtils.GetTime();
        private string transactionInvNum2 = CommonUtils.RandomString(6) + CommonUtils.RandomString(6) + CommonUtils.GetTime();
        private string authCode = null;
        private string authCode2 = null;
        private string draftFleetPONum = CommonUtils.RandomString(6) + CommonUtils.RandomString(4) + CommonUtils.GetTime();
        private string submittedFleetPONum = CommonUtils.RandomString(6) + CommonUtils.RandomString(4) + CommonUtils.GetTime();
        private string draftDealerPOQNum = CommonUtils.RandomString(6) + CommonUtils.RandomString(4) + CommonUtils.GetTime();
        private string submittedDealerPOQNum = CommonUtils.RandomString(6) + CommonUtils.RandomString(4) + CommonUtils.GetTime();

        CreateNewEntityPage CreateNewEntityPage;
        MasterBillingStatementConfigurationPage MasterBillingStatementConfigurationPage;
        AccountMaintenancePage AccountMaintenancePage;
        AccountMaintenanceAspx AccountMaintenanceaspxPage;
        PartsPage PartsPage;
        PricePage PricePage;
        PriceDetailPage priceDetPage;
        BillingScheduleManagementPage BillingScheduleManagementPage;
        InvoiceEntryPage InvoiceEntryPage;
        CreateAuthorizationPage CreateAuthorizationPage;
        FleetInvoiceTransactionLookupPage FITLPage;
        InvoiceOptionsPage InvoiceOptionPopUpPage;
        CreateNewInvoicePage CreateNewInvoicePage;
        InvoiceOptionsAspx InvoiceOptionsAspxPage;
        POEntryPage POEntryPage;
        FleetPOPOQTransactionLookupPage FleetPOPOQTransactionLookupPage;
        POQEntryPage POQEntryPage;

        [Category(TestCategory.Smoke)]
        [Test, Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23550" })]
        public void TC_23550(string UserType, int Order)
        {
            menu.OpenPopUpPage(Pages.CreateNewEntity);
            CreateNewEntityPage = new CreateNewEntityPage(driver);
            CreateNewEntityPage.EnterText(FieldNames.DisplayName, dealerName);
            CreateNewEntityPage.EnterText(FieldNames.LegalName, dealerName);
            Task t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
            CreateNewEntityPage.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (CreateNewEntityPage.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                CreateNewEntityPage.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
            CreateNewEntityPage.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            CreateNewEntityPage.EnterText(FieldNames.AccountCode, dealerName);
            CreateNewEntityPage.EnterText(FieldNames.AccountingCode, dealerName);
            CreateNewEntityPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(CreateNewEntityPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            CreateNewEntityPage.EnterText(FieldNames.Address1, dealerName);
            CreateNewEntityPage.EnterText(FieldNames.City, dealerName);
            if (CreateNewEntityPage.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                CreateNewEntityPage.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (CreateNewEntityPage.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                CreateNewEntityPage.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            CreateNewEntityPage.EnterText(FieldNames.Zip, "55555");
            CreateNewEntityPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            CreateNewEntityPage.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            CreateNewEntityPage.ButtonClick(ButtonsAndMessages.Save);
            if (!CreateNewEntityPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{dealerName}]");
            }
            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealerName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, dealerName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Dealer Created with Code: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23551" })]
        public void TC_23551(string UserType, int Order)
        {
            menu.OpenPopUpPage(Pages.CreateNewEntity);
            CreateNewEntityPage = new CreateNewEntityPage(driver);

            Task t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
            CreateNewEntityPage.SelectValueTableDropDown(FieldNames.EnrollmentType, menu.RenameMenuField(EntityType.Fleet));
            t.Wait();
            t.Dispose();
            t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
            CreateNewEntityPage.ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();
            if (CreateNewEntityPage.GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = Task.Run(() => CreateNewEntityPage.WaitForStalenessOfElement(FieldNames.DisplayName));
                CreateNewEntityPage.SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }

            CreateNewEntityPage.EnterText(FieldNames.DisplayName, fleetName);
            CreateNewEntityPage.EnterText(FieldNames.LegalName, fleetName);
            CreateNewEntityPage.SelectValueListBoxByScroll(FieldNames.ProgramCode, "PCARD");
            CreateNewEntityPage.EnterTextAfterClear(FieldNames.AccountCode, fleetName);
            CreateNewEntityPage.EnterText(FieldNames.AccountingCode, fleetName);
            CreateNewEntityPage.ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(CreateNewEntityPage.IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            CreateNewEntityPage.EnterText(FieldNames.Address1, fleetName);
            CreateNewEntityPage.EnterText(FieldNames.City, fleetName);
            if (CreateNewEntityPage.GetValueOfDropDown(FieldNames.Country) != "US")
            {
                CreateNewEntityPage.SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (CreateNewEntityPage.GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                CreateNewEntityPage.SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            CreateNewEntityPage.EnterText(FieldNames.Zip, "55555");
            CreateNewEntityPage.EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            CreateNewEntityPage.EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            CreateNewEntityPage.EnterTextAfterClear(FieldNames.CreditLimit, "55555555");
            CreateNewEntityPage.ButtonClick(ButtonsAndMessages.Save);
            if (!CreateNewEntityPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{fleetName}]");
            }
            EntityDetails entityDetails = CommonUtils.GetEntityDetails(fleetName);
            Assert.IsFalse(entityDetails.CorcentricLocation, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, fleetName, FieldNames.CorcentricLocation) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.CorcentricLocation.ToString()));
            Assert.IsFalse(entityDetails.FinanceChargeExempt, GetErrorMessage(ErrorMessages.IncorrectValueEntityCreation, fleetName, FieldNames.FinanceChargeExempt) + GetErrorMessage(LoggerMesages.ExpectedValue, "False", entityDetails.FinanceChargeExempt.ToString()));
            Console.WriteLine($"Fleet Created with Code: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(3), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21059" })]
        public void TC_21059(string UserType, int Order)
        {
            menu.OpenPage(Pages.MasterBillingStatementConfiguration);
            MasterBillingStatementConfigurationPage = new MasterBillingStatementConfigurationPage(driver);
            MasterBillingStatementConfigurationPage.PopulateGrid();
            CreateNewMasterBillingStatementConfigurationPage newStatementPage = MasterBillingStatementConfigurationPage.OpenCreateNewStatement();
            var errorMsgs = newStatementPage.CreateStatementConfiguration(fleetName, dealerName, dealerAccountingCode, communityStatementName);
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });
            MasterBillingStatementConfigurationPage.SwitchToMainWindow();
            Assert.AreEqual(MasterBillingStatementConfigurationPage.RenameMenuField(Pages.MasterBillingStatementConfiguration), MasterBillingStatementConfigurationPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            MasterBillingStatementConfigurationPage.PopulateGrid(communityStatementName);
            Assert.IsTrue(MasterBillingStatementConfigurationPage.IsAnyDataOnGrid(), ErrorMessages.NoDataOnGrid);
            Console.WriteLine($"Dealer Used for Creating Master Missing Statement Config: [{dealerName}]");
            Console.WriteLine($"Fleet Used for Creating Master Missing Statement Config: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(4), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23225" })]
        public void TC_23225(string UserType, int Order)
        {
            menu.OpenPage(Pages.AccountMaintenance);
            AccountMaintenancePage = new AccountMaintenancePage(driver);
            AccountMaintenanceaspxPage = new AccountMaintenanceAspx(driver);

            AccountMaintenancePage.LoadDataOnGrid(dealerName, EntityType.Dealer);
            AccountMaintenancePage.ClickHyperLinkOnGrid(TableHeaders.Name);

            AccountMaintenanceaspxPage.SelectRelationShipType(FieldNames.PaymentTerms, FieldNames.PaymentTermsTable, menu.RenameMenuField("All Fleets"));
            AccountMaintenanceaspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
            AccountMaintenanceaspxPage.SelectValueFirstRow(FieldNames.PaymentTerms);
            AccountMaintenanceaspxPage.SelectValueTableDropDown(FieldNames.BillingCycle, "Weekly");
            AccountMaintenanceaspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
            AccountMaintenanceaspxPage.SelectValueTableDropDown(FieldNames.StatementEndDay, "Friday");
            AccountMaintenanceaspxPage.SelectValueTableDropDown(FieldNames.StatementType, "One statement per due date");
            AccountMaintenanceaspxPage.SelectValueTableDropDown(FieldNames.AccelerationType, "None");
            AccountMaintenanceaspxPage.SelectValueTableDropDown(FieldNames.EffectiveDateBasedOn, menu.RenameMenuField("Dealer Invoice Date"));
            AccountMaintenanceaspxPage.EnterTextAfterClear(FieldNames.EffectiveDate, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-7)));
            AccountMaintenanceaspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(AccountMaintenanceaspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            AccountMaintenanceaspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentTerms);
            Assert.AreEqual(1, AccountMaintenanceaspxPage.GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);
            Console.WriteLine($"Dealer Used for Creating Pay Terms Relation: [{dealerName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(5), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20255" })]
        public void TC_20255(string UserType, int Order)
        {
            menu.OpenPage(Pages.Parts);
            PartsPage = new PartsPage(driver);
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(PartsPage.RenameMenuField(Pages.Parts), PartsPage.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(PartsPage.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(PartsPage.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                PartsPage.AreFieldsAvailable(Pages.Parts).ForEach(x => { Assert.Fail(x); });
                PartsPage.PopulateGrid(partNumber);

                var errormsgs = PartsPage.VerifyEditFields(Pages.Parts, ButtonsAndMessages.New);
                if (applicationContext.client.ToLower() == "ameriquestcorp")
                {
                    errormsgs.AddRange(PartsPage.CreateNewDecentralizedPart(partNumber, dealerName));
                }
                else
                {
                    errormsgs.AddRange(PartsPage.CreateNewPart(partNumber));
                }

                foreach (var errormsg in errormsgs)
                {
                    Assert.Fail(errormsg);
                }
                Console.WriteLine($"Part Number: [{partNumber}]");
            });

        }

        [Category(TestCategory.Smoke)]
        [Test, Order(6), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20261" })]
        public void TC_20261(string UserType, int Order)
        {
            menu.OpenPage(Pages.Price);
            PricePage = new PricePage(driver);
            Part part = PartsUtil.GetPartDetails(partNumber);
            Assert.Multiple(() =>
            {
                PricePage.PopulateGrid(partNumber);
                var errorMsgs = PricePage.CreatePrice(part);
                foreach (var errormsg in errorMsgs)
                {
                    Assert.Fail(errormsg);
                }
                PricePage.PopulateGrid(partNumber);
                PricePage.IsNestedGridOpen();
                errorMsgs.AddRange(PricePage.ValidateNestedGridTabs("Price Detail"));
                priceDetPage = new PriceDetailPage(driver);
                errorMsgs.AddRange(priceDetPage.CreateNewPriceDetail());
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
            Console.WriteLine($"Successfully Created CenPrice for Part: [{partNumber}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(7), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21255" })]
        public void TC_21255(string UserType, int Order)
        {
            menu.OpenPage(Pages.BillingScheduleManagement);
            BillingScheduleManagementPage = new BillingScheduleManagementPage(driver);

            CreateNewBillingScheduleManagementPage CreateNewBillingScheduleManagementPage = BillingScheduleManagementPage.OpenCreateBillingSchedule();
            var errorMsgs = CreateNewBillingScheduleManagementPage.CreateBillingSchedule(fleetName, dealerName, "Core Price");
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }
            BillingScheduleManagementPage.SelectFirstRowMultiSelectDropDown(FieldNames.CompanyName, TableHeaders.AccountCode, fleetName);
            if (!BillingScheduleManagementPage.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for CompanyName [{fleetName}]");
            }
            Console.WriteLine($"Successfully Created Core Price for: [{fleetName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, Order(8), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21254" })]
        public void TC_21254(string UserType, int Order)
        {
            menu.OpenPage(Pages.BillingScheduleManagement);
            BillingScheduleManagementPage = new BillingScheduleManagementPage(driver);

            CreateNewBillingScheduleManagementPage CreateNewBillingScheduleManagementPage = BillingScheduleManagementPage.OpenCreateBillingSchedule();
            var errorMsgs = CreateNewBillingScheduleManagementPage.CreateBillingSchedule(fleetName, dealerName, "Unit Price");
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }
            BillingScheduleManagementPage.SelectFirstRowMultiSelectDropDown(FieldNames.CompanyName, TableHeaders.AccountCode, fleetName);
            if (!BillingScheduleManagementPage.IsAnyDataOnGrid())
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid + $" for CompanyName [{fleetName}]");
            }
            Console.WriteLine($"Successfully Created Unit Price for: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(9), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20561" })]
        public void TC_20561(string UserType, int Order)
        {
            menu.OpenPage(Pages.InvoiceEntry);
            InvoiceEntryPage = new InvoiceEntryPage(driver);

            CreateNewInvoicePage CreateNewInvoicePage = InvoiceEntryPage.OpenCreateNewInvoice();

            var errorMsgs = CreateNewInvoicePage.CreateNewInvoice(fleetName, dealerName, dealerInvNum);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (CreateNewInvoicePage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                CreateNewInvoicePage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, partNumber);
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.AddLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, partNumber);
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.AddTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            if (CreateNewInvoicePage.GetValueOfDropDown(FieldNames.TaxType) != "State")
            {
                CreateNewInvoicePage.SelectValueByScroll(FieldNames.TaxType, "State");
            }
            CreateNewInvoicePage.SetValue(FieldNames.Amount, "1.00");
            CreateNewInvoicePage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.NewTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SelectValueByScroll(FieldNames.TaxType, "PST");
            CreateNewInvoicePage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            CreateNewInvoicePage.SetValue(FieldNames.Amount, "1.00");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            Assert.AreEqual(2, CreateNewInvoicePage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            CreateNewInvoicePage.UploadFile(0, "UploadFiles//SamplePDF.pdf");
            CreateNewInvoicePage.Click(ButtonsAndMessages.UploadAttachments);
            CreateNewInvoicePage.WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);

            CreateNewInvoicePage.UploadFile(0, "UploadFiles//SamplePDF.pdf");
            CreateNewInvoicePage.Click(ButtonsAndMessages.UploadAttachments);
            CreateNewInvoicePage.WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);

            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();
            Console.WriteLine($"Successfully Created Draft Invoice: [{dealerInvNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(10), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20562" })]
        public void TC_20562(string UserType, int Order)
        {
            menu.OpenPage(Pages.InvoiceEntry);
            InvoiceEntryPage = new InvoiceEntryPage(driver);
            InvoiceEntryPage.GridLoad();
            InvoiceEntryPage.FilterTable(TableHeaders.DealerInv_, dealerInvNum);
            InvoiceEntryPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv_);

            CreateNewInvoicePage CreateNewInvoicePage;

            CreateNewInvoicePage = new CreateNewInvoicePage(driver);
            CreateNewInvoicePage.Click(ButtonsAndMessages.EditLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SelectValueByScroll(FieldNames.Type, "Rental", ButtonsAndMessages.Edit);
            CreateNewInvoicePage.WaitForLoadingIcon();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            Assert.AreEqual("", CreateNewInvoicePage.GetValue(FieldNames.Item, ButtonsAndMessages.Edit));
            CreateNewInvoicePage.EnterText(FieldNames.Item, partNumber, ButtonsAndMessages.Edit);
            CreateNewInvoicePage.ClearText(FieldNames.ItemDescription, ButtonsAndMessages.Edit);
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.WaitForTextToBePresentInElementLocated("Description Error Label", ButtonsAndMessages.PleaseEnterDescription);
            string errorText = CreateNewInvoicePage.GetText("Description Error Label");
            Assert.AreEqual(ButtonsAndMessages.PleaseEnterDescription, errorText);
            CreateNewInvoicePage.EnterText(FieldNames.ItemDescription, partNumber, ButtonsAndMessages.Edit);
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();
            Assert.IsFalse(CreateNewInvoicePage.IsTextVisible(ButtonsAndMessages.PleaseEnterDescription));
            Assert.IsFalse(CreateNewInvoicePage.IsElementVisible("Description Error Label"));
            CreateNewInvoicePage.Click(ButtonsAndMessages.EditTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SelectValueByScroll(FieldNames.TaxType, "QST");
            CreateNewInvoicePage.SetValue(FieldNames.Amount, "2.00");
            CreateNewInvoicePage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            Assert.AreEqual(2, CreateNewInvoicePage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            CreateNewInvoicePage.UploadFile(0, "UploadFiles//SamplePDF.pdf");
            CreateNewInvoicePage.Click(ButtonsAndMessages.UploadAttachments);
            CreateNewInvoicePage.WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);

            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteLineItem);
            Assert.AreEqual(CreateNewInvoicePage.GetAlertMessage(), ButtonsAndMessages.DeleteLineItemAlert);
            CreateNewInvoicePage.DismissAlert();
            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteLineItem);
            CreateNewInvoicePage.AcceptAlert();
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteTax);
            Assert.AreEqual(CreateNewInvoicePage.GetAlertMessage(), ButtonsAndMessages.ConfirmDeleteAlert);
            CreateNewInvoicePage.DismissAlert();
            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteTax);
            CreateNewInvoicePage.AcceptAlert();
            CreateNewInvoicePage.WaitForLoadingIcon();
            Assert.AreEqual(1, CreateNewInvoicePage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoDeletionFailed);

            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteAttachment);
            Assert.AreEqual(CreateNewInvoicePage.GetAlertMessage(), ButtonsAndMessages.DeleteAttachmentAlert);
            CreateNewInvoicePage.DismissAlert();
            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteAttachment);
            CreateNewInvoicePage.AcceptAlert();

            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteInvoice);
            Assert.AreEqual(CreateNewInvoicePage.GetAlertMessage(), ButtonsAndMessages.DeleteInvoiceAlert);
            CreateNewInvoicePage.DismissAlert();
            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteInvoice);
            CreateNewInvoicePage.AcceptAlert();
            CreateNewInvoicePage.AcceptAlert(out string msg);
            if (!CreateNewInvoicePage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while deleting draft invoice [{dealerInvNum}]");
            }

            Assert.AreEqual("Manual Invoice deleted successfully.", msg);
            Console.WriteLine($"Successfully deleted Draft Invoice: [{dealerInvNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(11), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23590" })]
        public void TC_23590(string UserType, int Order)
        {
            menu.OpenPage(Pages.CreateAuthorization, false, true);
            menu.SwitchIframe();
            CreateAuthorizationPage = new CreateAuthorizationPage(driver);

            CreateAuthorizationPage.SelectValueByScroll(FieldNames.TransactionType, "Service");
            CreateAuthorizationPage.EnterDateInInvoiceDate();
            CreateAuthorizationPage.EnterDealerCode(dealerName);
            CreateAuthorizationPage.EnterFleetCode(fleetName);
            CreateAuthorizationPage.ClickContinue();
            CreateAuthorizationPage.WaitForAnyElementLocatedBy(FieldNames.InvoiceAmount);
            CreateAuthorizationPage.WaitForElementToBeClickable(FieldNames.InvoiceAmount);
            CreateAuthorizationPage.EnterInvoiceAmount("50.00");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.PurchaseOrderNumber, "po_1");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.InvoiceNumber, transactionInvNum);
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.UnitNumber, "123");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.VehicleID, "435");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.VehicleYear, "1964");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.VehicleLicense, "POU2456");
            CreateAuthorizationPage.ClickCreateAuthorization();
            Assert.IsTrue(CreateAuthorizationPage.IsElementVisibleOnScreen("Authorization Code Label"));
            CreateAuthorizationPage.CheckForText("Successful transaction.");
            authCode = CreateAuthorizationPage.GetText("Authorization Code Label");
            Assert.IsNotNull(authCode);
            Assert.AreNotEqual("Not Authorized", authCode);
            Console.WriteLine($"Authorization Created Successfully: [{authCode}]. Invoice Number: [{transactionInvNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(12), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20563" })]
        public void TC_20563(string UserType, int Order)
        {
            menu.OpenPage(Pages.InvoiceEntry);
            InvoiceEntryPage = new InvoiceEntryPage(driver);

            CreateNewInvoicePage CreateNewInvoicePage = InvoiceEntryPage.OpenCreateNewInvoice();

            var errorMsgs = CreateNewInvoicePage.CreateNewAuthInvoice(fleetName, dealerName, authCode, transactionInvNum);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (CreateNewInvoicePage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                CreateNewInvoicePage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.AddLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.AddTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            if (CreateNewInvoicePage.GetValueOfDropDown(FieldNames.TaxType) != "State")
            {
                CreateNewInvoicePage.SelectValueByScroll(FieldNames.TaxType, "State");
            }
            CreateNewInvoicePage.SetValue(FieldNames.Amount, "1.00");
            CreateNewInvoicePage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.NewTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SelectValueByScroll(FieldNames.TaxType, "PST");
            CreateNewInvoicePage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            CreateNewInvoicePage.SetValue(FieldNames.Amount, "1.00");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            Assert.AreEqual(2, CreateNewInvoicePage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            CreateNewInvoicePage.UploadFile(0, "UploadFiles//SamplePDF.pdf");
            CreateNewInvoicePage.Click(ButtonsAndMessages.UploadAttachments);
            CreateNewInvoicePage.WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);

            CreateNewInvoicePage.UploadFile(0, "UploadFiles//SamplePDF.pdf");
            CreateNewInvoicePage.Click(ButtonsAndMessages.UploadAttachments);
            CreateNewInvoicePage.WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);

            CreateNewInvoicePage.Click(ButtonsAndMessages.SubmitInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.Continue);
            CreateNewInvoicePage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg);
            if (!CreateNewInvoicePage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Creating Auth Invoice invoice [{transactionInvNum}]");
            }
            Console.WriteLine($"Successfully Created Auth Invoice: [{transactionInvNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(13), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20567" })]
        public void TC_20567(string UserType, int Order)
        {
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            FITLPage = new FleetInvoiceTransactionLookupPage(driver);

            FITLPage.LoadDataOnGrid(transactionInvNum);
            FITLPage.FilterTable(TableHeaders.TransactionType, "Parts");
            FITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);

            OffsetTransactionPage OffsetTransactionPopUpPage = InvoiceOptionPopUpPage.CreateRebill();

            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.RebillTheInvoice);
            Assert.IsTrue(OffsetTransactionPopUpPage.IsRadioButtonChecked(ButtonsAndMessages.RebillTheInvoice));
            OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.Comments);
            OffsetTransactionPopUpPage.EnterText(FieldNames.Comments, "Comments");

            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.Rebill);
            OffsetTransactionPopUpPage.SwitchToMainWindow();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(8));
            FITLPage.SwitchToPopUp();
            CreateNewInvoicePage = new CreateNewInvoicePage(driver);

            CreateNewInvoicePage.WaitForElementToBeVisible(ButtonsAndMessages.EditLineItem);
            CreateNewInvoicePage.Click(ButtonsAndMessages.EditLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SelectValueByScroll(FieldNames.Type, "Rental", ButtonsAndMessages.Edit);
            CreateNewInvoicePage.WaitForLoadingIcon();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            Assert.AreEqual("", CreateNewInvoicePage.GetValue(FieldNames.Item, ButtonsAndMessages.Edit));
            CreateNewInvoicePage.EnterText(FieldNames.Item, partNumber, ButtonsAndMessages.Edit);
            CreateNewInvoicePage.EnterText(FieldNames.ItemDescription, partNumber, ButtonsAndMessages.Edit);
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.EditTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SelectValueByScroll(FieldNames.TaxType, "QST");
            CreateNewInvoicePage.SetValue(FieldNames.Amount, "2.00");
            CreateNewInvoicePage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            Assert.AreEqual(2, CreateNewInvoicePage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            CreateNewInvoicePage.UploadFile(0, "UploadFiles//SamplePDF.pdf");
            CreateNewInvoicePage.Click(ButtonsAndMessages.UploadAttachments);
            CreateNewInvoicePage.WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);

            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteLineItem);
            Assert.AreEqual(CreateNewInvoicePage.GetAlertMessage(), ButtonsAndMessages.DeleteLineItemAlert);
            CreateNewInvoicePage.AcceptAlert();
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteTax);
            Assert.AreEqual(CreateNewInvoicePage.GetAlertMessage(), ButtonsAndMessages.ConfirmDeleteAlert);
            CreateNewInvoicePage.AcceptAlert();
            CreateNewInvoicePage.WaitForLoadingIcon();
            Assert.AreEqual(1, CreateNewInvoicePage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoDeletionFailed);

            CreateNewInvoicePage.Click(ButtonsAndMessages.DeleteAttachment);
            Assert.AreEqual(CreateNewInvoicePage.GetAlertMessage(), ButtonsAndMessages.DeleteAttachmentAlert);
            CreateNewInvoicePage.AcceptAlert();

            CreateNewInvoicePage.Click(ButtonsAndMessages.SubmitInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.Continue);
            CreateNewInvoicePage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg);
            if (!CreateNewInvoicePage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Clonning Auth Invoice [{transactionInvNum}]");
            }

            Console.WriteLine($"Successfully Cloned Auth Invoice: [{transactionInvNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(14), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23590_2" })]
        public void TC_23590_2(string UserType, int Order)
        {
            menu.OpenPage(Pages.CreateAuthorization, false, true);
            menu.SwitchIframe();
            CreateAuthorizationPage = new CreateAuthorizationPage(driver);
            
            CreateAuthorizationPage.SelectValueByScroll(FieldNames.TransactionType, "Service");
            CreateAuthorizationPage.EnterDateInInvoiceDate();
            CreateAuthorizationPage.EnterDealerCode(dealerName);
            CreateAuthorizationPage.EnterFleetCode(fleetName);
            CreateAuthorizationPage.ClickContinue();
            CreateAuthorizationPage.WaitForAnyElementLocatedBy(FieldNames.InvoiceAmount);
            CreateAuthorizationPage.WaitForElementToBeClickable(FieldNames.InvoiceAmount);
            CreateAuthorizationPage.EnterInvoiceAmount("50.00");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.PurchaseOrderNumber, "po_1");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.InvoiceNumber, transactionInvNum2);
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.UnitNumber, "123");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.VehicleID, "435");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.VehicleYear, "1964");
            CreateAuthorizationPage.EnterTextAfterClear(FieldNames.VehicleLicense, "POU2456");
            CreateAuthorizationPage.ClickCreateAuthorization();
            Assert.IsTrue(CreateAuthorizationPage.IsElementVisibleOnScreen("Authorization Code Label"));
            CreateAuthorizationPage.CheckForText("Successful transaction.");
            authCode2 = CreateAuthorizationPage.GetText("Authorization Code Label");
            Assert.IsNotNull(authCode2);
            Assert.AreNotEqual("Not Authorized", authCode2);
            Console.WriteLine($"Authorization Created Successfully: [{authCode2}]. Invoice Number: [{transactionInvNum2}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(15), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20563_2" })]
        public void TC_20563_2(string UserType, int Order)
        {
            menu.OpenPage(Pages.InvoiceEntry);
            InvoiceEntryPage = new InvoiceEntryPage(driver);
            CreateNewInvoicePage CreateNewInvoicePage = InvoiceEntryPage.OpenCreateNewInvoice();

            var errorMsgs = CreateNewInvoicePage.CreateNewAuthInvoice(fleetName, dealerName, authCode2, transactionInvNum2);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (CreateNewInvoicePage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                CreateNewInvoicePage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.AddLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveLineItem);
            CreateNewInvoicePage.WaitForLoadingIcon();

            CreateNewInvoicePage.Click(ButtonsAndMessages.AddTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            if (CreateNewInvoicePage.GetValueOfDropDown(FieldNames.TaxType) != "State")
            {
                CreateNewInvoicePage.SelectValueByScroll(FieldNames.TaxType, "State");
            }
            CreateNewInvoicePage.SetValue(FieldNames.Amount, "1.00");
            CreateNewInvoicePage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.NewTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.SelectValueByScroll(FieldNames.TaxType, "PST");
            CreateNewInvoicePage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            CreateNewInvoicePage.SetValue(FieldNames.Amount, "1.00");
            CreateNewInvoicePage.Click(ButtonsAndMessages.SaveTax);
            CreateNewInvoicePage.WaitForLoadingIcon();
            Assert.AreEqual(2, CreateNewInvoicePage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            CreateNewInvoicePage.UploadFile(0, "UploadFiles//SamplePDF.pdf");
            CreateNewInvoicePage.Click(ButtonsAndMessages.UploadAttachments);
            CreateNewInvoicePage.WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);

            CreateNewInvoicePage.UploadFile(0, "UploadFiles//SamplePDF.pdf");
            CreateNewInvoicePage.Click(ButtonsAndMessages.UploadAttachments);
            CreateNewInvoicePage.WaitForMsg(ButtonsAndMessages.UploadingFilesPleaseWaitElipsis);

            CreateNewInvoicePage.Click(ButtonsAndMessages.SubmitInvoice);
            CreateNewInvoicePage.WaitForLoadingIcon();
            CreateNewInvoicePage.Click(ButtonsAndMessages.Continue);
            CreateNewInvoicePage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, invoiceMsg);
            if (!CreateNewInvoicePage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Creating Auth Invoice [{transactionInvNum2}]");
            }
            Console.WriteLine($"Successfully Created Auth Invoice2 for Reversal: [{transactionInvNum2}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(16), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20568" })]
        public void TC_20568(string UserType, int Order)
        {
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            FITLPage = new FleetInvoiceTransactionLookupPage(driver);

            FITLPage.LoadDataOnGrid(transactionInvNum2);
            FITLPage.FilterTable(TableHeaders.TransactionType, "Parts");
            FITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);

            OffsetTransactionPage OffsetTransactionPopUpPage = InvoiceOptionPopUpPage.CreateRebill();

            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.CreateAReversal);
            Assert.IsTrue(OffsetTransactionPopUpPage.IsRadioButtonChecked(ButtonsAndMessages.CreateAReversal));
            OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.ReversalReason);
            Task t = Task.Run(() => OffsetTransactionPopUpPage.WaitForStalenessOfElement("Dealer For Reversal"));
            OffsetTransactionPopUpPage.SelectValueTableDropDown(FieldNames.ReversalReason, "Billed twice");
            t.Wait();
            t.Dispose();
            OffsetTransactionPopUpPage.EnterText(FieldNames.FleetOrDealerApprover, "Fleet");
            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.Reverse);

            if (!OffsetTransactionPopUpPage.IsTextVisible(ButtonsAndMessages.ReversalTransactionCompletedSuccessfully, true))
            {
                Assert.Fail($"Some error occurred while Reserval of Auth Invoice: [{transactionInvNum2}]");
            }

            Console.WriteLine($"Successfully Reserval of Auth Invoice2: [{transactionInvNum2}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, Order(17), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20564" })]
        public void TC_20564(string UserType, int Order)
        {
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            FITLPage = new FleetInvoiceTransactionLookupPage(driver);

            FITLPage.LoadDataOnGrid(transactionInvNum);
            FITLPage.FilterTable(TableHeaders.TransactionType, "Parts");
            FITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
            InvoiceOptionPopUpPage.SwitchIframe();
            InvoiceOptionPopUpPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
            Task t = Task.Run(() => InvoiceOptionsAspxPage.WaitForStalenessOfElement(FieldNames.Notes));
            InvoiceOptionsAspxPage.SimpleSelectOptionByText("Reason", "Not Our Invoice");
            t.Wait();
            t.Dispose();
            InvoiceOptionsAspxPage.EnterText(FieldNames.Notes, "This Invoice doesnt belong to us");
            InvoiceOptionsAspxPage.UploadFile("Upload file", "UploadFiles//SamplePDF.pdf");
            InvoiceOptionsAspxPage.Click(ButtonsAndMessages.Submit);
            InvoiceOptionsAspxPage.WaitForLoadingGrid();
            InvoiceOptionsAspxPage.ClosePopupWindow();
            FITLPage.SwitchToMainWindow();
            FITLPage.LoadDataOnGrid(transactionInvNum);
            FITLPage.FilterTable(TableHeaders.TransactionType, "Parts");
            Assert.AreEqual("Current-Disputed", FITLPage.GetFirstRowData(TableHeaders.TransactionStatus));
            Console.WriteLine($"Successfully Created Dispute of Auth Invoice: [{transactionInvNum}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, Order(18), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20565" })]
        public void TC_20565(string UserType, int Order)
        {
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            FITLPage = new FleetInvoiceTransactionLookupPage(driver);

            FITLPage.LoadDataOnGrid(transactionInvNum);
            FITLPage.FilterTable(TableHeaders.TransactionType, "Parts");
            FITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
            InvoiceOptionPopUpPage.SwitchIframe();
            InvoiceOptionPopUpPage.Click(ButtonsAndMessages.InvoiceOptions);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
            InvoiceOptionsAspxPage.SelectValueTableDropDown("Pending Action By", "Corcentric");
            Task t = Task.Run(() => InvoiceOptionsAspxPage.WaitForStalenessOfElement("Resolution Note"));
            InvoiceOptionsAspxPage.SimpleSelectOptionByText("Action", "Resolve Dispute");
            t.Wait();
            t.Dispose();
            InvoiceOptionsAspxPage.EnterText("Resolution Note", "Issue Solved");

            InvoiceOptionsAspxPage.Click(ButtonsAndMessages.Save);
            InvoiceOptionsAspxPage.WaitForLoadingGrid();
            InvoiceOptionsAspxPage.ClosePopupWindow();
            FITLPage.SwitchToMainWindow();
            FITLPage.LoadDataOnGrid(transactionInvNum);
            FITLPage.FilterTable(TableHeaders.TransactionType, "Parts");
            Assert.AreEqual("Current-Resolved", FITLPage.GetFirstRowData(TableHeaders.TransactionStatus));
            Console.WriteLine($"Successfully Resolved Dispute of Auth Invoice: [{transactionInvNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(19), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20566" })]
        public void TC_20566(string UserType, int Order)
        {
            menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
            FITLPage = new FleetInvoiceTransactionLookupPage(driver);

            FITLPage.LoadDataOnGrid(transactionInvNum);
            FITLPage.FilterTable(TableHeaders.TransactionType, "Parts");
            FITLPage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);

            InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
            InvoiceOptionPopUpPage.SwitchIframe();
            InvoiceOptionPopUpPage.Click(ButtonsAndMessages.InvoiceOptions);

            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(1);
            InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
            
            if (InvoiceOptionsAspxPage.GetSelectedValueSimpleSelect("Reason") != "Pricing Error")
            {
                Task t = Task.Run(() => InvoiceOptionsAspxPage.WaitForStalenessOfElement(FieldNames.Notes));
                InvoiceOptionsAspxPage.SimpleSelectOptionByText("Reason", "Pricing Error");
                t.Wait();
                t.Dispose();
            }
            InvoiceOptionsAspxPage.EnterText(FieldNames.Notes, "This Pricing is incorrect");
            InvoiceOptionsAspxPage.UploadFile("Upload file", "UploadFiles//SamplePDF.pdf");
            InvoiceOptionsAspxPage.Click(ButtonsAndMessages.ReDispute);
            InvoiceOptionsAspxPage.WaitForLoadingGrid();
            InvoiceOptionsAspxPage.ClosePopupWindow();
            FITLPage.SwitchToMainWindow();
            FITLPage.LoadDataOnGrid(transactionInvNum);
            FITLPage.FilterTable(TableHeaders.TransactionType, "Parts");
            Assert.AreEqual("Current-Disputed", FITLPage.GetFirstRowData(TableHeaders.TransactionStatus));
            Console.WriteLine($"Successfully Created ReDispute of Auth Invoice: [{transactionInvNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(20), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "14991" })]
        public void TC_14991(string UserType, int Order)
        {
            menu.OpenPage(Pages.AccountMaintenance);
            AccountMaintenancePage = new AccountMaintenancePage(driver);
            AccountMaintenanceaspxPage = new AccountMaintenanceAspx(driver);

            AccountMaintenancePage.LoadDataOnGrid(fleetName, EntityType.Fleet);
            AccountMaintenancePage.ClickHyperLinkOnGrid(TableHeaders.Name);
            AccountMaintenanceaspxPage.SelectRelationShipType(FieldNames.POAcceptanceRules, FieldNames.POAcceptanceTable, menu.RenameMenuField(dealerName));
            AccountMaintenanceaspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
            AccountMaintenanceaspxPage.SelectValueMultiSelectDropDown(FieldNames.TransactionType, TableHeaders.TransactionType, "All");
            AccountMaintenanceaspxPage.SelectValueMultiSelectDropDown(FieldNames.LineType, TableHeaders.LineType, "All");
            AccountMaintenanceaspxPage.Click(FieldNames.AllowNewPO);
            AccountMaintenanceaspxPage.Click(FieldNames.ApplyPricingRulesToPO);
            AccountMaintenanceaspxPage.Click(FieldNames.AllowChangePO);
            AccountMaintenanceaspxPage.WaitForElementToHaveFocus("RelationshipTypeInput");
            AccountMaintenanceaspxPage.Click(FieldNames.AllowDuplicatePO);
            AccountMaintenanceaspxPage.ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(AccountMaintenanceaspxPage.IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            AccountMaintenanceaspxPage.Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.POAcceptanceRules);
            Assert.AreEqual(1, AccountMaintenanceaspxPage.GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);
            Console.WriteLine($"Fleet Used for Creating PO Acceptance Rule Relation: [{fleetName}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(21), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20569" })]
        public void TC_20569(string UserType, int Order)
        {
            menu.OpenPage(Pages.POEntry);
            POEntryPage = new POEntryPage(driver);
            POEntryPage.GridLoad();

            POEntryAspx POEntryAspxPage = POEntryPage.OpenCreateNewPO();

            var errorMsgs = POEntryAspxPage.CreateNewPO(fleetName, dealerName, draftFleetPONum);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();

            POEntryAspxPage.Click(ButtonsAndMessages.AddLineItem);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();

            POEntryAspxPage.Click(ButtonsAndMessages.AddTax);
            POEntryAspxPage.WaitForLoadingIcon();
            if (POEntryAspxPage.GetValueOfDropDown(FieldNames.TaxType) != "State")
            {
                POEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "State");
            }
            POEntryAspxPage.SetValue(FieldNames.Amount, "1.00");
            POEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            POEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.Click(ButtonsAndMessages.NewTax);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "PST");
            POEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            POEntryAspxPage.SetValue(FieldNames.Amount, "1.00");
            POEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POEntryAspxPage.WaitForLoadingIcon();
            Assert.AreEqual(2, POEntryAspxPage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            POEntryAspxPage.Click(ButtonsAndMessages.SavePO);
            POEntryAspxPage.WaitForLoadingIcon();
            Console.WriteLine($"Successfully Created Draft PO: [{draftFleetPONum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(22), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20571" })]
        public void TC_20571(string UserType, int Order)
        {
            menu.OpenPage(Pages.FleetPOPOQTransactionLookup);
            FleetPOPOQTransactionLookupPage = new FleetPOPOQTransactionLookupPage(driver);
            FleetPOPOQTransactionLookupPage.PopulateGridWithDocumentNumber(draftFleetPONum);

            FleetPOPOQTransactionLookupPage.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);

            POEntryAspx POEntryAspxPage;

            POEntryAspxPage = new POEntryAspx(driver);
            POEntryAspxPage.Click(ButtonsAndMessages.EditLineItem);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Rental", ButtonsAndMessages.Edit);
            POEntryAspxPage.WaitForLoadingIcon();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            Assert.AreEqual("", POEntryAspxPage.GetValue(FieldNames.Item, ButtonsAndMessages.Edit));
            POEntryAspxPage.EnterText(FieldNames.Item, partNumber, ButtonsAndMessages.Edit);
            POEntryAspxPage.ClearText(FieldNames.ItemDescription, ButtonsAndMessages.Edit);
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.WaitForTextToBePresentInElementLocated("Description Error Label", ButtonsAndMessages.PleaseEnterDescription);
            string errorText = POEntryAspxPage.GetText("Description Error Label");
            Assert.AreEqual(ButtonsAndMessages.PleaseEnterDescription, errorText);
            POEntryAspxPage.EnterText(FieldNames.ItemDescription, partNumber, ButtonsAndMessages.Edit);
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();
            Assert.IsFalse(POEntryAspxPage.IsTextVisible(ButtonsAndMessages.PleaseEnterDescription));
            Assert.IsFalse(POEntryAspxPage.IsElementVisible("Description Error Label"));
            POEntryAspxPage.Click(ButtonsAndMessages.EditTax);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "QST");
            POEntryAspxPage.SetValue(FieldNames.Amount, "2.00");
            POEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            POEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POEntryAspxPage.WaitForLoadingIcon();
            Assert.AreEqual(2, POEntryAspxPage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            POEntryAspxPage.Click(ButtonsAndMessages.DeleteLineItem);
            Assert.AreEqual(POEntryAspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteLineItemAlert);
            POEntryAspxPage.DismissAlert();
            POEntryAspxPage.Click(ButtonsAndMessages.DeleteLineItem);
            POEntryAspxPage.AcceptAlert();
            POEntryAspxPage.WaitForLoadingIcon();

            POEntryAspxPage.Click(ButtonsAndMessages.DeleteTax);
            Assert.AreEqual(POEntryAspxPage.GetAlertMessage(), ButtonsAndMessages.ConfirmDeleteAlert);
            POEntryAspxPage.DismissAlert();
            POEntryAspxPage.Click(ButtonsAndMessages.DeleteTax);
            POEntryAspxPage.AcceptAlert();
            POEntryAspxPage.WaitForLoadingIcon();
            Assert.AreEqual(1, POEntryAspxPage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoDeletionFailed);

            POEntryAspxPage.Click(ButtonsAndMessages.DeletePO);
            Assert.AreEqual(POEntryAspxPage.GetAlertMessage(), ButtonsAndMessages.DeletePOAlert);
            POEntryAspxPage.DismissAlert();
           
            Console.WriteLine($"Successfully Edited Draft PO: [{draftFleetPONum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(23), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23567" })]
        public void TC_23567(string UserType, int Order)
        {
            menu.OpenPage(Pages.POEntry);
            POEntryPage = new POEntryPage(driver);
            POEntryPage.GridLoad();
            
            POEntryAspx POEntryAspxPage = POEntryPage.OpenCreateNewPO();

            var errorMsgs = POEntryAspxPage.CreateNewPO(fleetName, dealerName, submittedFleetPONum);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();

            POEntryAspxPage.Click(ButtonsAndMessages.AddLineItem);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POEntryAspxPage.WaitForLoadingIcon();

            POEntryAspxPage.Click(ButtonsAndMessages.AddTax);
            POEntryAspxPage.WaitForLoadingIcon();
            if (POEntryAspxPage.GetValueOfDropDown(FieldNames.TaxType) != "State")
            {
                POEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "State");
            }
            POEntryAspxPage.SetValue(FieldNames.Amount, "1.00");
            POEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            POEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.Click(ButtonsAndMessages.NewTax);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "PST");
            POEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            POEntryAspxPage.SetValue(FieldNames.Amount, "1.00");
            POEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POEntryAspxPage.WaitForLoadingIcon();
            Assert.AreEqual(2, POEntryAspxPage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            POEntryAspxPage.Click(ButtonsAndMessages.SubmitPO);
            POEntryAspxPage.WaitForLoadingIcon();
            POEntryAspxPage.Click(ButtonsAndMessages.Continue);
            POEntryAspxPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POSubmissionCompletedSuccessfully, invoiceMsg);
            if (!POEntryAspxPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Submitting PO:  [{submittedFleetPONum}]");
            }
            Console.WriteLine($"Successfully Submitting PO: [{submittedFleetPONum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(24), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23568" })]
        public void TC_23568(string UserType, int Order)
        {
            menu.OpenPage(Pages.POQEntry);
            POQEntryPage = new POQEntryPage(driver);
            POQEntryPage.GridLoad();

            POQEntryAspx POQEntryAspxPage = POQEntryPage.OpenCreateNewPOQ();

            var errorMsgs = POQEntryAspxPage.CreateNewPOQ(fleetName, dealerName, draftDealerPOQNum);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POQEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POQEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();

            POQEntryAspxPage.Click(ButtonsAndMessages.AddLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();

            POQEntryAspxPage.Click(ButtonsAndMessages.AddTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            if (POQEntryAspxPage.GetValueOfDropDown(FieldNames.TaxType) != "State")
            {
                POQEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "State");
            }
            POQEntryAspxPage.SetValue(FieldNames.Amount, "1.00");
            POQEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.Click(ButtonsAndMessages.NewTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "PST");
            POQEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            POQEntryAspxPage.SetValue(FieldNames.Amount, "1.00");
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            Assert.AreEqual(2, POQEntryAspxPage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            POQEntryAspxPage.Click(ButtonsAndMessages.SavePOQ);
            POQEntryAspxPage.WaitForLoadingIcon();
            Console.WriteLine($"Successfully Created Draft POQ: [{draftDealerPOQNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(25), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21064" })]
        public void TC_21064(string UserType, int Order)
        {
            menu.OpenPage(Pages.FleetPOPOQTransactionLookup);
            FleetPOPOQTransactionLookupPage = new FleetPOPOQTransactionLookupPage(driver);
            FleetPOPOQTransactionLookupPage.PopulateGridWithDocumentNumber(draftDealerPOQNum);

            FleetPOPOQTransactionLookupPage.ClickHyperLinkOnGrid(TableHeaders.DocumentNumber);

            POQEntryAspx POQEntryAspxPage;

            POQEntryAspxPage = new POQEntryAspx(driver);
            POQEntryAspxPage.Click(ButtonsAndMessages.EditLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Rental", ButtonsAndMessages.Edit);
            POQEntryAspxPage.WaitForLoadingIcon();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            Assert.AreEqual("", POQEntryAspxPage.GetValue(FieldNames.Item, ButtonsAndMessages.Edit));
            POQEntryAspxPage.EnterText(FieldNames.Item, partNumber, ButtonsAndMessages.Edit);
            POQEntryAspxPage.ClearText(FieldNames.ItemDescription, ButtonsAndMessages.Edit);
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.WaitForTextToBePresentInElementLocated("Description Error Label", ButtonsAndMessages.PleaseEnterDescription);
            string errorText = POQEntryAspxPage.GetText("Description Error Label");
            Assert.AreEqual(ButtonsAndMessages.PleaseEnterDescription, errorText);
            POQEntryAspxPage.EnterText(FieldNames.ItemDescription, partNumber, ButtonsAndMessages.Edit);
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();
            Assert.IsFalse(POQEntryAspxPage.IsTextVisible(ButtonsAndMessages.PleaseEnterDescription));
            Assert.IsFalse(POQEntryAspxPage.IsElementVisible("Description Error Label"));
            POQEntryAspxPage.Click(ButtonsAndMessages.EditTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "QST");
            POQEntryAspxPage.SetValue(FieldNames.Amount, "2.00");
            POQEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            Assert.AreEqual(2, POQEntryAspxPage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            POQEntryAspxPage.Click(ButtonsAndMessages.DeleteLineItem);
            Assert.AreEqual(POQEntryAspxPage.GetAlertMessage(), ButtonsAndMessages.DeleteLineItemAlert);
            POQEntryAspxPage.DismissAlert();
            POQEntryAspxPage.Click(ButtonsAndMessages.DeleteLineItem);
            POQEntryAspxPage.AcceptAlert();
            POQEntryAspxPage.WaitForLoadingIcon();

            POQEntryAspxPage.Click(ButtonsAndMessages.DeleteTax);
            Assert.AreEqual(POQEntryAspxPage.GetAlertMessage(), ButtonsAndMessages.ConfirmDeleteAlert);
            POQEntryAspxPage.DismissAlert();
            POQEntryAspxPage.Click(ButtonsAndMessages.DeleteTax);
            POQEntryAspxPage.AcceptAlert();
            POQEntryAspxPage.WaitForLoadingIcon();
            Assert.AreEqual(1, POQEntryAspxPage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoDeletionFailed);

            POQEntryAspxPage.Click(ButtonsAndMessages.DeletePOQ);
            Assert.AreEqual(POQEntryAspxPage.GetAlertMessage(), ButtonsAndMessages.DeletePOQAlert);
            POQEntryAspxPage.DismissAlert();
            POQEntryAspxPage.Click(ButtonsAndMessages.DeletePOQ);
            POQEntryAspxPage.AcceptAlert();
            POQEntryAspxPage.AcceptAlert(out string msg);
            if (!POQEntryAspxPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while deleting draft POQ [{draftDealerPOQNum}]");
            }

            Assert.AreEqual("Manual Purchase Order Quote deleted successfully.", msg);
            
            Console.WriteLine($"Successfully Edited and Deleted Draft POQ: [{draftDealerPOQNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(26), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20570" })]
        public void TC_20570(string UserType, int Order)
        {
            menu.OpenPage(Pages.POQEntry);
            POQEntryPage = new POQEntryPage(driver);
            POQEntryPage.GridLoad();

            POQEntryAspx POQEntryAspxPage = POQEntryPage.OpenCreateNewPOQ();

            var errorMsgs = POQEntryAspxPage.CreateNewPOQ(fleetName, dealerName, submittedDealerPOQNum);
            foreach (var errorMsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errorMsg));
            }

            if (POQEntryAspxPage.GetValueOfDropDown(FieldNames.Type) != "Parts")
            {
                POQEntryAspxPage.SelectValueByScroll(FieldNames.Type, "Parts");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

            POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();

            POQEntryAspxPage.Click(ButtonsAndMessages.AddLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.SearchAndSelectValue(FieldNames.Item, partNumber);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveLineItem);
            POQEntryAspxPage.WaitForLoadingIcon();

            POQEntryAspxPage.Click(ButtonsAndMessages.AddTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            if (POQEntryAspxPage.GetValueOfDropDown(FieldNames.TaxType) != "State")
            {
                POQEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "State");
            }
            POQEntryAspxPage.SetValue(FieldNames.Amount, "1.00");
            POQEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax1");
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.Click(ButtonsAndMessages.NewTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.SelectValueByScroll(FieldNames.TaxType, "PST");
            POQEntryAspxPage.EnterTextAfterClear(FieldNames.TaxID, "AutomTax2");
            POQEntryAspxPage.SetValue(FieldNames.Amount, "1.00");
            POQEntryAspxPage.Click(ButtonsAndMessages.SaveTax);
            POQEntryAspxPage.WaitForLoadingIcon();
            Assert.AreEqual(2, POQEntryAspxPage.GetRowCount("Tax Info Table", true), ErrorMessages.TaxInfoAdditionFailed);

            POQEntryAspxPage.Click(ButtonsAndMessages.SubmitPOQ);
            POQEntryAspxPage.WaitForLoadingIcon();
            POQEntryAspxPage.Click(ButtonsAndMessages.Continue);
            POQEntryAspxPage.AcceptAlert(out string invoiceMsg);
            Assert.AreEqual(ButtonsAndMessages.POQSubmissionCompletedSuccessfully, invoiceMsg);
            if (!POQEntryAspxPage.WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while Submitting POQ:  [{submittedDealerPOQNum}]");
            }
            Console.WriteLine($"Successfully Submitted POQ: [{submittedDealerPOQNum}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(27) ,TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21324" })]
        public void TC_21324(string UserType, int Order)
        {
            menu.OpenPage(Pages.BillingScheduleManagement);
            BillingScheduleManagementPage = new BillingScheduleManagementPage(driver);

            BillingScheduleManagementPage.PopulateGrid(fleetName, string.Empty);
            BillingScheduleManagementPage.FilterTable(menu.RenameMenuField(TableHeaders.Fleet), fleetName);
            BillingScheduleManagementPage.FilterTable(TableHeaders.AdjustmentOfPrice, "Core Price");
            BillingScheduleManagementPage.ClickHyperLinkOnGrid(TableHeaders.Commands);

            var aspxPage = new CreateNewBillingScheduleManagementPage(driver);
            Assert.AreEqual(dealerName + " " + dealerName + " " + dealerName + " " + dealerName + " " + "AL", aspxPage.GetValueOfDropDown(FieldNames.Dealer), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(fleetName + " " + fleetName + " " + fleetName + " " + fleetName + " " + "AL" + " " + "Billing", aspxPage.GetValueOfDropDown(FieldNames.Fleet), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("Core Price", aspxPage.GetValueOfDropDown(FieldNames.AdjustmentOfPrice), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("All", aspxPage.GetValueOfDropDown(FieldNames.AccountingDocumentType), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(CommonUtils.GetCurrentDate(), aspxPage.GetValue(FieldNames.EffectiveDate), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("All", aspxPage.GetValueOfDropDown(FieldNames.Currency), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("Amount", aspxPage.GetValueOfDropDown(FieldNames.Type), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("10.2355", aspxPage.GetValue(FieldNames.Value), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("AFLD", aspxPage.GetValueOfDropDown(FieldNames.PriceLevel), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("No Tier", aspxPage.GetValueOfDropDown(FieldNames.TierType), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(dealerName + "Adj", aspxPage.GetValue(FieldNames.AdjustmentName), ErrorMessages.ValueMisMatch);

            aspxPage.UpdateBSMFields("15.0000", dealerName + "Adjustment");

            Console.WriteLine($"Successfully Updated Core Price for: [{fleetName}]");

        }

        [Category(TestCategory.Smoke)]
        [Test, Order(28) ,TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21256" })]
        public void TC_21256(string UserType, int Order)
        {
            menu.OpenPage(Pages.BillingScheduleManagement);
            BillingScheduleManagementPage = new BillingScheduleManagementPage(driver);

            BillingScheduleManagementPage.PopulateGrid(fleetName, string.Empty);
            BillingScheduleManagementPage.FilterTable(menu.RenameMenuField(TableHeaders.Fleet), fleetName);
            BillingScheduleManagementPage.FilterTable(TableHeaders.AdjustmentOfPrice, "Unit Price");
            BillingScheduleManagementPage.ClickHyperLinkOnGrid(TableHeaders.Commands);

            var aspxPage = new CreateNewBillingScheduleManagementPage(driver);

            Assert.AreEqual(dealerName + " " + dealerName + " " + dealerName + " " + dealerName + " " + "AL", aspxPage.GetValueOfDropDown(FieldNames.Dealer), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(fleetName + " " + fleetName + " " + fleetName + " " + fleetName + " " + "AL" + " " + "Billing", aspxPage.GetValueOfDropDown(FieldNames.Fleet), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("Unit Price", aspxPage.GetValueOfDropDown(FieldNames.AdjustmentOfPrice), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("All", aspxPage.GetValueOfDropDown(FieldNames.AccountingDocumentType), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(CommonUtils.GetCurrentDate(), aspxPage.GetValue(FieldNames.EffectiveDate), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("All", aspxPage.GetValueOfDropDown(FieldNames.Currency), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("Amount", aspxPage.GetValueOfDropDown(FieldNames.Type), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("10.2355", aspxPage.GetValue(FieldNames.Value), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("AFLD", aspxPage.GetValueOfDropDown(FieldNames.PriceLevel), ErrorMessages.ValueMisMatch);
            Assert.AreEqual("No Tier", aspxPage.GetValueOfDropDown(FieldNames.TierType), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(dealerName + "Adj", aspxPage.GetValue(FieldNames.AdjustmentName), ErrorMessages.ValueMisMatch);

            aspxPage.UpdateBSMFields("15.0000", dealerName + "Adjustment");

            Console.WriteLine($"Successfully Updated Unit Price for: [{fleetName}]");

        }



    }
}
