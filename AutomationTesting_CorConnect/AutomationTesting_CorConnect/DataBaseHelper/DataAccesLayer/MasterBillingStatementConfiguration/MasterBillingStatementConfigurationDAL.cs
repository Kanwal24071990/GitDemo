using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.MasterBillingStatementConfiguration
{
    internal class MasterBillingStatementConfigurationDAL : BaseDataAccessLayer
    {
        internal bool DeleteStatementConfiguration(string communityStatementName)
        {
            string query = @"select count(*) from statementConfig_tb where communityStatementName=@commStateName";
            int commStateNameCount = -1;

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@commStateName", communityStatementName),
            };

            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    commStateNameCount = reader.GetInt32(0);
                }
            }

            if (commStateNameCount == 1)
            {
                query = @"
                declare @communityStatementName nvarchar(100)

                set @communityStatementName=@commStateName --- enter the created invoice download template name

                delete from statementSenderRel_tb  from statementconfig_tb s inner join  statementSenderRel_tb sr on s.statementconfigid = sr.statementconfigid
                where communityStatementName=@communityStatementName

                delete from dbo.statementGrouping_tb  from statementconfig_tb s inner join  dbo.statementGrouping_tb sg on s.statementconfigid = sg.statementconfigid
                where communityStatementName=@communityStatementName

                delete from dbo.statementInvType_tb  from statementconfig_tb s inner join  dbo.statementInvType_tb st on s.statementconfigid = st.statementconfigid
                where communityStatementName=@communityStatementName

                delete from dbo.statementSorting_tb from statementConfig_tb s inner join dbo.statementSorting_tb ss on s.statementConfigId = ss.statementConfigId
                where communityStatementName=@communityStatementName

                delete from statementconfig_tb where communityStatementName=@communityStatementName;";

                sp = new()
                {
                    new SqlParameter("@commStateName", communityStatementName),
                };

                if (ExecuteNonQuery(query, sp, false) < 1)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
