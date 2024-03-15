using AutomationTesting_CorConnect.PageObjects.ManagePaymentAcceleration.AccelerationProgram;
using AutomationTesting_CorConnect.Resources;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.PageObjects.ManagePaymentAcceleration
{
    internal class ManagePaymentAccelerationPage : Commons
    {
        internal ManagePaymentAccelerationPage(IWebDriver webDriver) : base(webDriver, Pages.ManagePaymentAcceleration)
        {
        }

        internal AccelerationProgramPage OpenAccelerationProgram()
        {
            ButtonClick(ButtonsAndMessages.CreateNewAccelerationTerm);
            SwitchToPopUp();
            return new AccelerationProgramPage(driver);
        }

        internal void SearchByAccelerationTermName(string accelerationTermName)
        {
            EnterTextAfterClear(FieldNames.AccelerationTermName, accelerationTermName);
            ClickSearch();
            WaitForMsg(ButtonsAndMessages.PleaseWait);
        }

        internal void AcceptAlertMessage(out string msg)
        {
            try
            {
                AcceptAlert(out msg);
            }
            catch (UnhandledAlertException ex)
            {
                AcceptAlert(out msg);
            }
        }
    }
    
}

