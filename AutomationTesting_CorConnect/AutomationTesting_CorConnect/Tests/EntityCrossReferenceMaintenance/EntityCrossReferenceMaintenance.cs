using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.EntityCrossReferenceMaintenance;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.EntityCrossReferenceMaintenance;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Tests.EntityCrossReferenceMaintenance
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Entity Cross Reference Maintenance")]
    internal class EntityCrossReferenceMaintenance : DriverBuilderClass
    {

        EntityCrossReferenceMaintenancePage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenPage(Pages.EntityCrossReferenceMaintenance);
            Page = new EntityCrossReferenceMaintenancePage(driver);
        }


        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20141" })]
        public void TC_20141(string UserType)
        {
            Assert.Multiple(() =>
            {
                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
                Assert.AreEqual(Page.GetPageLabel(), Pages.EntityCrossReferenceMaintenance, GetErrorMessage(ErrorMessages.PageCaptionMisMatch));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Search), GetErrorMessage(ErrorMessages.SearchButtonNotFound));
                Assert.IsTrue(Page.IsButtonVisible(ButtonsAndMessages.Clear), GetErrorMessage(ErrorMessages.ClearButtonNotFound));

                LoggingHelper.LogMessage(LoggerMesages.ValidatingPageFields);
                Page.AreFieldsAvailable(Pages.EntityCrossReferenceMaintenance).ForEach(x=>{ Assert.Fail(x); });

                EntityCrossReferenceMaintenanceUtil.GetCorCentricCode(out string corCentricCode, out string type);

                if (corCentricCode == null || type == null)
                {
                    Assert.Fail(GetErrorMessage(ErrorMessages.ErrorGettingData));
                }

                if (!string.IsNullOrEmpty(corCentricCode))
                {
                    List<string> errorMsgs = new List<string>();
                    errorMsgs.AddRange(Page.ValidateTableHeadersFromFile());

                    Page.PopulateGrid(corCentricCode);

                    Assert.IsTrue(Page.ClickAddNewCrossReferenceEntry());
                    Page.ClosePopupWindow();

                    errorMsgs.AddRange(Page.ValidateGridButtons(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset, ButtonsAndMessages.NewEntry));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));

                    Assert.AreEqual(ButtonsAndMessages.DeleteAlertMessage, Page.ValidateDelete(), GetErrorMessage(ErrorMessages.AlertMessageMisMatch));

                    foreach (var errorMsg in errorMsgs)
                    {
                        Assert.Fail(GetErrorMessage(errorMsg));
                    }
                }               

            });
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "20254" })]
        public void TC_20254(string UserType)
        {
            EntityCrossReferenceMaintenanceUtil.GetCorCentricCode(out string corCentricCode, out string type);
            
            Assert.That(corCentricCode,Is.Not.Null.And.Not.Empty, GetErrorMessage(ErrorMessages.ErrorGettingData));
            Assert.That(type, Is.Not.Null.And.Not.Empty, GetErrorMessage(ErrorMessages.ErrorGettingData));

            Page.PopulateGrid(corCentricCode);

            var addNewCrossReferenceEntry = Page.OpenAddNewCrossReferenceEntry();

            Assert.IsTrue(addNewCrossReferenceEntry.ValidateDropDown(FieldNames.EntityType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.EntityType));
            Assert.IsTrue(addNewCrossReferenceEntry.ValidateDropDown(FieldNames.CrossReferenceType), GetErrorMessage(ErrorMessages.DropDownNotFound, FieldNames.EntityType));
            Assert.IsTrue(addNewCrossReferenceEntry.VerifyValue(FieldNames.EntityType, Page.RenameMenuField("Dealer"), Page.RenameMenuField("Fleet")));
            Assert.IsTrue(addNewCrossReferenceEntry.VerifyValue(FieldNames.CrossReferenceType, "EDICode", "Intercommunity"));
            Assert.IsTrue(addNewCrossReferenceEntry.IsInputFieldVisible(FieldNames.CrossReferenceCode));
            Assert.IsTrue(addNewCrossReferenceEntry.IsInputFieldVisible(FieldNames.SourceBillTo));
            Assert.IsTrue(addNewCrossReferenceEntry.IsElementDisplayed(ButtonsAndMessages.ADD));
            Assert.IsTrue(addNewCrossReferenceEntry.IsElementDisplayed(ButtonsAndMessages.Cancel));

            addNewCrossReferenceEntry.ClickAdd();

            Assert.IsTrue(addNewCrossReferenceEntry.CheckForText("Entity Type is Required.", true));
            Assert.IsTrue(addNewCrossReferenceEntry.CheckForText("Cross Reference Type is Required."));
            Assert.IsTrue(addNewCrossReferenceEntry.CheckForText("Cross Reference Code is required"));
            Assert.IsTrue(addNewCrossReferenceEntry.CheckForText("Account Code is Required."));

            addNewCrossReferenceEntry.AddNewCrossRefrence(corCentricCode, type, out string crossRefCode, out bool isCorrectDropDown);

            Assert.IsTrue(addNewCrossReferenceEntry.CheckForText("Record saved successfully."));
            addNewCrossReferenceEntry.ClickClose();
            Assert.AreEqual(addNewCrossReferenceEntry.GetWindowsCount(), 1);

            var errormsgs = Page.Delete(TableHeaders.CrossReferenceCode, crossRefCode);

            foreach (var errormsg in errormsgs)
            {
                Assert.Fail(GetErrorMessage(errormsg));
            }

        }
    }
}