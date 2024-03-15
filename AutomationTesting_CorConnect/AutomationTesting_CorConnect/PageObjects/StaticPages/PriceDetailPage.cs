using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.PageObjects.StaticPages
{
    internal class PriceDetailPage : StaticPage
    {
        internal PriceDetailPage(IWebDriver webDriver) : base(webDriver, Pages.PriceDetail)
        {
        }

        internal List<string> CreateNewPriceDetail()
        {
            List<string> errorMsgs = new List<string>();
            Click(ButtonsAndMessages.New);
            WaitForLoadingMessage();
            WaitForEditGrid();
            try
            {
                if (!IsElementDisplayed(FieldNames.PriceLevel))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PriceLevel));
                }
                if (!IsElementDisplayed(FieldNames.UnitPrice))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UnitPrice));
                }
                if (!IsElementDisplayed(FieldNames.CorePrice))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CorePrice));
                }
                if (!IsElementDisplayed(FieldNames.FET))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FET));
                }
                if (errorMsgs.Count == 0)
                {
                    EnterTextAfterClear(FieldNames.UnitPrice, "1");
                    EnterTextAfterClear(FieldNames.CorePrice, "1");
                    EnterTextAfterClear(FieldNames.FET, "1");
                    Click(ButtonsAndMessages.Insert);
                    WaitForLoadingMessage();
                    string message = GetMessageOfPerformedOperation();
                    if (!message.Contains(ButtonsAndMessages.InsertSuccessMessage))
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.InsertOperationFailed, message));
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            finally
            {
                Click(ButtonsAndMessages.Close);
                WaitForEditGridClose();
            }
            return errorMsgs;
        }
    }
}
