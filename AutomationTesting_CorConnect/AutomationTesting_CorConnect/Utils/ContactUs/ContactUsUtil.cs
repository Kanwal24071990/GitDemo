using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ContactUs;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Utils.ContactUs
{
    internal static class ContactUsUtil
    {
        internal static List<string> GetPageDetails()
        {
            return new ContactUsDAL().GetPageDetails();
        }
    }
}
