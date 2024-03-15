using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.DealerInvoices;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerInvoices;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.DealerInvoices
{
    [Binding]
    [Scope(Feature = "DealerInvoices")]
    internal class DealerInvoicesStepDefinitions : DriverBuilderClass
    {
        DealerInvoicesPage DIPage;


        [When(@"Advanced Search by DateRange value ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue)
        {
            DIPage = new DealerInvoicesPage(driver);
            DIPage.SwitchToAdvanceSearch();
            DIPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            DIPage.LoadDataOnGrid();

        }


        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {
            if (DIPage.IsAnyDataOnGrid())
            {
                string gridCount = DIPage.GetPageCounterTotal();
                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = DealerInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Last 14 days":
                        int DateRangeLast14Days = DealerInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast14Days, int.Parse(gridCount), "Last 14 days count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = DealerInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = DealerInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last month":
                        int DateRangeLastmonth = DealerInvoicesUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLastmonth, int.Parse(gridCount), "Last month count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = DealerInvoicesUtils.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }

        }

        [When(@"User navigates to Advanced Search")]
        public void WhenNavigatesToAdvancedSearch()
        {
            DIPage = new DealerInvoicesPage(driver);
            DIPage.SwitchToAdvanceSearch();
        }

        [Then(@"The message ""([^""]*)"" is shown")]
        public void ThenTheMessageIsShown(string expectedLast185DaysMessage)
        {
            string actualLast185DaysMessage = DIPage.GetText(By.XPath("//table//span[contains(@id, 'AdvancedSearchNotes')]"));
            Assert.AreEqual(actualLast185DaysMessage, expectedLast185DaysMessage, ErrorMessages.MessageMismatch);

        }

        [When(@"User selects From date greater than 185 days on Advanced Search")]
        public void WhenUserSelectsFromDateGreaterThanDaysOnAdvancedSearch()
        {
            DIPage = new DealerInvoicesPage(driver);
            DIPage.SwitchToAdvanceSearch();
            DIPage.EnterFromDate(CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(-185)));

        }

        [Then(@"The message Date Range cannot exceed 185 days is shown as a tooltip")]
        public void ThenTheMessageIsShownAsATooltip()
        {
            Assert.IsTrue(DIPage.IsErrorIconVisibleWithTitle(ButtonsAndMessages.DateRangeError), "No error displayed for date range exceeding 185 days.");

        }

    }

}


