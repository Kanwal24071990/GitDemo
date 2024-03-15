using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceDetailReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetInvoiceDetailReport;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.FleetInvoiceDetailReport
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Invoice Detail Report")]
    internal class FleetInvoiceDetailReport : DriverBuilderClass
    {
        FleetInvoiceDetailReportPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetInvoiceDetailReport);
            Page = new FleetInvoiceDetailReportPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23589" })]
        public void TC_23589(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(menu.RenameMenuField(Pages.FleetInvoiceDetailReport), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
                Assert.IsTrue(buttons.Count > 0);

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                FleetInvoiceDetailReportUtils.GetData(out string from, out string to);
                Page.LoadDataOnGrid(from, to);

                Page.WaitForGridLoad();
                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                   
                    string filterHeaderName = TableHeaders.ProgramInvoiceNumber;
                    if (!Page.IsTableHeaderExists(filterHeaderName))
                    {
                        filterHeaderName = TableHeaders.ProgramInv_;
                    }
                    Page.ClickTableHeader(filterHeaderName);
                    Page.ClickTableHeader(filterHeaderName);
                    string programInvoiceNumber = Page.GetFirstRowData(filterHeaderName);
                    if (!string.IsNullOrEmpty(programInvoiceNumber))
                    {
                        Page.FilterTable(filterHeaderName, programInvoiceNumber);
                        Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(filterHeaderName, programInvoiceNumber), ErrorMessages.NoRowAfterFilter);
                        Page.ClearFilter();
                        Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                        Page.FilterTable(filterHeaderName, programInvoiceNumber);
                        Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(filterHeaderName, programInvoiceNumber), ErrorMessages.NoRowAfterFilter);
                        Page.ResetFilter();
                        Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                    }
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

        [Category(TestCategory.FunctionalTest)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25832" })]
        public void TC_25832(string UserType, string fleetUser)
        {
            var errorMsgs = Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, null);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Dealer, null));
            menu.ImpersonateUser(fleetUser, Constants.UserType.Fleet);
            menu.OpenPage(Pages.FleetInvoiceDetailReport);
            errorMsgs.AddRange(Page.VerifyLocationMultipleColumnsDropdown(FieldNames.CompanyName, LocationType.ParentShop, Constants.UserType.Fleet, fleetUser));
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
