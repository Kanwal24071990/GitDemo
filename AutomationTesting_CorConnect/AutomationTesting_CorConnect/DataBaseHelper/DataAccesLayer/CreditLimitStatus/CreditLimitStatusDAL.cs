using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreditLimitStatus
{
    internal class CreditLimitStatusDAL : BaseDataAccessLayer
    {
        internal string GetLastRunDateOfCreditLimitJob(int clientId)
        {
            string query = "SELECT LastRunDate FROM [CorConnectCommon].[dbo].[creditLastRun_tb] WHERE clientId = @clientId";

            List<SqlParameter> sp = new()
                    {
                        new SqlParameter("@clientId", clientId),
                    };
            using (var reader = ExecuteReader(query, sp, false))
            {
                if (reader.Read())
                {
                    return reader.GetDateTimeValue("LastRunDate", "yyyy-MM-dd HH:mm").Trim();
                }
            }
            return null;
        }
        internal CreditStagingHistoryDetails GetCreditLimitDataFromStagingHistory(string corcentriccode, int clientid)
        {
            CreditStagingHistoryDetails creditstaginghistory = null;
            try
            {
                string query = "select top 1 creditLimit, creditAvailable,totalAR from [CorConnectCommon].[dbo].[creditstaginghistory_tb] where buyercode='" + corcentriccode + "' and clientid = '" + clientid + "' order by id desc";

                using (var reader = ExecuteReader(query, false))

                {
                    if (reader.Read())
                    {
                        creditstaginghistory = new CreditStagingHistoryDetails();
                        creditstaginghistory.CreditLimit = Convert.ToInt32(reader.GetDouble(0));
                        creditstaginghistory.AvailableCreditLimit = Convert.ToInt32(reader.GetDecimal(1));
                        creditstaginghistory.TotalAR = Convert.ToInt32(reader.GetDouble(2));

                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return creditstaginghistory;
        }
        internal int GetCreditStagingHistoryCount(string corcentriccode, int clientid)
        {
            int rowCount = 0;

            string query = @"select count(*) from [CorConnectCommon].dbo.creditstaginghistory_tb where buyerCode= '" + corcentriccode + "' and clientid='" + clientid + "' ";
          
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        rowCount = reader.GetInt32(0);

                    }
                }
            return rowCount;
        }
    }
}