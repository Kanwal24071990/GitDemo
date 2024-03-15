using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.SubCommunityManagement
{
    internal class AddRemittanceAddressPage : PopUpPage
    {
        internal AddRemittanceAddressPage(IWebDriver WebDriver) : base(WebDriver, "Add Remittance Address")
        {
        }

        internal void PopulateRemitFields(string remitToName)
        {
            SelectValueByScroll(FieldNames.Country, "US");
            SelectValueByScroll(FieldNames.Currency, "USD");
            EnterTextAfterClear(FieldNames.RemitToName,remitToName);
            EnterTextAfterClear(FieldNames.Address1, "Automation Headquaters USA");
            EnterTextAfterClear(FieldNames.City, "LU");
            EnterTextAfterClear(FieldNames.Zip, "47123");
            EnterTextAfterClear(FieldNames.Phone, "1593575286");
            EnterTextAfterClear(FieldNames.Email, "test123@gmail.com");
            SelectValueByScroll(FieldNames.State_Province, "Alabama");
            UploadFile(FieldNames.InvoiceLogo, "UploadFiles//SampleLogo.jpg");
        }

        }
}
