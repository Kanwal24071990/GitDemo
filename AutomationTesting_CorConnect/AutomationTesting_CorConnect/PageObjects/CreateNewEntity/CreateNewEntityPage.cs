using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.PageObjects.CreateNewEntity;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;

using NUnit.Framework;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.CreateNewEntity
{
    internal class CreateNewEntityPage : PopUpPage
    {
        internal CreateNewEntityPage(IWebDriver driver) : base(driver, Pages.CreateNewEntity)
        {
        }

        internal void CreateDealerEntity(string dealer)
        {
            EnterText(FieldNames.DisplayName, dealer);
            EnterText(FieldNames.LegalName, dealer);
            System.Threading.Tasks.Task t = System.Threading.Tasks.Task.Run(() => WaitForStalenessOfElement(FieldNames.DisplayName));
            SelectValueTableDropDown(FieldNames.EnrollmentType, RenameMenuField(EntityType.Dealer));
            t.Wait();
            t.Dispose();
            if (GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = System.Threading.Tasks.Task.Run(() => WaitForStalenessOfElement(FieldNames.DisplayName));
                SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }
            t = System.Threading.Tasks.Task.Run(() => WaitForStalenessOfElement(FieldNames.DisplayName));
            ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();

            EnterText(FieldNames.AccountCode, dealer);
            EnterText(FieldNames.AccountingCode, dealer);
            ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            EnterText(FieldNames.Address1, dealer);
            EnterText(FieldNames.City, dealer);
            if (GetValueOfDropDown(FieldNames.Country) != "US")
            {
                SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            EnterText(FieldNames.Zip, "55555");
            EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            ButtonClick(ButtonsAndMessages.Save);
            if (!WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{dealer}]");
            }
            EntityDetails entityDetails = CommonUtils.GetEntityDetails(dealer);
            Assert.IsFalse(entityDetails.CorcentricLocation);
            Assert.IsFalse(entityDetails.FinanceChargeExempt);
        }

        internal void CreateFleetEntity(string fleet)
        {
            System.Threading.Tasks.Task t = System.Threading.Tasks.Task.Run(() => WaitForStalenessOfElement(FieldNames.DisplayName));
            SelectValueTableDropDown(FieldNames.EnrollmentType, RenameMenuField(EntityType.Fleet));
            t.Wait();
            t.Dispose();
            t = System.Threading.Tasks.Task.Run(() => WaitForStalenessOfElement(FieldNames.DisplayName));
            ClickElement(FieldNames.MasterLocation);
            t.Wait();
            t.Dispose();
            if (GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
            {
                t = System.Threading.Tasks.Task.Run(() => WaitForStalenessOfElement(FieldNames.DisplayName));
                SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                t.Wait();
                t.Dispose();
            }

            EnterText(FieldNames.DisplayName, fleet);
            EnterText(FieldNames.LegalName, fleet);
            //CreateNewEntityPage.SelectValueListBoxByScroll(FieldNames.ProgramCode, "PCARD");
            EnterTextAfterClear(FieldNames.AccountCode, fleet);
            EnterText(FieldNames.AccountingCode, fleet);
            ClickElement(FieldNames.EligibleForTranSubmission);
            Assert.IsTrue(IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
            EnterText(FieldNames.Address1, fleet);
            EnterText(FieldNames.City, fleet);
            if (GetValueOfDropDown(FieldNames.Country) != "US")
            {
                SelectValueTableDropDown(FieldNames.Country, "US");
            }
            if (GetValueOfDropDown(FieldNames.State) != "Alabama")
            {
                SelectValueTableDropDown(FieldNames.State, "Alabama");
            }
            EnterText(FieldNames.Zip, "55555");
            EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
            EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            EnterTextAfterClear(FieldNames.CreditLimit, "55555555");
            ButtonClick(ButtonsAndMessages.Save);
            if (!WaitForPopupWindowToClose())
            {
                Assert.Fail($"Some error occurred while creating entity. CorcentricCode [{fleet}]");
            }
            EntityDetails entityDetails = CommonUtils.GetEntityDetails(fleet);
            Assert.IsFalse(entityDetails.CorcentricLocation);
            Assert.IsFalse(entityDetails.FinanceChargeExempt);
        }

        internal void InputMandatoryFieldsFleet(string entityLocation, string fleetName)
        {
            System.Threading.Tasks.Task t;
            if (entityLocation == "Buyer Billing")
            {
                if (GetValueOfDropDown(FieldNames.LocationType) != LocationType.Billing.ToString())
                {
                    t = System.Threading.Tasks.Task.Run(() => WaitForStalenessOfElement(FieldNames.DisplayName));
                    SelectValueTableDropDown(FieldNames.LocationType, LocationType.Billing.ToString());
                    t.Wait();
                    t.Dispose();
                }
                EnterText(FieldNames.DisplayName, fleetName);
                EnterText(FieldNames.LegalName, fleetName);
                if (IsElementVisible(FieldNames.ProgramCode))
                {
                    SelectValueListBoxByScroll(FieldNames.ProgramCode, "PCARD");
                    t = System.Threading.Tasks.Task.Run(() => WaitForStalenessOfElement(FieldNames.DisplayName));
                    ButtonClick(CommonUtils.HtmlEncode(ButtonsAndMessages.SymbolDoubleGreaterThan));
                    t.Wait();
                    t.Dispose();
                }
                EnterTextAfterClear(FieldNames.AccountCode, fleetName);
                EnterText(FieldNames.AccountingCode, fleetName);
                ClickElement(FieldNames.EligibleForTranSubmission);
                Assert.IsTrue(IsCheckBoxChecked(FieldNames.EligibleForTranSubmission));
                EnterText(FieldNames.Address1, fleetName);
                EnterText(FieldNames.City, fleetName);
                EnterTextAfterClear(FieldNames.CreditLimit, "55555555");
                if (GetValueOfDropDown(FieldNames.Country) != "US")
                {
                    SelectValueTableDropDown(FieldNames.Country, "US");
                }
                if (GetValueOfDropDown(FieldNames.State) != "Alabama")
                {
                    SelectValueTableDropDown(FieldNames.State, "Alabama");
                }
                EnterText(FieldNames.Zip, "55555");
                EnterText(FieldNames.PhoneNumber, "(999) 999-9999");
                EnterTextAfterClear(FieldNames.ProgramStartDate, CommonUtils.GetCurrentDate());
            }

        }
    }
}

