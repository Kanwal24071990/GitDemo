using AutomationTesting_CorConnect.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ManageDisputes
{
    internal class ManageDisputesDAL : BaseDataAccessLayer
    {
        internal void GetDisputeOwnerDetails(out string FirstName)
        {
            FirstName = string.Empty;
            string query = null;

            try
            {
                query = @"exec usp_GetUserDetailByFilter @filter1=N'',@filter2=N'webcore_corconnect',@filter3=N'',@filter=N'',@beginIndex=1,@endIndex=1,@isReqByFilter=1";
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        FirstName = reader.GetString(1);
                    }
                }

            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                FirstName = null;
                
            }
        }
    }
}
