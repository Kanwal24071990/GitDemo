using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.BulkActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.BulkActions
{
    internal class BulkActionsUtil
    {
        internal static int GetRowCountFromTable(string table)
        {
            return new BulkActionsDAL().GetRowCountFromTable(table);
        }

        internal static List<string> GetReasons()
        {
            return new BulkActionsDAL().GetReasons();
        }

    }
}
