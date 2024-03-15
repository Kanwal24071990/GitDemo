using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.TaxReviewReport;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.PageObjects.TransactionActivity;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.TransactionActivity;

namespace AutomationTesting_CorConnect.Tests.TransactionActivity
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Transaction Activity")]
    internal class TransactionActivity : DriverBuilderClass
    {
        TransactionActivityPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.TransactionActivity);
            Page = new TransactionActivityPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24400" })]
        public void TC_24400(string UserType)
        {
            List<string> errorMsgs = new List<string>();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.TransactionActivity), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(Page.IsElementVisible(FieldNames.CompanyName), ErrorMessages.ElementNotPresent + " : " + FieldNames.SellerName);
                Assert.IsTrue(Page.IsElementVisible(FieldNames.DealerName), ErrorMessages.ElementNotPresent + " : " + FieldNames.BuyerName);
                Assert.IsTrue(Page.IsElementVisible(FieldNames.FromDate), ErrorMessages.ElementNotPresent + " : " + FieldNames.FromDate);
                Assert.IsTrue(Page.IsElementVisible(FieldNames.ToDate), ErrorMessages.ElementNotPresent + " : " + FieldNames.ToDate);
            });
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());
            TransactionActivityUtil.GetSellerAndFromDate(out string FromDate, out string SellerName);
            Page.PopulateGrid(FromDate, SellerName.Trim());
            if (Page.IsAnyDataOnGrid())
            {
                string docNumber = Page.GetFirstRowData(TableHeaders.DocNumber_);
                Page.FilterTable(TableHeaders.DocNumber_, docNumber);
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ValueMisMatch);
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                Page.FilterTable(TableHeaders.DocNumber_, CommonUtils.RandomString(10));
                Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
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
