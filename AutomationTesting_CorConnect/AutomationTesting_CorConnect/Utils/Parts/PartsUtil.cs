using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.Parts;
using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.Parts
{
    internal class PartsUtil
    {
        internal static string GetDecentralizedPartNumber()
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            return new PartsDAL().GetDecentralizedPartNumber();
        }

        internal static bool DeleteDecentralizedPart(string partNumber)
        {
            LoggingHelper.LogMessage(LoggerMesages.DeletingPart);
            return new PartsDAL().DeleteDecentralizedPart(partNumber);
        }

        internal static string GetPartNumber()
        {
            LoggingHelper.LogMessage(LoggerMesages.RetrivingDataFromDB);
            return new PartsDAL().GetPartNumber();
        }

        internal static Part GetPartDetails(string partNumber)
        {
            return new PartsDAL().GetPartDetails(partNumber);
        }
    }
}
