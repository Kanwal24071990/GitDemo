using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetInvoiceTransactionLookup;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.FleetInvoiceTransactionLookup
{
    [Binding]
    [Scope(Feature = "FleetInvoiceTransactionLookup")]
    internal class FleetInvoiceTransactionLookupStepDefinitions : DriverBuilderClass
    {
        FleetInvoiceTransactionLookupPage FITLPage;


        [When(@"Advanced Search by DateRange value ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue)
        {
            FITLPage = new FleetInvoiceTransactionLookupPage(driver);
            FITLPage.SwitchToAdvanceSearch();
            FITLPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            FITLPage.LoadDataOnGrid();

        }


        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {
            if (FITLPage.IsAnyDataOnGrid())
            {
                string gridCount = FITLPage.GetPageCounterTotal();
                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = FleetInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Last 14 days":
                        int DateRangeLast14Days = FleetInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast14Days, int.Parse(gridCount), "Last 14 days count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = FleetInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = FleetInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last month":
                        int DateRangeLastmonth = FleetInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLastmonth, int.Parse(gridCount), "Last month count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = FleetInvoiceTransactionLookupUtil.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }

        }
        [When(@"User navigates to Advanced Search")]
        public void WhenNavigatesToAdvancedSearch()
        {
            FITLPage = new FleetInvoiceTransactionLookupPage(driver);
            FITLPage.SwitchToAdvanceSearch();
        }

        [Then(@"The message ""([^""]*)"" is shown")]
        public void ThenTheMessageIsShown(string expectedLast185DaysMessage)
        {
            string actualLast185DaysMessage = FITLPage.GetText(By.XPath("//table//span[contains(@id, 'AdvancedSearchNotes')]"));
            Assert.AreEqual(actualLast185DaysMessage, expectedLast185DaysMessage, ErrorMessages.MessageMismatch);

        }
        [When(@"User selects From date greater than 185 days on Advanced Search")]
        public void WhenUserSelectsFROMDateGreaterThanDaysOnAdvancedSearch()
        {
            FITLPage = new FleetInvoiceTransactionLookupPage(driver);
            FITLPage.SwitchToAdvanceSearch();
            FITLPage.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-185)));

        }

        [Then(@"The message Date Range cannot exceed 185 days is shown as a tooltip")]
        public void ThenTheMessageIsShownAsATooltip()
        {
            Assert.IsTrue(FITLPage.IsErrorIconVisibleWithTitle(ButtonsAndMessages.DateRangeError), "No error displayed for date range exceeding 185 days.");

        }
    }

}
