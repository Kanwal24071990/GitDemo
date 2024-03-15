using AutomationTesting_CorConnect.Helper;
using System;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.TaxCodeConfiguration
{
    internal class TaxCodeConfigurationDAL: BaseDataAccessLayer
    {
        internal string GetLineType()
        {

            string query = @"SELECT top 1 l.name AS [Line Type] FROM TaxCodeConfiguration_tb t
                            WITH (NOLOCK)
                            INNER JOIN lookup_tb l WITH (NOLOCK) on t.lineType = l.lookUpId 
                            WHERE parentLookUpCode = 55 
                            ORDER BY name ASC, t.IsActive DESC";

            try
            {
                using (var reader = ExecuteReader(query, false))
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }

                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
            }

            return null;
        }
    }
}
