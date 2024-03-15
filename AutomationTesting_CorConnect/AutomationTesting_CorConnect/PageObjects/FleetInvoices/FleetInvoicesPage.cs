using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AutomationTesting_CorConnect.PageObjects.FleetInvoices
{
    internal class FleetInvoicesPage : Commons
    {
        internal FleetInvoicesPage(IWebDriver driver) : base(driver, Pages.FleetInvoices) { }

        internal void PopulateGrid(string fromDate, string toDate, string paymentMethod)
        {
            SwitchToAdvanceSearch();
            WaitForElementToBeClickable(GetElement(FieldNames.From));
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            SelectValueTableDropDown(FieldNames.PaymentMethod, paymentMethod);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void LoadDataOnGrid(string fromDate, string toDate , bool enableGroupBy = true)
        {
            if (GetValueDropDown(FieldNames.SearchType) != "Advanced Search")
            {
                SwitchToAdvanceSearch();
            }
            WaitForElementToBeClickable(GetElement(FieldNames.From));
            if (enableGroupBy = true)
            {
                ClickElement(FieldNames.EnableGroupBy);
            }
            EnterFromDate(fromDate);
            EnterToDate(toDate);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
           
        }

        internal void GridLoadWithInvNum(string invoiceNumber)
        {
            EnterTextAfterClear(FieldNames.DealerInvoiceNumber, invoiceNumber);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
          
        }

            internal List<string> VerifyOnlinePaymentMethod(List<FleetInvoiceObject> fleetInvoicesList)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                FleetInvoiceObject fleetInvoice = null;
                string paymentMethod = null;
                for (int i = 0; i < 5; i++)
                {
                    fleetInvoice = fleetInvoicesList[CommonUtils.GenerateRandom(0, fleetInvoicesList.Count - 1)];
                    FilterTableByColumnCount(TableHeaders.ProgramInvoiceNumber, fleetInvoice.InvoiceNumber);
                    if (GetRowCount() > 0)
                    {
                        paymentMethod = gridHelper.GetElementByIndex(TableHeaders.PaymentMethod).Trim();
                        if (fleetInvoice.IsOnline && paymentMethod != "Online")
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.IncorrectValue, FieldNames.PaymentMethod) +
                                string.Format(LoggerMesages.ExpectedValue, "Online", paymentMethod) + $" InvoiceNumber [{fleetInvoice.InvoiceNumber}]");
                        }
                        else if (!fleetInvoice.IsOnline && !string.IsNullOrEmpty(paymentMethod))
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.IncorrectValue, FieldNames.PaymentMethod) +
                                string.Format(LoggerMesages.ExpectedValue, "", paymentMethod) + $" InvoiceNumber [{fleetInvoice.InvoiceNumber}]");
                        }
                    }
                    else
                    {
                        errorMsgs.Add(ErrorMessages.NoRowAfterFilter + $" InvoiceNumber [{fleetInvoice.InvoiceNumber}]");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }

            return errorMsgs;
        }

        internal void SelectLoadBookmarkFirstRow()
        {
            SelectValueFirstRow(FieldNames.LoadBookmark);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        internal void SelectDateRangeFirstRow()
        {
            SelectValueFirstRowOpenByFieldClick(FieldNames.DateRange);
        }

        internal void SelectDealerGroupFirstRow()
        {
            SelectValueFirstRow(FieldNames.DealerGroup);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        internal void SelectFleetGroupFirstRow()
        {
            SelectValueFirstRow(FieldNames.FleetGroup);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        public void LoadDataOnGrid(string DealerInvoiceNumber)
        {
            ClearText(FieldNames.DealerInvoiceNumber);
            EnterText(FieldNames.DealerInvoiceNumber, DealerInvoiceNumber);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        public void LoadDataOnGridWithProgramInvoiceNumber(string ProgramInvoiceNumber)
        {
            ClearText(FieldNames.ProgramInvoiceNumber);
            EnterText(FieldNames.ProgramInvoiceNumber, ProgramInvoiceNumber);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        public void LoadDataOnGridWithProgramInvoiceNumber(string ProgramInvoiceNumber, string Date)
        {
            SwitchToAdvanceSearch();
            EnterTextAfterClear(FieldNames.ProgramInvoiceNumber, ProgramInvoiceNumber);
            EnterFromDate(Date);
            EnterToDate(Date);
            ClearText(FieldNames.ProgramInvoiceNumber);
            EnterText(FieldNames.ProgramInvoiceNumber, ProgramInvoiceNumber);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal void SelectDateRange(string value)
        {
            if (value != GetValueDropDown(FieldNames.DateRange))
            {
                SelectValueTableDropDown(FieldNames.DateRange, value);
                WaitForMsg(ButtonsAndMessages.PleaseWait);
            }
        }

        internal void SwitchToLastSevenDayDateRange(string value = "Last 7 days")
        {
            if (value != GetValueDropDown(FieldNames.DateRange))
            {
                SelectValueTableDropDown(FieldNames.DateRange, value);
                WaitForMsg(ButtonsAndMessages.PleaseWait);
            }
            else
            {
                SelectValueTableDropDown(FieldNames.DateRange, "Customized date");
                WaitForMsg(ButtonsAndMessages.PleaseWait);
                SelectValueTableDropDown(FieldNames.DateRange, value);
                WaitForMsg(ButtonsAndMessages.PleaseWait);
            }
        }

        internal void SelectTransactionStatusAndDateType(string transactionStatus, string dateType, string dateRange, int from = 0)
        {
            if (GetValueDropDown(FieldNames.DateType) != dateType)
            {
                SelectValueByScroll(FieldNames.DateType, dateType);
            }

            if (GetValueDropDown(FieldNames.DateRange) != dateRange)
            {
                SearchAndSelectValueAfterOpenWithoutClear(FieldNames.DateRange, dateRange);
                WaitForMsg(ButtonsAndMessages.PleaseWait);
            }
            if (from != 0)
            {
                EnterTextAfterClear(FieldNames.From, CommonUtils.GetDateAddTimeSpan(TimeSpan.FromDays(from)));
            }
            EnterTextAfterClear(FieldNames.To, CommonUtils.GetCurrentDate());
            SelectValueMultiSelectDropDown(FieldNames.TransactionStatus, TableHeaders.TransactionStatus, transactionStatus);
            ButtonClick(ButtonsAndMessages.Search);
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }
        internal void ClearGrid()
        {
            ButtonClick(ButtonsAndMessages.Clear);
            AcceptAlert();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);
            WaitForGridLoad();
        }

        internal void GoToInvoiceOptions(string programInvNumber)
        {
            SetFilterCreiteria(TableHeaders.ProgramInvoiceNumber, FilterCriteria.Equals);
            FilterTable(TableHeaders.ProgramInvoiceNumber, programInvNumber);
            ClickHyperLinkOnGrid(TableHeaders.ProgramInvoiceNumber);
            InvoiceOptionsPage InvoiceOptionPopUpPage;
            InvoiceOptionPopUpPage = new InvoiceOptionsPage(driver);
            InvoiceOptionPopUpPage.SwitchIframe();
            InvoiceOptionPopUpPage.Click(ButtonsAndMessages.InvoiceOptions);
        }


    }
}
