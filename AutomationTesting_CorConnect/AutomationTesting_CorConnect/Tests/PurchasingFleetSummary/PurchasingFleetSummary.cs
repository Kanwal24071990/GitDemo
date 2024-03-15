using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.PageObjects.PurchasingFleetSummary;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.PurchasingFleetSummary;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.PageObjects.DisplayPreferences;

namespace AutomationTesting_CorConnect.Tests.PurchasingFleetSummary
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Purchasing Fleet Summary")]
    internal class PurchasingFleetSummary : DriverBuilderClass
    {
        PurchasingFleetSummaryPage Page;
        DisplayPreferencesPage DisplayPrefPage;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.PurchasingFleetSummary);
            Page = new PurchasingFleetSummaryPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23611" })]
        public void TC_23611(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.PurchasingFleetSummary), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            PurchasingFleetSummaryUtils.GetData(out string from, out string to);
            Page.LoadDataOnGrid(string.Empty, string.Empty, from, to);

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                string dealerName = Page.GetFirstRowData(TableHeaders.DealerName);
                Page.FilterTable(TableHeaders.DealerName, dealerName);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.DealerName, dealerName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                Page.FilterTable(TableHeaders.DealerName, CommonUtils.RandomString(10));
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
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23839" })]
        public void TC_23839(string UserType)
        {
            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");

            Assert.IsTrue(new DMSServices().SubmitInvoiceWithAllNonPartItems(invoiceNum, dealerCode, fleetCode, fromDate));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);
            List<string> itemIds = CommonUtils.GetInvoiceLineDetailsItemIds(invoiceSectionObj.InvoiceSectionId);
            Assert.IsTrue(itemIds.All(x => string.IsNullOrEmpty(x)));
            Assert.That(itemIds, Has.Count.EqualTo(13));

            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesLabor, "Labor Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesLabor = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesLabor))));
            Assert.AreEqual(totalSalesLabor, Page.GetFirstRowData(TableHeaders.LaborTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesSublet, "Sublet Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesSublet = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesSublet))));
            Assert.AreEqual(totalSalesSublet, Page.GetFirstRowData(TableHeaders.SubletTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesShopSupplies, "Shop Supplies Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesShopSupplies = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesShopSupplies))));
            Assert.AreEqual(totalSalesShopSupplies, Page.GetFirstRowData(TableHeaders.ShopSuppliesTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesFreight, "Frieght Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesFreight = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesFreight))));
            Assert.AreEqual(totalSalesFreight, Page.GetFirstRowData(TableHeaders.FreightTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesOther, "Other Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesOther = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesOther))));
            Assert.AreEqual(totalSalesOther, Page.GetFirstRowData(TableHeaders.OtherTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalTax, "Tax Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalTax = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalTax))));
            Assert.AreEqual(totalTax, Page.GetFirstRowData(TableHeaders.TaxTotal));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23838" })]
        public void TC_23838(string UserType)
        {
            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            string enginePart = CommonUtils.GetPartByCategory("engine");
            string vendorPart = CommonUtils.GetPartByCategory("vendor");
            string proprietaryPart = CommonUtils.GetPartByCategory("f");
            string unrecognizedPart = CommonUtils.GetPartNotInCategory();
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");
            Assert.IsTrue(new DMSServices().SubmitInvoiceWithPartItems(invoiceNum, dealerCode, fleetCode, fromDate, enginePart, proprietaryPart, vendorPart, unrecognizedPart));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);

            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesVendor, "Vendor Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesVendor = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesVendor))));
            if (totalSalesVendor == "0.00")
            {
                totalSalesVendor = "0";
            }
            Assert.AreEqual(totalSalesVendor, Page.GetFirstRowData(TableHeaders.VendorPartsTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.VendorPartsTotalSales));

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesProprietary, "Proprietary Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesProprietary = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesProprietary))));
            if (totalSalesProprietary == "0.00")
            {
                totalSalesProprietary = "0";
            }
            Assert.AreEqual(totalSalesProprietary, Page.GetFirstRowData(TableHeaders.ProprietaryPartsTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.ProprietaryPartsTotalSales));

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesEngine, "Engine Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesEngine = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesEngine))));
            if (totalSalesEngine == "0.00")
            {
                totalSalesEngine = "0";
            }
            Assert.AreEqual(totalSalesEngine, Page.GetFirstRowData(TableHeaders.EnginePartsTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.EnginePartsTotalSales));


        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23780" })]
        public void TC_23780(string UserType)
        {
            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            string enginePart = CommonUtils.GetPartByCategory("engine");
            string vendorPart = CommonUtils.GetPartByCategory("vendor");
            string proprietaryPart = CommonUtils.GetPartByCategory("f");
            string unrecognizedPart = CommonUtils.GetPartNotInCategory();
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");
            Assert.IsTrue(new DMSServices().SubmitInvoiceWithAllLineItems(invoiceNum, dealerCode, fleetCode, fromDate, enginePart, proprietaryPart, vendorPart, unrecognizedPart));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);

            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesLabor, "Labor Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesLabor = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesLabor))));
            Assert.AreEqual(totalSalesLabor, Page.GetFirstRowData(TableHeaders.LaborTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.LaborTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesSublet, "Sublet Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesSublet = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesSublet))));
            Assert.AreEqual(totalSalesSublet, Page.GetFirstRowData(TableHeaders.SubletTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.SubletTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesShopSupplies, "Shop Supplies Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesShopSupplies = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesShopSupplies))));
            Assert.AreEqual(totalSalesShopSupplies, Page.GetFirstRowData(TableHeaders.ShopSuppliesTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.ShopSuppliesTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesFreight, "Frieght Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesFreight = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesFreight))));
            Assert.AreEqual(totalSalesFreight, Page.GetFirstRowData(TableHeaders.FreightTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.FreightTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesOther, "Other Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesOther = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesOther))));
            Assert.AreEqual(totalSalesOther, Page.GetFirstRowData(TableHeaders.OtherTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.OtherTotalSales));
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalTax, "Tax Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalTax = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalTax))));
            Assert.AreEqual(totalTax, Page.GetFirstRowData(TableHeaders.TaxTotal), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.TaxTotal));

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesVendor, "Vendor Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesVendor = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesVendor))));
            if (totalSalesVendor == "0.00")
            {
                totalSalesVendor = "0";
            }
            Assert.AreEqual(totalSalesVendor, Page.GetFirstRowData(TableHeaders.VendorPartsTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.VendorPartsTotalSales));

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesProprietary, "Proprietary Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesProprietary = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesProprietary))));
            if (totalSalesProprietary == "0.00")
            {
                totalSalesProprietary = "0";
            }
            Assert.AreEqual(totalSalesProprietary, Page.GetFirstRowData(TableHeaders.ProprietaryPartsTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.ProprietaryPartsTotalSales));

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesEngine, "Engine Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesEngine = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesEngine))));
            if (totalSalesEngine == "0.00")
            {
                totalSalesEngine = "0";
            }
            Assert.AreEqual(totalSalesEngine, Page.GetFirstRowData(TableHeaders.EnginePartsTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.EnginePartsTotalSales));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23709" })]
        public void TC_23709(string UserType)
        {
            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            string enginePart = CommonUtils.GetPartByCategory("engine");
            string vendorPart = CommonUtils.GetPartByCategory("vendor");
            string proprietaryPart = CommonUtils.GetPartByCategory("f");
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");
            Assert.IsTrue(new DMSServices().SubmitInvoiceWithPartItemsRecognized(invoiceNum, dealerCode, fleetCode, fromDate, enginePart, proprietaryPart, vendorPart));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);

            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesVendor, "Vendor Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesVendor = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesVendor))));
            if (totalSalesVendor == "0.00")
            {
                totalSalesVendor = "0";
            }
            Assert.AreEqual(totalSalesVendor, Page.GetFirstRowData(TableHeaders.VendorPartsTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.VendorPartsTotalSales));

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesProprietary, "Proprietary Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesProprietary = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesProprietary))));
            if (totalSalesProprietary == "0.00")
            {
                totalSalesProprietary = "0";
            }
            Assert.AreEqual(totalSalesProprietary, Page.GetFirstRowData(TableHeaders.ProprietaryPartsTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.ProprietaryPartsTotalSales));

            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesEngine, "Engine Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesEngine = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesEngine))));
            if (totalSalesEngine == "0.00")
            {
                totalSalesEngine = "0";
            }
            Assert.AreEqual(totalSalesEngine, Page.GetFirstRowData(TableHeaders.EnginePartsTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.EnginePartsTotalSales));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23700" })]
        public void TC_23700(string UserType)
        {
            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");
            Assert.IsTrue(new DMSServices().SubmitInvoiceWithLineItemType(invoiceNum, dealerCode, fleetCode, fromDate, "F", "Frieght"));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);
            List<string> itemIds = CommonUtils.GetInvoiceLineDetailsItemIds(invoiceSectionObj.InvoiceSectionId);
            Assert.IsTrue(itemIds.All(x => string.IsNullOrEmpty(x)));
            Assert.That(itemIds, Has.Count.EqualTo(1));
            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesFreight, "Frieght Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesFreight = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesFreight))));
            Assert.AreEqual(totalSalesFreight, Page.GetFirstRowData(TableHeaders.FreightTotalSales));

        }


        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23697" })]
        public void TC_23697(string UserType)
        {

            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");
            Assert.IsTrue(new DMSServices().SubmitInvoiceWithLineItemType(invoiceNum, dealerCode, fleetCode, fromDate, "S", "Shop suplies"));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);
            List<string> itemIds = CommonUtils.GetInvoiceLineDetailsItemIds(invoiceSectionObj.InvoiceSectionId);
            Assert.IsTrue(itemIds.All(x => string.IsNullOrEmpty(x)));
            Assert.That(itemIds, Has.Count.EqualTo(1));
            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesShopSupplies, "Shop Supplies Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesShopSupplies = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesShopSupplies))));
            Assert.AreEqual(totalSalesShopSupplies, Page.GetFirstRowData(TableHeaders.ShopSuppliesTotalSales));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23696" })]
        public void TC_23696(string UserType)
        {

            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");
            Assert.IsTrue(new DMSServices().SubmitInvoiceWithLineItemType(invoiceNum, dealerCode, fleetCode, fromDate, "L", "Labor"));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);
            List<string> itemIds = CommonUtils.GetInvoiceLineDetailsItemIds(invoiceSectionObj.InvoiceSectionId);
            Assert.IsTrue(itemIds.All(x => string.IsNullOrEmpty(x)));
            Assert.That(itemIds, Has.Count.EqualTo(1));
            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesLabor, "Labor Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesLabor = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesLabor))));
            Assert.AreEqual(totalSalesLabor, Page.GetFirstRowData(TableHeaders.LaborTotalSales), GetErrorMessage(ErrorMessages.IncorrectValue, TableHeaders.LaborTotalSales));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23692" })]
        public void TC_23692(string UserType)
        {
            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");

            Assert.IsTrue(new DMSServices().SubmitInvoiceWithNonPartItems(invoiceNum, dealerCode, fleetCode, fromDate));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);
            List<string> itemIds = CommonUtils.GetInvoiceLineDetailsItemIds(invoiceSectionObj.InvoiceSectionId);
            Assert.IsTrue(itemIds.All(x => string.IsNullOrEmpty(x)));
            Assert.That(itemIds, Has.Count.EqualTo(8));

            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesOther, "Other Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesOther = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesOther))));
            Assert.AreEqual(totalSalesOther, Page.GetFirstRowData(TableHeaders.OtherTotalSales));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23688" })]
        public void TC_23688(string UserType)
        {
            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");
            string unrecognizedPart = CommonUtils.GetPartNotInCategory();

            Assert.IsTrue(new DMSServices().SubmitInvoiceWithLineItemType(invoiceNum, dealerCode, fleetCode, fromDate, "P", "152pJytesh"));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);
            List<string> itemIds = CommonUtils.GetInvoiceLineDetailsItemIds(invoiceSectionObj.InvoiceSectionId);
            Assert.IsTrue(itemIds.All(x => string.IsNullOrEmpty(x)));
            Assert.That(itemIds, Has.Count.EqualTo(1));

            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesUnrecognized, "Unrecognized Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesUnrecognized = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesUnrecognized))));
            Assert.AreEqual(totalSalesUnrecognized, Page.GetFirstRowData(TableHeaders.UnrecognizedTotalSales));

            invoiceNum = CommonUtils.RandomString(6);
            Assert.IsTrue(new DMSServices().SubmitInvoiceWithLineItemType(invoiceNum, dealerCode, fleetCode, fromDate, "P", unrecognizedPart));
            invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);

            Page.GridLoad();
            PurchasingFleetSummaryUtils.GetTotalSales(out totalSalesUnrecognized, "Unrecognized Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesUnrecognized = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesUnrecognized))));
            Assert.AreEqual(totalSalesUnrecognized, Page.GetFirstRowData(TableHeaders.UnrecognizedTotalSales));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23687" })]
        public void TC_23687(string UserType)
        {
            string invoiceNum = CommonUtils.RandomString(6);
            string dealerCode = CommonUtils.GetDealerCode();
            string fleetCode = CommonUtils.GetFleetCode();
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");


            Assert.IsTrue(new DMSServices().SubmitInvoiceWithLineSubletItemTypes(invoiceNum, dealerCode, fleetCode, fromDate));
            InvoiceObject invoiceObj = CommonUtils.GetInvoiceByTransactionNumber(invoiceNum);
            Assert.IsNotNull(invoiceObj);
            InvoiceObject invoiceSectionObj = CommonUtils.GetInvoiceSectionByInvoiceId(invoiceObj.InvoiceId);
            Assert.IsNotNull(invoiceSectionObj);
            List<string> itemIds = CommonUtils.GetInvoiceLineDetailsItemIds(invoiceSectionObj.InvoiceSectionId);
            Assert.IsTrue(itemIds.All(x => string.IsNullOrEmpty(x)));
            Assert.That(itemIds, Has.Count.EqualTo(2));

            Page.LoadDataOnGrid(dealerCode, fleetCode, string.Empty, string.Empty);
            PurchasingFleetSummaryUtils.GetTotalSales(out string totalSalesSublet, "Sublet Total", dealerCode, fleetCode, fromDateDB, toDateDB);
            totalSalesSublet = String.Format("{0:n}", Convert.ToInt64(Math.Floor(Convert.ToDouble(totalSalesSublet))));
            Assert.AreEqual(totalSalesSublet, Page.GetFirstRowData(TableHeaders.SubletTotalSales));

        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23614" })]
        public void TC_23614(string UserType)
        {

            List<string> headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.Dealer);
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
            int totalCount = Page.GetTotalCountMultiSelectDropDown(FieldNames.Dealer);
            string entityCountFromDB = CommonUtils.GetDealerEntityCount();
            Assert.AreEqual(int.Parse(entityCountFromDB), totalCount);
            string dealerCode = CommonUtils.GetDealerCode();
            Assert.True(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealerCode));
            string inactiveDealerCode = CommonUtils.GetInactiveDealerCode();
            Assert.True(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, inactiveDealerCode));
            string terminatedDealerCode = CommonUtils.GetTerminatedDealerCode();
            Assert.True(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, terminatedDealerCode));
            string suspendedDealerCode = CommonUtils.GetSuspendedDealerCode();
            Assert.True(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, suspendedDealerCode));
            string dealerMaster = AccountMaintenanceUtil.GetDealerCode(LocationType.MasterBilling);
            Assert.True(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealerMaster));
            string dealerBilling = AccountMaintenanceUtil.GetDealerCode(LocationType.Billing);
            Assert.True(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealerBilling));
            string dealerShop = AccountMaintenanceUtil.GetDealerCode(LocationType.Shop);
            Assert.True(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealerShop));
            string fleetCodeMaster = AccountMaintenanceUtil.GetFleetCode(LocationType.MasterBilling);
            Assert.IsFalse(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, fleetCodeMaster));
            string fleetCodeBilling = AccountMaintenanceUtil.GetFleetCode(LocationType.Billing);
            Assert.IsFalse(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, fleetCodeBilling));
            string fleetCodeShop = AccountMaintenanceUtil.GetFleetCode(LocationType.Shop);
            Assert.IsFalse(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, fleetCodeShop));
            string partiallyEnrolledDealer = CommonUtils.GetPartiallyEnrolledDealerCode();
            Assert.IsFalse(Page.VerifyDataInMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, partiallyEnrolledDealer));
            if (Page.IsNextPageMultiSelectDropdown(FieldNames.Dealer))
            {
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
            }
            Page.SelectAllRowsMultiSelectDropDown(FieldNames.Dealer);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Dealer), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Dealer));

            List<string> headerNamesFleetDD = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.Fleet);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.Country), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Country, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.EntityCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, FieldNames.ContainsLocation));
            });
            int totalCountFleetDD = Page.GetTotalCountMultiSelectDropDown(FieldNames.Fleet);
            string fleetEntityCountFromDB = CommonUtils.GetFleetEntityCount();
            Assert.AreEqual(int.Parse(fleetEntityCountFromDB), totalCountFleetDD);

            Assert.IsTrue(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, fleetCodeMaster));
            Assert.IsTrue(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, fleetCodeBilling));
            Assert.IsTrue(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, fleetCodeShop));
            string inactiveFleetCode = CommonUtils.GetInactiveFleetCode();
            Assert.IsTrue(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, inactiveFleetCode));
            string terminatedFleetCode = CommonUtils.GetTerminatedFleetCode();
            Assert.IsTrue(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, terminatedFleetCode));
            string suspendedFleetCode = CommonUtils.GetSuspendedFleetCode();
            Assert.IsTrue(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, suspendedFleetCode));
            Assert.IsFalse(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, dealerMaster));
            Assert.IsFalse(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, dealerBilling));
            Assert.IsFalse(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, dealerShop));
            string partiallyEnrolledFleet = CommonUtils.GetPartiallyEnrolledFleetCode();
            Assert.IsFalse(Page.VerifyDataInMultiSelectDropDown(FieldNames.Fleet, TableHeaders.AccountCode, partiallyEnrolledFleet));
            Page.SelectAllRowsMultiSelectDropDown(FieldNames.Fleet);
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.Fleet), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Fleet));
            if (Page.IsNextPageMultiSelectDropdown(FieldNames.Fleet))
            {
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.Country), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Country, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.EntityCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, FieldNames.ContainsLocation));
                    Assert.IsTrue(headerNamesFleetDD.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, FieldNames.ContainsLocation));
                });
            }

            menu.OpenNextPage(Pages.PurchasingFleetSummary, Pages.DisplayPreferences, true);
            DisplayPrefPage = new DisplayPreferencesPage(driver);
            DisplayPrefPage.SwitchIframe();
            DisplayPrefPage.EnterTextAfterClear(FieldNames.NumberRowsDisplayedPerPage, "10");
            DisplayPrefPage.EnterTextAfterClear(FieldNames.NumberRowsDisplayedPerDropdown, "10");
            DisplayPrefPage.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.LocaleSettings, "en-AU");
            DisplayPrefPage.SaveDisplayPreferences();
            menu.OpenNextPage(Pages.DisplayPreferences, Pages.PurchasingFleetSummary);
            Page = new PurchasingFleetSummaryPage(driver);
            Page.SwitchToMainWindow();
            Page.GridLoad();
            var rowCount = Page.GetRowCountCurrentPage();
            Assert.That(rowCount, Is.LessThanOrEqualTo(10));
            Page.SelectAllRowsMultiSelectDropDown(FieldNames.Dealer);
            string selectedRowsDealer = Page.GetSelectedRowCountMultiSelectDropDown(FieldNames.Dealer);
            Assert.AreEqual("10", selectedRowsDealer);
            Page.SelectAllRowsMultiSelectDropDown(FieldNames.Fleet);
            string selectedRowsFleet = Page.GetSelectedRowCountMultiSelectDropDown(FieldNames.Fleet);
            Assert.AreEqual("10", selectedRowsFleet);
            var fromDate = DateTime.ParseExact(Page.GetValue(FieldNames.From), CommonUtils.GetClientDateFormat(), null);
            var toDate = DateTime.ParseExact(Page.GetValue(FieldNames.To), CommonUtils.GetClientDateFormat(), null);
            var fromDateDB = fromDate.ChangeDateFormat("yyyy-MM-dd");
            var toDateDB = toDate.ChangeDateFormat("yyyy-MM-dd");
            Page.ClearSelectionMultiSelectDropDown(FieldNames.Dealer);
            Page.ClearSelectionMultiSelectDropDown(FieldNames.Fleet);
            Page.SelectFirstRowMultiSelectDropDown(FieldNames.Dealer, TableHeaders.AccountCode, dealerCode);
            Page.GridLoad();
            PurchasingFleetSummaryUtils.GetTotalInvoiceCount(out string totalInvCount, dealerCode, fromDateDB, toDateDB);
            Assert.AreEqual(totalInvCount, Page.GetFirstRowData(TableHeaders.InvoiceCount));
        }

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25875" })]
        public void TC_25875(string UserType, string DealerUser)
        {
            var errorMsgs = Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Dealer, LocationType.ParentShop, Constants.UserType.Dealer, null);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Fleet, LocationType.ParentShop, Constants.UserType.Fleet, null));
            menu.ImpersonateUser(DealerUser, Constants.UserType.Dealer);
            menu.OpenPage(Pages.PurchasingFleetSummary);
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Dealer, LocationType.ParentShop, Constants.UserType.Dealer, DealerUser));
            errorMsgs.AddRange(Page.VerifyLocationCountMultiSelectDropdown(FieldNames.Fleet, LocationType.ParentShop, Constants.UserType.Fleet, null));
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail(errorMsg);
                }
            });
        }
    }
}
