using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.UserGroupSetup
{
    internal class UserGroupSetupDAL : BaseDataAccessLayer
    {
        internal int GetWebCoreUserID(string username)
        {
            string query = @"select U_ID from WEBCORE_UID where userID = '" + username + "';";

            using (var reader = ExecuteReader(query, true))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            return -1;
        }

        internal int GetFunctionIDByFunctionName(string functionName)
        {
            string query = @"select Sp_ID from SP_LOOKUP where display_description like '" + functionName + "';";

            using (var reader = ExecuteReader(query, true))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            return -1;
        }

        internal int GetUserGroupIDByName(string userGroupName)
        {
            string query = @"select UG_ID from user_group where user_group like '" + userGroupName + "';";

            using (var reader = ExecuteReader(query, true))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }
            LoggingHelper.LogMessage("User doesn't exist... ");  // no data exist

            return -1;
        }

        internal Boolean CheckFunctionAccessInUserGroup(string userGroupName, string functionName, bool isReadOnly)
        {
            int ug_id = GetUserGroupIDByName(userGroupName);

            int sp_id = GetFunctionIDByFunctionName(functionName);

            if (sp_id == -1 || ug_id == -1)
            {
                LoggingHelper.LogMessage("usergroup or function doesn't exist... ");  // no data exist
            }

            string query = @"select top 1 ReadOnly,IsDeleted from user_group_function_assigned where assign_Sp_ID=@sp_id and UG_ID=@ug_id order by UG_AssignID desc";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@sp_id", sp_id),
                new SqlParameter("@ug_id", ug_id),
            };


            using (var reader = ExecuteReader(query, sp, true))
            {
                if (reader.Read())
                {
                    bool isReadOnlyVal = reader.GetBoolean(0);
                    bool isDeleted = reader.GetBoolean(1);

                    if (isReadOnly != isReadOnlyVal || isDeleted)
                    {
                        return false;       // either the record is deleted or expected readonly value doesn't match
                    }
                    return true;
                }
            }
            return false;
        }
    }
}

