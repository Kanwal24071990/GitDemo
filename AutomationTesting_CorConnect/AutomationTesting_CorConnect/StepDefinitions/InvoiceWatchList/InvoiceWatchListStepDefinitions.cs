using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.CreateInvoiceWatchList;
using AutomationTesting_CorConnect.PageObjects.InvoiceWatchList;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.CreateInvoiceWatchList;
using AutomationTesting_CorConnect.Utils.InvoiceWatchList;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static System.Net.Mime.MediaTypeNames;

namespace AutomationTesting_CorConnect.StepDefinitions.InvoiceWatchList
{
    [Binding]
    internal class InvoiceWatchListStepDefinitions : DriverBuilderClass
    {
        InvoiceWatchListPage invWatchListPage;
        CreateInvoiceWatchListPage createInvoiceWatchListPage;
        string dealer;
        string fleet;
        string isActive;
        string salesRepresentative;
        protected void OpenCreateInvoiceWatchListPage()
        {
            invWatchListPage = new InvoiceWatchListPage(driver);
            invWatchListPage.PopulateGrid();
            invWatchListPage.OpenCreateInvoiceWatchListPage();
            createInvoiceWatchListPage = new CreateInvoiceWatchListPage(driver);
        }

        [Given(@"User is on Create Invoice Watch List page")]
        public void GivenUserIsOnCreateInvoiceWatchListPage()
        {
            OpenCreateInvoiceWatchListPage();
        }

        [When(@"User is on Create Invoice Watch List page")]
        public void WhenUserIsOnCreateInvoiceWatchListPage()
        {
            OpenCreateInvoiceWatchListPage();
        }

        [Then(@"""([^""]*)"" dropdown is verified")]
        public void ThenDropdownIsVerified(string dropDownName)
        {
            Assert.Multiple(() =>
            {
                List<string> headerNames = createInvoiceWatchListPage.GetHeaderNamesMultiSelectDropDown(dropDownName);
                Assert.IsTrue(headerNames.Count > 0);
                Assert.IsTrue(headerNames.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, dropDownName + " Dropdown"));
                Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, dropDownName + " Dropdown"));
                Assert.IsTrue(headerNames.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, dropDownName + " Dropdown"));
                Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, dropDownName + " Dropdown"));
                Assert.IsTrue(headerNames.Contains(TableHeaders.Country), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Country, dropDownName + " Dropdown"));
                Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, dropDownName + " Dropdown"));
                Assert.IsTrue(headerNames.Contains(TableHeaders.EntityCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, dropDownName + " Dropdown"));
                Assert.IsTrue(headerNames.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, dropDownName + " Dropdown"));
                createInvoiceWatchListPage.OpenMultiSelectDropDown(dropDownName);
                Assert.IsTrue(createInvoiceWatchListPage.IsClearSelectionButtonVisible(dropDownName));
                Assert.IsTrue(createInvoiceWatchListPage.IsPaginationAvailableMultiSelectDropDown(dropDownName));
                Assert.IsTrue(createInvoiceWatchListPage.GetRecordsSelectedText(dropDownName).Contains("0 record(s) selected"));
                Assert.IsTrue(createInvoiceWatchListPage.GetFirstPageCheckBoxesMultiSelectDropDown(dropDownName).Count > 0);
                Assert.IsTrue(createInvoiceWatchListPage.AreFiltersAvailableForMultiSelectDropDown(dropDownName));
                int entitiesCount = dropDownName.ToLower().Contains("dealer") ? CreateInvoiceWatchListUtil.GetDealerEntitiesCount() : CreateInvoiceWatchListUtil.GetFleetEntitiesCount();
                Assert.IsTrue(createInvoiceWatchListPage.GetTotalCountTextMultiSelectDropDown(dropDownName).Contains(entitiesCount.ToString() + " items"));
            });
        }

        [Then(@"User can select (.*) records from ""([^""]*)"" dropdown")]
        public void ThenUserCanSelectRecordsFromDropdown(int recordsCount, string dropDownName)
        {
            createInvoiceWatchListPage.OpenMultiSelectDropDown(dropDownName);
            IReadOnlyCollection<IWebElement> checkBoxes = createInvoiceWatchListPage.GetFirstPageCheckBoxesMultiSelectDropDown(dropDownName);
            int index = 0;
            foreach (IWebElement checkBox in checkBoxes)
            {
                checkBox.Click();
                index++;
                if (index == recordsCount)
                {
                    break;
                }
            }

            var recordsSelectedText = createInvoiceWatchListPage.GetRecordsSelectedText(dropDownName);
            string expectedValue = string.Format("{0} record(s) selected", recordsCount);
            Assert.IsTrue(recordsSelectedText.ToString().Contains(expectedValue));
        }

        [When(@"Invoice watch list is created with ""([^""]*)"" locations")]
        public void WhenInvoiceWatchListIsCreatedWithLocations(string locationsType)
        {
            dealer = string.Empty;
            fleet = string.Empty;
            createInvoiceWatchListPage = new CreateInvoiceWatchListPage(driver);
            Task task1 = Task.Run(() =>
            {
                if (locationsType.ToLower().Equals("active"))
                {
                    dealer = CreateInvoiceWatchListUtil.GetActiveDealer();
                    fleet = CreateInvoiceWatchListUtil.GetActiveFleet();
                }
                else if (locationsType.ToLower().Equals("inactive"))
                {
                    dealer = CreateInvoiceWatchListUtil.GetActiveDealer();
                    fleet = CreateInvoiceWatchListUtil.GetActiveFleet();
                    if (!string.IsNullOrEmpty(dealer) && !string.IsNullOrEmpty(fleet))
                    {
                        Assert.IsTrue(CreateInvoiceWatchListUtil.ChangeIsActiveValue(dealer, false), ErrorMessages.RecordNotUpdatedDB);
                        Assert.IsTrue(CreateInvoiceWatchListUtil.ChangeIsActiveValue(fleet, false), ErrorMessages.RecordNotUpdatedDB);
                    }
                }
                else if (locationsType.ToLower().Equals("terminated"))
                {
                    dealer = CreateInvoiceWatchListUtil.GetTerminatedDealer();
                    fleet = CreateInvoiceWatchListUtil.GetTerminatedFleet();
                }
                else if (locationsType.ToLower().Equals("duplicate"))
                {
                    dealer = CreateInvoiceWatchListUtil.GetExistingInvoiceDealer();
                    fleet = CreateInvoiceWatchListUtil.GetExistingInvoiceFleet();
                }
            });
            task1.Wait();
            task1.Dispose();
            Assert.IsTrue(!string.IsNullOrEmpty(dealer), FieldNames.Dealer + " DropDown: " + ErrorMessages.NoRecordInDB);
            Assert.IsTrue(!string.IsNullOrEmpty(fleet), FieldNames.Fleet + " DropDown: " + ErrorMessages.NoRecordInDB);
            try
            {
                salesRepresentative = CommonUtils.RandomAlphabets(8);
                createInvoiceWatchListPage.EnterTextAfterClear(FieldNames.SalesRepresentative, salesRepresentative);
                createInvoiceWatchListPage.OpenAndFilterMultiSelectDropdown(FieldNames.Dealer, TableHeaders.DisplayName, dealer);
                createInvoiceWatchListPage.SelectFirstRowMultiSelectDropDown(FieldNames.Dealer);
                createInvoiceWatchListPage.OpenAndFilterMultiSelectDropdown(FieldNames.Fleet, TableHeaders.DisplayName, fleet);
                createInvoiceWatchListPage.SelectFirstRowMultiSelectDropDown(FieldNames.Fleet);
                createInvoiceWatchListPage.SelectDateToday(FieldNames.StartDate);
                createInvoiceWatchListPage.Click(ButtonsAndMessages.Save);
                if (!locationsType.ToLower().Equals("duplicate"))
                {
                    Assert.IsTrue(createInvoiceWatchListPage.CheckForText(ButtonsAndMessages.RecordHasBeenSavedSuccessfully), ErrorMessages.RecordNotCreated);
                    if (locationsType.ToLower().Equals("inactive"))
                    {
                        CreateInvoiceWatchListUtil.ChangeIsActiveValue(dealer, true);
                        CreateInvoiceWatchListUtil.ChangeIsActiveValue(fleet, true);
                    }
                }

            }
            catch (Exception e)
            {
                LoggingHelper.LogException(e.Message);
                if (locationsType.ToLower().Equals("inactive"))
                {
                    CreateInvoiceWatchListUtil.ChangeIsActiveValue(dealer, true);
                    CreateInvoiceWatchListUtil.ChangeIsActiveValue(fleet, true);
                }
            }

        }

        [Then(@"Invoice watch list record is created successfully")]
        public void ThenInvoiceWatchListRecordIsCreatedSuccessfully()
        {
            invWatchListPage = new InvoiceWatchListPage(driver);
            createInvoiceWatchListPage.SwitchToMainWindow();
            invWatchListPage.PopulateGrid();
            invWatchListPage.FilterTable(FieldNames.SalesRepresentative, salesRepresentative);
            Assert.IsTrue(invWatchListPage.GetRowCount() == 1, ErrorMessages.RecordNotCreated);
        }

        [When(@"User updates invoice watch list")]
        public void WhenUserUpdatesInvoiceWatchList()
        {
            invWatchListPage = new InvoiceWatchListPage(driver);
            createInvoiceWatchListPage = new CreateInvoiceWatchListPage(driver);
            driver.Close();
            createInvoiceWatchListPage.SwitchToMainWindow();
            InvoiceWatchListUtils.GetData(out string StartDateFrom, out string StartDateTo);
            invWatchListPage.LoadDataOnGrid(StartDateFrom, StartDateTo);
            if (invWatchListPage.IsAnyDataOnGrid())
            {
                dealer = invWatchListPage.GetFirstRowData(TableHeaders.Dealer);
                isActive = invWatchListPage.GetFirstRowData(TableHeaders.Active);
                invWatchListPage.ClickHyperLinkOnGrid(TableHeaders.Commands);
                invWatchListPage.SwitchToPopUp();
                createInvoiceWatchListPage = new CreateInvoiceWatchListPage(driver);
                salesRepresentative = CommonUtils.RandomAlphabets(10);
                createInvoiceWatchListPage.EnterTextAfterClear(FieldNames.SalesRepresentative, salesRepresentative);
                createInvoiceWatchListPage.Click(FieldNames.Active);
                createInvoiceWatchListPage.Click(ButtonsAndMessages.Save);
                Assert.IsTrue(createInvoiceWatchListPage.CheckForText(ButtonsAndMessages.RecordHasBeenSavedSuccessfully), ErrorMessages.RecordNotUpdated);
            }
            else
            {
                Assert.Fail(ErrorMessages.NoDataOnGrid);
            }
        }


        [Then(@"Invoice watch list record is updated successfully")]
        public void ThenInvoiceWatchListRecordIsUpdatedSuccessfully()
        {
            createInvoiceWatchListPage.SwitchToMainWindow();
            invWatchListPage.PopulateGrid();
            invWatchListPage.FilterTable(FieldNames.SalesRepresentative, salesRepresentative);
            Assert.IsTrue(invWatchListPage.GetFirstRowData(TableHeaders.Active) != isActive, ErrorMessages.RecordNotUpdated);
            Assert.IsTrue(invWatchListPage.GetFirstRowData(FieldNames.SalesRepresentative) == salesRepresentative, ErrorMessages.RecordNotUpdated);
        }

        [Then(@"Duplicate entries message is verified")]
        public void ThenDuplicateEntriesMessageIsVerified()
        {
            createInvoiceWatchListPage.WaitForElementToBeVisible(ButtonsAndMessages.DuplicateEntries);
            Assert.IsTrue(driver.FindElement(By.XPath("//span[contains(@id, 'lblMesage')]")).Text.Contains(ErrorMessages.MultipleDuplicateEntries));
        }

        [When(@"Invoice watch list is created using (.*) locations")]
        public void WhenInvoiceWatchListIsCreatedWithLocations(int locationCount)
        {
            if (locationCount > 0)
            {
                createInvoiceWatchListPage = new CreateInvoiceWatchListPage(driver);
                salesRepresentative = CommonUtils.RandomAlphabets(8);
                createInvoiceWatchListPage.EnterTextAfterClear(FieldNames.SalesRepresentative, salesRepresentative);
                createInvoiceWatchListPage.SelectDateToday(FieldNames.StartDate);
                createInvoiceWatchListPage.OpenMultiSelectDropDown(FieldNames.Dealer);
                createInvoiceWatchListPage.SelectAllRowsMultiSelectDropDownWithoutOpen(FieldNames.Dealer);
                int currentCountDealer;
                int.TryParse(createInvoiceWatchListPage.GetRecordsSelectedText(FieldNames.Dealer).Split(' ')[0], out currentCountDealer);
                while (currentCountDealer < locationCount)
                {
                    if (!createInvoiceWatchListPage.GoToNextPageMultiSelectDropDown(FieldNames.Dealer))
                        break;
                    createInvoiceWatchListPage.SelectAllRowsMultiSelectDropDownWithoutOpen(FieldNames.Dealer);
                    int.TryParse(createInvoiceWatchListPage.GetRecordsSelectedText(FieldNames.Dealer).Split(' ')[0], out currentCountDealer);
                }
                createInvoiceWatchListPage.CloseMultiselectDropDown(FieldNames.Dealer);

                createInvoiceWatchListPage.OpenMultiSelectDropDown(FieldNames.Fleet);
                createInvoiceWatchListPage.SelectAllRowsMultiSelectDropDownWithoutOpen(FieldNames.Fleet);
                int currentCountFleet;
                int.TryParse(createInvoiceWatchListPage.GetRecordsSelectedText(FieldNames.Fleet).Split(' ')[0], out currentCountFleet);
                while (currentCountFleet < locationCount)
                {
                    if (!createInvoiceWatchListPage.GoToNextPageMultiSelectDropDown(FieldNames.Fleet))
                        break;
                    createInvoiceWatchListPage.SelectAllRowsMultiSelectDropDownWithoutOpen(FieldNames.Fleet);
                    int.TryParse(createInvoiceWatchListPage.GetRecordsSelectedText(FieldNames.Fleet).Split(' ')[0], out currentCountFleet);
                }
                createInvoiceWatchListPage.CloseMultiselectDropDown(FieldNames.Fleet);
                createInvoiceWatchListPage.Click(ButtonsAndMessages.Save);
            }
            else
            {
                throw new Exception("Invalid argument");
            }
        }

    }
}
