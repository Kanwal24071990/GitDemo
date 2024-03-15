using System.Collections.Generic;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class LookupNames
    {
        internal Dictionary<string, Dictionary<string, string>> LookupProperty;

        private static LookupNames instance = null;

        private LookupNames()
        {
            LookupProperty = new Dictionary<string, Dictionary<string, string>>();
        }

        internal static LookupNames Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LookupNames();
                }

                return instance;
            }
        }
    }
}
