using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ManageUsers
{
    internal class ManageUsersDAL : BaseDataAccessLayer
    {
        
        internal string GetUserGroup(string entityType)
        {
            string query = null;
            if (entityType.ToLower() == "dealer")
            {
                query = @"select top 1 USER_GROUP from USER_GROUP where IsDeleted = 0 and EntityType_ID = 1 Order by Creation_Date DESC";

                using (var reader = ExecuteReader(query, true))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }

            else if (entityType.ToLower() == "fleet")
            {
                query = @"select top 1 USER_GROUP from USER_GROUP where IsDeleted = 0 and EntityType_ID = 2 Order by Creation_Date DESC";

                using (var reader = ExecuteReader(query, true))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
            return string.Empty;
        }

        internal UserDetails GetUserDetails()
        {
            UserDetails userDetails = null;
            try
            {
                string spName = @"Select top 1 email,userName,firstName,lastName from user_tb order by 1 desc";


                using (var reader = ExecuteReader(spName, false))

                {
                    if (reader.Read())
                    {
                        userDetails = new UserDetails();
                        userDetails.Email = reader.GetStringValue("email");
                        userDetails.Username = reader.GetStringValue("userName");
                        userDetails.FirstName = reader.GetStringValue("firstName");
                        userDetails.LastName = reader.GetStringValue("lastName");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return userDetails;
        }


        internal UserDetails GetUserDetails(string userName)
        {
            UserDetails userDetails = null;
            try
            {
                string spName = @"Select username, firstname,lastname,email,phone,ext,cell,fax,securitycode,isActive,isNotificationUser,WebCore_UID from user_tb where username=@name";
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@name", userName),
                };

                using (var reader = ExecuteReader(spName,sp, false))

                {
                    if (reader.Read())
                    {
                        userDetails = new UserDetails();
                        userDetails.Email = reader.GetStringValue("email");
                        userDetails.Username = reader.GetStringValue("userName");
                        userDetails.FirstName = reader.GetStringValue("firstName");
                        userDetails.LastName = reader.GetStringValue("lastName");
                        userDetails.phone = reader.GetStringValue("phone");
                        userDetails.ext = reader.GetStringValue("ext");
                        userDetails.cell = reader.GetStringValue("cell");
                        userDetails.fax = reader.GetStringValue("fax");
                        userDetails.securitycode = reader.GetStringValue("securityCode");
                        userDetails.isActive = reader.GetBoolean(reader.GetReaderIndex("isActive"));
                        userDetails.isNotificationUser = reader.GetBoolean(reader.GetReaderIndex("isNotificationUser"));
                        userDetails.WebCoreUID= reader.GetInt32(reader.GetReaderIndex("WebCore_UID"));
                        
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return userDetails;
        }
        internal UserDetails GetUserWebCoreLanguageID(string Val)
        {
            UserDetails webcoreuserDetails = null;
            try
            {
                string spName = @"select Value from DisplayPref_User where DisplayPrefTypeId=8 and U_ID=@Val";
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@Val", Val),
                };

                using (var reader = ExecuteReader(spName, sp, true))

                {
                    if (reader.Read())
                    {
                        webcoreuserDetails = new UserDetails();
                        webcoreuserDetails.LanguageID = reader.GetStringValue("Value");

                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return webcoreuserDetails;
        }
        internal UserDetails GetUserLanguage(string Val)
        {
            UserDetails userlanguageDetails = null;
            try
            {
                string spName = @"select description from lookup_tb where lookupid=@Val";
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@Val", Val),
                };

                using (var reader = ExecuteReader(spName, sp, false))

                {
                    if (reader.Read())
                    {
                        userlanguageDetails = new UserDetails();
                        userlanguageDetails.UserLanguage = reader.GetStringValue("description");

                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return userlanguageDetails;
        }
        internal UserDetails GetEmailSpecialCharacter(string specialCharacter)
        {
            UserDetails userDetails = null;
            try
            {
                string spName = @"select top 1 email from user_tb where email like '%"+specialCharacter+"%'";


                using (var reader = ExecuteReader(spName, false))

                {
                    if (reader.Read())
                    {
                        userDetails = new UserDetails();
                        userDetails.Email = reader.GetStringValue("email");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return userDetails;
        }

        internal UserDetails GetUserDetailsUTF()
        {
            UserDetails userDetails = null;
            try
            {
                string spName = @"select top 1 email,userName,firstName,lastName from user_tb where userName Like '%[^'    + CHAR(9) + CHAR(10) + CHAR(13)+ CHAR(32) + '-' + CHAR(126)+ ']%' and firstName like '%[^'    + CHAR(9) + CHAR(10) + CHAR(13)+ CHAR(32) + '-' + CHAR(126)+ ']%' and lastName like '%[^'    + CHAR(9) + CHAR(10) + CHAR(13)+ CHAR(32) + '-' + CHAR(126)+ ']%' COLLATE Latin1_General_Bin";


                using (var reader = ExecuteReader(spName, false))

                {
                    if (reader.Read())
                    {
                        userDetails = new UserDetails();
                        userDetails.Email = reader.GetStringValue("email");
                        userDetails.Username = reader.GetStringValue("userName");
                        userDetails.FirstName = reader.GetStringValue("firstName");
                        userDetails.LastName = reader.GetStringValue("lastName");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return userDetails;
        }

        internal UserDetails GetUserDetailsWithMaxCharacter()
        {
            UserDetails userDetails = null;
            int maxEmail = 200;
            int maxName = 225;
            try
            {
                string spName = @"Select top 1 email,userName,firstName,lastName from user_tb group by email,userName,firstName,lastName having LEN(email)='"+maxEmail+"' and LEN(userName)='"+maxEmail+"' and LEN(firstName)= '"+maxName+ "' and LEN(lastName)='" + maxName + "'";


                using (var reader = ExecuteReader(spName, false))

                {
                    if (reader.Read())
                    {
                        userDetails = new UserDetails();
                        userDetails.Email = reader.IsDBNull(0) ? "" : reader.GetStringValue(0);
                        userDetails.Username = reader.IsDBNull(1) ? "" : reader.GetStringValue(1);
                        userDetails.FirstName = reader.IsDBNull(2) ? "" : reader.GetStringValue(2);
                        userDetails.LastName = reader.IsDBNull(3) ? "" : reader.GetStringValue(3);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return userDetails;
        }

        internal string GetSuperAdminUser()
        {
            string query = null;
            try
            {

                query = @"select top 1 username from USER_tb where Isactive = 1 and issuperadmin=1 Order by userid DESC";

                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;



            }
            return null;

        }

        internal string GetSelectAllCount(string userID)
        {
            string query1 = null;
            try
            {
                query1 = @"select count (entitydetailid) from emailnotificationuser_tb where reportid=3 and userId='"+ userID + "'";

                using (var reader = ExecuteReader(query1, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;



            }
            return null;

        }

        internal string GetUserIdforAdmin(string userName)
        {
            string query1 = null;
            try
            {
                query1 = @"select userId from USER_tb where userName = '" + userName + "'";

                using (var reader = ExecuteReader(query1, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;



            }
            return null;

        }

        internal string GetSelectAllCountforDR(string userid)
        {
            string query1 = null;
            try
            {
                query1 = @"select count (entitydetailid) from emailnotificationuser_tb where reportid=1 and userId='"+ userid + "'";

                using (var reader = ExecuteReader(query1, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0).ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;



            }
            return null;

        }
    }
}
