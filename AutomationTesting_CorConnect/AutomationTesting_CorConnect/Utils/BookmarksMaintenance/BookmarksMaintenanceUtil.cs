using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.BookmarksMaintenance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.BookmarksMaintenance
{
    internal class BookmarksMaintenanceUtil
    {

        internal static string GetBookMark()
        {
            return new BookmarksMaintenanceDAL().GetData();
        }
    }
}
