using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.Parts;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationTesting_CorConnect.PageObjects.Parts
{
    internal class PartsPage : Commons
    {
        internal PartsPage(IWebDriver driver) : base(driver, Pages.Parts) { }

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

        internal List<string> CreateNewDecentralizedPart(string partNumber, string companyName)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                gridHelper.ClickNewButton();
                WaitForLoadingMessage();
                gridHelper.WaitForEditGrid();
                Dictionary<string, string> fields;
                errorMsgs.AddRange(VerifyFields(RenameMenuField(Pages.Parts), ButtonsAndMessages.New, out fields));
                if (fields == null || fields.Count == 0)
                {
                    throw new Exception(string.Format(ErrorMessages.FieldsNotFoundException, Pages.Parts));
                }
                EnterTextAfterClear(FieldNames.PartNumber, partNumber, ButtonsAndMessages.New);
                EnterTextAfterClear(FieldNames.LongDescription, partNumber, ButtonsAndMessages.New);
                EnterTextAfterClear(FieldNames.UOM, partNumber.Substring(0, 4), ButtonsAndMessages.New);
                if (!string.IsNullOrEmpty(companyName))
                {
                    var entityDetails = CommonUtils.GetEntityDetails(companyName);
                    SetDropdownTableSelectInputValue(FieldNames.CompanyName, entityDetails.EntityDetailId.ToString(), ButtonsAndMessages.New);
                }
                else
                {
                    wait10Sec.Until(d =>
                    {
                        try
                        {
                            SelectValueFirstRow(FieldNames.CompanyName, ButtonsAndMessages.New);
                        }
                        catch (ElementClickInterceptedException)
                        {
                            return false;
                        }
                        return true;
                    });
                }
                gridHelper.ClickInsertButton();
                WaitForLoadingMessage();
                string message = gridHelper.GetMessageOfPerformedOperation();
                if (!message.Contains(ButtonsAndMessages.InsertSuccessMessage))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.InsertOperationFailed, message));
                }
                gridHelper.CloseEditGrid();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.ToString());
            }
            return errorMsgs;
        }

        internal List<string> EditDecentralizedPart(string partNumber)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                gridHelper.ClickEdit();
                gridHelper.WaitForEditGrid();
                Dictionary<string, string> fields;
                errorMsgs.AddRange(VerifyFields(Pages.Parts, ButtonsAndMessages.Edit, out fields));
                if (fields == null || fields.Count == 0)
                {
                    throw new Exception(string.Format(ErrorMessages.FieldsNotFoundException, Pages.Parts));
                }
                EnterTextAfterClear(FieldNames.LongDescription, partNumber + " edited", ButtonsAndMessages.Edit);
                //EnterTextAfterClear(fields.Where(x => x.Key.Contains(FieldNames.UOM)).FirstOrDefault().Key, partNumber.Reverse().ToString().Substring(0, 4));
                gridHelper.UpdateEditGrid();
                string message = gridHelper.GetMessageOfPerformedOperation();
                if (!message.Contains(ButtonsAndMessages.RecordUpdatedMessage))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.UpdateOperationFailed, message));
                }
                gridHelper.CloseEditGrid();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> DeleteDecentralizedPart(string partNumber)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                FilterTable(TableHeaders.Active, "True");
                gridHelper.ClickDeleteButton();
                SwitchToPopUp();
                AcceptAlert(out string alertMsg);
                WaitForLoadingMessage();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal void DeletePart(string partNumber, bool isHardDelete)
        {
            LoggingHelper.LogMessage(string.Format(LoggerMesages.DeletingPart, partNumber));
            if (IsAnyDataOnGrid() && !isHardDelete)
            {
                FilterTable(TableHeaders.Active, "True");
                gridHelper.ClickDeleteButton();
                SwitchToPopUp();
                AcceptAlert(out string alertMsg);
                WaitForLoadingMessage();
            }
            else
            {
                bool isDeleted = PartsUtil.DeleteDecentralizedPart(partNumber);
                if (!isDeleted)
                {
                    LoggingHelper.LogMessage($"Part [{partNumber}] not deleted.");
                }
            }
        }

        internal List<string> CreateNewPart(string partNumber)
        {
            List<string> errorMsgs = new List<string>();
            ClickNew();
            WaitForElementToBeClickable(FieldNames.PartNumber, ButtonsAndMessages.New);
            WaitForElementToBeClickable(FieldNames.UOM, ButtonsAndMessages.New);
            EnterTextAfterClear(FieldNames.PartNumber, partNumber, ButtonsAndMessages.New);
            EnterTextAfterClear(FieldNames.LongDescription, partNumber, ButtonsAndMessages.New);
            EnterTextAfterClear(FieldNames.UOM, partNumber.Substring(0, 4), ButtonsAndMessages.New);
            InsertEditGrid();
            string message = gridHelper.GetMessageOfPerformedOperation();
            if (!message.Contains(ButtonsAndMessages.InsertSuccessMessage))
            {
                errorMsgs.Add(string.Format(ErrorMessages.InsertOperationFailed, message));
            }
            gridHelper.CloseEditGrid();
            return errorMsgs;
        }
    }
}
