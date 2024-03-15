using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.FleetPartsCrossReference;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.FleetPartsCrossReference;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.FleetPartsCrossReference
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Fleet Parts Cross Reference")]
    internal class FleetPartsCrossReference : DriverBuilderClass
    {
        FleetPartsCrossReferencePage Page;
        private string fleetPartNum = CommonUtils.RandomString(7);

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.FleetPartsCrossReference);
            Page = new FleetPartsCrossReferencePage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20140" })]
        public void TC_20140(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.RenameMenuField(Pages.FleetPartsCrossReference), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.FleetPartsCrossReference).ForEach(x=>{ Assert.Fail(x); });

                Assert.AreEqual(string.Empty, Page.GetValueDropDown(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.FleetCode));
                Assert.AreEqual(string.Empty, Page.GetValueDropDown(FieldNames.FleetPartNumber), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.FleetPartNumber));
                Assert.AreEqual(string.Empty, Page.GetValue(FieldNames.CommunityPartNumber), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.CommunityPartNumber));
                Assert.AreEqual(string.Empty, Page.GetValue(FieldNames.DealerCode), GetErrorMessage(ErrorMessages.InvalidDefaultValue, FieldNames.DealerCode));

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                FleetPartsCrossReferenceUtil.GetData(out string fleetCode);
                Page.PopolateGrid(fleetCode);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter,
                        ButtonsAndMessages.ClearFilter,
                        ButtonsAndMessages.Reset,
                        menu.RenameMenuField(ButtonsAndMessages.CreatePartsCrossReference)));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));                                       
                }
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "21246" })]
        public void TC_21246(string UserType)
        {
            FleetPartsCrossReferenceUtil.GetData(out string fleetDisplayName);
            FleetPartsCrossReferenceUtil.GetCommunityPartNumber(fleetDisplayName, out string communityPartNumber);
            Page.PopolateGrid(fleetDisplayName);
            var createNewPartsCrossReference = Page.OpenCreatePartsCrossReference();
            Assert.IsTrue(createNewPartsCrossReference.CreatePartsCrossReference(fleetDisplayName, communityPartNumber, fleetPartNum), "Failed to Create Part Cross Reference");
            Page.ButtonClick(ButtonsAndMessages.Clear);
            Page.AcceptAlert();
            Page.WaitForMsg(ButtonsAndMessages.Loading);
            Page.WaitForGridLoad();
            Page.SearchByFleetPartNumber(fleetPartNum);
            Assert.AreEqual(1, Page.GetRowCount());
            Assert.AreEqual(fleetPartNum, Page.GetFirstRowData(TableHeaders.FleetPartNumber));

        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22240" })]
        public void TC_22240(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.FleetPartsCrossReference), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            Page.OpenDropDown(FieldNames.FleetCode);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));

            Page.OpenDropDown(FieldNames.FleetCode);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));

            Page.SelectValueFirstRow(FieldNames.FleetCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));

            string fleetCode = CommonUtils.GetFleetCode();
            Page.SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleetCode);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetCode), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetCode));

            Page.OpenDropDown(FieldNames.FleetPartNumber);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetPartNumber), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetPartNumber));

            Page.OpenDropDown(FieldNames.FleetPartNumber);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetPartNumber), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetPartNumber));

            Page.SelectValueFirstRow(FieldNames.FleetPartNumber);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetPartNumber), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetPartNumber));

            string fleetPartNumber = Page.GetValueDropDown(FieldNames.FleetPartNumber).Trim();
            Page.SearchAndSelectValueAfterOpen(FieldNames.FleetPartNumber, fleetPartNumber);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.FleetPartNumber), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.FleetPartNumber));

        }
    }
}
