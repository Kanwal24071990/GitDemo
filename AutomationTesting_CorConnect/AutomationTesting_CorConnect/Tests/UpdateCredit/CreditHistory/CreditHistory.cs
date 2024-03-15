using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.UpdateCredit;
using AutomationTesting_CorConnect.PageObjects.UpdateCredit.CreditHistory;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.UpdateCredit.CreditHistory;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Linq;


namespace AutomationTesting_CorConnect.Tests.UpdateCredit.CreditHistory
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Credit History")]
    internal class CreditHistory : DriverBuilderClass
    {
        CreditHistoryPage Page;
        CreditHistoryUtil creditHistoryUtil = new CreditHistoryUtil();

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.UpdateCredit, false, true);
            Page = new UpdateCreditPage(driver).OpenCreditHistory();
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17638" })]
        public void TC_17638(string UserType)
        {
            Page.SelectBillingValue(out string corCentricCode);
            Page.WaitForMsg(ButtonsAndMessages.PleaseWait);
            var gridData = creditHistoryUtil.GetTableData(corCentricCode, out decimal creditLimit, out string currency);

            Assert.Multiple(() =>
            {
                Assert.AreEqual((double)creditLimit, double.Parse(Page.GetFirstValueFromGrid("Main Table", "HeaderRow", "Credit Limit")), GetErrorMessage(ErrorMessages.CreditLimitMisMatch));
                Assert.AreEqual(gridData.Select(C => C.updateDate).ToList(), CommonUtils.RemoveTimeZone(Page.GetElementsList("Main Table", "HeaderRow", "Update Date")), GetErrorMessage(ErrorMessages.UpdateDateMisMatch));
                Assert.AreEqual(gridData.Select(C => C.UserName).ToList(), Page.GetElementsList("Main Table", "HeaderRow", "UserName"), GetErrorMessage(ErrorMessages.UserNameMisMatch));
                Assert.AreEqual(gridData.Select(C => C.firstName).ToList(), Page.GetElementsList("Main Table", "HeaderRow", "First Name"), GetErrorMessage(ErrorMessages.FirstNameMisMatch));
                Assert.AreEqual(gridData.Select(C => C.lastName).ToList(), Page.GetElementsList("Main Table", "HeaderRow", "Last Name"), GetErrorMessage(ErrorMessages.LastNameMisMatch));
                Assert.AreEqual(gridData.Select(C => C.isImpersonated).ToList(), Page.GetElementsList("Main Table", "HeaderRow", "Impersonation"), GetErrorMessage(ErrorMessages.ImpersonationColumnMisMatch));
                Assert.AreEqual(gridData.Select(C => C.impersonatedUserName).ToList(), Page.GetElementsList("Main Table", "HeaderRow", "Impersonated UserName"), GetErrorMessage(ErrorMessages.ImpersonatedColumnMisMatch));
                Assert.AreEqual(gridData.Select(C => C.creditLimit).ToList(), Page.GetElementsList("Main Table", "HeaderRow", "Credit Limit").Select(decimal.Parse).ToList(), GetErrorMessage(ErrorMessages.ImpersonatedColumnMisMatch));
                Assert.IsFalse(Page.GetElementsList("Main Table", "HeaderRow", "Currency").Any(x => x != currency.Trim()), GetErrorMessage(ErrorMessages.CurrencyMisMatch));
            });
        }
    }
}
