using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.FleetLocations;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.PageObjects.RelationshipSearch;
using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;

namespace AutomationTesting_CorConnect.Tests.RelationshipSearch
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Relationship Search")]
    internal class RelationshipSearch : DriverBuilderClass
    {
        RelationshipSearchPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPopUpPage(Pages.RelationshipSearch);
            Page = new RelationshipSearchPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "17348" })]
        public void TC_17348(string UserType)
        {

            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), ErrorMessages.ClearButtonNotFound);
            Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), ErrorMessages.SearchButtonNotFound);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.EntityType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.EntityType));
            Assert.AreEqual(Page.RenameMenuField(EntityType.Dealer), Page.GetValueOfDropDown(FieldNames.EntityType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.EntityType));
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocationType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.LocationType));
            Assert.AreEqual("All", Page.GetValueOfDropDown(FieldNames.LocationType), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.LocationType));
            Assert.IsTrue(Page.IsMultiSelectDropDownClosed(FieldNames.RelationshipType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.GroupName));

            List<string> headerNames = Page.GetHeaderNamesMultiSelectDropDown(FieldNames.Location);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(headerNames.Contains(TableHeaders.DisplayName), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.DisplayName, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.City), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.City, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.Address), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Address, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.State), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.State, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.Country), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.Country, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.LocationType), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.LocationType, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.EntityCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.EntityCode, FieldNames.ContainsLocation));
                Assert.IsTrue(headerNames.Contains(TableHeaders.AccountCode), GetErrorMessage(ErrorMessages.ColumnMissing, FieldNames.AccountCode, FieldNames.ContainsLocation));
            });
            Page.SelectValueMultiSelectDropDown(FieldNames.RelationshipType, TableHeaders.RelationshipType, "Currency Code");
            Page.LoadDataOnGrid();
            List<string> errorMsgs = new List<string>();
            if (Page.IsAnyDataOnGrid())
            {
                Page.FilterRightGrid(TableHeaders.LocationType, "Billing");
                Assert.AreEqual("Billing", Page.GetFirstValueFromGrid(TableHeaders.LocationType));
                Page.ClearFilter();
                Assert.IsTrue(Page.GetRowCount() > 0, ErrorMessages.ClearFilterNotWorking);
                List<string> headers = new List<String>()
                    {
                        TableHeaders.Dealer,
                        TableHeaders.DealerCode,
                        TableHeaders.Fleet,
                        TableHeaders.FleetCode,
                        TableHeaders.RelationshipCategory,
                        TableHeaders.LocationType,
                        TableHeaders.RelationshipType,

                    };
                errorMsgs.AddRange(Page.ValidateTableHeaders());

                if (Page.IsNextPage())
                {
                    Page.GoToPage(2);
                }
            }
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
