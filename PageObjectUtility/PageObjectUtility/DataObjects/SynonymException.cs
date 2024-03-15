using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectUtility.DataObjects
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
