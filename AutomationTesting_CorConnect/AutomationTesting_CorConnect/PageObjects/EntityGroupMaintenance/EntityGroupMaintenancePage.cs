using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.PageObjects.EntityGroupMaintenance
{
    internal class EntityGroupMaintenancePage : Commons
    {
        internal EntityGroupMaintenancePage(IWebDriver webDriver) : base(webDriver, Pages.EntityGroupMaintenance)
        {
        }

        internal void PopulateGrid(string groupName, string groupType)
        {
            if (!string.IsNullOrEmpty(groupType) && GetValueDropDown(FieldNames.GroupType) != groupType)
            {
                SearchAndSelectValue(FieldNames.GroupType, groupType);
            }
            if (!string.IsNullOrEmpty(groupName))
            {
                SearchAndSelectValue(FieldNames.GroupName, groupName);
            }
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
            WaitForGridLoad();
        }

        internal List<string> CreateNewEntityGroupMaintenance(string groupName, string groupType, string entityType)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                gridHelper.ClickNewButton();
                WaitForLoadingMessage();
                gridHelper.WaitForEditGrid();
                EnterTextAfterClear(FieldNames.GroupName, groupName, ButtonsAndMessages.New);
                EnterTextAfterClear(FieldNames.GroupDescription, "TestRltnshpgrp1 Desc", ButtonsAndMessages.New);
                SelectValueByScroll(FieldNames.GroupType, groupType, ButtonsAndMessages.New);
                SelectValueByScroll(FieldNames.EntityType, entityType, ButtonsAndMessages.New);
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
    }
}
