using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.User;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.User
{
    internal class UserDAL : BaseDataAccessLayer
    {
        internal void GetUserDetails(string userName, out string firstName, out string lastName, out string email, out string cell)
        {
            firstName = string.Empty;
            lastName = string.Empty;
            email = string.Empty; ;
            cell = string.Empty;

            string query = "Select firstname,lastname, email,cell from user_tb where username=@userName";

            List<SqlParameter> sp = new()
            {
                new SqlParameter("@userName", userName),
            };

            using (var reader = ExecuteReader(query, sp ,false))
            {
                if (reader.Read())
                {
                    firstName = reader.GetString(0);
                    lastName = reader.GetString(1);
                    email = reader.GetString(2);
                    cell = reader.GetString(3);
                }
            }

        }
    }
}
