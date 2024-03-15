using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.ManageUsers;
using AutomationTesting_CorConnect.Resources;
using NUnit.Framework;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using AutomationTesting_CorConnect.PageObjects.ManageUsers.AddNewUser;
using System;
using AutomationTesting_CorConnect.Helper;
using System.Collections.Generic;
using AutomationTesting_CorConnect.Utils.ManageUsers;
using AutomationTesting_CorConnect.DataObjects;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.ManageUsers
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage Users")]
    internal class ManageUsers : DriverBuilderClass
    {

        ManageUsersPage page;

        private string dealerUserName = CommonUtils.RandomString(7);
        private string dealerUserNameUpd = CommonUtils.RandomAlphabets(7);
        private string fleetUserName = CommonUtils.RandomString(7);
        private string fleetUserNameUpd = CommonUtils.RandomAlphabets(7);
        private string email = CommonUtils.GetRandomEmail();

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ManageUsers);
            page = new ManageUsersPage(driver);

        }

        [Category(TestCategory.Smoke)]
        [Test, Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21049" })]
        public void TC_21049(string UserType)
        {
            page.SearchByUserName(CommonUtils.GetUserName());
            var addNewUserPage = page.OpenCreateUser();

            addNewUserPage.CreateDealerUser(dealerUserName, email);

            page.EnterText(FieldNames.UserName, dealerUserName);
            page.SearchByUserName(dealerUserName);

            Assert.AreEqual(page.GetRowCount(), 1);
            Assert.AreEqual(dealerUserName, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(dealerUserName, page.GetFirstRowData(TableHeaders.FirstName));
            Assert.AreEqual(dealerUserName, page.GetFirstRowData(TableHeaders.LastName));
            Console.WriteLine("Dealer User Created Successfully: " + dealerUserName);
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21050" })]
        public void TC_21050(string UserType)
        {
            page.SearchByUserName(dealerUserName);
            page.ClickHyperLinkOnGrid(TableHeaders.Commands);
            var aspxPage = new AddNewUserPage(driver);

            Assert.AreEqual(dealerUserName, aspxPage.GetValue(FieldNames.UserName), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(dealerUserName, aspxPage.GetValue(FieldNames.FirstName), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(dealerUserName, aspxPage.GetValue(FieldNames.LastName), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(email, aspxPage.GetValue(FieldNames.Email), ErrorMessages.ValueMisMatch);

            aspxPage.UpdateUserFields(dealerUserNameUpd, email);

            page.SearchByUserName(dealerUserName);
            Assert.IsFalse(page.IsAnyDataOnGrid());
            page.SearchByUserName(dealerUserNameUpd);
            Assert.AreEqual(page.GetRowCount(), 1);
            Assert.AreEqual(dealerUserNameUpd, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(dealerUserNameUpd, page.GetFirstRowData(TableHeaders.FirstName));
            Assert.AreEqual(dealerUserNameUpd, page.GetFirstRowData(TableHeaders.LastName));
            Console.WriteLine("Dealer User Updated Successfully from " + dealerUserName + " to " + dealerUserNameUpd);
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(3), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21251" })]
        public void TC_21251(string UserType)
        {
            page.SearchByUserName(dealerUserNameUpd);
            page.ClickHyperLinkOnGrid(TableHeaders.Commands);
            var aspxPage = new AddNewUserPage(driver);
            aspxPage.DeactivateUser();

            page.SearchByUserName(dealerUserNameUpd);
            Assert.AreEqual(page.GetRowCount(), 1);
            Assert.AreEqual("False", page.GetFirstRowData(TableHeaders.Active));
            Console.WriteLine("Dealer User Deactivated Successfully: " + dealerUserNameUpd);
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(4), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21248" })]
        public void TC_21248(string UserType)
        {
            page.SearchByUserName(CommonUtils.GetUserName());
            var addNewUserPage = page.OpenCreateUser();

            addNewUserPage.CreateFleetUser(fleetUserName, email);

            page.EnterText(FieldNames.UserName, fleetUserName);
            page.SearchByUserName(fleetUserName);

            Assert.AreEqual(page.GetRowCount(), 1);
            Assert.AreEqual(fleetUserName, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(fleetUserName, page.GetFirstRowData(TableHeaders.FirstName));
            Assert.AreEqual(fleetUserName, page.GetFirstRowData(TableHeaders.LastName));
            Console.WriteLine("Fleet User Created Successfully: " + fleetUserName);
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(5), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21249" })]
        public void TC_21249(string UserType)
        {
            page.SearchByUserName(fleetUserName);
            page.ClickHyperLinkOnGrid(TableHeaders.Commands);
            var aspxPage = new AddNewUserPage(driver);

            Assert.AreEqual(fleetUserName, aspxPage.GetValue(FieldNames.UserName), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(fleetUserName, aspxPage.GetValue(FieldNames.FirstName), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(fleetUserName, aspxPage.GetValue(FieldNames.LastName), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(email, aspxPage.GetValue(FieldNames.Email), ErrorMessages.ValueMisMatch);

            aspxPage.UpdateUserFields(fleetUserNameUpd, email);

            page.SearchByUserName(fleetUserName);
            Assert.IsFalse(page.IsAnyDataOnGrid());
            page.SearchByUserName(fleetUserNameUpd);
            Assert.AreEqual(page.GetRowCount(), 1);
            Assert.AreEqual(fleetUserNameUpd, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(fleetUserNameUpd, page.GetFirstRowData(TableHeaders.FirstName));
            Assert.AreEqual(fleetUserNameUpd, page.GetFirstRowData(TableHeaders.LastName));
            Console.WriteLine("Fleet User Updated Successfully from " + fleetUserName + " to " + fleetUserNameUpd);
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(6), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21250" })]
        public void TC_21250(string UserType)
        {
            page.SearchByUserName(fleetUserNameUpd);
            page.ClickHyperLinkOnGrid(TableHeaders.Commands);
            var aspxPage = new AddNewUserPage(driver);
            aspxPage.DeactivateUser();

            page.SearchByUserName(fleetUserNameUpd);
            Assert.AreEqual(page.GetRowCount(), 1);
            Assert.AreEqual("False", page.GetFirstRowData(TableHeaders.Active));
            Console.WriteLine("Fleet User Deactivated Successfully: " + fleetUserNameUpd);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24226" })]
        public void TC_24226(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ManageUsers), page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            page.AreFieldsAvailable(Pages.ManageUsers).ForEach(x => { Assert.Fail(x); });
            var buttons = page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.Impersonate, ButtonsAndMessages.CreateUser);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(page.ValidateTableHeadersFromFile());
            
            page.LoadDataOnGrid();

            if (page.IsAnyDataOnGrid())
            {
                string username = page.GetFirstRowData(TableHeaders.UserName);
                page.FilterTable(TableHeaders.UserName, username);
                Assert.IsTrue(page.VerifyFilterDataOnGridByHeader(TableHeaders.UserName, username), ErrorMessages.NoRowAfterFilter);
                page.FilterTable(TableHeaders.UserName, CommonUtils.RandomString(10));
                Assert.IsTrue(page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                page.ClearFilter();
                Assert.IsTrue(page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                page.FilterTable(TableHeaders.UserName, CommonUtils.RandomString(10));
                Assert.IsTrue(page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                page.ResetFilter();
                Assert.IsTrue(page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);

                errorMsgs.AddRange(page.ValidateGridButtons(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Impersonate, ButtonsAndMessages.CreateUser));
                errorMsgs.AddRange(page.ValidateTableDetails(true, true));

                page.FilterTable(TableHeaders.Active, FieldNames.True);
                username = page.GetFirstRowData(TableHeaders.UserName);
                List<int> dataContentIds = CommonUtils.DeactivateActivatedDataContent();
                page.ClickRadioButton();
                page.ImpersonateUser(username);
                CommonUtils.ActivateDeactivatedDataContent(dataContentIds);               
            }
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22265" })]
        public void TC_22265(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ManageUsers), page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));

            page.LoadDataOnGrid();

            var addNewUserPage = page.OpenCreateUser();

            addNewUserPage.SelectValueTableDropDown(FieldNames.UserType_PermLevel_, "Regular Users");
            addNewUserPage.SearchAndSelectValueWithoutLoadingMsg(FieldNames.EntityType, addNewUserPage.RenameMenuField("Dealer"));
            addNewUserPage.WaitForLoadingGrid();

            addNewUserPage.OpenMultiSelectDropDown(FieldNames.BillingLocations);
            addNewUserPage.ClickFieldLabel(FieldNames.BillingLocations);
            Assert.IsTrue(addNewUserPage.IsMultiSelectDropDownClosed(FieldNames.BillingLocations), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.BillingLocations));

            addNewUserPage.SelectFirstRowMultiSelectDropDown(FieldNames.BillingLocations);
            addNewUserPage.ClearSelectionMultiSelectDropDown(FieldNames.BillingLocations);
            Assert.IsTrue(addNewUserPage.IsMultiSelectDropDownClosed(FieldNames.BillingLocations), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.BillingLocations));

            addNewUserPage.SelectAllRowsMultiSelectDropDown(FieldNames.BillingLocations);
            addNewUserPage.ClearSelectionMultiSelectDropDown(FieldNames.BillingLocations);

            addNewUserPage.SelectFirstRowMultiSelectDropDown(FieldNames.BillingLocations, TableHeaders.Country, "US");
            Assert.IsTrue(addNewUserPage.IsMultiSelectDropDownClosed(FieldNames.BillingLocations), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.BillingLocations));

            Assert.IsTrue(addNewUserPage.IsNextPageMultiSelectDropdown(FieldNames.BillingLocations));
            addNewUserPage.ClearSelectionMultiSelectDropDown(FieldNames.BillingLocations);

            addNewUserPage.OpenMultiSelectDropDown(FieldNames.ShopLocations);
            addNewUserPage.ClickFieldLabel(FieldNames.ShopLocations);
            Assert.IsTrue(addNewUserPage.IsMultiSelectDropDownClosed(FieldNames.ShopLocations), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ShopLocations));

            addNewUserPage.SelectFirstRowMultiSelectDropDown(FieldNames.ShopLocations);
            addNewUserPage.ClearSelectionMultiSelectDropDown(FieldNames.ShopLocations);
            Assert.IsTrue(addNewUserPage.IsMultiSelectDropDownClosed(FieldNames.ShopLocations), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ShopLocations));

            addNewUserPage.SelectAllRowsMultiSelectDropDown(FieldNames.ShopLocations);
            addNewUserPage.ClearSelectionMultiSelectDropDown(FieldNames.ShopLocations);

            addNewUserPage.SelectFirstRowMultiSelectDropDown(FieldNames.ShopLocations, TableHeaders.Country, "US");
            Assert.IsTrue(addNewUserPage.IsMultiSelectDropDownClosed(FieldNames.ShopLocations), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.ShopLocations));

            Assert.IsTrue(addNewUserPage.IsNextPageMultiSelectDropdown(FieldNames.ShopLocations));
            addNewUserPage.ClearSelectionMultiSelectDropDown(FieldNames.ShopLocations);

            addNewUserPage.OpenMultiSelectDropDown(FieldNames.UserGroup);
            addNewUserPage.ClickFieldLabel(FieldNames.UserGroup);
            Assert.IsTrue(addNewUserPage.IsMultiSelectDropDownClosed(FieldNames.UserGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.UserGroup));

            addNewUserPage.SelectFirstRowMultiSelectDropDown(FieldNames.UserGroup, false);
            addNewUserPage.ClearSelectionMultiSelectDropDown(FieldNames.UserGroup);
            Assert.IsTrue(addNewUserPage.IsMultiSelectDropDownClosed(FieldNames.UserGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.UserGroup));

            string userGroup = ManageUsersUtils.GetUserGroup("Dealer");

            addNewUserPage.SelectValueMultiSelectDropDown(FieldNames.UserGroup, TableHeaders.GroupName, userGroup);
            Assert.IsTrue(addNewUserPage.IsMultiSelectDropDownClosed(FieldNames.UserGroup), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.UserGroup));

            Assert.IsTrue(addNewUserPage.IsNextPageMultiSelectDropdown(FieldNames.UserGroup));

        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24677" })]
        public void TC_24677(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(page.GetPageLabel(), Pages.ManageUsers, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            page.AreFieldsAvailable(Pages.ManageUsers).ForEach(x => { Assert.Fail(x); });

            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            page.LoadDataOnGrid();

            UserDetails userDetails = ManageUsersUtils.GetUserDetails();
            string email = userDetails.Email;
            string userName = userDetails.Username;
            string firstName = userDetails.FirstName;
            string lastName = userDetails.LastName;
            page.EnterTextAfterClear(FieldNames.EmailAddress, email);
            page.LoadDataOnGrid();
            Assert.AreEqual(email, page.GetFirstRowData(TableHeaders.Email_));
            page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, email);
            page.EnterTextAfterClear(FieldNames.UserName, userName);
            page.LoadDataOnGrid();
            Assert.AreEqual(email, page.GetFirstRowData(TableHeaders.Email_));
            Assert.AreEqual(userName, page.GetFirstRowData(TableHeaders.UserName));
            page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, email);
            page.EnterTextAfterClear(FieldNames.UserName, userName);
            page.EnterTextAfterClear(FieldNames.FirstName, firstName);
            page.LoadDataOnGrid();
            Assert.AreEqual(email, page.GetFirstRowData(TableHeaders.Email_));
            Assert.AreEqual(userName, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(firstName, page.GetFirstRowData(TableHeaders.FirstName));
            page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, email);
            page.EnterTextAfterClear(FieldNames.UserName, userName);
            page.EnterTextAfterClear(FieldNames.FirstName, firstName);
            page.EnterTextAfterClear(FieldNames.LastName, lastName);
            page.LoadDataOnGrid();
            Assert.AreEqual(email, page.GetFirstRowData(TableHeaders.Email_));
            Assert.AreEqual(userName, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(firstName, page.GetFirstRowData(TableHeaders.FirstName));
            Assert.AreEqual(lastName, page.GetFirstRowData(TableHeaders.LastName));
            page.ClearGrid();

            UserDetails emailWithSpecialChar1 = ManageUsersUtils.GetEmailSpecialCharacter("-");
            string specialCharEmail1 = emailWithSpecialChar1.Email;
            page.EnterTextAfterClear(FieldNames.EmailAddress, specialCharEmail1);
            page.LoadDataOnGrid();
            Assert.AreEqual(specialCharEmail1, page.GetFirstRowData(TableHeaders.Email_));
            page.ClearGrid();

            UserDetails getUserDetailsUTF = ManageUsersUtils.GetUserDetailsUTF();
            string emailUTF = getUserDetailsUTF.Email;
            string userNameUTF = getUserDetailsUTF.Username;
            string firstNameUTF = getUserDetailsUTF.FirstName;
            string lastNameUTF = getUserDetailsUTF.LastName;
            page.EnterTextAfterClear(FieldNames.EmailAddress, emailUTF);
            page.EnterTextAfterClear(FieldNames.UserName, userNameUTF);
            page.EnterTextAfterClear(FieldNames.FirstName, firstNameUTF);
            page.EnterTextAfterClear(FieldNames.LastName, lastNameUTF);
            page.LoadDataOnGrid();
            Assert.AreEqual(emailUTF, page.GetFirstRowData(TableHeaders.Email_));
            Assert.AreEqual(userNameUTF, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(firstNameUTF, page.GetFirstRowData(TableHeaders.FirstName));
            Assert.AreEqual(lastNameUTF, page.GetFirstRowData(TableHeaders.LastName));
            page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, email.ToLower());
            page.EnterTextAfterClear(FieldNames.UserName, userName.ToLower());
            page.EnterTextAfterClear(FieldNames.FirstName, firstName.ToLower());
            page.EnterTextAfterClear(FieldNames.LastName, lastName.ToLower());
            page.LoadDataOnGrid();
            Assert.AreEqual(email, page.GetFirstRowData(TableHeaders.Email_));
            Assert.AreEqual(userName, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(firstName, page.GetFirstRowData(TableHeaders.FirstName));
            Assert.AreEqual(lastName, page.GetFirstRowData(TableHeaders.LastName));
            page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, email.ToUpper());
            page.EnterTextAfterClear(FieldNames.UserName, userName.ToUpper());
            page.EnterTextAfterClear(FieldNames.FirstName, firstName.ToUpper());
            page.EnterTextAfterClear(FieldNames.LastName, lastName.ToUpper());
            page.LoadDataOnGrid();
            Assert.AreEqual(email, page.GetFirstRowData(TableHeaders.Email_));
            Assert.AreEqual(userName, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(firstName, page.GetFirstRowData(TableHeaders.FirstName));
            Assert.AreEqual(lastName, page.GetFirstRowData(TableHeaders.LastName));
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24678" })]
        public void TC_24678(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(page.GetPageLabel(), Pages.ManageUsers, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            page.AreFieldsAvailable(Pages.ManageUsers).ForEach(x => { Assert.Fail(x); });

            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            UserDetails userDetails = ManageUsersUtils.GetUserDetails();
            string validEmail = userDetails.Email;
            string validUserName = userDetails.Username;
            string validFirstName = userDetails.FirstName;
            string validLastName = userDetails.LastName;
            string invalidEmail = CommonUtils.GetRandomEmail();
            string invalidUsername = CommonUtils.RandomString(6);
            string invalidFirstName = CommonUtils.RandomString(6);
            string invalidLastName = CommonUtils.RandomString(6);

            page.EnterTextAfterClear(FieldNames.EmailAddress, invalidEmail);
            page.LoadDataOnGrid();
            Assert.IsFalse(page.IsAnyDataOnGrid());

            page.EnterTextAfterClear(FieldNames.EmailAddress, invalidEmail);
            page.EnterTextAfterClear(FieldNames.UserName, invalidUsername);
            page.LoadDataOnGrid();
            Assert.IsFalse(page.IsAnyDataOnGrid());

            page.EnterTextAfterClear(FieldNames.EmailAddress, validEmail);
            page.EnterTextAfterClear(FieldNames.UserName, validUserName);
            page.EnterTextAfterClear(FieldNames.FirstName, invalidFirstName);
            page.LoadDataOnGrid();
            Assert.IsFalse(page.IsAnyDataOnGrid());

            page.EnterTextAfterClear(FieldNames.EmailAddress, validEmail);
            page.EnterTextAfterClear(FieldNames.UserName, invalidUsername);
            page.EnterTextAfterClear(FieldNames.FirstName, validFirstName);
            page.EnterTextAfterClear(FieldNames.LastName, invalidLastName);
            page.LoadDataOnGrid();
            Assert.IsFalse(page.IsAnyDataOnGrid());

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24679" })]
        public void TC_24679(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(page.GetPageLabel(), Pages.ManageUsers, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            page.AreFieldsAvailable(Pages.ManageUsers).ForEach(x => { Assert.Fail(x); });

            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            UserDetails userDetailsMaxLenght = ManageUsersUtils.GetUserDetailsWithMaxCharacter();
            string validEmailWithMaxLength = userDetailsMaxLenght.Email;
            string validUserNameWithMaxLength = userDetailsMaxLenght.Username;
            string validFirstNameWithMaxLength = userDetailsMaxLenght.FirstName;
            string validLastNameWithMaxLength = userDetailsMaxLenght.LastName;

            page.EnterTextAfterClear(FieldNames.EmailAddress, validEmailWithMaxLength);
            page.LoadDataOnGrid();
            Assert.AreEqual(validEmailWithMaxLength, page.GetFirstRowData(TableHeaders.Email_));
            page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, validEmailWithMaxLength);
            page.EnterTextAfterClear(FieldNames.UserName, validUserNameWithMaxLength);
            page.EnterTextAfterClear(FieldNames.FirstName, validFirstNameWithMaxLength);
            page.EnterTextAfterClear(FieldNames.LastName, validLastNameWithMaxLength);
            page.LoadDataOnGrid();
            Assert.AreEqual(validEmailWithMaxLength, page.GetFirstRowData(TableHeaders.Email_));
            Assert.AreEqual(validUserNameWithMaxLength, page.GetFirstRowData(TableHeaders.UserName));
            Assert.AreEqual(validFirstNameWithMaxLength, page.GetFirstRowData(TableHeaders.FirstName));
            Assert.AreEqual(validLastNameWithMaxLength, page.GetFirstRowData(TableHeaders.LastName));
            page.ClearGrid();

            //CON-24903:[Admin - Manage Users] Email Address Field Accepting More Than 200 Characters
            //page.EnterTextAfterClear(FieldNames.EmailAddress, CommonUtils.RandomString(220));
            //Assert.AreEqual(200, page.GetValue(FieldNames.EmailAddress).Length);
            //page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, "a");
            page.LoadDataOnGrid();
            Assert.IsTrue(page.IsAnyDataOnGrid());

            page.EnterTextAfterClear(FieldNames.EmailAddress, "a");
            page.EnterTextAfterClear(FieldNames.UserName, "a");
            page.EnterTextAfterClear(FieldNames.FirstName, "a");
            page.EnterTextAfterClear(FieldNames.LastName, "a");
            page.LoadDataOnGrid();
            Assert.IsTrue(page.IsAnyDataOnGrid());
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24680" })]
        public void TC_24680(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(page.GetPageLabel(), Pages.ManageUsers, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            page.AreFieldsAvailable(Pages.ManageUsers).ForEach(x => { Assert.Fail(x); });

            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
            Assert.IsTrue(page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

            UserDetails userDetails = ManageUsersUtils.GetUserDetails();
            string validEmail = userDetails.Email.ToLower();
            string validUserName = userDetails.Username.ToLower();
            string validFirstName = userDetails.FirstName.ToLower();
            string validLastName = userDetails.LastName.ToLower();

            string[] Email = validEmail.Split("@");
            string partialEmail = Email[1].ToLower();
            var partialUsername = validUserName.Substring(0, (int)(validUserName.Length / 2)).ToLower();
            var partialFirstName = validFirstName.Substring(0, (int)(validFirstName.Length / 2)).ToLower();
            var partialLastName = validLastName.Substring(0, (int)(validLastName.Length / 2)).ToLower();

            page.EnterTextAfterClear(FieldNames.EmailAddress, partialEmail);
            page.LoadDataOnGrid();
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.Email_).ToLower().Contains(partialEmail));
            page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, partialEmail);
            page.EnterTextAfterClear(FieldNames.UserName, partialUsername);
            page.LoadDataOnGrid();
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.Email_).ToLower().Contains(partialEmail));
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.UserName).ToLower().Contains(partialUsername));
            page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, partialEmail);
            page.EnterTextAfterClear(FieldNames.UserName, partialUsername);
            page.EnterTextAfterClear(FieldNames.FirstName, partialFirstName);
            page.LoadDataOnGrid();
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.Email_).ToLower().Contains(partialEmail));
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.UserName).ToLower().Contains(partialUsername));
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.FirstName).ToLower().Contains(partialFirstName));
            page.ClearGrid();

            page.EnterTextAfterClear(FieldNames.EmailAddress, partialEmail);
            page.EnterTextAfterClear(FieldNames.UserName, partialUsername);
            page.EnterTextAfterClear(FieldNames.FirstName, partialFirstName);
            page.EnterTextAfterClear(FieldNames.LastName, partialLastName);
            page.LoadDataOnGrid();
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.Email_).ToLower().Contains(partialEmail));
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.UserName).ToLower().Contains(partialUsername));
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.FirstName).ToLower().Contains(partialFirstName));
            Assert.IsTrue(page.GetFirstRowData(TableHeaders.LastName).ToLower().Contains(partialLastName));
            page.ClearGrid();
        }
    }
}

