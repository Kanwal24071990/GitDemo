using System.Collections.Generic;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class ClientConfiguration
    {
        public string Client { get; set; }
        public string URL { get; set; }
        public string DMS { get; set; }
        public Database Database { get; set; }
        public List<Users> Users { get; set; }
        public string CommunityCode { get; set; } 
    }

    internal class Database
    {
        public string DBName { get; set; }
        public string WebCoreDBName { get; set; }
        public string EOPDBName { get; set; }
        public string Type { get; set; }
    }

    internal class Users
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
    }

}
