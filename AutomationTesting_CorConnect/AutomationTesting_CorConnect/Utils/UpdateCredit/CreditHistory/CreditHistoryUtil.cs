using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreditHistory;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Utils.UpdateCredit.CreditHistory
{
    internal class CreditHistoryUtil
    {
        CreditHistoryDAL creditHistoryDAL = new CreditHistoryDAL();

        internal string GetCorCentricCode()
        {
            return creditHistoryDAL.GetCorCentricCode();
        }

        internal List<DataObjects.CreditHistoryObjects> GetTableData(string corCentricCode, out decimal creditLimit, out string currency)
        {
            return creditHistoryDAL.GetTableData(corCentricCode, out creditLimit, out currency);
        }
    }
}
