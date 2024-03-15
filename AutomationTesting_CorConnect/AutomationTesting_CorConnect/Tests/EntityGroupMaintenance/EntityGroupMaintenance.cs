using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.EntityGroupMaintenance;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.EntityGroupMaintenance;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.EntityGroupMaintenance
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Entity Group Maintenance")]
    internal class EntityGroupMaintenance : DriverBuilderClass
    {
        EntityGroupMaintenancePage Page;
        private string groupNameFleet = CommonUtils.RandomString(6);
        private string groupNameDealer = CommonUtils.RandomString(6);

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.EntityGroupMaintenance);
            Page = new EntityGroupMaintenancePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20208" })]
        public void TC_20208(string UserType)
        {
            var groupName = EntityGroupMaintenanceUtil.GetGroupName();
            Assert.IsNotNull(groupName, GetErrorMessage(ErrorMessages.ErrorGettingData));

            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.RenameMenuField(Pages.EntityGroupMaintenance), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.EntityGroupMaintenance).ForEach(x => { Assert.Fail(x); });

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                Page.PopulateGrid(groupName, string.Empty);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtonsWithoutExport());
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                    
                }

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(1), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21253" })]
        public void TC_21253(string UserType)
        {
            Page.PopulateGrid(string.Empty, FieldNames.RelationshipGroups);

            var errorMsgs = Page.CreateNewEntityGroupMaintenance(groupNameFleet, FieldNames.RelationshipGroups, menu.RenameMenuField(FieldNames.Fleet));

            foreach (var errormsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errormsg));
            }

            Page.PopulateGrid(groupNameFleet, FieldNames.RelationshipGroups);
            Assert.AreEqual(Page.GetRowCount(), 1);
            Assert.AreEqual(groupNameFleet, Page.GetFirstRowData(TableHeaders.GroupName), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(menu.RenameMenuField(FieldNames.Fleet), Page.GetFirstRowData(TableHeaders.EntityType), ErrorMessages.ValueMisMatch);
            Console.WriteLine($"Group Name created for Type Fleet: [{groupNameFleet}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(2), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21252" })]
        public void TC_21252(string UserType)
        {
            Page.PopulateGrid(string.Empty, FieldNames.RelationshipGroups);

            var errorMsgs = Page.CreateNewEntityGroupMaintenance(groupNameDealer, FieldNames.RelationshipGroups, menu.RenameMenuField(EntityType.Dealer));

            foreach (var errormsg in errorMsgs)
            {
                Assert.Fail(GetErrorMessage(errormsg));
            }

            Page.PopulateGrid(groupNameDealer, FieldNames.RelationshipGroups);
            Assert.AreEqual(Page.GetRowCount(), 1);
            Assert.AreEqual(groupNameDealer, Page.GetFirstRowData(TableHeaders.GroupName), ErrorMessages.ValueMisMatch);
            Assert.AreEqual(menu.RenameMenuField(EntityType.Dealer), Page.GetFirstRowData(TableHeaders.EntityType), ErrorMessages.ValueMisMatch);
            Console.WriteLine($"Group Name created for Type Dealer: [{groupNameDealer}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, Order(3), TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21325" })]
        public void TC_21325(string UserType)
        {
            Page.PopulateGrid(groupNameDealer, "Relationship Groups");
            Page.ClickEdit();
            Page.EnterTextAfterClear(FieldNames.GroupDescription, "TestRltnshpgrp1 Upd", ButtonsAndMessages.Edit);
            Page.ClickElement(FieldNames.IsActive_);
            Page.UpdateEditGrid();
            Assert.AreEqual(Page.GetEditMsg(), "The record has been updated successfully.\r\nPlease use Close button to exit from update form.");
            Assert.IsTrue(Page.IsCheckBoxUnchecked(FieldNames.IsActive_), ErrorMessages.CheckBoxChecked);
            Console.WriteLine($"Group Name Unactivated: [{groupNameDealer}]");
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22237" })]
        public void TC_22237(string UserType)
        {
            Page.OpenDropDown(FieldNames.GroupName);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.GroupName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.GroupName));

            Page.OpenDropDown(FieldNames.GroupName);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.GroupName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.GroupName));

            Page.SelectValueFirstRow(FieldNames.GroupName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.GroupName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.GroupName));

            string groupName = Page.GetValueDropDown(FieldNames.GroupName).Trim();
            Page.SearchAndSelectValueAfterOpen(FieldNames.GroupName, groupName);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.GroupName), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.GroupName));

            Page.ClickPageTitle();
            Page.OpenDropDown(FieldNames.GroupType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.GroupType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.GroupType));

            Page.OpenDropDown(FieldNames.GroupType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.GroupType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.GroupType));

            Page.SelectValueFirstRow(FieldNames.GroupType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.GroupType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.GroupType));

            string groupType = Page.GetValueDropDown(FieldNames.GroupType).Trim();
            Page.SearchAndSelectValueAfterOpen(FieldNames.GroupType, groupType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.GroupType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.GroupType));

        }
    }
}
