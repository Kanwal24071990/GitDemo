using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils.DealerInvoiceTransactionLookup;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using AutomationTesting_CorConnect.Utils;

namespace AutomationTesting_CorConnect.StepDefinitions.DealerInvoiceTransactionLookup
{
    [Binding]
    [Scope(Feature = "DealerInvoiceTransactionLookup")]
    internal class DealerInvoiceTransactionLookupStepDefinitions : DriverBuilderClass
    {
        DealerInvoiceTransactionLookupPage DITLPage;


        [When(@"Advanced Search by DateRange value ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue)
        {
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);
            DITLPage.SwitchToAdvanceSearch();
            DITLPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            DITLPage.LoadDataOnGrid();

        }


        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {
            if (DITLPage.IsAnyDataOnGrid())
            {
                string gridCount = DITLPage.GetPageCounterTotal();

                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = DealerInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Last 14 days":
                        int DateRangeLast14Days = DealerInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast14Days, int.Parse(gridCount), "Last 14 days count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = DealerInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = DealerInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last month":
                        int DateRangeLastmonth = DealerInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLastmonth, int.Parse(gridCount), "Last month count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = DealerInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }


        }
        [When(@"User navigates to Advanced Search")]
        public void WhenNavigatesToAdvancedSearch()
        {
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);
            DITLPage.SwitchToAdvanceSearch();
        }

        [Then(@"The message ""([^""]*)"" is shown")]
        public void ThenTheMessageIsShown(string expectedLast185DaysMessage)
        {
            string actualLast185DaysMessage = DITLPage.GetText(By.XPath("//table//span[contains(@id, 'AdvancedSearchNotes')]"));
            Assert.AreEqual(actualLast185DaysMessage, expectedLast185DaysMessage, ErrorMessages.MessageMismatch);

        }

        [When(@"User selects From date greater than 185 days on Advanced Search")]
        public void WhenUserSelectsFromDateGreaterThanDaysOnAdvancedSearch()
        {
            DITLPage = new DealerInvoiceTransactionLookupPage(driver);
            DITLPage.SwitchToAdvanceSearch();
            DITLPage.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-185)));

        }

        [Then(@"The message Date Range cannot exceed 185 days is shown as a tooltip")]
        public void ThenTheMessageIsShownAsATooltip()
        {
            Assert.IsTrue(DITLPage.IsErrorIconVisibleWithTitle(ButtonsAndMessages.DateRangeError), "No error displayed for date range exceeding 185 days.");

        }
    }
}
