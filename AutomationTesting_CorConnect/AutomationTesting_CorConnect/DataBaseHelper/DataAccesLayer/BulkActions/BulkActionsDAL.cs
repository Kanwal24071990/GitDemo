using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.BulkActions
{
    internal class BulkActionsDAL : BaseDataAccessLayer
    {
        internal int GetRowCountFromTable(string table)
        {
            string query = "select count(*) from " + table;

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            return -1;
        }

        internal List<string> GetReasons()
        {
            List<string> reasons = new List<string>();
            string query = "select description from lookup_tb where parentlookupcode = 46";
            using (var reader = ExecuteReader(query, false))
            {

                while (reader.Read())
                {
                    reasons.Add(reader.GetString(0));

                }

            }
            return reasons;
        }
    }
}
