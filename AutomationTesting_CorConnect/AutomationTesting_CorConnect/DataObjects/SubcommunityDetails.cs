using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class SubcommunityDetails
    {
        public string SubCommunityType { get; set; }
        public string SubCommunityName { get; set; }
        public string SubCommunityCode { get; set; }

        public string SFTPHost { get; set; }
        public string SFTPFolder { get; set; }

        public int SftpPort { get; set; }

        public string sftpUsername { get; set; }

        public string sftpPassword { get; set; }

        public string draftStatementSftpLocation { get; set; }

        public string dunningLetterSftpLocation { get; set; }
    }
}
