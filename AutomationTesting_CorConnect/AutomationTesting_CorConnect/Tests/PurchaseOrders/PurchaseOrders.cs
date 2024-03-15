using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.PurchaseOrders;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.PurchaseOrders;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.PurchaseOrders
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Purchase Orders")]
    internal class PurchaseOrders : DriverBuilderClass
    {
        PurchaseOrdersPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.PurchaseOrders);
            Page = new PurchaseOrdersPage(driver);
        }

        [Category(TestCategory.EOPSmoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24392" })]
        public void TC_24392(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.PurchaseOrders), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.PONumber), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));

            Assert.IsTrue(Page.IsCheckBoxDisplayed(FieldNames.EnableGroupBy), GetErrorMessage(ErrorMessages.CheckBoxNotDisplayed));

            List<string> headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.DealerName); // On UI Buyer Name
            Assert.Multiple(() =>
            {
                Assert.IsTrue(headerNames.Contains(TableHeaders.PartnerName), GetErrorMessage(ErrorMessages.ColumnMissing, TableHeaders.PartnerName, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, FieldNames.ContainsLocation));
            });

            List<string> headerNamesSeller = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.CompanyName); // On UI  Seller Name
            Assert.Multiple(() =>
            {
                Assert.IsTrue(headerNamesSeller.Contains(TableHeaders.PartnerName), GetErrorMessage(ErrorMessages.ColumnMissing, TableHeaders.PartnerName, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesSeller.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesSeller.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNamesSeller.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, FieldNames.ContainsLocation));
            });

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            PurchaseOrdersUtils.GetData(out string FromDate, out string ToDate);
            Page.LoadDataOnGrid(FromDate, ToDate);

            if (Page.IsAnyDataOnGrid())
            {
               
                Page.SetFilterCreiteria(TableHeaders.Vendor, FilterCriteria.Equals);

                string vendorName = Page.GetFirstRowData(TableHeaders.Vendor);
                Page.FilterTable(TableHeaders.Vendor, vendorName);
                Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Vendor, vendorName), ErrorMessages.NoRowAfterFilter);
                Page.FilterTable(TableHeaders.Vendor, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);

                string docNumber = Page.GetFirstRowData(TableHeaders.PO_);
                Page.FilterTable(TableHeaders.PO_, docNumber);

                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, false));

                Page.ClearFilter();
                Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateNestedGridButtonsWithoutExport(ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                    List<string> headers = new List<String>()
                    {
                        TableHeaders.LineNumber,
                        TableHeaders.VendorPart,
                        TableHeaders.MemberPart,
                        TableHeaders.Quantity,
                        TableHeaders.UnitPrice,
                        TableHeaders.LineExtended,

                    };
                    errorMsgs.AddRange(Page.ValidateNestedTableHeaders(headers.ToArray()));
                    errorMsgs.AddRange(Page.ValidateNestedGridButtonsWithoutExport(ButtonsAndMessages.Reset));                    
                }
                Page.ClickNestedGridTab("Document Tracking");
                if (Page.IsAnyDataOnGrid())
                {
                    Page.ValidateNestedTableDetails(true, false);
                }

                Page.ClickNestedGridTab("Purchase Order Items Fill");
                if (Page.IsAnyDataOnGrid())
                {
                    Page.ValidateNestedTableDetails(true, false);
                }

                Page.ClickNestedGridTab("Purchase Order ASN Shipment");
                if (Page.IsAnyDataOnGrid())
                {
                    Page.ValidateNestedTableDetails(true, false);
                }

                Page.ClickNestedGridTab("Document Fill");
                if (Page.IsAnyDataOnGrid())
                {
                    Page.ValidateNestedTableDetails(true, false);
                }

                if (Page.IsNextPage())
                {
                    Page.GoToPage(2);
                }
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
