using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Threading;

namespace AutomationTesting_CorConnect.PageObjects.ManageUsers.AddNewUser
{
    internal class AddNewUserPage : PopUpPage
    {
        internal AddNewUserPage(IWebDriver driver) : base(driver, Pages.AddNewUser)
        {
        }

        internal void CreateDealerUser(string userName, string email)
        {
            SelectValueTableDropDown(FieldNames.UserType_PermLevel_, "Regular Users");
            SearchAndSelectValueWithoutLoadingMsg(FieldNames.EntityType, RenameMenuField("Dealer"));
            WaitForLoadingGrid();
            SelectFirstRowMultiSelectDropDown(FieldNames.BillingLocations);
            SelectFirstRowMultiSelectDropDownWithoutClear(FieldNames.UserGroup);
            EnterText(FieldNames.UserName, userName);
            EnterText(FieldNames.FirstName, userName);
            EnterText(FieldNames.LastName, userName);
            EnterText(FieldNames.Email, email);
            Click(ButtonsAndMessages.CreateUser);
            WaitForLoadingGrid();
            CheckForText("User has been created successfully.", true);
            Click(ButtonsAndMessages.Close);
            SwitchToMainWindow();
        }

        internal void CreateFleetUser(string userName, string email)
        {
            SelectValueTableDropDown(FieldNames.UserType_PermLevel_, "Regular Users");
            SearchAndSelectValueWithoutLoadingMsg(FieldNames.EntityType, RenameMenuField("Fleet"));
            WaitForLoadingGrid();
            EnterText(FieldNames.UserName, userName);
            EnterText(FieldNames.FirstName, userName);
            EnterText(FieldNames.LastName, userName);
            EnterText(FieldNames.Email, email);
            ClickElement(FieldNames.Title);
            SelectFirstRowMultiSelectDropDown(FieldNames.BillingLocations);
            ClickElement(FieldNames.Title);
            SelectFirstRowMultiSelectDropDownWithoutClear(FieldNames.UserGroup);
            Click(ButtonsAndMessages.CreateUser);
            WaitForLoadingGrid();
            CheckForText("User has been created successfully.", true);
            Click(ButtonsAndMessages.Close);
            SwitchToMainWindow();
        }
        internal void CreateUser(string userName, string email, string userType, string entityType , string loctype,string language)
        {

            if (GetValueDropDown(FieldNames.UserType_PermLevel_) != userType)
            {
                SelectValueTableDropDown(FieldNames.UserType_PermLevel_, userType);
                WaitForLoadingIcon();
            }                              
            EnterText(FieldNames.UserName, userName);
            EnterText(FieldNames.FirstName, userName);
            EnterText(FieldNames.LastName, userName);
            EnterText(FieldNames.Email, email);
            //if (language == "English")
            //{
            //    SearchAndSelectValue(FieldNames.Language, "English");
                
            //}
            //else if (language == "French")
            //{
            //    SearchAndSelectValue(FieldNames.Language, "French (CA)-Français (CA)");
                

            //}
            //else if (language == "Spanish")
            //{
            //    SearchAndSelectValue(FieldNames.Language, "Spanish-Español");               
            //}
            //WaitForLoadingIcon();           
            if (entityType != null)
            {
                SearchAndSelectValue(FieldNames.EntityType, RenameMenuField(entityType));
                WaitForLoadingIcon();
                ClickElement(FieldNames.Title);
                switch (loctype)
                {
                    case "Billing":

                        SelectFirstRowMultiSelectDropDown(FieldNames.BillingLocations);

                        break;
                    case "Shop":
                        SelectFirstRowMultiSelectDropDown(FieldNames.ShopLocations);
                        break;

                    case "Subcommunity":
                        if (userType == "Entity Admin")
                        {
                            SelectValueRowByIndex(FieldNames.SubCommunity, 1);
                            WaitForLoadingIcon();
                        }
                        else
                        {
                            // Do nothing
                        }

                        break;
                }
            }
            ClickElement(FieldNames.Title);
            SelectFirstRowMultiSelectDropDownWithoutClear(FieldNames.UserGroup);
            Click(ButtonsAndMessages.CreateUser);
            WaitForLoadingGrid();
            CheckForText("User has been created successfully.", true);
            Click(ButtonsAndMessages.Close);
            SwitchToMainWindow();
        }
        
        internal void CreateNotifyUser(string userName, string email, string userType, string entityType, string loctype ,string language)
        {
            ClickElement(FieldNames.IsNotificationUser);
            CreateUser(userName, email, userType, entityType, loctype , language);

        }      
        internal void UpdateUserFields(string userName, string email)
        {
            
            EnterTextAfterClear(FieldNames.UserName, userName);
            EnterTextAfterClear(FieldNames.FirstName, userName );
            EnterTextAfterClear(FieldNames.LastName, userName );
            EnterTextAfterClear(FieldNames.Email, "upd." + email);
            Click(ButtonsAndMessages.SaveUser);
            WaitForLoadingGrid();
            CheckForText("User has been saved successfully.");
            Click(ButtonsAndMessages.Close);
            SwitchToMainWindow();
        }
        internal string UpdateUserFields(string userName, string email,string randomValue)
        {
            string languageSelected = null;
            EnterTextAfterClear(FieldNames.UserName, userName);
            EnterTextAfterClear(FieldNames.FirstName, userName + randomValue);
            EnterTextAfterClear(FieldNames.LastName, userName + randomValue);
            EnterTextAfterClear(FieldNames.Email, "upd." + email);
            EnterTextAfterClear(FieldNames.WorkNo, userName + randomValue);
            EnterTextAfterClear(FieldNames.Ext, userName + randomValue);
            EnterTextAfterClear(FieldNames.CellNo, userName + randomValue);
            EnterTextAfterClear(FieldNames.FaxNo, userName + randomValue);
            EnterTextAfterClear(FieldNames.SecurityCode, userName + randomValue);
            //if (GetValueOfDropDown(FieldNames.Language) == "English")
            //{
            //    SearchAndSelectValue(FieldNames.Language, "French (CA)-Français (CA)");
            //    languageSelected = "French (CA)-Français (CA)";
            //}
            //else if (GetValueOfDropDown(FieldNames.Language) == "French (CA)-Français (CA)")
            //{
            //    SearchAndSelectValue(FieldNames.Language, "Spanish-Español");
            //    languageSelected = "Spanish-Español";
            //}
            //else if (GetValueOfDropDown(FieldNames.Language) == "Spanish-Español")
            //{
            //    SearchAndSelectValue(FieldNames.Language, "English");
            //    languageSelected = "English";
                
            //}            
            ClickElement(FieldNames.IsNotificationUser);
            Click(ButtonsAndMessages.SaveUser);
            WaitForLoadingGrid();
            CheckForText("User has been saved successfully.");
            return languageSelected;
        }



        internal void DeactivateUser()
        {
            ClickElement(FieldNames.IsActive_);
            Click(ButtonsAndMessages.SaveUser);
            WaitForLoadingGrid();
            CheckForText("User has been created successfully.");
            Click(ButtonsAndMessages.Close);
            SwitchToMainWindow();
        }

        internal List<string> VerifyRegularUserFields(string entityType)
        {
            List<string> errorMsgs = new List<string>();
          
            if (!IsElementDisplayed(FieldNames.UserType_PermLevel_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserType_PermLevel_));
            }
            if (GetValueOfDropDown(FieldNames.UserType_PermLevel_) != "Regular Users")
            {
                errorMsgs.Add(string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.UserType_PermLevel_));
            }
            if (!VerifyValueDropDown(FieldNames.UserType_PermLevel_, "Regular Users", "Entity Admin", "Community Admin", "SuperAdmin"))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.UserType_PermLevel_));
            }            
            if (!IsElementDisplayed(FieldNames.EntityType))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityType));
            }
            if (!VerifyValueDropDown(FieldNames.EntityType, "Dealer", "Fleet"))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.EntityType));
            }          
            if (!IsElementDisplayed(FieldNames.SubCommunity))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
            }
            if (!IsElementDisplayed(FieldNames.BillingLocations))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.BillingLocations));
            }
            if (!IsElementDisplayed(FieldNames.ShopLocations))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ShopLocations));
            }
            if (!IsElementDisplayed(FieldNames.UserGroup))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserGroup));
            }
            if (!IsElementDisplayed(FieldNames.UserName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserName));
            }
            if (!IsElementDisplayed(FieldNames.FirstName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
            }
            if (!IsElementDisplayed(FieldNames.LastName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
            }
            if (!IsElementDisplayed(FieldNames.WorkNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.WorkNo));
            }
            if (!IsElementDisplayed(FieldNames.Ext))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Ext));
            }
            if (!IsElementDisplayed(FieldNames.CellNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CellNo));
            }
            if (!IsElementDisplayed(FieldNames.FaxNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FaxNo));
            }
            if (!IsElementDisplayed(FieldNames.Email))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
            }
            if (!IsElementDisplayed(FieldNames.SecurityCode))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SecurityCode));
            }
            if (!IsElementDisplayed(FieldNames.ContactTimes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ContactTimes));
            }
            if (!IsCheckBoxDisplayed(FieldNames.ActivateAllDefaultUserGroupNotifications))
            {
                 errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ActivateAllDefaultUserGroupNotifications));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsNotificationUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsNotificationUser));
            }
            //if (!IsElementDisplayed(FieldNames.Language))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
            //}
            //if (GetValueOfDropDown(FieldNames.Language) != "English")
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Language));
            //}
            //if (!VerifyValueDropDown(FieldNames.Language, "English", "French (CA)-Français (CA)", "Spanish-Español"))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.Language));
            //}       
            if (!IsButtonVisible(ButtonsAndMessages.CreateUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.CreateUser));
            }
            if (!IsButtonVisible(ButtonsAndMessages.Close))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.Close));
            }
            if (entityType =="Fleet")
            {
                if (!IsElementVisible(FieldNames.InvoiceApprovalLimits))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApprovalLimits));
                }
            }
           


            return errorMsgs;
        }
        internal List<string> VerifyEntityAdminUserFields(string entityType)
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.UserType_PermLevel_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserType_PermLevel_));
            }
            if (GetValueOfDropDown(FieldNames.UserType_PermLevel_) != "Regular Users")
            {
                errorMsgs.Add(string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.UserType_PermLevel_));
            }
            if (!VerifyValueDropDown(FieldNames.UserType_PermLevel_, "Regular Users", "Entity Admin", "Community Admin", "SuperAdmin"))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.UserType_PermLevel_));
            }
            if (!IsElementDisplayed(FieldNames.EntityType))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.EntityType));
            }
            if (!VerifyValueDropDown(FieldNames.EntityType, "Dealer", "Fleet"))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.EntityType));
            }       
            if (!IsElementDisplayed(FieldNames.SubCommunity))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
            }
            if (!IsElementDisplayed(FieldNames.BillingLocations))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.BillingLocations));
            }
            if (!IsElementDisplayed(FieldNames.ShopLocations))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ShopLocations));
            }
            if (!IsElementDisplayed(FieldNames.UserGroup))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserGroup));
            }
            if (!IsElementDisplayed(FieldNames.UserName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserName));
            }
            if (!IsElementDisplayed(FieldNames.FirstName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
            }
            if (!IsElementDisplayed(FieldNames.LastName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
            }
            if (!IsElementDisplayed(FieldNames.WorkNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.WorkNo));
            }
            if (!IsElementDisplayed(FieldNames.Ext))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Ext));
            }
            if (!IsElementDisplayed(FieldNames.CellNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CellNo));
            }
            if (!IsElementDisplayed(FieldNames.FaxNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FaxNo));
            }
            if (!IsElementDisplayed(FieldNames.Email))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
            }
            if (!IsElementDisplayed(FieldNames.SecurityCode))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SecurityCode));
            }
            if (!IsElementDisplayed(FieldNames.ContactTimes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ContactTimes));
            }
            if (!IsCheckBoxDisplayed(FieldNames.ActivateAllDefaultUserGroupNotifications))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ActivateAllDefaultUserGroupNotifications));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsNotificationUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsNotificationUser));
            }
            //if (!IsElementDisplayed(FieldNames.Language))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
            //}
            //if (GetValueOfDropDown(FieldNames.Language) != "English")
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Language));
            //}
            //if (!VerifyValueDropDown(FieldNames.Language, "English", "French (CA)-Français (CA)", "Spanish-Español"))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.Language));
            //}           
            if (!IsButtonVisible(ButtonsAndMessages.CreateUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.CreateUser));
            }
            if (!IsButtonVisible(ButtonsAndMessages.Close))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.Close));
            }
            if (entityType == "Fleet")
            {
                if (!IsElementVisible(FieldNames.InvoiceApprovalLimits))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApprovalLimits));
                }
            }
            


            return errorMsgs;
        }

        internal List<string> VerifyCommunityAdminUserFields()
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.UserType_PermLevel_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserType_PermLevel_));
            }
            if (GetValueOfDropDown(FieldNames.UserType_PermLevel_) != "Regular Users")
            {
                errorMsgs.Add(string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.UserType_PermLevel_));
            }
            if (!VerifyValueDropDown(FieldNames.UserType_PermLevel_, "Regular Users", "Entity Admin", "Community Admin", "SuperAdmin"))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.UserType_PermLevel_));
            }
            if (!IsElementDisplayed(FieldNames.UserGroup))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserGroup));
            }
            if (!IsElementDisplayed(FieldNames.UserName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserName));
            }
            if (!IsElementDisplayed(FieldNames.FirstName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
            }
            if (!IsElementDisplayed(FieldNames.LastName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
            }
            if (!IsElementDisplayed(FieldNames.WorkNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.WorkNo));
            }
            if (!IsElementDisplayed(FieldNames.Ext))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Ext));
            }
            if (!IsElementDisplayed(FieldNames.CellNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CellNo));
            }
            if (!IsElementDisplayed(FieldNames.FaxNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FaxNo));
            }
            if (!IsElementDisplayed(FieldNames.Email))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
            }
            if (!IsElementDisplayed(FieldNames.SecurityCode))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SecurityCode));
            }
            if (!IsElementDisplayed(FieldNames.ContactTimes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ContactTimes));
            }          
            if (!IsCheckBoxDisplayed(FieldNames.IsNotificationUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsNotificationUser));
            }
            //if (!IsElementDisplayed(FieldNames.Language))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
            //}
            //if (GetValueOfDropDown(FieldNames.Language) != "English")
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Language));
            //}
            //if (!VerifyValueDropDown(FieldNames.Language, "English", "French (CA)-Français (CA)", "Spanish-Español"))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.Language));
            //}
           
            return errorMsgs;
        }
        internal List<string> VerifySuperAdminUserFields()
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.UserType_PermLevel_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserType_PermLevel_));
            }
            if (GetValueOfDropDown(FieldNames.UserType_PermLevel_) != "Regular Users")
            {
                errorMsgs.Add(string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.UserType_PermLevel_));
            }
            if (!VerifyValueDropDown(FieldNames.UserType_PermLevel_, "Regular Users", "Entity Admin", "Community Admin", "SuperAdmin"))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.UserType_PermLevel_));
            }
            if (!IsElementDisplayed(FieldNames.UserGroup))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserGroup));
            }
            if (!IsElementDisplayed(FieldNames.UserName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserName));
            }
            if (!IsElementDisplayed(FieldNames.FirstName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
            }
            if (!IsElementDisplayed(FieldNames.LastName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
            }
            if (!IsElementDisplayed(FieldNames.WorkNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.WorkNo));
            }
            if (!IsElementDisplayed(FieldNames.Ext))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Ext));
            }
            if (!IsElementDisplayed(FieldNames.CellNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CellNo));
            }
            if (!IsElementDisplayed(FieldNames.FaxNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FaxNo));
            }
            if (!IsElementDisplayed(FieldNames.Email))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
            }
            if (!IsElementDisplayed(FieldNames.SecurityCode))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SecurityCode));
            }
            if (!IsElementDisplayed(FieldNames.ContactTimes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ContactTimes));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsNotificationUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsNotificationUser));
            }
            //if (!IsElementDisplayed(FieldNames.Language))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
            //}
            //if (GetValueOfDropDown(FieldNames.Language) != "English")
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.InvalidDefaultValue, FieldNames.Language));
            //}
            //if (!VerifyValueDropDown(FieldNames.Language, "English", "French (CA)-Français (CA)", "Spanish-Español"))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.Language));
            //}
          
            return errorMsgs;
            
        }


        internal List<string> VerifyRegularUserFieldsOnEdit(string entityType)
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.UserName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserName));
            }
            if (!IsElementDisplayed(FieldNames.FirstName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
            }
            if (!IsElementDisplayed(FieldNames.LastName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
            }
            if (!IsElementDisplayed(FieldNames.WorkNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.WorkNo));
            }
            if (!IsElementDisplayed(FieldNames.Ext))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Ext));
            }
            if (!IsElementDisplayed(FieldNames.CellNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CellNo));
            }
            if (!IsElementDisplayed(FieldNames.FaxNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FaxNo));
            }
            if (!IsElementDisplayed(FieldNames.Email))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
            }
            if (!IsElementDisplayed(FieldNames.SecurityCode))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SecurityCode));
            }
            if (!IsElementDisplayed(FieldNames.ContactTimes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ContactTimes));
            }
            if (!IsElementDisplayed(FieldNames.IsActive_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsActive_));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsActive_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsActive_));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsSuspended))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsSuspended));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsNotificationUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsNotificationUser));
            }
            //if (!IsElementDisplayed(FieldNames.Language))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
            //}            
            //if (!VerifyValueDropDown(FieldNames.Language, "English", "French (CA)-Français (CA)", "Spanish-Español"))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.Language));
            //}         
            if (!IsButtonVisible(ButtonsAndMessages.SaveUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.SaveUser));
            }
            if (!IsButtonVisible(ButtonsAndMessages.SendNewTempPassword))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.SendNewTempPassword));
            }
            if (!IsButtonEnabled(ButtonsAndMessages.SendNewTempPassword))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SendNewTempPassword));
            }
            if (!IsButtonVisible(ButtonsAndMessages.Close))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.Close));
            }
            if (entityType == "Fleet")
            {
                if (!IsElementVisible(FieldNames.InvoiceApprovalLimits))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApprovalLimits));
                }
            }
           

            return errorMsgs;
        }

        internal List<string> VerifyEntityAdminUserFieldsOnEdit(string entityType)
        {
            List<string> errorMsgs = new List<string>();

            if (!IsElementDisplayed(FieldNames.SubCommunity))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SubCommunity));
            }
            if (!IsElementDisplayed(FieldNames.UserName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserName));
            }
            if (!IsElementDisplayed(FieldNames.FirstName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
            }
            if (!IsElementDisplayed(FieldNames.LastName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
            }
            if (!IsElementDisplayed(FieldNames.WorkNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.WorkNo));
            }
            if (!IsElementDisplayed(FieldNames.Ext))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Ext));
            }
            if (!IsElementDisplayed(FieldNames.CellNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CellNo));
            }
            if (!IsElementDisplayed(FieldNames.FaxNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FaxNo));
            }
            if (!IsElementDisplayed(FieldNames.Email))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
            }
            if (!IsElementDisplayed(FieldNames.SecurityCode))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SecurityCode));
            }
            if (!IsElementDisplayed(FieldNames.ContactTimes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ContactTimes));
            }
            if (!IsElementDisplayed(FieldNames.IsActive_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsActive_));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsActive_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsActive_));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsSuspended))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsSuspended));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsNotificationUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsNotificationUser));
            }
            //if (!IsElementDisplayed(FieldNames.Language))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
            //}          
            //if (!VerifyValueDropDown(FieldNames.Language, "English", "French (CA)-Français (CA)", "Spanish-Español"))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.Language));
            //}
           
            if (!IsButtonVisible(ButtonsAndMessages.SaveUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.SaveUser));
            }
            if (!IsButtonVisible(ButtonsAndMessages.SendNewTempPassword))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.SendNewTempPassword));
            }
            if (!IsButtonEnabled(ButtonsAndMessages.SendNewTempPassword))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SendNewTempPassword));
            }
            if (!IsButtonVisible(ButtonsAndMessages.Close))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.Close));
            }
            if (entityType == "Fleet")
            {
                if (!IsElementVisible(FieldNames.InvoiceApprovalLimits))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.InvoiceApprovalLimits));
                }
            }
            
            return errorMsgs;
        }
        internal List<string> VerifyCommunityAdminUserFieldsOnEdit()
        {
            List<string> errorMsgs = new List<string>();

    
            if (!IsElementDisplayed(FieldNames.UserName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserName));
            }
            if (!IsElementDisplayed(FieldNames.FirstName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
            }
            if (!IsElementDisplayed(FieldNames.LastName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
            }
            if (!IsElementDisplayed(FieldNames.WorkNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.WorkNo));
            }
            if (!IsElementDisplayed(FieldNames.Ext))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Ext));
            }
            if (!IsElementDisplayed(FieldNames.CellNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CellNo));
            }
            if (!IsElementDisplayed(FieldNames.FaxNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FaxNo));
            }
            if (!IsElementDisplayed(FieldNames.Email))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
            }
            if (!IsElementDisplayed(FieldNames.SecurityCode))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SecurityCode));
            }
            if (!IsElementDisplayed(FieldNames.ContactTimes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ContactTimes));
            }
            if (!IsElementDisplayed(FieldNames.IsActive_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsActive_));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsActive_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsActive_));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsSuspended))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsSuspended));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsNotificationUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsNotificationUser));
            }
            //if (!IsElementDisplayed(FieldNames.Language))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
            //}
            //if (!VerifyValueDropDown(FieldNames.Language, "English", "French (CA)-Français (CA)", "Spanish-Español"))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.Language));
            //}
            if (!IsButtonVisible(ButtonsAndMessages.SaveUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.SaveUser));
            }
            if (!IsButtonVisible(ButtonsAndMessages.SendNewTempPassword))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.SendNewTempPassword));
            }
            if (!IsButtonEnabled(ButtonsAndMessages.SendNewTempPassword))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SendNewTempPassword));
            }
            if (!IsButtonVisible(ButtonsAndMessages.Close))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.Close));
            }

            return errorMsgs;
        }
        internal List<string> VerifySuperAdminUserFieldsOnEdit()
        {
            List<string> errorMsgs = new List<string>();


            if (!IsElementDisplayed(FieldNames.UserName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.UserName));
            }
            if (!IsElementDisplayed(FieldNames.FirstName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FirstName));
            }
            if (!IsElementDisplayed(FieldNames.LastName))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.LastName));
            }
            if (!IsElementDisplayed(FieldNames.WorkNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.WorkNo));
            }
            if (!IsElementDisplayed(FieldNames.Ext))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Ext));
            }
            if (!IsElementDisplayed(FieldNames.CellNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.CellNo));
            }
            if (!IsElementDisplayed(FieldNames.FaxNo))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.FaxNo));
            }
            if (!IsElementDisplayed(FieldNames.Email))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Email));
            }
            if (!IsElementDisplayed(FieldNames.SecurityCode))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.SecurityCode));
            }            
            if (!IsElementDisplayed(FieldNames.ContactTimes))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.ContactTimes));
            }
            if (!IsElementDisplayed(FieldNames.IsActive_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsActive_));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsActive_))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsActive_));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsSuspended))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsSuspended));
            }
            if (!IsCheckBoxDisplayed(FieldNames.IsNotificationUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.IsNotificationUser));
            }
            //if (!IsElementDisplayed(FieldNames.Language))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Language));
            //}
            //if (!VerifyValueDropDown(FieldNames.Language, "English", "French (CA)-Français (CA)", "Spanish-Español"))
            //{
            //    errorMsgs.Add(string.Format(ErrorMessages.ListElementsMissing, FieldNames.Language));
            //}        
            if (!IsButtonVisible(ButtonsAndMessages.SaveUser))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.SaveUser));
            }
            if (!IsButtonVisible(ButtonsAndMessages.SendNewTempPassword))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.SendNewTempPassword));
            }
            if (!IsButtonEnabled(ButtonsAndMessages.SendNewTempPassword))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ButtonDisabled, ButtonsAndMessages.SendNewTempPassword));
            }
            if (!IsButtonVisible(ButtonsAndMessages.Close))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, ButtonsAndMessages.Close));
            }

            return errorMsgs;
        }


    }
}
