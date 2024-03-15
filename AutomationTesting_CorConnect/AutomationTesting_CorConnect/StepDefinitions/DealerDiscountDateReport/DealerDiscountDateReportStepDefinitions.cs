using AutomationTesting_CorConnect.Tests.DealerDiscountDateReport;
using AutomationTesting_CorConnect.Utils.DealerDiscountDateReport;
using AutomationTesting_CorConnect.Resources;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using AutomationTesting_CorConnect.PageObjects.DealerDiscountDateReport;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.DriverBuilder;

namespace AutomationTesting_CorConnect.StepDefinitions.DealerDiscountDateReport
{
    [Binding]
    [Scope(Feature = "DealerDiscountDateReport")]
    internal class DealerDiscountDateReportStepDefinitions : DriverBuilderClass
    {
        DealerDiscountDateReportPage DDDRPage;


        [When(@"Search by DateRange value ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue)
        {
            DDDRPage = new DealerDiscountDateReportPage(driver);
            //DDDRPage.SwitchToAdvanceSearch();
            DDDRPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            DDDRPage.LoadDataOnGrid();

        }


        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {
            if (DDDRPage.IsAnyDataOnGrid())
            {
                string gridCount = DDDRPage.GetPageCounterTotal();
                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = DealerDiscountDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Last 14 days":
                        int DateRangeLast14Days = DealerDiscountDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast14Days, int.Parse(gridCount), "Last 14 days count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = DealerDiscountDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = DealerDiscountDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last month":
                        int DateRangeLastmonth = DealerDiscountDateReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLastmonth, int.Parse(gridCount), "Last month count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = DealerDiscountDateReportUtils.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }
            /* else
             {
                 Assert.Fail(ErrorMessages.NoDataOnGrid);
             } */

        }

    }

}
