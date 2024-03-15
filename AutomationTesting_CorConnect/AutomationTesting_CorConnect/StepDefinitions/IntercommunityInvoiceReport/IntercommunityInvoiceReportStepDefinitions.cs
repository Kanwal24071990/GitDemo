﻿using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.IntercommunityInvoiceReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.IntercommunityInvoiceReport;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.IntercommunityInvoiceReport
{
    [Binding]
    [Scope(Feature = "IntercommunityInvoiceReport")]
    internal class IntercommunityInvoiceReportStepDefinitions : DriverBuilderClass
    {
        IntercommunityInvoiceReportPage IIRPage;


        [When(@"Search by DateRange value ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue)
        {
            IIRPage = new IntercommunityInvoiceReportPage(driver);
            IIRPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            IIRPage.LoadDataOnGrid();

        }


        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {
            if (IIRPage.IsAnyDataOnGrid())
            {
                string gridCount = IIRPage.GetPageCounterTotal();
                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = IntercommunityInvoiceReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Last 14 days":
                        int DateRangeLast14Days = IntercommunityInvoiceReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast14Days, int.Parse(gridCount), "Last 14 days count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = IntercommunityInvoiceReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = IntercommunityInvoiceReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last month":
                        int DateRangeLastmonth = IntercommunityInvoiceReportUtils.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLastmonth, int.Parse(gridCount), "Last month count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = IntercommunityInvoiceReportUtils.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }


        }

    }

}