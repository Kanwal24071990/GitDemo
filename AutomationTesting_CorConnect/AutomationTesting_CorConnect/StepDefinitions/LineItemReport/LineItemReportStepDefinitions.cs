using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.LineItemReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.LineItemReport;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.LineItemReport
{
    [Binding]
    [Scope(Feature = "LineItemReport")]
    internal class LineItemReportStepDefinitions : DriverBuilderClass
    {
        LineItemReportPage LIRPage;


        [When(@"Search by DateRange value ""([^""]*)"" for selected dealer ""([^""]*)"" and fleet ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue, string dealer, string fleet)
        {
            LIRPage = new LineItemReportPage(driver);
            //IIRPage.SwitchToAdvanceSearch();
            LIRPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            LIRPage.WaitForMsg(ButtonsAndMessages.PleaseWait);
            LIRPage.OpenAndFilterMultiSelectDropdown(FieldNames.DealerCriteriaCompanyName, TableHeaders.AccountCode, dealer);
            LIRPage.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCriteriaCompanyName);
            LIRPage.OpenAndFilterMultiSelectDropdown(FieldNames.FleetCriteriaCompanyName, TableHeaders.AccountCode, fleet);
            LIRPage.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCriteriaCompanyName);
            LIRPage.LoadDataOnGrid();
        }

        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {
            if (LIRPage.IsAnyDataOnGrid())
            {
                string gridCount = LIRPage.GetPageCounterTotal();

                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = LineItemReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Current Quarter":
                        int DateRangeCurrentQuarter = LineItemReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentQuarter, int.Parse(gridCount), "Current Quarter count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = LineItemReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = LineItemReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last 12 months":
                        int DateRangeLast12months = LineItemReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast12months, int.Parse(gridCount), "Last 12 months count mismatch");
                        break;
                    case "YTD":
                        int DateRangeYTD = LineItemReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeYTD, int.Parse(gridCount), "YTD count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = LineItemReportUtils.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }


        }

    }


}