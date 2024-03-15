using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.POTransactionLookup;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.POTransactionLookup;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.POTransactionLookup
{
    [Binding]
    [Scope(Feature = "POTransactionLookup")]
    internal class POTransactionLookupStepDefinitions : DriverBuilderClass
    {
        POTransactionLookupPage POTLPage;


        [When(@"Search by DateRange value ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue)
        {
            POTLPage = new POTransactionLookupPage(driver);
            POTLPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            POTLPage.LoadDataOnGrid();

        }


        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {

            if (POTLPage.IsAnyDataOnGrid())
            {
                string gridCount = POTLPage.GetPageCounterTotal();

                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = POTransactionLookupUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Last 14 days":
                        int DateRangeLast14Days = POTransactionLookupUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast14Days, int.Parse(gridCount), "Last 14 days count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = POTransactionLookupUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = POTransactionLookupUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last month":
                        int DateRangeLastmonth = POTransactionLookupUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLastmonth, int.Parse(gridCount), "Last month count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = POTransactionLookupUtils.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }


        }

    }

}
