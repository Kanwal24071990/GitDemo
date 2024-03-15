using AutomationTesting_CorConnect.Helper;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.AssignedEntityFunctions
{
    internal class AssignedEntityFunctionsDAL : BaseDataAccessLayer
    {
        internal bool GetCheckboxData(bool isChecked = false)
        {
            string query;
            LoggingHelper.LogMessage("Ischecked: " + isChecked);
            if (isChecked)
            {
                LoggingHelper.LogMessage("Check data row exist in unchecked data.");
                query = @"select top 1 * from ENTITYTYPE_FUNCTION_ASSIGNED e inner join SP_LOOKUP s on e.Assign_SP_ID =s.SPName_ID where s.Display_Description='Contact Us' and e.EntityType_ID=3 and e.IsDeleted=1 and Delete_Date is not null";
            }
            else
            {
                LoggingHelper.LogMessage("Check data row exist in checked data.");
                query = @"select top 1 * from ENTITYTYPE_FUNCTION_ASSIGNED e inner join SP_LOOKUP s on e.Assign_SP_ID =s.SPName_ID where s.Display_Description='Contact Us' and e.EntityType_ID=3 and e.IsDeleted=0 and Delete_Date is null";
            }

            using (var reader = ExecuteReader(query, true))
            {
                while (reader.Read())
                {
                    LoggingHelper.LogMessage("Row found in Database.");
                    return true;
                }
            }
            return false;

        }
    }
}
