using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ManagePaymentAcceleration.AccelerationProgram
{
    internal class AccelerationProgramPage : PopUpPage
    {
        internal AccelerationProgramPage(IWebDriver driver) : base(driver, Pages.AccelerationProgram)
        {
        }

        internal bool CreateNewAccelerationProgram(string accelerationProgramName)
        {
            EnterText(FieldNames.AccelerationProgramName, accelerationProgramName);
            EnterText(FieldNames.DueDateadjustment_days_, "10");
            EnterText(FieldNames.AccelerationFeeValue, "10");
            SearchAndSelectValueWithoutLoadingMsg(FieldNames.AccelerationFeeValueType, "Amount");
            Click(FieldNames.AROnly_);
            Click(FieldNames.APOnly_);

            Click(ButtonsAndMessages.Save);

            var isMsgDisplayed=CheckForText("Record saved successfully.", true);
            Click(ButtonsAndMessages.Cancel);
            SwitchToMainWindow();

            return isMsgDisplayed;
        }
    }
}
