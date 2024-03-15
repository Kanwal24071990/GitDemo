using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ManageApprovals;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.ManageApprovals
{

    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage Approvals")]
    internal class ManageApprovals : DriverBuilderClass
    {
        ManageApprovalsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.ManageApprovals);
            Page = new ManageApprovalsPage(driver);

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22213" })]
        public void TC_22213(string UserType)
        {
            Page.OpenDropDown(FieldNames.Country);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.OpenDropDown(FieldNames.Country);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.SelectValueFirstRow(FieldNames.Country);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.Country, "US");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));

            Page.OpenDropDown(FieldNames.LocationApprovalStatus);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationApprovalStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationApprovalStatus));
            Page.OpenDropDown(FieldNames.LocationApprovalStatus);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationApprovalStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationApprovalStatus));
            Page.SelectValueFirstRow(FieldNames.LocationApprovalStatus);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationApprovalStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationApprovalStatus));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.LocationApprovalStatus, "Disabled");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationApprovalStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationApprovalStatus));

            Page.OpenDropDown(FieldNames.LocationType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.OpenDropDown(FieldNames.LocationType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.SelectValueFirstRow(FieldNames.LocationType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.LocationType, "Billing");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));

        }
        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24610" })]
        public void TC_24610(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ManageApprovals), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);

            Page.OpenDropDown(FieldNames.Country);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.Country), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.Country));
            Page.OpenDropDown(FieldNames.LocationType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationType));
            Page.OpenDropDown(FieldNames.LocationApprovalStatus);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationApprovalStatus), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocationApprovalStatus));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
            Page.AreFieldsAvailable(Pages.ManageApprovals).ForEach(x => { Assert.Fail(x); });
            var buttons = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.SaveApprovalStatus);
            Assert.IsTrue(buttons.Count > 0);

            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

            Page.LoadDataOnGrid();

            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                string displayname = Page.GetFirstRowData(TableHeaders.DisplayName);

                Page.SetFilterCreiteria(TableHeaders.DisplayName, FilterCriteria.Equals);

                Page.FilterTable(TableHeaders.DisplayName, displayname);
                Assert.IsTrue(Page.IsInputVisible(ButtonsAndMessages.SaveApprovalStatus));
                errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));               
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
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24663" })]
        public void TC_24663(string UserType)
        {
            Page.LoadDataOnGrid();

            if (Page.IsAnyDataOnGrid())
            {
                List<string> errorMsgs = new List<string>();
                string displayname = Page.GetFirstRowData(TableHeaders.DisplayName);
                Page.FilterTable(TableHeaders.DisplayName, displayname);

                Assert.IsTrue(Page.IsNestedGridOpen(), GetErrorMessage(ErrorMessages.NestedTableNotVisible));

                if (Page.IsAnyDataOnGrid())
                {
                    Assert.IsTrue(Page.IsInputVisible(ButtonsAndMessages.SaveUsersAssignments));
                    errorMsgs = Page.ValidateNestedGridButtonsWithoutExport(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter);
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                    List<string> headers = new List<String>()
                    {
                        TableHeaders.UserName,
                        TableHeaders.HierarchyAccessEnabled,
                        TableHeaders.FirstName,
                        TableHeaders.LastName,
                        TableHeaders.email,
                        TableHeaders.AdminLevel,
                        TableHeaders.UserGroups,
                        TableHeaders.Active,
                        TableHeaders.ApprovalAccess,

                    };
                    errorMsgs.AddRange(Page.ValidateNestedTableHeaders());
                    string Username = Page.GetFirstRowDataNestedTable(TableHeaders.Username_);
                    Page.FilterNestedTable(TableHeaders.Username_, Username);
                    errorMsgs = Page.ValidateNestedGridButtonsWithoutExport(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter);
                    Assert.Multiple(() =>
                    {
                        foreach (var errorMsg in errorMsgs)
                        {
                            Assert.Fail(GetErrorMessage(errorMsg));
                        }
                    });

                }
            }
            Page.ClickNestedGridTab("Assign Approvers To Location");
          
            if (Page.IsAnyDataOnGrid())
            {
                List<string> errorMsgs = new List<string>();
                errorMsgs = Page.ValidateGridButtons(ButtonsAndMessages.Reset,ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter);
                Assert.IsTrue(Page.IsInputVisible(ButtonsAndMessages.SaveApprovers));
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                List<string> headers = new List<String>()
                    {
                        TableHeaders.UserName,
                        TableHeaders.HierarchyAccessEnabled,
                        TableHeaders.FirstName,
                        TableHeaders.LastName,
                        TableHeaders.email,
                        TableHeaders.AdminLevel,
                        TableHeaders.UserGroups,
                        TableHeaders.Active,
                        TableHeaders.ApprovalAccess,

                    };
                errorMsgs.AddRange(Page.ValidateNestedTableHeaders());
                string username_ = Page.GetFirstRowDataNestedTable(TableHeaders.Username_);
                Page.FilterNestedTable(TableHeaders.Username_, username_);
                errorMsgs = Page.ValidateNestedGridButtonsWithoutExport(ButtonsAndMessages.Reset, ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter);
                Assert.Multiple(() =>
                {
                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                });
            }
        }
    }
}

