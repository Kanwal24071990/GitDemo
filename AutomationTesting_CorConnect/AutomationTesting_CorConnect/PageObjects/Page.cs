using OpenQA.Selenium;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using System;
using Test_CorConnect.src.main.com.corcentric.test.pageobjects.helpers;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer;
using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.Resources;
using Newtonsoft.Json.Linq;
using AutomationTesting_CorConnect.Utils;
using SeleniumExtras.WaitHelpers;

namespace AutomationTesting_CorConnect.PageObjects
{
    internal class Page : PageOperations<JObject>
    {
        private By Grid = null;

        private string GridXpath = "(//span[text()='{0}']/ancestor::table)[1]//following-sibling::table[contains(@class,'MySplitter')]";
        private By FleetCreditGrid = By.XPath(string.Format("//div[contains(@id,'btnPdfExport_CD')]"));

        protected GridHelper gridHelper;
        protected MultiSelectHelper multiSelectHelper;
        protected TableDropDownHelper tableDropDownHelper;
        protected DatePickerHelper datePickerHelper;
        protected BookmarkDialogHelper bookmarkDialogHelper;
        public string PageName { get; private set; }

        internal Page(IWebDriver webDriver, string page) : base(webDriver)
        {
            PageName = page;
            PageElements = LoadElements<JObject>(page);
            gridHelper = new GridHelper(driver);
            tableDropDownHelper = new TableDropDownHelper(driver);
            multiSelectHelper = new MultiSelectHelper(driver);
            datePickerHelper = new DatePickerHelper(driver);
            baseDataAccessLayer = BaseDataAccessLayer.GetInstance();
            bookmarkDialogHelper = new BookmarkDialogHelper(driver);
        }

        protected void GetGridXpath(string PageName)
        {
            Grid = By.XPath(string.Format(GridXpath, RenameMenuField(PageName)));
        }

        internal string GetValue(string caption)
        {
            return base.GetValue(GetElement(caption));
        }

        internal List<string> VerifyEditFields(string page, string button = "")
        {
            page = RenameMenuField(page);
            var errorMsgs = new List<string>();
            try
            {
                var fields = baseDataAccessLayer.GetEditsField(page);

                if (fields.Count == 0 || fields == null)
                {
                    throw new Exception(string.Format(ErrorMessages.FieldsNotFoundException, page));
                }


                if (button != string.Empty)
                {
                    var toRemove = fields.Where(x => x.Key != button);

                    foreach (var field in toRemove)
                    {
                        fields.Remove(field.Key);
                    }
                }

                foreach (var field in fields)
                {
                    gridHelper.ClickAnchorButton(TableHeaders.Commands, field.Key);
                    WaitForLoadingMessage();
                    gridHelper.WaitForEditGrid();

                    if (field.Key == ButtonsAndMessages.New)
                    {
                        if (!gridHelper.IsInsertDisplayed())
                        {
                            errorMsgs.Add(ErrorMessages.InsertButtonMissing);
                        }
                    }

                    else if (field.Key == ButtonsAndMessages.Edit)
                    {
                        if (!gridHelper.IsUpdateDisplayed())
                        {
                            errorMsgs.Add(ErrorMessages.UpdateButtonMissing);
                        }
                    }

                    if (!gridHelper.IsCloseDisplayed())
                    {
                        errorMsgs.Add(ErrorMessages.CloseButtonMissing);
                    }

                    fields.TryGetValue(field.Key, out var elements);

                    if (elements.Count == 0 || elements == null)
                    {
                        throw new Exception(string.Format(ErrorMessages.FieldsNotFoundException, page));
                    }

                    foreach (var element in elements)
                    {
                        switch (element.Value.ToUpper())
                        {
                            case "ASPXDATEEDIT":
                            case "ASPXTEXTBOX":
                            case "ASPXMEMO":
                                if (!IsTextBoxDisplayed(element.Key))
                                {
                                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, element.Key));
                                }
                                break;
                            case "ASPXCOMBOBOX":
                                if (!tableDropDownHelper.VerifyDropDown(GetElement(element.Key)))
                                {
                                    errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, element.Key));
                                }
                                break;
                            case "ASPXCHECKBOX":
                                if (!IsCheckBoxDisplayed(element.Key))
                                {
                                    errorMsgs.Add(string.Format(ErrorMessages.CheckBoxMissing, element.Key));
                                }
                                break;
                            case "ASPXLABEL":
                                if (!IsLabelDisplayed(element.Key))
                                {
                                    errorMsgs.Add(string.Format(ErrorMessages.LabelMissing, element.Key));
                                }
                                break;
                            default:
                                {
                                    throw new NotImplementedException();
                                }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                errorMsgs.Add(ex.ToString());
            }
            finally
            {
                gridHelper.CloseEditGrid();
            }

            return errorMsgs;
        }

        public List<string> AreFieldsAvailable(string page)
        {
            var fields = baseDataAccessLayer.GetFields(new Menu(driver).GetPageCaption(page), out bool isDefaultFields);

            if (!isDefaultFields && CommonUtils.GetClientLower() != appContext.DefaultClient.ToLower())
            {
                fields = ReplaceCaptions(fields);
            }

            if (fields.Count == 0)
            {
                throw new Exception(string.Format(ErrorMessages.FieldsNotFoundException, page));
            }

            List<string> errorMsgs = new List<string>();
            if (fields.Any(x => x.Value.ParamName == "SearchType"))
            {
                if (fields.Any(x => x.Value.QuickSearch == true))
                {
                    var quickSearchFields = fields.Where(x => x.Value.QuickSearch == true).ToDictionary(x => x.Key, x => x.Value);
                    errorMsgs.AddRange(VerifyFieldsAvailability(quickSearchFields));
                }
                if (fields.Any(x => x.Value.AdvancedSearch == true))
                {
                    SwitchToAdvanceSearch();
                    var advSearchFields = fields.Where(x => x.Value.AdvancedSearch == true).ToDictionary(x => x.Key, x => x.Value);
                    errorMsgs.AddRange(VerifyFieldsAvailability(advSearchFields));
                }
            }
            else
            {
                errorMsgs.AddRange(VerifyFieldsAvailability(fields));
            }
            if (errorMsgs.Count > 0)
            {
                errorMsgs = errorMsgs.Select(x => "AreFieldsAvailable: " + x).ToList();
            }
            return errorMsgs;
        }

        private List<string> VerifyFieldsAvailability(Dictionary<string, Fields> fields)
        {
            List<string> errorMsgs = new List<string>();
            foreach (var field in fields)
            {
                switch (field.Value.ParamType.ToUpper())
                {
                    case "ASPXDATEEDIT":
                    case "ASPXTEXTBOX":
                        if (!IsTextBoxDisplayed(field.Key))
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.TextBoxMissing, field.Key));
                        }
                        break;
                    case "ASPXCOMBOBOX":
                        if (!tableDropDownHelper.VerifyDropDown(GetElement(field.Key)))
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, field.Key));
                        }
                        break;

                    case "ASPXCHECKBOX":
                        if (!IsCheckBoxDisplayed(field.Key))
                        {
                            errorMsgs.Add($"CheckBox [{field.Key}] missing.");
                        }
                        break;
                    case "ASPXLABEL":
                        if (!IsLabelDisplayed(field.Key))
                        {
                            errorMsgs.Add($"Label [{field.Key}] missing.");
                        }
                        break;
                    case "MULTISELECTCONTROL":
                        if (!multiSelectHelper.VerifyDropDown(GetElement(field.Key)))
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, field.Key));
                        }
                        break;
                    case "ASPXBUTTON":
                        if (!IsButtonVisible(field.Key))
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.ButtonMissing, field.Key));
                        }
                        break;
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }
            return errorMsgs;
        }

        private Dictionary<string, Fields> ReplaceCaptions(Dictionary<string, Fields> Fields)
        {
            var dictionary = new Dictionary<string, Fields>();

            foreach (var field in Fields)
            {

                var index = PageElements.SelectTokens("$..ParamName")
              .Select(t => t.Value<string>())
              .ToList().IndexOf(field.Value.ParamName);
                var caption = PageElements.Properties().ElementAt(index).Name.ToString();

                dictionary.Add(caption, field.Value);
            }

            return dictionary;
        }

        public override T LoadElements<T>(string page)
        {
            try
            {
                using (StreamReader r = new StreamReader(GetPageObjectPath() + "\\" + page.Replace("/", "") + ".json"))
                {
                    string jsonString = r.ReadToEnd();
                    var jsonData = JObject.Parse(jsonString);
                    return (T)Convert.ChangeType(jsonData, typeof(T));

                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return default;
            }
        }


        protected override By GetElement(string Name, string? section = null)
        {
            if (section != null)
            {
                Name = section + "_" + Name;
            }

            var element = PageElements[Name];

            if (element[CommonUtils.GetClientLower()] == null)
            {
                return new CreateBy(element["default"].ToObject<Xpath>());

            }

            return new CreateBy(element[CommonUtils.GetClientLower()].ToObject<Xpath>());
        }

        internal List<string> VerifyFields(string page, string fieldType, out Dictionary<string, string> fields)
        {
            var errorMsgs = new List<string>();
            try
            {
                if (!(new string[] { ButtonsAndMessages.New, ButtonsAndMessages.Edit }.Any(fieldType.Contains)))
                {
                    throw new Exception(string.Format(ErrorMessages.FieldNotRecognized, fieldType));
                }

                var newEditFields = baseDataAccessLayer.GetEditsField(page);
                newEditFields.TryGetValue(fieldType, out fields);


                if (fields.Count == 0 || fields == null)
                {
                    throw new Exception(string.Format(ErrorMessages.FieldsNotFoundException, page));
                }

                if (fieldType == ButtonsAndMessages.New)
                {
                    if (!gridHelper.IsInsertDisplayed())
                    {
                        errorMsgs.Add(ErrorMessages.InsertButtonMissing);
                    }
                }
                else if (fieldType == ButtonsAndMessages.Edit)
                {
                    if (!gridHelper.IsUpdateDisplayed())
                    {
                        errorMsgs.Add(ErrorMessages.UpdateButtonMissing);
                    }
                }

                if (!gridHelper.IsCloseDisplayed())
                {
                    errorMsgs.Add(ErrorMessages.CloseButtonMissing);
                }

                errorMsgs.AddRange(VerifyNewEditFieldsAvailability(fields));

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
                fields = new Dictionary<string, string>();
            }
            return errorMsgs;
        }


        private List<string> VerifyNewEditFieldsAvailability(Dictionary<string, string> fields)
        {
            List<string> errorMsgs = new List<string>();
            foreach (var element in fields)
            {
                switch (element.Value.ToUpper())
                {
                    case "ASPXDATEEDIT":
                    case "ASPXTEXTBOX":
                    case "ASPXMEMO":
                        if (!IsTextBoxDisplayed(element.Key))
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, element.Key));
                        }
                        break;
                    case "ASPXCOMBOBOX":
                        if (!tableDropDownHelper.VerifyDropDown(GetElement(element.Key)))
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.DropDownNotFound, element.Key));
                        }
                        break;
                    case "ASPXCHECKBOX":
                        if (!IsCheckBoxDisplayed(element.Key))
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.CheckBoxMissing, element.Key));
                        }
                        break;
                    case "ASPXLABEL":
                        if (!IsLabelDisplayed(element.Key))
                        {
                            errorMsgs.Add(string.Format(ErrorMessages.LabelMissing, element.Key));
                        }
                        break;
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }
            return errorMsgs;
        }

        internal void WaitForGridLoad()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(Grid));
        }

        internal void WaitForFleetCreditGridLoad()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(FleetCreditGrid));
        }

        internal void WaitforGridToHide(string page)
        {
            GetGridXpath(page);
            WaitForElementToBeInvisible(Grid);
        }

        internal void SwitchToAdvanceSearch()
        {
            tableDropDownHelper.SelectValue(GetElement(FieldNames.SearchType), "Advanced Search");
            WaitForLoadingMessage();
            WaitForGridLoad();
        }

        internal void SwitchToQuickSearch()
        {
            tableDropDownHelper.SelectValue(GetElement(FieldNames.SearchType), "Quick Search");
            WaitForLoadingMessage();
            WaitForGridLoad();
        }



        public bool IsScreenExemptedForClientUserType(string screenName)
        {
            return IsScreenExemptedForClientUserType(applicationContext.ApplicationContext.GetInstance().UserData.Type.NameUpperCase, screenName);
        }

        public bool IsScreenExemptedForClientUserType(string userType, string screenName)
        {
            var exemptedClients = ApplicationContext.GetInstance().skippedScreens.FirstOrDefault(x => x.ScreenName == screenName)?.Clients;
            if (userType == Constants.UserType.Admin.NameUpperCase)
            {
                return exemptedClients.Any(x => x.ClientName.ToLower() == CommonUtils.GetClientLower() && x.SkipAdminUser);
            }
            Client client = exemptedClients.FirstOrDefault(x => x.ClientName.ToLower() == CommonUtils.GetClientLower());
            if (client != null && !client.SkipAdminUser)
            {
                if (userType == Constants.UserType.Fleet.NameUpperCase)
                {
                    return exemptedClients.Any(x => x.ClientName.ToLower() == CommonUtils.GetClientLower() && x.SkipFleetUser);
                }
                if (userType == Constants.UserType.Dealer.NameUpperCase)
                {
                    return exemptedClients.Any(x => x.ClientName.ToLower() == CommonUtils.GetClientLower() && x.SkipDealerUser);
                }
            }
            return false;
        }


    }
}