using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.DealerInvoicePreApprovalReport;
using AutomationTesting_CorConnect.PageObjects.PartPurchasesReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.DealerInvoicePreApprovalReport;
using AutomationTesting_CorConnect.Utils.PartPurchasesReport;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.PartPurchasesReport
{
    [Binding]
    [Scope(Feature = "PartPurchasesReport")]
    internal class PartPurchasesReportStepDefinitions : DriverBuilderClass
    {
        PartPurchasesReportPage PPRPage;

        [Then(@"Dropdown ""([^""]*)"" should have valid values on pop-up ""([^""]*)"" page")]
        public void PerformPopUpPageDropdownValuesValidation(string dropdown, string pageName)
        {
            switch (dropdown)
            {
                case "Date Range":
                    PPRPage = new PartPurchasesReportPage(driver);
                    string[] dateRangePopUpPagesOptions = { "Customized date", "Last 7 days", "Current month", "Current Quarter", "YTD", "Last 12 months" };
                    Assert.IsTrue(PPRPage.VerifyValueDropDown(FieldNames.DateRange, dateRangePopUpPagesOptions));
                    PPRPage.ClickPageTitle();
                    break;
            }

        }

        [When(@"Search by DateRange value ""([^""]*)"" for selected dealer ""([^""]*)"" and fleet ""([^""]*)""")]
        public void WhenSearchByDateRangeValueForSelectedDealerAndFleet(string DateRangeValue, string dealer, string fleet)
        {
            PPRPage = new PartPurchasesReportPage(driver);
            PPRPage.OpenAndFilterMultiSelectDropdown(FieldNames.DealerCompanyName, TableHeaders.AccountCode, dealer);
            PPRPage.SelectFirstRowMultiSelectDropDown(FieldNames.DealerCompanyName);
            PPRPage.ClickPageTitle();
            PPRPage.OpenAndFilterMultiSelectDropdown(FieldNames.FleetCompanyName, TableHeaders.AccountCode, fleet);
            PPRPage.SelectFirstRowMultiSelectDropDown(FieldNames.FleetCompanyName);
            PPRPage.ClickPageTitle();
            PPRPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            PPRPage.ButtonClick(ButtonsAndMessages.Search);
            PPRPage.WaitForMsg(ButtonsAndMessages.Processing);
            PPRPage.WaitForMsg(ButtonsAndMessages.LoadingWithElipsis);

        }

        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRetreiveDataOnSearchGridForForTheSelectedDealerAndFleet(string dateRangeValue)
        {
            if (!PPRPage.IsAnyDataOnPopupPageGrid())
            {
                string qtyColumnUIFractionalCount = PPRPage.GetText(FieldNames.Quantity);
                decimal decimalValue = decimal.Parse(qtyColumnUIFractionalCount);
                int qtyColumnUIIntCount = Convert.ToInt32(decimalValue);
                switch (dateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = PartPurchasesReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, qtyColumnUIIntCount, "Last 7 days count mismatch");
                        break;
                    case "Current Quarter":
                        int DateRangeCurrentQuarter = PartPurchasesReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentQuarter, qtyColumnUIIntCount, "Current Quarter count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = PartPurchasesReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, qtyColumnUIIntCount, "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = PartPurchasesReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, qtyColumnUIIntCount, "Current Month count mismatch");
                        break;
                    case "Last 12 months":
                        int DateRangeLast12months = PartPurchasesReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast12months, qtyColumnUIIntCount, "Last 12 Months count mismatch");
                        break;
                    case "YTD":
                        int DateRangeYTD = PartPurchasesReportUtils.GetCountByDateRange(dateRangeValue, 0);
                        Assert.AreEqual(DateRangeYTD, qtyColumnUIIntCount, "YTD count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = PartPurchasesReportUtils.GetCountByDateRange(dateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, qtyColumnUIIntCount, "Customized date count mismatch");
                        break;

                }
            }

        }
    }
}

