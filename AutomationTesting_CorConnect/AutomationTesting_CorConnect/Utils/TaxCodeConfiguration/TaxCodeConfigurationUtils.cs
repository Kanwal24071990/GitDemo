using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.TaxCodeConfiguration;

namespace AutomationTesting_CorConnect.Utils.TaxCodeConfiguration
{
    internal class TaxCodeConfigurationUtils
    {
        internal static string GetLineType()
        {
            return new TaxCodeConfigurationDAL().GetLineType();
        }
    }
}
