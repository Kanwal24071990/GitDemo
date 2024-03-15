using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ASN;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.ASN;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.ASN
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("ASN")]
    internal class ASN : DriverBuilderClass
    {
        ASNPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ASN);
            Page = new ASNPage(driver);
        }

        [Category(TestCategory.EOPSmoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24393" })]
        public void TC_24393(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ASN), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.DocumentNumber), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));
            Assert.IsTrue(Page.IsInputFieldVisible(FieldNames.PONumber), GetErrorMessage(ErrorMessages.FieldsNotDisplayed));
            
            Assert.IsTrue(Page.IsCheckBoxDisplayed(FieldNames.EnableGroupBy),GetErrorMessage(ErrorMessages.CheckBoxNotDisplayed));

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            ASNUtils.GetData(out string FromDate, out string ToDate);
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

                string docNumber = Page.GetFirstRowData(TableHeaders.DocNumber_);
                Page.FilterTable(TableHeaders.DocNumber_, docNumber);
               
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, false));

                Page.ClearFilter();
                Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));

                if (Page.IsAnyDataOnGrid())
                { 
                    errorMsgs.AddRange(Page.ValidateNestedGridButtonsWithoutExport(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                    List<string> headers = new List<String>()
                    {
                        TableHeaders.PONumber,
                        TableHeaders.LineNumber,
                        TableHeaders.MemberPart,
                        TableHeaders.VendorPart,
                        TableHeaders.Quantity,
                        TableHeaders.Description,
                        
                    };
                    errorMsgs.AddRange(Page.ValidateNestedTableHeaders());
                    string poNumber = Page.GetFirstRowDataNestedTable(TableHeaders.PONumber);
                    Page.FilterNestedTable(TableHeaders.PONumber, poNumber);
                    errorMsgs.AddRange(Page.ValidateNestedGridButtonsWithoutExport(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));                    
                }
                Page.ClickNestedGridTab("Document Tracking");
                if(Page.IsAnyDataOnGrid())
                {
                    Page.ValidateNestedTableDetails(true,false);
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
