using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataProvider;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.ManageUserNotifications;
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

namespace AutomationTesting_CorConnect.Tests.ManageUserNotifications
{
    [TestFixture]
    [Parallelizable(ParallelScope.Self)]
    [AllureNUnit]
    [AllureSuite("Manage User Notifications")]
    internal class ManageUserNotifications : DriverBuilderClass
    {
        ManageUserNotificationsPage Page;

        [SetUp]
        public void Setup()
        {
            menu.OpenSingleGridPage(Pages.ManageUserNotifications);
            Page = new ManageUserNotificationsPage(driver);
        }

        [Category(TestCategory.Smoke)]
        [Test, TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.TestData), new object[] { "24218" })]
        public void TC_24218(string UserType)
        {

            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageCaption);
            Assert.AreEqual(menu.RenameMenuField(Pages.ManageUserNotifications), Page.GetPageLabel(), GetErrorMessage(ErrorMessages.PageCaptionMisMatch));
            LoggingHelper.LogMessage(LoggerMesages.ValidatingPageButtons);
            var errorMsgs = Page.ValidateTableHeadersFromFile();
            Assert.Multiple(() =>
            {
                if (Page.IsAnyDataOnGrid())
                {
                    errorMsgs.AddRange(Page.ValidateGridButtonsWithoutExport(ButtonsAndMessages.ApplyFilter, ButtonsAndMessages.ClearFilter, ButtonsAndMessages.Reset));
                    errorMsgs.AddRange(Page.ValidateTableDetails(true, true));
                    string description = Page.GetFirstRowData(TableHeaders.Description);
                    Page.FilterTable(TableHeaders.Description, description);
                    Assert.IsTrue(Page.VerifyFilterDataOnGridByHeader(TableHeaders.Description, description), ErrorMessages.NoRowAfterFilter);
                    Page.FilterTable(TableHeaders.Description, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ClearFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ClearFilterNotWorking);
                    Page.FilterTable(TableHeaders.Description, CommonUtils.RandomString(10));
                    Assert.IsTrue(Page.GetRowCountCurrentPage() <= 0, ErrorMessages.FilterNotWorking);
                    Page.ResetFilter();
                    Assert.IsTrue(Page.GetRowCountCurrentPage() > 0, ErrorMessages.ResetNotWorking);
                    Assert.IsTrue(Page.IsNestedGridOpen(2), GetErrorMessage(ErrorMessages.NestedTableNotVisible));

                    List<string> nestedheaders = new List<String>()
                        {
                            TableHeaders.UserName,
                            TableHeaders.FirstName,
                            TableHeaders.LastName,
                            TableHeaders.SSOUser,
                            TableHeaders.Configuration,
                            TableHeaders.AttachmentType,
                            TableHeaders.ScheduleType,
                            TableHeaders.ScheduleDay,
                            TableHeaders.TransactionTypes,
                            TableHeaders.LocationAssigned,
                            TableHeaders.ScheduleCreated,
                            TableHeaders.Subscribed
                        };
                    errorMsgs.AddRange(Page.ValidateNestedTableHeaders(nestedheaders.ToArray()));
                    Assert.IsTrue(Page.IsNestedGridOpenByLevel(2, 2), GetErrorMessage(ErrorMessages.NestedTableNotVisible));

                    if (Page.IsAnyDataOnGrid())
                    {
                        List<string> internalnestedheaders = new List<String>()
                        {
                            TableHeaders.Name,
                            TableHeaders.HierarchyNotificationAccess,
                            TableHeaders.Address,
                            TableHeaders.City,
                            TableHeaders.State,
                            TableHeaders.EntityType,
                            TableHeaders.LocationType,
                            TableHeaders.AccountCode,
                            TableHeaders.EntityCode,
                            TableHeaders.ParentLocation,
                            TableHeaders.BillingLocation,
                            TableHeaders.NotificationLocationAccess
                        };
                        errorMsgs.AddRange(Page.ValidateNestedTableHeadersByLevel(2, internalnestedheaders.ToArray()));
                    }                   
                }
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
        }
    }
}

