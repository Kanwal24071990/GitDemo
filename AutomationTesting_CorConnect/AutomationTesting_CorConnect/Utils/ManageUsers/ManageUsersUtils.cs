using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ManageUsers;
using AutomationTesting_CorConnect.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.ManageUsers
{
    internal class ManageUsersUtils
    {
        internal static string GetUserGroup(string entityType)
        {
            return new ManageUsersDAL().GetUserGroup(entityType);
        }
        internal static UserDetails GetUserDetails()
        {
            return new ManageUsersDAL().GetUserDetails();
        }
        internal static UserDetails GetUserDetails(string userName)
        {
            return new ManageUsersDAL().GetUserDetails(userName);
        }
        internal static UserDetails GetUserWebCoreLanguageID(string Val)
        {
            return new ManageUsersDAL().GetUserWebCoreLanguageID(Val);
        }
        internal static UserDetails GetUserLanguage(string Val)
        {
            return new ManageUsersDAL().GetUserLanguage(Val);
        }
        internal static UserDetails GetEmailSpecialCharacter(string specialCharacter)
        {
            return new ManageUsersDAL().GetEmailSpecialCharacter(specialCharacter);

        }
        internal static UserDetails GetUserDetailsUTF()
        {
            return new ManageUsersDAL().GetUserDetailsUTF();
        }

        internal static UserDetails GetUserDetailsWithMaxCharacter()
        {
            return new ManageUsersDAL().GetUserDetailsWithMaxCharacter();
        }

        internal static string GetSuperAdminUser()
        {
            return new ManageUsersDAL().GetSuperAdminUser();
        }

        internal static string GetSelectAllCount(string userID)
        {
            return new ManageUsersDAL().GetSelectAllCount(userID);
        }

        internal static string GetSelectAllCountforDR(string UserId)
        {
            return new ManageUsersDAL().GetSelectAllCountforDR(UserId);
        }

        internal static string GetUserIdforAdmin(string userName)
        {
            return new ManageUsersDAL().GetUserIdforAdmin(userName);
        }
    }
}
