using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.CreateAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.Utils.CreateAuthorization
{
    internal class CreateAuthorizationUtils
    {
        internal static List<string> GetDisplayNames(string entityType)
        {
            return new CreateAuthorizationDAL().GetDisplayName(entityType);
        }
    }
}
