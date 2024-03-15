using AutomationTesting_CorConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationTesting_CorConnect.Constants;
using System.Data.SqlClient;
using AutomationTesting_CorConnect.Utils;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreateAuthorization
{
    internal class CreateAuthorizationDAL : BaseDataAccessLayer
    {
        internal List<string> GetDisplayName(string entityType)
        {
            List<string> displayNames = new List<string>();
            try
            {
                string spName = "WC_Acc_GetEntity_ddl_dms";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@UserID", GetUserId()),
                    new SqlParameter("@PermLevel", 98),
                    new SqlParameter("@filter", string.Empty),
                    new SqlParameter("@beginIndex", 1),
                    new SqlParameter("@endIndex", 30),
                    new SqlParameter("@entityType", entityType),
                    new SqlParameter("@isReqByFilter", false),
                    new SqlParameter("@includeNonTransactionableEntity", false)
                };

                using (var oReader = ExecuteSP(spName, sp, false))
                {
                    while (oReader.Read())
                    {
                        displayNames.Add(oReader.GetStringValue("DisplayName"));
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                throw;
            }
            return displayNames;
        }
    }
}