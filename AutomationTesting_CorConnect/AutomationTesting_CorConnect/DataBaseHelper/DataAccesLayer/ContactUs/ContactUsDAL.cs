using System.Collections.Generic;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.ContactUs
{
    internal class ContactUsDAL : BaseDataAccessLayer
    {

        internal List<string> GetPageDetails()
        {
            var list = new List<string>();
            string query = "select * from uiCustomizationPropertySettings_tb where pageid=2 and controlid in (4,6) and name='Text'";

            using (var reader = ExecuteReader(query, false))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(2));
                }
            }

            return list;

        }
    }
}
