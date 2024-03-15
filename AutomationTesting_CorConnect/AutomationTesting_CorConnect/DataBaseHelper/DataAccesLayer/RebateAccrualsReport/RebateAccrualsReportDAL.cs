using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.RebateAccrualsReport
{
    internal class RebateAccrualsReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string contractName)
        {
            contractName = string.Empty;
            string query = null;

            try
            {
                string userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                if (userType == "ADMIN")
                {
                    query = @"SELECT TOP 1 [ContractName] FROM (SELECT rc.contractName AS [ContractName],rc.statusId,rc.effectiveStartDate AS [EffectiveStartDate],rc.effectiveEndDate AS[EffectiveEndDate], 
                            rap.payerIdValue, rap.payerId, rap.receiverId, rap.receiverIdValue, rad.lineDetailId,rap.accrebateamt AS accrebateamt, rap.invEligibiltyCalcDate AS accrualDate, 
                            ra.rebateaccrualid, rad.rebateCalcTypeValue, rad.rebateCalcTypeId,rad.accTranAmount, ROW_NUMBER() OVER(PARTITION BY rap.invoiceid, ra.rebateContractId, payeridvalue, 
                            receiveridvalue, rad.lineDetailId ORDER BY ra.rebateaccrualid DESC) AS RowNum FROM [dbo].[rebateAccrualPayables_tb] RAP WITH(NOLOCK) INNER JOIN 
                            .[dbo].[rebateAccrual_tb] RA WITH(NOLOCK) ON RA.rebateAccrualId = RAP.rebateAccrualId INNER JOIN 
                            .[dbo].[rebateAccrualDetails_tb] RAD WITH(NOLOCK) ON RAP.rebateAccrualDetailId = RAD.rebateAccrualDetailId INNER JOIN 
                            .[dbo].[rebateContract_tb] RC WITH(NOLOCK) ON RC.rebateContractId = RA.rebateContractId INNER JOIN (SELECT distinct commonAbbr FROM 
                            .[dbo].[CurrencyCodes_tb] WITH(NOLOCK)) CC ON CC.commonAbbr = RAD.invCurrencyCode INNER JOIN (SELECT lookupid FROM 
                            .[dbo].[lookup_tb] WITH(NOLOCK) WHERE parentLookUpCode = 314 AND isActive = 1) SC ON SC.lookUpId = RC.statusId LEFT JOIN 
                            .[dbo].[rebateParentContract_tb] RPC WITH(NOLOCK) ON RPC.rebateParentContractId = RC.rebateParentContractId AND RPC.isActive = 1 WHERE RC.isActive = 1) 
                            RS WHERE RS.RowNum = 1 GROUP BY RS.ContractName ORDER BY RS.ContractName";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            contractName = reader.GetString(0);
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                contractName = null;
            }
        }
    }
}
