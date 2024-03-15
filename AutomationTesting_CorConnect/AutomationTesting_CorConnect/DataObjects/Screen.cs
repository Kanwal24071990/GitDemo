using System.Collections.Generic;

namespace AutomationTesting_CorConnect.DataObjects
{
    public class Screen
    {
        public string ScreenName { get; set; }
        public List<Client> Clients { get;set; }
    }

    public class Client
    {
        public string ClientName { get; set; }
        public bool SkipAdminUser { get; set; } = true;
        public bool SkipFleetUser { get; set; } = true;
        public bool SkipDealerUser { get; set; } = true;
    }
}
