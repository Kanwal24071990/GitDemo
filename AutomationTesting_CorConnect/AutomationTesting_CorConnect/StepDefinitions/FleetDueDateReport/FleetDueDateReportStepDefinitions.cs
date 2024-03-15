using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.FleetDueDateReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetDueDateReport;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.FleetDueDateReport
{
    [Binding]
    [Scope(Feature = "FleetDueDateReport")]
    internal class FleetDueDateReportStepDefinitions : DriverBuilderClass
    {
        FleetDueDateReportPage FDUDRPage;

        [When(@"Search by DateRange value ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue)
        {
            FDUDRPage = new FleetDueDateReportPage(driver);
            FDUDRPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            FDUDRPage.LoadDataOnGrid();

        }


        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {
            if (FDUDRPage.IsAnyDataOnGrid())
            {
                string gridCount = FDUDRPage.GetPageCounterTotal();
                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = FleetDueDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Last 14 days":
                        int DateRangeLast14Days = FleetDueDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast14Days, int.Parse(gridCount), "Last 14 days count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = FleetDueDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = FleetDueDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last month":
                        int DateRangeLastmonth = FleetDueDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLastmonth, int.Parse(gridCount), "Last month count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = FleetDueDateReportUtils.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }


        }


    }

}
