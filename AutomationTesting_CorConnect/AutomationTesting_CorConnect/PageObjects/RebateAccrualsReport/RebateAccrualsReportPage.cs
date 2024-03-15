using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.PageObjects.RebateAccrualsReport
{
    internal class RebateAccrualsReportPage : PopUpPage
    {
        internal RebateAccrualsReportPage(IWebDriver driver) : base(driver, Pages.RebateAccrualsReport)
        {
        }

        internal void LoadDataOnGrid(string contractName)
        {
            SelectValueByScroll(FieldNames.ContractName, contractName);
            WaitForMsg(ButtonsAndMessages.Processing);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.Processing);
            WaitForMsg(ButtonsAndMessages.Loading);
            WaitForGridLoad();
        }

        internal List<string> AreFieldsAvailable()
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.LoadBookmark))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LoadBookmark));
            }
            if (!IsElementVisible(FieldNames.ContractName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ContractName));
            }
            if (!IsElementDisplayed(FieldNames.ParentContractName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ParentContractName));
            }
            if (!IsElementVisible(FieldNames.Currency))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Currency));
            }
            if (!IsElementDisplayed(FieldNames.Status))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Status));
            }
            if (!IsElementVisible(FieldNames.Payer))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Payer));
            }
            if (!IsElementVisible(FieldNames.PayerLocation))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PayerLocation));
            }
            if (!IsElementVisible(FieldNames.Receiver))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Receiver));
            }
            if (!IsElementVisible(FieldNames.ReceiverLocation))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ReceiverLocation));
            }
            if (!IsElementVisible(FieldNames.DateRange))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DateRange));
            }
            if (!IsElementVisible(FieldNames.From))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.From));
            }
            if (!IsElementVisible(FieldNames.To))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.To));
            }
            if (!IsElementVisible(FieldNames.GroupBy))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.GroupBy));
            }

            return errorMsgs;
        }
    }
}
