using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.TaxCodeConfiguration;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.TaxCodeConfiguration;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.TaxCodeConfiguration
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Tax Code Configuration")]
    internal class TaxCodeConfiguration : DriverBuilderClass
    {
        TaxCodeConfigurationPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.TaxCodeConfiguration);
            Page = new TaxCodeConfigurationPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20137" })]
        public void TC_20137(string UserType)
        {
            string lineType = TaxCodeConfigurationUtils.GetLineType();
            Assert.NotNull(lineType, GetErrorMessage(ErrorMessages.ErrorGettingData));

            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.TaxCodeConfiguration, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.TaxCodeConfiguration).ForEach(x=>{ Assert.Fail(x); });

                List<string> errorMsgs = new List<string>();
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                Page.PopulateGrid(lineType);

                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                    
                    errorMsgs.AddRange(Page.VerifyEditFields(Pages.TaxCodeConfiguration));

                    if (Page.GetRowCountCurrentPage() > 0)
                    {
                        Assert.AreEqual(ButtonsAndMessages.DeleteAlertMessage, Page.ValidateDelete(), GetErrorMessage(ErrorMessages.AlertMessageMisMatch));
                    }
                }

                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "22234" })]
        public void TC_22234(string UserType)
        {
            Page.OpenDropDown(FieldNames.LineType);
            Page.ClickPageTitle();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LineType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LineType));
            Page.OpenDropDown(FieldNames.LineType);
            Page.ScrollDiv();
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LineType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LineType));
            Page.SelectValueTableDropDown(FieldNames.LineType, TaxCodeConfigurationUtils.GetLineType());
            Assert.IsTrue(Page.IsDropDownClosed(FieldNames.LineType), GetErrorMessage(ErrorMessages.DropDownNotClosed, FieldNames.LineType));
        }
    }
}
