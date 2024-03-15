using AutomationTesting_CorConnect.applicationContext;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using NLog;
using Npgsql;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer
{
    internal class BaseDataAccessLayer
    {
        private static BaseDataAccessLayer baseDataAccessLayer;
        
        internal static Logger Logger { get; set; }

        protected BaseDataAccessLayer()
        {
            
        }

        internal static BaseDataAccessLayer GetInstance()
        {
            if (baseDataAccessLayer == null)
            {
                baseDataAccessLayer = new BaseDataAccessLayer();
            }

            LoggingHelper.LogMessage(LoggerMesages.ReturningLoggingContext);
            return baseDataAccessLayer;
        }

        internal Dictionary<string, string> GetCaptions()
        {
            string query = "select daimlercode, name from lookup_tb where parentlookupcode=1";
            var lookupNames = new Dictionary<string, string>();

            using (var oReader = ExecuteReader(query, false))
            {
                while (oReader.Read())
                {
                    lookupNames.Add(oReader.GetString(0), oReader.GetString(1));
                }
            }

            return lookupNames;
        }

        internal int GetDefaultFromDate()
        {
            string query = @"Declare @@value VARCHAR(30);
            select @@value = value from DisplayPref_User where u_id = @userId and DisplayPrefTypeId = 4;
            IF @@value is null
                select top 1 @@value = DefaultValue from DisplayPref_Type where DisplayPrefTypeId = 4;
            select @@value as value; ";

            int userid = GetUserId();

            if (userid == -1)
            {
                throw new Exception(ErrorMessages.InvalidUserID);
            }
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@userId", GetUserId()),
            };

            try
            {
                using (var oReader = ExecuteReader(query, sp, true))
                {
                    if (oReader.Read())
                    {
                        return int.Parse(oReader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }
            return 7;
        }

        internal Dictionary<string, Fields> GetFields(string pageCaption, out bool isDefaultFields)
        {
            if (pageCaption.EndsWith("Credit Limit"))
            {
                pageCaption = pageCaption.Replace("Credit Limit", "Credit");
            }
            Dictionary<string, Fields> fields = new();
            isDefaultFields = false;
            string queryString = "select Has_DynamicFilter,HideDateStaticSearch from SP_LOOKUP where Display_Description=@Fname";
            bool dynamicFiler = false;
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@Fname", pageCaption),
            };

            using (var oReader = ExecuteReader(queryString, sp, true))
            {
                if (oReader.Read())
                {
                    if (oReader.IsDBNull(0))
                    {
                        dynamicFiler = false;
                    }
                    else
                    {
                        dynamicFiler = oReader.GetBoolean(0);
                    }

                    if (!dynamicFiler)
                    {
                        isDefaultFields = true;
                        fields.Add("Company Name", new Fields() { ParamName = null, ParamType = "ASPxComboBox" });
                        bool hideDateStaticSearch = false;
                        if (oReader.IsDBNull(1))
                        {
                            hideDateStaticSearch = false;

                        }
                        else
                        {
                            hideDateStaticSearch = oReader.GetBoolean(1);

                            if (!hideDateStaticSearch)
                            {
                                fields.Add("From", new Fields() { ParamName = null, ParamType = "AspxDateEdit" });
                                fields.Add("To", new Fields() { ParamName = null, ParamType = "AspxDateEdit" });
                            }
                        }
                    }
                }
            }

            if (dynamicFiler)
            {
                queryString = "SELECT * FROM SP_SEARCH_PARAMETERS a INNER JOIN SP_LOOKUP b ON a.SP_ID = b.Sp_ID where a.Sp_ID = (select Top 1 Sp_ID from SP_LOOKUP  where Display_Description = @Fname and IsActive = 1 Order by Creation_Date desc) and isVisible = 1 order by Param_Order;";
                sp = new()
                {
                    new SqlParameter("@Fname", pageCaption),
                };
                List<Fields> rawFields = new List<Fields>();
                using (var oReader = ExecuteReader(queryString, sp, true))
                {
                    while (oReader.Read())
                    {
                        rawFields.Add(new Fields()
                        {
                            ParamCaption = Regex.Replace(oReader["Param_Caption"].ToString(), "<.*?>", String.Empty).Trim(),
                            ParamName = (string)oReader["Param_Name"],
                            ParamType = (string)oReader["Param_SpDisplayControl"],
                            QuickSearch = oReader.GetBooleanValue("QuickSearch"),
                            AdvancedSearch = oReader.GetBooleanValue("AdvancedSearch")
                        });
                    }
                }
                fields = CommonUtils.CheckForDuplicates(rawFields).ToDictionary(x => x.ParamCaption, y => y);
            }
            if (fields.Count == 0 && !dynamicFiler)
            {
                isDefaultFields = true;
                fields.Add("Company Name", new Fields() { ParamName = null, ParamType = "ASPxComboBox" });
                fields.Add("From", new Fields() { ParamName = null, ParamType = "AspxDateEdit" });
                fields.Add("To", new Fields() { ParamName = null, ParamType = "AspxDateEdit" });
            }
            return fields;
        }


        public List<string> GetHeaders(string pageCaption)
        {
            List<string> headers = new List<string>();

            string queryString = "select Field_Caption, Default_Field_Caption, Field_Name from SP_FIELD_SETTINGS s inner join sp_fields f on s.SpField_ID=f.SpField_ID where sp_id in (select sp_id from  sp_lookup where display_description =@Fname) and s.IsVisible=1 and u_id=0";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@Fname", pageCaption),
            };

            using (var oReader = ExecuteReader(queryString, sp, true))
            {
                while (oReader.Read())
                {
                    string headerName = oReader.GetStringValue(0);
                    if (string.IsNullOrEmpty(headerName))
                    {
                        headerName = oReader.GetStringValue(1);
                        if (string.IsNullOrEmpty(headerName))
                        {
                            headerName = oReader.GetStringValue(2);
                        }
                    }
                    headerName = headerName.Trim();
                    if (!string.IsNullOrEmpty(headerName))
                    {
                        headers.Add(headerName);
                    }
                }
            }

            return headers;
        }

        protected int GetUserId()
        {
            string queryString = "select WebCore_UID from user_tb where userName=@UserName";

            try
            {
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                };

                using (var oReader = ExecuteReader(queryString, sp, false))
                {
                    if (oReader.Read())
                    {
                        return oReader.GetInt32(0);
                    }
                }
            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return -1;
        }

        internal string GetCountry()
        {
            string query = "select top 1 description from lookup_tb where parentLookUpCode=12";
            try
            {
                using (var oReader = ExecuteReader(query, false))
                {
                    if (oReader.Read())
                    {
                        return oReader.GetString(0);
                    }
                }
            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return null;

        }

        private SqlConnection GetConnection(string client, bool isWebCoreDB)
        {
            try
            {
                SqlConnection conn;

                var db = ApplicationContext.GetInstance().ClientConfigurations.FirstOrDefault(x => x.Client.ToUpper() == client.ToUpper()).Database;

                if (db != null)
                {
                    var connectionString = DataBase.GetDBClientConfig(db.Type);

                    conn = new SqlConnection(connectionString.ConnectionString);
                    conn.Open();
                    conn.ChangeDatabase(isWebCoreDB ? db.WebCoreDBName : db.DBName);
                    return conn;

                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return null;
        }

        protected SqlDataReader ExecuteReader(string query, List<SqlParameter> sqlParameters, bool isWebCoreDB)
        {
            try
            {
                var connection = GetConnection(CommonUtils.GetClientLower(), isWebCoreDB);

                if (connection != null)
                {
                    SqlCommand oCmd = new(query, connection);
                    oCmd.Parameters.AddRange(sqlParameters.ToArray());
                    return oCmd.ExecuteReader();
                }

                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                LoggingHelper.LogException(e.Message);

                return null;

            }
        }

        protected SqlDataReader ExecuteQuery(string query, bool isWebCoreDB)
        {
            try
            {
                var connection = GetConnection(CommonUtils.GetClientLower(), isWebCoreDB);

                if (connection != null)
                {
                    SqlCommand oCmd = new(query, connection);
                    return oCmd.ExecuteReader();
                }

                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                LoggingHelper.LogException(e.Message);
                return null;
            }
        }

        protected SqlDataReader ExecuteReader(string query, bool isWebCoreDB)
        {
            try
            {
                var connection = GetConnection(CommonUtils.GetClientLower(), isWebCoreDB);

                if (connection != null)
                {
                    SqlCommand oCmd = new(query, connection);
                    LoggingHelper.LogMessage(LoggerMesages.ExecutingQuery);
                    return oCmd.ExecuteReader();
                }

                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                LoggingHelper.LogException(e.Message);
                return null;
            }
        }

        internal Dictionary<string, Dictionary<string, string>> GetEditsField(string page)
        {
            var pageFields = GetFields(page, out bool isDefaultFields);
            var dataTable = new DataTable();
            var fields = new Dictionary<string, Dictionary<string, string>>();

            string query = " select Param_Caption, Param_Name, Param_SpDisplayControl ,Edit_Type from SP_PARAMETERS p inner join SP_EDITS e on p.SpEditable_ID=e.SpEditable_ID inner join SP_EDIT_TYPE_LOOKUP l on e.SPEditType_ID=l.SPEditType_ID  where Sp_ID = (select Sp_ID from SP_LOOKUP where Display_Description = @page) and IsVisible=1";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@page", page),
            };

            using (var oReader = ExecuteReader(query, sp, true))
            {
                dataTable.Load(oReader);

                var duplicates = dataTable.AsEnumerable()
                    .GroupBy(r => r[0])
                    .Where(gr => gr.Count() > 1)
                    .Select(g => g.Key);


                foreach (string d in duplicates)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["Param_Caption"].ToString() == d)
                        {
                            dr["Param_Caption"] = CommonUtils.GetEditsButtonCaption(dr["Edit_Type"].ToString().Trim()) + "_" + dr["Param_Caption"];
                        }
                    }
                }
            }

            var editTypes = dataTable.DefaultView.ToTable(true, "Edit_Type").Rows;

            foreach (DataRow dr in editTypes)
            {
                DataRow[] filteredRows = dataTable.Select("Edit_Type LIKE '%" + dr["Edit_Type"].ToString().Trim() + "%'");
                var dictionary = new Dictionary<string, string>();

                foreach (var row in filteredRows)
                {
                    if (pageFields.ContainsKey(row[0].ToString()))
                    {
                        dictionary.Add(CommonUtils.GetEditsButtonCaption(dr["Edit_Type"].ToString().Trim()) + "_" + row[0].ToString().Replace("*", string.Empty), row[2].ToString());
                    }
                    else
                    {
                        dictionary.Add(row[0].ToString().Replace("*", string.Empty), row[2].ToString());
                    }
                }

                fields.Add(CommonUtils.GetEditsButtonCaption(dr["Edit_Type"].ToString().Trim()), dictionary);

            }

            return fields;
        }

        public int ExecuteNonQuery(string query, bool isWebCoreDB)
        {
            try
            {
                var connection = GetConnection(ApplicationContext.GetInstance().client, isWebCoreDB);

                if (connection != null)
                {
                    SqlCommand oCmd = new(query, connection);
                    oCmd.CommandTimeout = 60;
                    return oCmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                LoggingHelper.LogException(e.ToString());
                throw e;
            }
            return 0;
        }

        public int ExecuteNonQuery(string query, List<SqlParameter> sqlParameters, bool isWebCoreDB)
        {
            try
            {
                var connection = GetConnection(ApplicationContext.GetInstance().client, isWebCoreDB);

                if (connection != null)
                {
                    SqlCommand oCmd = new(query, connection);
                    oCmd.Parameters.AddRange(sqlParameters.ToArray());
                    return oCmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                LoggingHelper.LogException(e.Message);
            }

            return 0;
        }


        /// <summary>
        /// To get the entityId (Fleet/Dealer) of provided corcentricCode
        /// </summary>
        /// <param name="corcentricCode"></param>
        /// <returns></returns>
        protected int GetEntityId(string corcentricCode)
        {
            string queryString = "select entityDetailId from entitydetails_tb where corcentricCode = @corcentricCode";
            try
            {
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@corcentricCode", corcentricCode),
                };

                using (var oReader = ExecuteReader(queryString, sp, false))
                {
                    if (oReader.Read())
                    {
                        return oReader.GetInt32(0);
                    }
                }
            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return -1;
        }

        protected SqlDataReader ExecuteSP(string spName, List<SqlParameter> sqlParameters, bool isWebCoreDB)
        {
            try
            {
                var connection = GetConnection(ApplicationContext.GetInstance().client, isWebCoreDB);

                if (connection != null)
                {
                    SqlCommand oCmd = new(spName, connection);
                    oCmd.CommandType = CommandType.StoredProcedure;
                    oCmd.Parameters.AddRange(sqlParameters.ToArray());
                    return oCmd.ExecuteReader();
                }

                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                LoggingHelper.LogException(e.Message);

                return null;

            }
        }

        internal List<AuditTrailTable> GetAuditTrails(int rowCount = 10)
        {
            string query = "select top (@rowCount) * from auditTrail_tb order by auditid desc";

            List<AuditTrailTable> auditTrailList = null;
            List<SqlParameter> sp = new()
            {
                new SqlParameter("@rowCount", rowCount)
            };


            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.HasRows)
                {
                    auditTrailList = new List<AuditTrailTable>();
                    while (reader.Read())
                    {
                        auditTrailList.Add(new AuditTrailTable()
                        {
                            AuditId = reader.GetInt32("AuditID"),
                            UserId = reader.GetInt32("UserID"),
                            AuditAppName = reader.GetStringValue("AuditAppName"),
                            AuditTable = reader.GetStringValue("AuditTable"),
                            AuditAction = reader.GetStringValue("AuditAction"),
                            AuditStatement = reader.GetStringValue("AuditStatement"),
                            ActionDate = reader.GetDateTime("ActionDate"),
                            AuditOriginalValue = reader.GetStringValue("AuditOriginalValue"),
                            AuditNewValue = reader.GetStringValue("AuditNewValue")
                        });
                    }
                }
            }
            return auditTrailList;
        }

        internal string GetLocaleSettingsForLoggedInUser()
        {
            string queryString = @"DECLARE @@databaseName NVARCHAR(100) = (SELECT DataSourceName FROM WEBCORE_DATA_SOURCE WHERE ConStringName = 'developmentString');
                    DECLARE @@user NVARCHAR(100) = @UserName;
                    DECLARE @@sql NVARCHAR(4000) = 'select top 1 * from displaypref_user where U_ID = 
                    (select WebCore_UID from ' + @@databaseName + '.[dbo].[user_tb] where username = ''' + @@user + ''')
                    and displaypreftypeid = (select top 1 DisplayPrefTypeId from displaypref_type where PreferenceType = ''Locale settings'')';
                    EXEC sys.sp_executesql @@sql;";
            try
            {
                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@UserName", TestContext.CurrentContext.Test.Properties["UserName"]?.First().ToString()),
                };

                using (var oReader = ExecuteReader(queryString, sp, true))
                {
                    if (oReader.Read())
                    {
                        return oReader.GetStringValue("Value").Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }
            return null;
        }

        private NpgsqlConnection GetConnectionNpgsql()
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection("Server=c2pdbp02-usea1a.cgb9fbzdzual.us-east-1.rds.amazonaws.com;Port=5432;User Id=corconnectuat_svc;Password=2J$FdWYNx$s;Database=corconnectplatform_uat");
                return conn;

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return null;
        }

        internal SubcommunityDetails GetSftpDetails(string fleet)
        {
            SubcommunityDetails entityDetails = null;
            string query = "select lookup_values.sftpserver,lookup_values.sftplocation,lookup_values.SftpPort,lookup_values.SftpUserName,lookup_values.sftppassword,lookup_values.draftstatementsftplocation,lookup_values.dunninglettersftplocation from master.lookup_values where lookupvalue=@lookUpName";
            try
            {
                using (NpgsqlConnection conn = GetConnectionNpgsql())
                {
                    DataTable dt = new DataTable();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("lookUpName", fleet.ToUpper());
                    using (NpgsqlDataReader dataReader = cmd.ExecuteReader())

                    {
                        if (dataReader.Read())
                        {
                            entityDetails = new SubcommunityDetails();
                            entityDetails.SFTPHost = dataReader.GetStringValue("sftpserver");
                            entityDetails.SFTPFolder = dataReader.GetStringValue("sftplocation");
                            entityDetails.SftpPort = dataReader.IsDBNull(2) ? 0 : dataReader.GetInt32(2);
                            entityDetails.sftpUsername = dataReader.IsDBNull(3) ? "" : dataReader.GetStringValue("SftpUserName");
                            entityDetails.sftpPassword = dataReader.IsDBNull(3) ? "" : dataReader.GetStringValue("sftppassword");
                            entityDetails.draftStatementSftpLocation = dataReader.GetStringValue("draftstatementsftplocation");
                            entityDetails.dunningLetterSftpLocation = dataReader.GetStringValue("dunninglettersftplocation");

                        }
                    }

                    conn.Close();

                }
            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return entityDetails;

        }

        internal void UpdateSftpCreds(string fleet)
        {
            string query = "update master.lookup_values set SftpPort = 22,SftpUserName = 'tncgtest',SftpPassword = 'D-i?JRzBAyktN{+1At3C' where lookupvalue = @lookUpName";
            try
            {
                using (NpgsqlConnection conn = GetConnectionNpgsql())
                {
                    DataTable dt = new DataTable();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("lookUpName", fleet.ToUpper());
                    NpgsqlDataReader dataReader = cmd.ExecuteReader();
                    conn.Close();
                }
            }

            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

        }
    }
}