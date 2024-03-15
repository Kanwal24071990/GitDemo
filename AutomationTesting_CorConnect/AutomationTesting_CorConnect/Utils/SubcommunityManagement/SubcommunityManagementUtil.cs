using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.SubcommunityManagement;
using AutomationTesting_CorConnect.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.SubcommunityManagement
{
    internal class SubcommunityManagementUtil
    {
        private static readonly BaseDataAccessLayer baseDataAccessLayer = BaseDataAccessLayer.GetInstance();

        internal static SubcommunityDetails GetSubCommunityDetails(string subComName)
        {
            return new SubcommunityManagementDAL().GetSubCommunityDetails(subComName);
        }

        internal static void UpdateSftpCreds(string fleet)
        {
            baseDataAccessLayer.UpdateSftpCreds(fleet);
        }

        internal static SubcommunityDetails GetSftpDetails(string fleet)
        {
            return baseDataAccessLayer.GetSftpDetails(fleet);
        }
    }
}
