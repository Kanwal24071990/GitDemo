using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectUtility
{
    internal class Clients
    {
        public string Client { get; set; }
        public string DBName { get; set; }
        public string WebCoreDBName { get; set; }
        public string Type { get; set; }
        public bool IsEOP { get; set; }
    }
}
