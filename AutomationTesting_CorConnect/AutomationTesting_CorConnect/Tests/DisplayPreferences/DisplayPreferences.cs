using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects.DisplayPreferences;
using AutomationTesting_CorConnect.Resources;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace AutomationTesting_CorConnect.Tests.DisplayPreferences
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Display Preferences")]
    internal class DisplayPreferences : DriverBuilderClass
    {
        DisplayPreferencesPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.DisplayPreferences, false, true);
            menu.SwitchIframe();
            Page = new DisplayPreferencesPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22211" })]
        public void TC_22211(string UserType)
        {
            Page.OpenDropDown(FieldNames.LocaleSettings);
            Page.ClickFieldLabel(FieldNames.LocaleSettings);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocaleSettings), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocaleSettings));
            Page.SelectValueFirstRow(FieldNames.LocaleSettings);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocaleSettings), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocaleSettings));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.LocaleSettings, "en-US (System Default)");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LocaleSettings), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LocaleSettings));

            Page.OpenDropDown(FieldNames.DefaultSearchType);
            Page.ClickFieldLabel(FieldNames.DefaultSearchType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DefaultSearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DefaultSearchType));
            Page.SelectValueFirstRow(FieldNames.DefaultSearchType);
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DefaultSearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DefaultSearchType));
            Page.SearchAndSelectValueAfterOpenWithoutClear(FieldNames.DefaultSearchType, "Advanced Search");
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.DefaultSearchType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.DefaultSearchType));


        }
    }
}
