using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.User;
using NUnit.Framework;
using System.Linq;

namespace AutomationTesting_CorConnect.Utils.User
{
    internal class UserUtils
    {
        internal static void GetUserDetails(string userName, out string firstName, out string lastName, out string email, out string cell)
        {
            new UserDAL().GetUserDetails(userName, out firstName, out lastName, out email, out cell);
        }

        internal static string GetUserPassword()
        {
            return ApplicationContext.GetInstance().ClientConfigurations.First(x => x.Client.ToLower() == CommonUtils.GetClientLower())
                 .Users.Find(x => x.User.ToUpper() == GetUserName().ToUpper()).Password;

        }

        internal static string GetUserName()
        {
            return TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString();
        }

    }
}
