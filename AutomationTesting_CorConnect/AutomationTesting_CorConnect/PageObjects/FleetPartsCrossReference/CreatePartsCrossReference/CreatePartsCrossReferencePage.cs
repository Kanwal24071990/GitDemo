using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.FleetPartsCrossReference.CreatePartsCrossReference
{
    internal class CreatePartsCrossReferencePage : PopUpPage
    {
        internal CreatePartsCrossReferencePage(IWebDriver driver) : base(driver, "Create Parts Cross Reference")
        {
        }

        internal bool CreatePartsCrossReference(string fleetCode, string communityPartNumber, string fleetPartNum)
        {
            SearchAndSelectValue(FieldNames.FleetCode, fleetCode);
            EnterTextAfterClear(FieldNames.FleetPartNumber, fleetPartNum);
            EnterTextAfterClear("Long Description", fleetPartNum);
            SearchAndSelectValueWithoutLoadingMsg(FieldNames.CommunityPartNumber, communityPartNumber);
            Click(ButtonsAndMessages.Insert);
            var isRecordInserted = CheckForText("Record inserted successfully.", true);
            Click(ButtonsAndMessages.Cancel);
            SwitchToMainWindow();
            return isRecordInserted;
        }
    }
}
