using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FranchiseCodeManagement
{
    internal class FranchiseCodeManagementDAL : BaseDataAccessLayer
    {
        internal List<string> GetFranchiseCodes()
        {
            List<string> franchiseCodes = new List<string>();
            string query = null;
            try
            {
                query = @"select  name from lookup_tb where parentlookupcode = 26";
                using (var reader = ExecuteReader(query, false))
                {
                    while (reader.Read())
                    {
                        franchiseCodes.Add(reader.GetString(0));

                    }
                }
                return franchiseCodes;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return franchiseCodes;
            }
        }

        internal List<string> GetNames()
        {
            List<string> names = new List<string>();
            string query = null;
            try
            {
                query = @"select lb.name FranchiseCode ,fs.name FranchiseCodeName  from lookup_tb lb inner join franchiseCodeSetup_tb fs on lb.lookupid=fs.lookupid 
                        UNION ALL
                        select name AS FranchiseCode , description AS  FranchiseCodeName from lookup_tb where  parentlookupcode=26 and lookupid not in (
                        select lookupid from franchisecodesetup_tb)";
                using (var reader = ExecuteReader(query, false))
                {
                    while (reader.Read())
                    {
                        names.Add(reader.GetString(1));

                    }
                }
                return names;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return names;
            }
        }

        internal List<string> GetVisibleCodes()
        {
            List<string> visibleCodes = new List<string>();
            string query = null;
            try
            {
                query = @"select lb.name FranchiseCode ,fs.visibleOnDealerLookUp from lookup_tb lb left join franchiseCodeSetup_tb fs on lb.lookupid=fs.lookupid where lb.parentlookupcode=26";
                using (var reader = ExecuteReader(query, false))
                {
                    while (reader.Read())
                    {
                        visibleCodes.Add(reader[1].ToString());

                    }
                }
                return visibleCodes;
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return visibleCodes;
            }
        }
    }
}
