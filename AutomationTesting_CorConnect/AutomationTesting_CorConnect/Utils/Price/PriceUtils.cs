using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.Price;

namespace AutomationTesting_CorConnect.Utils.Price
{
    internal class PriceUtils
    {
        internal static string GetPartNumber()
        {
            return new PriceDAL().GetPartNumber();
        }

        internal static bool DeletePartAndPrice(string partNumber)
        {
            return new PriceDAL().DeletePartAndPrice(partNumber);
        }
    }
}
