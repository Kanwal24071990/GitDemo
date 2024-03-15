using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.GPDraftStatements
{
    internal class GPDraftStatementsDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                query = @"SELECT TOP 1 CONVERT (Date , draftStatementCreateDate) AS FromDate, Convert (Date, draftStatementCreateDate) AS ToDate FROM GPDraftStatement_tb DS WITH(NOLOCK) INNER JOIN entitydetails_tb Fleet WITH(NOLOCK) ON Fleet.[entityDetailId] = DS.[entityDetailId] INNER JOIN GPDraftStatementDetail_tb DD WITH(NOLOCK) ON DS.[GPDraftStatementId] = DD.[gpDraftStatementId]WHERE DS.[isActive] = 1 ORDER BY draftStatementCreateDate DESC;";
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        FromDate = CommonUtils.ConvertDate(reader.GetDateTime(0));
                        ToDate = CommonUtils.ConvertDate(reader.GetDateTime(1));
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

        internal string GetSubCummunityLevelPCEntity()
        {
            var query = @"select Top 1 e.corcentricCode from entitydetails_tb e
inner Join subcommunity_tb s on e.subcommunityid=s.subcommunityid
inner Join subcommunityprogramcodes_tb spc on s.subcommunityid=spc.subcommunityid
inner join lookup_tb l on l.lookupid=spc.lookupid and l.isactive=1 and spc.isactive=1
inner join GPDraftStatement_tb G on G.entityDetailId = e.entityDetailId and G.isActive = 1
where l.description IS NOT NULL AND entityTypeId = 3 Order by G.GPDraftStatementId desc";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal LocationDetails GetLocationDetails()
        {
            LocationDetails locationDetails = null;
            try
            {
                string spName = @"select Top 1 e.corcentricCode, draftStatementCreateDate from entitydetails_tb e
inner Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
inner join lookup_tb l on l.lookupid=elr.lookupid and l.parentlookupcode=23 and l.isactive=1 
inner join GPDraftStatement_tb G on G.entityDetailId = e.entityDetailId and G.isActive = 1
where l.description IS NOT NULL Order by G.GPDraftStatementId desc";


                using (var reader = ExecuteReader(spName, false))

                {
                    if (reader.Read())
                    {
                        locationDetails = new LocationDetails();
                        locationDetails.locationCode = reader.GetStringValue("corcentricCode");
                        locationDetails.Date = CommonUtils.ConvertDate(reader.GetDateTime(1));

                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return locationDetails;
        }

        internal LocationDetails GetLocationDetailsforSubCom()
        {
            LocationDetails locationDetails = null;
            try
            {
                string spName = @"select Top 1 e.corcentricCode,  G.draftStatementCreateDate from entitydetails_tb e
inner Join subcommunity_tb s on e.subcommunityid=s.subcommunityid
inner Join subcommunityprogramcodes_tb spc on s.subcommunityid=spc.subcommunityid
inner join lookup_tb l on l.lookupid=spc.lookupid and l.isactive=1 and spc.isactive=1
inner join GPDraftStatement_tb G on G.entityDetailId = e.entityDetailId and G.isActive = 1
where l.description IS NOT NULL AND entityTypeId = 3 Order by G.GPDraftStatementId desc";


                using (var reader = ExecuteReader(spName, false))

                {
                    if (reader.Read())
                    {
                        locationDetails = new LocationDetails();
                        locationDetails.locationCode = reader.GetStringValue("corcentricCode");
                        locationDetails.Date = CommonUtils.ConvertDate(reader.GetDateTime(1));

                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex);
                throw;
            }
            return locationDetails;
        }
    }
}
