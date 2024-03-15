using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.SummaryRemittanceReport
{
    internal class SummaryRemittanceReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                string userType;

                if (TestContext.CurrentContext.Test.Properties["Type"].Count() > 0)
                {
                    userType = TestContext.CurrentContext.Test.Properties["Type"]?.First().ToString().ToUpper();
                }
                else
                {
                    userType = ScenarioContext.Current["ActionResult"].ToString().ToUpper();
                }

                if (userType == "ADMIN")
                {
                    query = @"SELECT top 1 Convert (Date , RH.pymtDate) as FromDate ,Convert (Date , RH.pymtDate) as ToDate FROM [gpRemittance_tb] RH WITH (NOLOCK) INNER JOIN [gpRemittanceDetails_tb] RD WITH (NOLOCK) on RD.gpRemittanceID = RH.gpRemittanceID INNER JOIN [entityDetails_tb] E WITH (NOLOCK) on RH.vendorAcctCode =E.accountingCode INNER JOIN [invoice_tb] I WITH (NOLOCK) on I.lookUpId = RD.invLookupId AND I.isActive =1 INNER JOIN [entityDetails_tb] F WITH (NOLOCK) on I.receiverBillToEntityDetailId =F.entityDetailId AND F.isActive =1 INNER JOIN [entityDetails_tb] FS WITH (NOLOCK) on I.receiverEntityDetailId = FS.entityDetailId AND FS.isActive =1 INNER JOIN [Transaction_tb] T WITH (NOLOCK) ON T.transactionId=I.transactionId INNER JOIN [entityDetails_tb] dcc WITH (NOLOCK) on i.senderEntityDetailId = dcc.entityDetailId WHERE E.isActive =1 AND E.entityTypeId = 2 AND E.locationTypeId =25 AND RH.isActive =1 ORDER BY RH.pymtDate DESC;";

                    using (var reader = ExecuteReader(query, false))
                    {
                        if (reader.Read())
                        {
                            FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                            ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                FromDate = null;
                ToDate = null;
            }
        }
    }
}
