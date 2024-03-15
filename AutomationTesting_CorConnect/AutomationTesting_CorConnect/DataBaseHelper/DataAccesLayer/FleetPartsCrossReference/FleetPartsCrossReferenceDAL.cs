using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetPartsCrossReference
{
    internal class FleetPartsCrossReferenceDAL : BaseDataAccessLayer
    {
        internal void GetData(out string fleetDisplayName)
        {
            fleetDisplayName = String.Empty;

            string query = @"SELECT top 1 t.displayName,t.corcentricCode AS AccountCode
                           , t.entityDetailId    
                           , [dbo].[Wc_udf_getLookupInfoFromLookupId]( t.entityTypeId,1) as EntityType    
                           , [dbo].[Wc_udf_getLookupInfoFromLookupId]( t.locationTypeId,1) as LocationType    
                             FROM entityDetails_tb  as t  WITH (NOLOCK)     
                             LEFT JOIN [dbo].[WC_udf_GetAvailableEntityByUserId] (0)   AS udf    
                            ON t.entityDetailId  = udf.entityDetailId    
                            INNER JOIN entityAddressRel_tb entAdd    
                            ON t.entityDetailId = entAdd.entityDetailId     
                            INNER JOIN address_tb adr on adr.addressId = entAdd.addressId
                            LEFT JOIN  receiverPartXref_tb rP on rP.receiverCorcentricCode=t.corcentricCode
                            where rp.receiverPartXrefId is null and t.entityTypeId = 3 and t.corcentricCode != '' and t.isActive = 1";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    fleetDisplayName = reader.GetString(1);
                }
            }
        }

        internal void GetCommunityPartNumber(string fleetCode, out string communityPartNumber)
        {
            communityPartNumber = String.Empty;

            string query = @"select top 1 REPLACE(LTRIM(RTRIM(partNumber)), '  ', ' ') from part_tb where isactive=1 and partNumber != '' and  partnumber NOT in (
            select communitypartnumber from receiverPartXref_tb where receiverCorcentricCode=N'" + fleetCode+"')";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    communityPartNumber = reader.GetString(0);
                }
            }
        }
    }
}
