using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreditLimitStatus;
using AutomationTesting_CorConnect.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.CreditLimitStatus
{
    internal class CreditLimitStatusUtil
    {

        internal static string GetLastRunDateOfCreditLimitJob(int clientid)
        {
            return new CreditLimitStatusDAL().GetLastRunDateOfCreditLimitJob(clientid);
        }

        internal static CreditStagingHistoryDetails GetCreditLimitDataFromStagingHistory(string corcentriccode, int clientid)
        {
            return new CreditLimitStatusDAL().GetCreditLimitDataFromStagingHistory(corcentriccode, clientid);
        }

        internal static int GetCreditStagingHistoryCount(string corcentriccode, int clientid)
        {
            return new CreditLimitStatusDAL().GetCreditStagingHistoryCount(corcentriccode , clientid);
        }


        





    }
}
