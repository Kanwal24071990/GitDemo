using AutomationTesting_CorConnect.Helper;
using System;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.EntityCrossReferenceMaintenance
{
    internal class EntityCrossReferenceMaintenanceDAL : BaseDataAccessLayer
    {

        internal void GetCorCentricCode(out string CorCentricCode, out string type)
        {
            CorCentricCode = string.Empty;
            type = string.Empty;
            var query = @"select top 1  CorcentricCode, l.name as [EntityType]
                           from EntityXRef_tb e WITH (NOLOCK)
                           inner join lookup_tb l WITH (NOLOCK) on l.lookUpId = e.EntityTypeId and l.parentLookUpCode = 1
                           inner join lookup_tb l2 WITH (NOLOCK) on l2.lookupId = e.XRefTypeId and l2.parentLookUpCode = 70  
                           order by e.IsActive desc,e.XrefCode";

            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        CorCentricCode= reader.GetString(0);
                        type= reader.GetString(1);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                CorCentricCode = null;
                type = null;
            }
        }
    }
}
