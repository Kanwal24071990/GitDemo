using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutomationTesting_CorConnect.PageObjects.Price
{
    internal class PricePage : Commons
    {
        internal PricePage(IWebDriver webDriver) : base(webDriver, Pages.Price)
        {
        }

        internal void PopulateGrid(string partNumber, bool waitforGrid = true)
        {
            EnterTextAfterClear(FieldNames.PartNumber, partNumber);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.ProcessingRequestMessage);

            if (waitforGrid)
            {
                WaitForGridLoad();
            }
        }

        internal List<string> CreatePrice(Part part)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                ClickNewButton();
                WaitForLoadingMessage();
                WaitForEditDialog();
                Dictionary<string, string> fields;
                errorMsgs.AddRange(VerifyFields(Pages.Price, ButtonsAndMessages.New, out fields));
                if (fields == null || fields.Count == 0)
                {
                    throw new Exception(string.Format(ErrorMessages.FieldsNotFoundException, Pages.Price));
                }

                SetDropdownTableSelectInputValue(fields.Where(x => x.Key.Contains(FieldNames.PartNumber)).FirstOrDefault().Key, part.PartId.ToString());
                EnterTextAfterClear(fields.Where(x => x.Key.Contains(FieldNames.EffectiveDate)).FirstOrDefault().Key, CommonUtils.GetCurrentDate());
                SearchAndSelectValueAfterOpenWithoutClear(fields.Where(x => x.Key.Contains(FieldNames.Currency)).FirstOrDefault().Key, "USD");
                ClickInsertButton();
                WaitForLoadingMessage();
                string message = GetMessageOfPerformedOperation();
                if (!message.Contains(ButtonsAndMessages.InsertSuccessMessage))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.InsertOperationFailed, message));
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            finally
            {
                CloseEditGrid();
                WaitForLoadingMessage();
            }
            return errorMsgs;
        }
    }
}
