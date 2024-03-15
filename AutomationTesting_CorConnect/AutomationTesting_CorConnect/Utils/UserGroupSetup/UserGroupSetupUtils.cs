using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.User;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.UserGroupSetup;
using NUnit.Framework;
using System;
using System.Linq;

namespace AutomationTesting_CorConnect.Utils.UserGroupSetup
{
    internal class UserGroupSetupUtils
    {
        static int userID = -1;
        internal static void CheckFunctionAccessInUserGroup(string UserGroup, string FunctionName, Boolean isReadOnly = false)
        {
            Boolean val = new UserGroupSetupDAL().CheckFunctionAccessInUserGroup(UserGroup, FunctionName, isReadOnly);
        }

        internal static int GetUserIdByUserName(string username)
        {
            if (userID == -1)
            {
                userID = new UserGroupSetupDAL().GetWebCoreUserID(username);

            }
            return userID;
        }

    }
}
