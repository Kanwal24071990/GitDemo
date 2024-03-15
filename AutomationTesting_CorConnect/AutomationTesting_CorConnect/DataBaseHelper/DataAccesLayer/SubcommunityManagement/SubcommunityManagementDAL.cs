using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.SubcommunityManagement
{
    internal class SubcommunityManagementDAL : BaseDataAccessLayer
    {
        internal SubcommunityDetails GetSubCommunityDetails(string subCommunityName)
        {
            SubcommunityDetails entityDetails = null;
            try
            {
                string spName = "select SftpServer, SftpLocation from subcommunity_tb where subCommunityName=@subComCode";

                List<SqlParameter> sp = new()
                {
                    new SqlParameter("@subComCode", subCommunityName),
                };

                using (var reader = ExecuteReader(spName, sp, false))

                {
                    if (reader.Read())
                    {
                        entityDetails = new SubcommunityDetails();
                        entityDetails.SFTPHost = reader.GetStringValue("SftpServer");
                        entityDetails.SFTPFolder = reader.GetStringValue("SftpLocation");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return entityDetails;
        }
    }
}
