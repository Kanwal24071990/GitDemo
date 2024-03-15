using System.Collections.Generic;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class SynonymException
    {
        public string ClientNameLower { get; set; }
        public List<RegexReplacement> RegexReplacements { get; set; }
    }
    internal class RegexReplacement
    {
        public string Pattern { get; set; }
        public string Replacement { get; set; }
    }
}
