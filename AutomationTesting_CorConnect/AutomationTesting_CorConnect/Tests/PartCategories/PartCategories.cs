using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.PartCategories;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Tests.PartCategories
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Part Categories")]
    internal class PartCategories : DriverBuilderClass
    {
        PartCategoriesPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.PartCategories, waitForGridLoad: false);
            Page = new PartCategoriesPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "23416" })]
        public void TC_23416(string UserType)
        {
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(Page.RenameMenuField(Pages.PartCategories), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

            var errorMsgs = Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset);
            if (Page.IsAnyDataOnGrid())
            {
                errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());
               
                if (Page.IsAnyDataOnGrid())
                {
                    Page.ClickEdit();
                    Dictionary<string, string> fields;
                    errorMsgs.AddRange(Page.VerifyFields(Pages.PartCategories, ButtonsAndMessages.Edit, out fields));
                    if (fields == null || fields.Count == 0)
                    {
                        throw new Exception(string.Format(ErrorMessages.FieldsNotFoundException, Pages.PartCategories));
                    }

                    Page.CloseEditGrid();
                    Page.ClickNew();
                    errorMsgs.AddRange(Page.VerifyFields(Pages.PartCategories, ButtonsAndMessages.New, out fields));
                    if (fields == null || fields.Count == 0)
                    {
                        throw new Exception(string.Format(ErrorMessages.FieldsNotFoundException, Pages.PartCategories));
                    }
                    Page.CloseEditGrid();

                    Assert.Multiple(() =>
                    {
                        foreach (var errorMsg in errorMsgs)
                        {
                            Assert.Fail(GetErrorMessage(errorMsg));
                        }
                    });

                    string category = Page.GetFirstRowData(TableHeaders.Category);
                    string description = Page.GetFirstRowData(TableHeaders.Description);
                    Page.FilterTable(TableHeaders.Category, category);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Description, description), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Description, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.Category, category);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Description, description), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Description, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                }
            }
        }
    }
}
