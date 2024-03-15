using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.DealerPurchasesReport;
using AutomationTesting_CorConnect.PageObjects.PartPurchasesReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerPurchaseReport;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.DealerPurchasesReport
{
    [Binding]
    [Scope(Feature = "DealerPurchasesReport")]
    internal class DealerPurchasesReportStepDefinitions : DriverBuilderClass
    {
        DealerPurchasesReportPage DPRPage;

        [Then(@"Dropdown ""([^""]*)"" should have valid values on pop-up page")]
        public void PerformPopUpPageDropdownValuesValidation(string dropdown)
        {
            DPRPage = new DealerPurchasesReportPage(driver);

            switch (dropdown)
            {
                case "Date Range":
                    string[] dateRangePopUpPagesOptions = { "Customized date", "Last 7 days", "Current month", "Current Quarter", "YTD", "Last 12 months" };
                    Assert.IsTrue(DPRPage.VerifyValueDropDown(FieldNames.DateRange, dateRangePopUpPagesOptions));
                    //DPRPage.ClickPageTitle();
                    break;
            }
        }

        [When(@"Search by DateRange value ""([^""]*)"" for selected dealer ""([^""]*)"" and fleet ""([^""]*)""")]
        public void WhenSearchByDateRangeValueForSelectedDealerAndFleet(string dateRangeValue, string dealer, string fleet)
        {
            DPRPage = new DealerPurchasesReportPage(driver);
            DPRPage.ClearSelectionMultiSelectDropDown(FieldNames.DealerCompanyName);
            DPRPage.OpenAndFilterMultiSelectDropdown(FieldNames.DealerCompanyName, TableHeaders.AccountCode, dealer);
            DPRPage.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCompanyName);
            DPRPage.ClickPageTitle();
            DPRPage.OpenAndFilterMultiSelectDropdown(FieldNames.FleetCompanyName, TableHeaders.AccountCode, fleet);
            DPRPage.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCompanyName);
            DPRPage.ClickPageTitle();
            DPRPage.SelectValueTableDropDown(FieldNames.DateRange, dateRangeValue);
            DPRPage.ButtonClick(ButtonsAndMessages.Search);
            DPRPage.WaitForMsg(ButtonsAndMessages.Processing);
            DPRPage.WaitForMsg(ButtonsAndMessages.LoadingWithElipsis);

        }


        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRetreiveDataOnSearchGridForForTheSelectedDealerAndFleet(string dateRangeValue)
        {
            if (!DPRPage.IsAnyDataOnPopupPageGrid())
            {
                string invoiceCountUI = DPRPage.GetText(FieldNames.InvoiceCount);
                switch (dateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = DealerPurchaseReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(invoiceCountUI), "Last 7 days count mismatch");
                        break;
                    case "Current Quarter":
                        int DateRangeCurrentQuarter = DealerPurchaseReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentQuarter, int.Parse(invoiceCountUI), "Current Quarter count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = DealerPurchaseReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(invoiceCountUI), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = DealerPurchaseReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(invoiceCountUI), "Current Month count mismatch");
                        break;
                    case "Last 12 months":
                        int DateRangeLast12months = DealerPurchaseReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast12months, int.Parse(invoiceCountUI), "Last 12 Months count mismatch");
                        break;
                    case "YTD":
                        int DateRangeYTD = DealerPurchaseReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeYTD, int.Parse(invoiceCountUI), "YTD count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = DealerPurchaseReportUtils.GetCountByDateRange(dateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(invoiceCountUI), "Customized date count mismatch");
                        break;

                }
            }

        }
    }
}

