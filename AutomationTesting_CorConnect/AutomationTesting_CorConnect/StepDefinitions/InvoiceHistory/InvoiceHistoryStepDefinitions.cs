using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.DMS;
using AutomationTesting_CorConnect.DriverBuilder;
using AutomationTesting_CorConnect.PageObjects;
using AutomationTesting_CorConnect.PageObjects.DealerInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.FleetInvoiceTransactionLookup;
using AutomationTesting_CorConnect.PageObjects.InvoiceEntry.CreateNewInvoice;
using AutomationTesting_CorConnect.PageObjects.InvoiceOptions;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.InvoiceHistory;
using AutomationTesting_CorConnect.Utils.User;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.StepDefinitions.InvoiceHistory
{
    [Binding]
    internal class InvoiceHistoryStepDefinitions : DriverBuilderClass
    {
        DealerInvoiceTransactionLookupPage DITLpage;
        InvoiceOptionsPage invoiceOptionsPage;
        CreateNewInvoicePage createNewInvoicePage;
        string invoiceNumber = "InvHistory" + CommonUtils.RandomString(6);
        private By invoiceHistoryDataRowPath = By.XPath("//tr[contains(@id, 'grdInvoiceHistory_DXDataRow')]");
        public static string currentUser;
        public static string userName;

        [Given(@"Invoice exists between buyer ""([^""]*)"" and supplier ""([^""]*)""")]
        public void GivenInvoiceExistsBetweenBuyerAndSupplier(string invoiceHistoryByr, string invoiceHistorySup)
        {
            Assert.IsTrue(new DMSServices().SubmitInvoice(invoiceNumber, invoiceHistorySup, invoiceHistoryByr), string.Format(ErrorMessages.FailedToSubmitInvoice, invoiceNumber));
            Console.WriteLine("Invoice number: " + invoiceNumber);
            OpenInvoice(invoiceNumber);
        }

        [Given(@"Invoice created with Bulk Reversal exists in the system")]
        public void GivenInvoiceCreatedWithBulkReversalExistsInTheSystem()
        {
            InvoiceHistoryUtil.GetBulkReversalInvoice(out invoiceNumber, out userName);
            Assert.IsFalse(string.IsNullOrEmpty(invoiceNumber), ErrorMessages.InvoiceDoesNotExist);
            Assert.IsFalse(string.IsNullOrEmpty(userName), ErrorMessages.UserNameMisMatch);
        }

        [When(@"The invoice is reversed")]
        public void WhenTheInvoiceIsReversed()
        {
            List<string> errorMsgs = new List<string>();
            errorMsgs.AddRange(DITLpage.ReverseInvoice(invoiceNumber));
            Assert.Multiple(() =>
            {
                foreach (var errorMsg in errorMsgs)
                {
                    Assert.Fail(GetErrorMessage(errorMsg));
                }
            });
            OffsetTransactionPage offsetTransactionPopUpPage = new OffsetTransactionPage(driver);
            offsetTransactionPopUpPage.ClosePopupWindow();
            invoiceOptionsPage.ClosePopupWindow();
        }

        [When(@"The invoice is rebilled")]
        public void WhenTheInvoiceIsRebilled()
        {
            OffsetTransactionPage OffsetTransactionPopUpPage = invoiceOptionsPage.CreateRebill();
            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.RebillTheInvoice);
            Assert.IsTrue(OffsetTransactionPopUpPage.IsRadioButtonChecked(ButtonsAndMessages.RebillTheInvoice));
            OffsetTransactionPopUpPage.WaitForElementToBePresent(FieldNames.CommentsRebill);
            OffsetTransactionPopUpPage.EnterText(FieldNames.CommentsRebill, "Comments");
            OffsetTransactionPopUpPage.Click(ButtonsAndMessages.Rebill);
            OffsetTransactionPopUpPage.SwitchToMainWindow();
            Thread.Sleep(TimeSpan.FromSeconds(8)); // Adding this wait because no explicit wait worked. It has be Thread.Sleep according to the behavior of the scenario.
            DITLpage.SwitchToPopUp();
            createNewInvoicePage = new CreateNewInvoicePage(driver);
            createNewInvoicePage.Click(ButtonsAndMessages.SubmitInvoice);
            createNewInvoicePage.AlertClick(ButtonsAndMessages.Continue);
            createNewInvoicePage.AcceptWindowAlert(out string alertMessage);
            Assert.AreEqual(alertMessage, ButtonsAndMessages.InvoiceSubmissionCompletedSuccessfully, ErrorMessages.AlertMessageMisMatch);
        }

        [When(@"The invoice is opened")]
        public void WhenTheInvoiceIsOpened()
        {
            OpenInvoice(invoiceNumber, false, true);       
        }

        [Then(@"""([^""]*)"" operation is captured in Invoice History grid")]
        public void ThenOperationIsCapturedInInvoiceHistoryGrid(string operationType)
        {
            if (applicationContext.ImpersonatedUserData != null)
            {
                applicationContext.ImpersonatedUserData = null;
            }
            switch (operationType)
            {
                case "Reverse":
                    {
                        invoiceNumber = invoiceNumber + "R";
                        break;
                    }
                case "Rebill":
                    {
                        invoiceNumber = invoiceNumber + "C";
                        break;
                    }
                default:
                    break;
            }
            if (!operationType.Contains("Bulk"))
            {
                DITLpage.SwitchToMainWindow();
                OpenInvoice(invoiceNumber);
            }
            ValidateInvoiceHistory(operationType);
            invoiceOptionsPage.ClosePopupWindow();
            invoiceOptionsPage.SwitchToPopUp();
            invoiceOptionsPage.SwitchIframe();
            invoiceOptionsPage.ButtonClick(ButtonsAndMessages.ViewFleetInvoice);
            ValidateInvoiceHistory(operationType, true);
            if (!operationType.Contains("Bulk"))
            {
                var client = applicationContext.ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower());
                var dealerUser = client.Users.First(x => x.Type == Constants.UserType.Dealer.ToString().ToLower());
                if (menu == null)
                {
                    menu = new Menu(driver);
                }
                menu.ImpersonateUser(dealerUser.User, Constants.UserType.Dealer);
                menu.OpenPage(Pages.DealerInvoiceTransactionLookup);
                OpenInvoice(invoiceNumber);
                ValidateInvoiceHistory(operationType, true);
                var fleetUser = client.Users.First(x => x.Type == Constants.UserType.Fleet.ToString().ToLower());
                menu.ImpersonateUser(fleetUser.User, Constants.UserType.Fleet);
                currentUser = "Fleet";
                menu.OpenPage(Pages.FleetInvoiceTransactionLookup);
                OpenInvoice(invoiceNumber, true);
                ValidateInvoiceHistory(operationType);
            }
        }

        private void ValidateInvoiceHistory(string operationType, bool closePopups = false)
        {
            invoiceOptionsPage.SwitchIframe();
            invoiceOptionsPage.Click(ButtonsAndMessages.InvoiceHistory);
            invoiceOptionsPage.SwitchToPopUp();
            if (string.IsNullOrEmpty(userName) && !operationType.Contains("Bulk"))
            {
                userName = ScenarioContext.Current["UserName"].ToString();
                Assert.IsFalse(string.IsNullOrEmpty(userName), ErrorMessages.UserNameMisMatch);
            }
            ReadOnlyCollection<IWebElement> dataRows = driver.FindElements(invoiceHistoryDataRowPath);
            ReadOnlyCollection<IWebElement> row1 = dataRows[0].FindElements(By.TagName("td"));
            string invoiceStateRow1 = row1[1].Text;
            string descriptionRow1 = row1[2].Text;
            Assert.IsFalse(string.IsNullOrEmpty(invoiceStateRow1), ErrorMessages.ValueNotFoundInInvoiceHistory);
            Assert.IsFalse(string.IsNullOrEmpty(descriptionRow1), ErrorMessages.ValueNotFoundInInvoiceHistory);
            Assert.IsTrue(invoiceStateRow1.Contains("Settled"), ErrorMessages.ValueNotFoundInInvoiceHistory);
            UserUtils.GetUserDetails(userName, out string firstName, out string lastName, out string email, out string cell);
            Assert.IsFalse(string.IsNullOrEmpty(firstName), ErrorMessages.NoRecordInDB);
            Assert.IsFalse(string.IsNullOrEmpty(lastName), ErrorMessages.NoRecordInDB);
            Assert.IsTrue(descriptionRow1.Contains(firstName), string.Format(ErrorMessages.ValueNotFoundInInvoiceHistory + " {0}", firstName));
            Assert.IsTrue(descriptionRow1.Contains(lastName), string.Format(ErrorMessages.ValueNotFoundInInvoiceHistory + " {0}", lastName));

            if (operationType.Contains("Reversal"))
            {
                ReadOnlyCollection<IWebElement> row2 = dataRows[1].FindElements(By.TagName("td"));
                string invoiceStateRow2 = row2[1].Text;
                string descriptionRow2 = row2[2].Text;
                Assert.IsFalse(string.IsNullOrEmpty(invoiceStateRow2), ErrorMessages.ValueNotFoundInInvoiceHistory);
                Assert.IsFalse(string.IsNullOrEmpty(descriptionRow2), ErrorMessages.ValueNotFoundInInvoiceHistory);
                Assert.IsTrue(invoiceStateRow2.Contains("Submitted"), ErrorMessages.ValueNotFoundInInvoiceHistory);
                Assert.IsTrue(descriptionRow2.Contains(firstName), string.Format(ErrorMessages.ValueNotFoundInInvoiceHistory + " {0}", firstName));
                Assert.IsTrue(descriptionRow2.Contains(lastName), string.Format(ErrorMessages.ValueNotFoundInInvoiceHistory + " {0}", lastName));
                if (operationType.Contains("Bulk"))
                {
                    Assert.IsTrue(descriptionRow2.Contains("Bulk Reversal"), string.Format(ErrorMessages.ValueNotFoundInInvoiceHistory + " {0}", "Bulk Reversal"));
                }
                else
                {
                    Assert.IsTrue(descriptionRow2.Contains("Reversal"), string.Format(ErrorMessages.ValueNotFoundInInvoiceHistory + " {0}", "Reversal"));
                }
                
            }
            else if (operationType.Contains("Rebill"))
            {
                if (currentUser != "Fleet")
                {
                    ReadOnlyCollection<IWebElement> row3 = dataRows[2].FindElements(By.TagName("td"));
                    string invoiceStateRow3 = row3[1].Text;
                    string descriptionRow3 = row3[2].Text;
                    Assert.IsFalse(string.IsNullOrEmpty(invoiceStateRow3), ErrorMessages.ValueNotFoundInInvoiceHistory);
                    Assert.IsFalse(string.IsNullOrEmpty(descriptionRow3), ErrorMessages.ValueNotFoundInInvoiceHistory);
                    Assert.IsTrue(invoiceStateRow3.Contains("Saved"), ErrorMessages.ValueNotFoundInInvoiceHistory);
                    Assert.IsTrue(descriptionRow3.Contains(firstName), string.Format(ErrorMessages.ValueNotFoundInInvoiceHistory + " {0}", firstName));
                    Assert.IsTrue(descriptionRow3.Contains(lastName), string.Format(ErrorMessages.ValueNotFoundInInvoiceHistory + " {0}", lastName));
                    Assert.IsTrue(descriptionRow3.Contains("Rebill"), string.Format(ErrorMessages.ValueNotFoundInInvoiceHistory + " {0}", "Rebill"));
                }
            }
            if (closePopups)
            {
                invoiceOptionsPage.ClosePopupWindow();
                DITLpage.SwitchToPopUp();
                invoiceOptionsPage.ClosePopupWindow();
            }
        }

        private void OpenInvoice(string invoiceNumber, bool isFleet = false, bool isProgramInvoiceNumber = false)
        {
            if (isFleet)
            {
                FleetInvoiceTransactionLookupPage FITLpage = new FleetInvoiceTransactionLookupPage(driver);
                if (isProgramInvoiceNumber)
                {
                    FITLpage.EnterTextAfterClear(FieldNames.ProgramInvoiceNumber, invoiceNumber);
                }
                else
                {
                    FITLpage.EnterTextAfterClear(FieldNames.DealerInvoiceNumber, invoiceNumber);
                }
                FITLpage.GridLoad();
                if (FITLpage.IsAnyDataOnGrid())
                {
                    FITLpage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                }
                else
                {
                    Assert.Fail(string.Format(ErrorMessages.NoResultForInvoice, invoiceNumber));
                }
            }
            else
            {
                DITLpage = new DealerInvoiceTransactionLookupPage(driver);
                if (isProgramInvoiceNumber)
                {
                    DITLpage.EnterTextAfterClear(FieldNames.ProgramInvoiceNumber, invoiceNumber);
                }
                else
                {
                    DITLpage.EnterTextAfterClear(FieldNames.DealerInvoiceNumber, invoiceNumber);
                }
                DITLpage.GridLoad();
                if (DITLpage.IsAnyDataOnGrid())
                {
                    DITLpage.ClickHyperLinkOnGrid(TableHeaders.DealerInv__spc);
                }
                else
                {
                    Assert.Fail(string.Format(ErrorMessages.NoResultForInvoice, invoiceNumber));
                }
            }
            invoiceOptionsPage = new InvoiceOptionsPage(driver);
            invoiceOptionsPage.SwitchToPopUp();
        }
    }
}
