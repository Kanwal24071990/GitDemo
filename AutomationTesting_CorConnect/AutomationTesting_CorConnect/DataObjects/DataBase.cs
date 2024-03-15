using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutomationTesting_CorConnect.DataObjects
{
    public class ConnectionStrings
    {
        [JsonProperty("@name")]
        public string Name { get; set; }

        [JsonProperty("@connectionString")]
        public string ConnectionString { get; set; }
    }

    public class DataBase
    {
        private static DataBase instance;


        public static DataBase GetDBConfig(string Path)
        {
            if (instance == null)
            {
                using (StreamReader r = new StreamReader(Path))
                {
                    string jsonString = r.ReadToEnd();
                    instance= JsonConvert.DeserializeObject<DataBase>(jsonString);
                }
            }

           return instance;
        }

        public static ConnectionStrings GetDBClientConfig(string client)
        {
                if (instance != null)
                {
                    return instance.connectionStrings.FirstOrDefault(x => x.Name == client);

                }
            
            
            return null;
        }

        public List<ConnectionStrings> connectionStrings { get; set; }
    }
}
