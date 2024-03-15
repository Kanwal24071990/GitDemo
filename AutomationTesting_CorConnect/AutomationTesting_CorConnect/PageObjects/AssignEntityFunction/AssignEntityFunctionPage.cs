using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.Utils.AssignEntityFunction;

namespace AutomationTesting_CorConnect.PageObjects.AssignEntityFunction
{
    internal class AssignEntityFunctionPage : Commons
    {
        readonly string corcentric = "Corcentric";
        readonly string community = "Community";
        readonly string dealer = "Dealer";
        readonly string fleet = "Fleet";

        internal AssignEntityFunctionPage(IWebDriver webDriver) : base(webDriver, Pages.AssignEntityFunction)
        {
        }

        public List<string> SearchFunctionInAccessGroup(string accessgroup, string functionName, bool setCheckboxValue = false)
        {
            List<string> errorMsgs = new List<string>();
            WaitForLoadingMessage();
            FilterTable(TableHeaders.EntityType, accessgroup);
            WaitForLoadingMessage();

            if (!IsElementVisible(TableHeaders.EntityType))
            {
                var menu = new Menu(driver);
                menu.OpenPage(Pages.AssignEntityFunction, false);
                WaitForLoadingMessage();
                FilterTable(TableHeaders.EntityType, accessgroup);
                WaitForLoadingMessage();
            }
            IsNestedGridOpen(1);
            FilterNestedTable(TableHeaders.FunctionName, functionName);

            if (GetRowCountNestedPage() > 0)
            {
                errorMsgs.Add(string.Format("{0} is visible in {1}", functionName, accessgroup));
            }
            return errorMsgs;
        }

        public List<string> SearchFunctionInAccessGroup(string accessgroup, Dictionary<string, string> functionList)
        {
            List<string> errorMsgs = new List<string>();
            SearchAccessGroupByName(accessgroup);

            IsNestedGridOpen(1);


            foreach (var function in functionList.Keys)
            {
                FilterNestedTable(TableHeaders.FunctionName, function);
                WaitForLoadingMessage();

                string isVisible = functionList[function];

                if (isVisible.ToLower().Equals("no") && GetRowCountNestedPage() > 0)
                { // check should not be visible but function exists in accessgroup
                    if (!IsNestedTableRowCheckBoxUnChecked())
                    { //if it is then it should be unchecked
                        errorMsgs.Add(string.Format("{0} is visible in {1} and checked.", function, accessgroup));

                    }
                }

                if (isVisible.ToLower().Equals("yes") && GetRowCountNestedPage() == 0)
                { // check should not be visible but function exists in accessgroup
                    errorMsgs.Add(string.Format("{0} is not visible in {1}", function, accessgroup));
                }

            }
            return errorMsgs;


        }

        public List<string> SearchAllFunctions()
        {
            Dictionary<string, Dictionary<string, string>> functionLists = LoadDataFromFile();
            List<string> errorMsgs = new List<string>();

            errorMsgs.AddRange(SearchFunctionInAccessGroup(corcentric, functionLists[corcentric]));
            errorMsgs.AddRange(SearchFunctionInAccessGroup(community, functionLists[community]));
            errorMsgs.AddRange(SearchFunctionInAccessGroup(dealer, functionLists[dealer]));
            errorMsgs.AddRange(SearchFunctionInAccessGroup(fleet, functionLists[fleet]));
            return errorMsgs;

        }

        /// <summary>
        /// Loads all the data from excel file where all the functions are listed
        /// Create separate list for each accessgroup so that we can open one accessgroup and then verify all functions list
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, string>> LoadDataFromFile()
        {
            string file = @"TestData/AssignEntityFunctions.xlsx";

            Dictionary<string, string> corcentricFunctions = new Dictionary<string, string>();
            Dictionary<string, string> communityFunctions = new Dictionary<string, string>();
            Dictionary<string, string> dealerFunctions = new Dictionary<string, string>();
            Dictionary<string, string> fleetFunctions = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, string>> AccessGroupFunctions = new Dictionary<string, Dictionary<string, string>>();
            var data = new System.Data.DataTable();
            try
            {
                ExcelParser parser = new();
                data = parser.ReadData(file, "FunctionsList$");
                for (int i = 1; i < data.Rows.Count; i++)
                {
                    var row = data.Rows[i];
                    var functionName = row[0].ToString();
                    corcentricFunctions.Add(functionName, row[1].ToString());
                    communityFunctions.Add(functionName, row[2].ToString());
                    dealerFunctions.Add(functionName, row[3].ToString());
                    fleetFunctions.Add(functionName, row[4].ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
            }

            AccessGroupFunctions.Add(corcentric, corcentricFunctions);
            AccessGroupFunctions.Add(community, communityFunctions);
            AccessGroupFunctions.Add(dealer, dealerFunctions);
            AccessGroupFunctions.Add(fleet, fleetFunctions);
            return AccessGroupFunctions;

        }

        public string ClickAndSaveCheckboxValue(int index, bool isChecked)
        {
            CheckNestedTableRowCheckBoxByIndex(index);
            ClickInputButton("Save Entity Function");
            LoggingHelper.LogMessage("Save button pressed. Checked Status is:" + isChecked);
            WaitForMsg(ButtonsAndMessages.Loading);
            bool dataExistsInDB = AssignEntityFunctionsUtil.GetCheckboxData(isChecked);

            if (!IsNestedTableRowCheckBoxUnChecked() && isChecked == true || !dataExistsInDB)
                return "Save Functionality Did Not Worked.Checkbox is still Unchecked.";

            if (IsNestedTableRowCheckBoxUnChecked() && isChecked == false || !dataExistsInDB)
                return "Save Functionality Did Not Worked.Checkbox is still Checked.";

            return "";
        }

        public string ClickCheckboxAndVerifyStatus(int index, bool isChecked)
        {
            // This will check checkbox status for both true and false value
            string result = ClickAndSaveCheckboxValue(index, isChecked);
            if (string.IsNullOrEmpty(result))
            {
                return ClickAndSaveCheckboxValue(index, !isChecked);
            }
            return result;

        }

        public void SearchAccessGroupByName(string accessgroup)
        {
            WaitForLoadingMessage();
            FilterTable(TableHeaders.EntityType, accessgroup);
            WaitForLoadingMessage();

            if (!IsTextVisible("Entity Type"))
            {
                var menu = new Menu(driver);
                menu.OpenPage(Pages.AssignEntityFunction, false);
                WaitForLoadingMessage();
                FilterTable(TableHeaders.EntityType, accessgroup);
                WaitForLoadingMessage();
            }
        }

    }
}
