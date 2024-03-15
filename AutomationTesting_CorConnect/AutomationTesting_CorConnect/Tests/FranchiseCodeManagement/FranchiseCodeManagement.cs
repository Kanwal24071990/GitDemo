using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.PartPurchasesReport;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.PageObjects.FranchiseCodeManagement;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FranchiseCodeManagement;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace AutomationTesting_CorConnect.Tests.FranchiseCodeManagement
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Franchise Code Management")]
    internal class FranchiseCodeManagement : DriverBuilderClass
    {
        FranchiseCodeManagementPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenSingleGridPage(Pages.FranchiseCodeManagement);
            Page = new FranchiseCodeManagementPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "25414" })]
        public void TC_25414(string UserType)
        {
            List<string> errorMsgs = new List<string>();
            Page.ClickPageTitle();
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.GetPageLabel(), Page.RenameMenuField(Pages.FranchiseCodeManagement), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile(false));
            if (Page.IsAnyDataOnGrid())
            {
                var franchiseCodes = FranchiseCodeManagementUtil.GetFranchiseCodes();
                Assert.IsTrue(franchiseCodes.Count > 0, ErrorMessages.NoRecordInDB);
                var names = FranchiseCodeManagementUtil.GetNames();
                Assert.IsTrue(names.Count > 0, ErrorMessages.NoRecordInDB);
                var gridRowCount = Page.GetPageCounterTotal();
                Assert.AreEqual(franchiseCodes.Count.ToString(), gridRowCount, ErrorMessages.ValueMisMatch);
                for (int i = 0; i < 3; i++)
                {
                    if (i < franchiseCodes.Count && i < names.Count)
                    {
                        Page.FilterTable(TableHeaders.FranchiseCode, franchiseCodes[i]);
                        Assert.IsTrue(Page.GetRowCountCurrentPage() == 1, ErrorMessages.NoRowAfterFilter);
                        Page.ResetFilter();
                        Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                        Page.FilterTable(TableHeaders.Name, names[i]);
                        Assert.IsTrue(Page.GetRowCountCurrentPage() == 1, ErrorMessages.NoRowAfterFilter);
                        Page.ResetFilter();
                        Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                    }
                }
                var visibleCodes = FranchiseCodeManagementUtil.GetVisibleCodes();
                Assert.IsTrue(visibleCodes.Count > 0, ErrorMessages.NoRecordInDB);
                if (visibleCodes.Contains("1") || visibleCodes.Contains(string.Empty))
                {
                    Page.FilterTable(TableHeaders.Display, "True");
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ValueMisMatch);
                }
                if (visibleCodes.Contains("0"))
                {
                    Page.FilterTable(TableHeaders.Display, "False");
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ValueMisMatch);
                }
                Page.ResetFilter();
                Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                Page.FilterTable(TableHeaders.Name, CommonUtils.RandomString(10));
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
