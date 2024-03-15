using AutomationTesting_CorConnect.DataObjects;
using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;


namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.DraftStatementReport
{
    internal class DraftStatementReportDAL : BaseDataAccessLayer
    {
        internal void GetData(out string FromDate, out string ToDate)
        {
            FromDate = string.Empty;
            ToDate = string.Empty;
            string query = null;

            try
            {
                query = @"SELECT   top 1 Convert (Date , DS.draftDate) as FromDate ,Convert (Date , DS.draftDate) as ToDate  FROM [GPDraftStatementDetail_tb] DD WITH (NOLOCK) INNER JOIN  [GPDraftStatement_tb] DS WITH (NOLOCK) ON DD.gpDraftStatementId =DS.GPDraftStatementId INNER JOIN  [entityDetails_tb] e WITH (NOLOCK) ON e.entitydetailid = DS.entityDetailId LEFT JOIN  [invoice_tb] I WITH (NOLOCK) ON DD.invlookupId =I.lookUpId WHERE DS.isActive = 1 AND(DD.isDisputed  in (0, 2, 4) OR(DD.isDisputed in (1, 3) AND DD.Amount > 0 AND DD.invLookupType = 'MI')) AND isnull(I.systemType, 0) <> 1 ORDER BY DS.draftDate DESC;";
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
                string spName = @"select Top 1 e.corcentricCode, G.draftDate from entitydetails_tb e
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

        internal LocationDetails GetLocationDetailsforSubcom()
        {
            LocationDetails locationDetails = null;
            try
            {
                string spName = @"select Top 1 e.corcentricCode,  G.draftDate from entitydetails_tb e
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
