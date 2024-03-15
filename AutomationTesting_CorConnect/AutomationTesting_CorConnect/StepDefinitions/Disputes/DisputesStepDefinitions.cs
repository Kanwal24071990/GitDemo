using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.PageObjects.Disputes;
using AutomationTesting_CorConnect.PageObjects.FleetInvoices;
using AutomationTesting_CorConnect.PageObjects.Disputes;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.PageObjects.LineItemReport;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.Disputes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using AutomationTesting_CorConnect.DriverBuilder;

namespace AutomationTesting_CorConnect.StepDefinitions.Disputes
{
    [Binding]
    [Scope(Feature = "Disputes")]
    [Scope(Feature = "DisputeResolutionGUI")]
    internal class DisputesStepDefinitions : DriverBuilderClass

    {
        DisputesPage Page;
        FleetInvoiceTransactionLookupPage FITLPage;
        FleetInvoicesPage FIPage;
        InvoiceOptionsPage InvoiceOptionPopUpPage;
        InvoiceOptionsAspx InvoiceOptionsAspxPage;
        DisputesPage DPage;
        string transactionInvNum = null;
        string programInvNumber = null;
        string invNumberValue = null;
        string from = null;
        string to = null;

        [Given(@"Validate ""([^""]*)"" dropdown values")]
        public void ValidateDropDownValues(string dropDownName)
        {
            throw new PendingStepException();
        }

        [When(@"Dispute is created with ""(.*)"" for invoice ""([^""]*)""")]
        public void CreateDispute(string reason, string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
            {
                transactionInvNum = CommonUtils.GetInvoiceNumberForDisputeCreation();
                if (string.IsNullOrEmpty(transactionInvNum))
                {
                    CommonUtils.ToggleValidateProgramCode(false);
                    string dealerCode = CommonUtils.GetDealerCode();
                    string fleetCode = CommonUtils.GetFleetCode();
                    transactionInvNum = CommonUtils.RandomString(6);
                    Assert.IsTrue(new DMSServices().SubmitInvoice(transactionInvNum, dealerCode, fleetCode));

                }
            }
            else
            {
                transactionInvNum = invoiceNumber;
            }

            FITLPage = new FleetInvoiceTransactionLookupPage(driver);
            FITLPage.LoadDataOnGridWithProgramInvoiceNumber(programInvNumber);
            FITLPage.GoToInvoiceOptions(transactionInvNum);
            driver.SwitchTo().Frame(1);
            driver.SwitchTo().Frame(0);
            InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
            if (InvoiceOptionsAspxPage.GetSelectedValueSimpleSelect("Reason") != reason)
            {
                Task t = Task.Run(() => InvoiceOptionsAspxPage.WaitForStalenessOfElement(FieldNames.Notes));
                InvoiceOptionsAspxPage.SimpleSelectOptionByText("Reason", reason);
                t.Wait();
                t.Dispose();
            }
            InvoiceOptionsAspxPage.EnterText(FieldNames.Notes, "This Invoice doesnt belong to us");
            InvoiceOptionsAspxPage.UploadFile("Upload file", "UploadFiles//SamplePDF.pdf");
            if (reason == "Bad PO")
            {
                InvoiceOptionsAspxPage.EnterText("Correct PO", CommonUtils.RandomString(6));
            }
            else if (reason == "Duplicate Invoice")
            {
                InvoiceOptionsAspxPage.EnterText("Duplicate Invoice Number", CommonUtils.GetSettledInvoice());
            }
            else if (reason == "Already Paid")
            {
                InvoiceOptionsAspxPage.EnterText("Who Was Paid", "Admin");
                InvoiceOptionsAspxPage.EnterText("Date Paid", CommonUtils.GetCurrentDate());
                InvoiceOptionsAspxPage.EnterText("Paid By Check Number", CommonUtils.RandomString(6));
            }
            else if (reason == "Does not match Quoted Contract Price")
            {
                InvoiceOptionsAspxPage.EnterText("Incorrect Line Item", "Line 1");
                InvoiceOptionsAspxPage.EnterText("Correct Price", "50.00");
            }
            else if (reason == "Warranty")
            {
                InvoiceOptionsAspxPage.EnterText("Warranty Item", "Line 1");

            }
            if (InvoiceOptionsAspxPage.IsElementVisible("Dispute Acknowledge"))
            {
                InvoiceOptionsAspxPage.Click("Dispute Acknowledge");
            }
            InvoiceOptionsAspxPage.Click(ButtonsAndMessages.Submit);
            InvoiceOptionsAspxPage.WaitForLoadingGrid();
            //  InvoiceOptionsAspxPage.ClosePopupWindow();
        }

        [Then(@"Invoice ""([^""]*)"" should be disputed successfully")]
        public void ThenInvoiceShouldBeDisputedSuccessfully(string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
            {
            }
            else
            {
                transactionInvNum = invoiceNumber;
            }

            FITLPage.SwitchToMainWindow();
            FITLPage.LoadDataOnGrid(transactionInvNum);
            string transactionStatus = FITLPage.GetFirstRowData(TableHeaders.TransactionStatus);
            StringAssert.Contains("Disputed", transactionStatus);

        }


        [When(@"User goes to Invoice Options window for ""([^""]*)"" on ""([^""]*)"" page")]
        public void WhenUserGoesToInvoiceOptionsWindowFor(string InvStatus, string pagename)
        {
            switch (InvStatus)
            {
                case "Dispute Creation":
                    programInvNumber = CommonUtils.GetInvoiceNumberForDisputeCreation();

                    if (string.IsNullOrEmpty(programInvNumber))
                    {
                        Assert.Fail(ErrorMessages.NoRecordInDB);
                    }
                    else
                    {
                        FIPage = new FleetInvoicesPage(driver);
                        FIPage.LoadDataOnGridWithProgramInvoiceNumber(programInvNumber);
                        FIPage.GoToInvoiceOptions(programInvNumber);
                    }
                    driver.SwitchTo().Frame(1);
                    driver.SwitchTo().Frame("fDisputeInfo");


                    break;
                case "Dispute Updation":
                    programInvNumber = DisputesUtil.GetDisputedInvoiceNumber();
                    DisputesUtil.GetDataByProgramInvNumber(programInvNumber, out string from, out string to);

                    if (string.IsNullOrEmpty(programInvNumber))
                    {
                        Assert.Fail(ErrorMessages.NoRecordInDB);
                    }
                    else
                    {
                        switch (pagename)
                        {
                            case "Fleet Invoices":
                                FIPage = new FleetInvoicesPage(driver);
                                FIPage.LoadDataOnGridWithProgramInvoiceNumber(programInvNumber);
                                FIPage.GoToInvoiceOptions(programInvNumber);
                                break;
                            case "Disputes":
                                Page = new DisputesPage(driver);
                                Page.PopulateGrid(from, to);
                                Page.GoToInvoiceOptions(programInvNumber);
                                break;
                        }
                    }
                    InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
                    InvoiceOptionPopUpPage.SwitchTab("Dispute Info");
                    driver.SwitchTo().Frame("fDisputeInfo");
                    InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                    InvoiceOptionsAspxPage.Click(ButtonsAndMessages.Edit);
                    break;
                case "Resolving Dispute":
                    programInvNumber = DisputesUtil.GetDisputedInvoiceNumber();
                    DisputesUtil.GetDataByProgramInvNumber(programInvNumber, out from, out to);

                    if (string.IsNullOrEmpty(programInvNumber))
                    {
                        Assert.Fail(ErrorMessages.NoRecordInDB);
                    }
                    else
                    {
                        switch (pagename)
                        {
                            case "Fleet Invoices":
                                FIPage = new FleetInvoicesPage(driver);
                                FIPage.LoadDataOnGridWithProgramInvoiceNumber(programInvNumber);
                                FIPage.GoToInvoiceOptions(programInvNumber);
                                break;
                            case "Disputes":
                                Page = new DisputesPage(driver);
                                Page.PopulateGrid(from, to);
                                Page.GoToInvoiceOptions(programInvNumber);
                                break;
                        }
                    }
                    InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
                    InvoiceOptionPopUpPage.SwitchIframe(1);
                    driver.SwitchTo().Frame("fDisputeResolution");
                    break;
                case "ReDispute":
                    programInvNumber = DisputesUtil.GetDisputeResolvedInvoiceNumber();
                    DisputesUtil.GetDataByProgramInvNumber(programInvNumber, out from, out to);

                    if (string.IsNullOrEmpty(programInvNumber))
                    {
                        Assert.Fail(ErrorMessages.NoRecordInDB);

                    }
                    else
                    {
                        switch (pagename)
                        {
                            case "Fleet Invoices":
                                FIPage = new FleetInvoicesPage(driver);
                                FIPage.LoadDataOnGridWithProgramInvoiceNumber(programInvNumber);
                                FIPage.GoToInvoiceOptions(programInvNumber);
                                break;
                            case "Disputes":
                                Page = new DisputesPage(driver);
                                Page.PopulateGrid(from, to);
                                Page.GoToInvoiceOptions(programInvNumber);
                                break;
                        }
                    }
                    InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
                    driver.SwitchTo().Frame(1);
                    driver.SwitchTo().Frame("fDisputeInfo");
                    break;

            }
        }
        [When(@"Search by DateRange value ""([^""]*)""")]
        public void WhenSearchByDateRangeValue(string DateRangeValue)
        {
            DPage = new DisputesPage(driver);
            //IIRPage.SwitchToAdvanceSearch();
            DPage.SelectValueTableDropDown(FieldNames.DateRange, DateRangeValue);
            DPage.LoadDataOnGrid();

        }

        [Then(@"Data for ""([^""]*)"" is shown on the results grid")]
        public void ThenRecordsCountOnSearchGridShouldBeAsPer(string DateRangeValue)
        {

            if (DPage.IsAnyDataOnGrid())
            {
                string gridCount = DPage.GetPageCounterTotal();
                switch (DateRangeValue)
                {
                    case "Last 7 days":
                        int DateRangeLast7Days = DisputesUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast7Days, int.Parse(gridCount), "Last 7 days count mismatch");
                        break;
                    case "Last 14 days":
                        int DateRangeLast14Days = DisputesUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast14Days, int.Parse(gridCount), "Last 14 days count mismatch");
                        break;
                    case "Last 185 days":
                        int DateRangeLast185Days = DisputesUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLast185Days, int.Parse(gridCount), "Last 185 days count mismatch");
                        break;
                    case "Current month":
                        int DateRangeCurrentMonth = DisputesUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeCurrentMonth, int.Parse(gridCount), "Current Month count mismatch");
                        break;
                    case "Last month":
                        int DateRangeLastmonth = DisputesUtil.GetCountByDateRange(DateRangeValue, 0);
                        Assert.AreEqual(DateRangeLastmonth, int.Parse(gridCount), "Last month count mismatch");
                        break;
                    case "Customized date":
                        string CustomizedDate = CommonUtils.GetCustomizedDate();
                        int CustomeDate = Convert.ToInt32(CustomizedDate) - 1;
                        int DateRangeCustomizedDate = DisputesUtil.GetCountByDateRange(DateRangeValue, CustomeDate);
                        Assert.AreEqual(DateRangeCustomizedDate, int.Parse(gridCount), "Customized date count mismatch");
                        break;
                }
            }


        }



        [Then(@"Valid Fields should be displayed for ""([^""]*)""")]

        public void ThenValidFieldsShouldBeDisplayedFor(string inv)
        {
            List<string> errorMsgs = new List<string>();

            switch (inv)
            {
                case "Current Invoice":
                    InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                    errorMsgs = InvoiceOptionsAspxPage.VerifyDisputeInfoFields();
                    programInvNumber = CommonUtils.GetInvoiceNumberForDisputeCreation();
                    invNumberValue = InvoiceOptionsAspxPage.GetText(FieldNames.InvoiceNumberLabel);
                    Assert.AreEqual(programInvNumber, invNumberValue, "invoice number mismatch");
                    break;
                case "Disputed Invoice":
                    InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                    errorMsgs = InvoiceOptionsAspxPage.VerifyUpdateDisputeInfoFields();
                    programInvNumber = DisputesUtil.GetDisputedInvoiceNumber();
                    invNumberValue = InvoiceOptionsAspxPage.GetText(FieldNames.InvoiceNumberLabel);
                    Assert.AreEqual(programInvNumber, invNumberValue, "invoice number mismatch");
                    break;
                case "Dispute Resolution":
                    InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                    errorMsgs = InvoiceOptionsAspxPage.VerifyDisputeResolutionFields();
                    programInvNumber = DisputesUtil.GetDisputedInvoiceNumber();
                    invNumberValue = InvoiceOptionsAspxPage.GetText(FieldNames.DisputeResolutionInvoiceNumber);
                    Assert.AreEqual(programInvNumber, invNumberValue, "invoice number mismatch");
                    break;
                case "ReDispute":
                    InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                    errorMsgs = InvoiceOptionsAspxPage.VerifyReDisputeFields();
                    programInvNumber = DisputesUtil.GetDisputeResolvedInvoiceNumber();
                    invNumberValue = InvoiceOptionsAspxPage.GetText(FieldNames.InvoiceNumberLabel);
                    Assert.AreEqual(programInvNumber, invNumberValue, "invoice number mismatch");
                    break;
            }
            Assert.Multiple(() =>
            {
                foreach (string errorMsg in errorMsgs)
                {
                    Assert.Fail("Dispute Info Fields " + errorMsg);
                }
            });

        }


        [When(@"User clicks on ""([^""]*)"" hyperlink from search grid")]
        public void WhenUserClicksOnHyperlinkFromSearchGrid(string InvStatus)
        {
            switch (InvStatus)
            {
                case "Disputed Invoice":
                    programInvNumber = DisputesUtil.GetDisputedInvoiceNumber();
                    DisputesUtil.GetDataByProgramInvNumber(programInvNumber, out string from, out string to);
                    if (string.IsNullOrEmpty(programInvNumber))
                    {
                        Assert.Fail(ErrorMessages.NoRecordInDB);
                    }
                    else
                    {
                        Page = new DisputesPage(driver);
                        Page.PopulateGrid(from, to);
                        Page.GoToInvoiceOptions(programInvNumber);
                    }
                    break;
                case "Dispute Resolved Inv":
                    programInvNumber = DisputesUtil.GetDisputeResolvedInvoiceNumber();
                    DisputesUtil.GetDataByProgramInvNumber(programInvNumber, out from, out to);

                    if (string.IsNullOrEmpty(programInvNumber))
                    {
                        Assert.Fail(ErrorMessages.NoRecordInDB);
                    }
                    else
                    {
                        Page = new DisputesPage(driver);
                        Page.PopulateGrid(from, to);
                        Page.GoToInvoiceOptions(programInvNumber);
                    }
                    break;
            }

        }

        [Then(@"User should land on Invoice Options window ""([^""]*)"" tab")]

        public void ThenUserShouldLandOnInvoiceOptionsWindowTab(string tab)
        {
            switch (tab)
            {
                case "Dispute Resolution":
                    InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
                    InvoiceOptionPopUpPage.SwitchIframe(1);
                    driver.SwitchTo().Frame("fDisputeResolution");
                    InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                    bool a = InvoiceOptionsAspxPage.IsElementDisplayed(FieldNames.ReasonDetailLabel);
                    Assert.IsTrue(a);
                    break;
                case "Dispute Info":
                    InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
                    driver.SwitchTo().Frame(1);
                    driver.SwitchTo().Frame("fDisputeInfo");
                    InvoiceOptionsAspxPage = new InvoiceOptionsAspx(driver);
                    bool b = InvoiceOptionsAspxPage.IsElementDisplayed(FieldNames.Company);
                    Assert.IsTrue(b);
                    break;
            }

        }



    }

}

