using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.FleetLookup
{
    internal class FleetLookupDAL : BaseDataAccessLayer
    {
        internal string GetCorCentricCode()
        {
            var query = @"select top 1 t.corcentricCode from entitydetails_tb AS t WITH (NOLOCK)
                        INNER JOIN entityaddressrel_tb entAdd WITH(NOLOCK) ON t.entitydetailid = entAdd.entitydetailid
                        INNER JOIN address_tb adr WITH(NOLOCK) ON adr.addressid = entAdd.addressid
                        where t.entitytypeid = 3 and t.enrollmentstatusid = 13 AND t.isActive = 1 and t.isterminated = 0";

            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                return null;
            }

            return string.Empty;
        }

        internal string GetSubCommunityAccountCodeforDealer()
        {
            var query = @"select Top 1 e.corcentricCode from entitydetails_tb e
                          inner Join subcommunity_tb s on e.subcommunityid=s.subcommunityid
                          inner Join subcommunityprogramcodes_tb spc on s.subcommunityid=spc.subcommunityid
                          inner join lookup_tb l on l.lookupid=spc.lookupid and l.isactive=1 and spc.isactive=1
                          where l.description IS NOT NULL and entityTypeId = 2";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetSubCommunityAccountCodeforFleet()
        {
            var query = @"select Top 1 e.corcentricCode from entitydetails_tb e
                          inner Join subcommunity_tb s on e.subcommunityid=s.subcommunityid
                          inner Join subcommunityprogramcodes_tb spc on s.subcommunityid=spc.subcommunityid
                          inner join lookup_tb l on l.lookupid=spc.lookupid and l.isactive=1 and spc.isactive=1
                          where l.description IS NOT NULL and entityTypeId = 3";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetEntityLevelAccountCodeforDealer()
        {
            var query = @"select  distinct Top 1 e.corcentricCode  from entitydetails_tb e
inner Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
inner join lookup_tb l on l.lookupid=elr.lookupid and l.parentlookupcode=23 and l.isactive=1 
where l.description IS NOT NULL";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetEntityLevelAccountCodeforFleet()
        {
            var query = @"select  distinct Top 1 e.corcentricCode  from entitydetails_tb e
inner Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
inner join lookup_tb l on l.lookupid=elr.lookupid and l.parentlookupcode=23 and l.isactive=1 
where l.description IS NOT NULL and entityTypeId = 3";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal List<string> GetEntityLevelPCValueforDealer()
        {
            var code = GetEntityLevelAccountCodeforDealer();
            List<string> ProgramCV = new List<string>();
            string query = @"select l.description from entitydetails_tb e
                          inner Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
                          inner join lookup_tb l on l.lookupid=elr.lookupid and l.parentlookupcode=23 and l.isactive=1 where e.corcentriccode='" + code + "'";
            using (var reader = ExecuteReader(query, false))
            {

                while (reader.Read())
                {
                    ProgramCV.Add(reader.GetString(0));

                }

            }
            return ProgramCV;

        }

        internal List<string> GetEntityLevelPCValueforFleet()
        {
            var code = GetEntityLevelAccountCodeforFleet();
            List<string> ProgramCV = new List<string>();
            string query = @"select l.description from entitydetails_tb e
                          inner Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
                          inner join lookup_tb l on l.lookupid=elr.lookupid and l.parentlookupcode=23 and l.isactive=1 where e.corcentriccode='" + code + "'";

            using (var reader = ExecuteReader(query, false))
            {
                while (reader.Read())
                {
                    ProgramCV.Add(reader.GetString(0));
                }
            }

            return ProgramCV;
        }

        internal string GetEntityLevelPCValueforFleetEnroll()
        {
            var code = GetEntityLevelAccountCodeforFleet();
            string query = @"select l.description from entitydetails_tb e
                          inner Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
                          inner join lookup_tb l on l.lookupid=elr.lookupid and l.parentlookupcode=23 and l.isactive=1 where e.corcentriccode='" + code + "'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetEnrollmentPCValue()
        {
            var query = @"select l.description from entitydetails_tb e
                          inner Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
                          inner join lookup_tb l on l.lookupid=elr.lookupid and l.parentlookupcode=23 and l.isactive=1 where e.corcentriccode='dealer164'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }

        internal string GetEnrollmentPCValueforDealer()
        {
            var query = @"select l.description from entitydetails_tb e
                          inner Join entityLookUpRelationship_tb elr on e.entitydetailid=elr.entitydetailid
                          inner join lookup_tb l on l.lookupid=elr.lookupid and l.parentlookupcode=23 and l.isactive=1 where e.corcentriccode='11383-5'";

            using (var reader = ExecuteReader(query, false))
            {
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }

            return null;
        }
    }
}
