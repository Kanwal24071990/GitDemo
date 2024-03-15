using AutomationTesting_CorConnect.Utils;
using System;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DunningStatus
{
    internal class DunningStatusDAL: BaseDataAccessLayer
    {
        internal void GetData(out string corcentricCode, out string from, out string to)
        {
            corcentricCode=String.Empty;
            from= String.Empty;
            to = String.Empty;

            string query = @"select top 1 ent.Corcentriccode , isnull (dunning.letterDate,getdate()) as fromdate , isnull (dunning.letterDate,getdate()) as todate from entityDetails_tb ent
                            LEFT JOIN dunningLetterMaster_tb dunning WITH(NOLOCK) ON dunning.entityDetailId = ent.entityDetailId
                            LEFT JOIN relDunningLetterConfigs_tb rel WITH(NOLOCK) ON rel.relDunningLetterConfigid = dunning.relDunningLetterConfigid
                            where ent.isActive = 1 and ent.entityTypeId = 3 and ent.locationTypeId = 25 and ent.enrollmentStatusId = 13";

            using (var reader = ExecuteReader(query, false)) 
            {
                if(reader.Read())
                {
                    corcentricCode = reader.GetString(0);
                    from= CommonUtils.ConvertDate( reader.GetDateTime(1).Date);
                    to = CommonUtils.ConvertDate(reader.GetDateTime(2).Date);
                }
            }
        }
    }
}
