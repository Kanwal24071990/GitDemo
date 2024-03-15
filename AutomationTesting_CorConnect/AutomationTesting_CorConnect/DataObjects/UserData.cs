using AutomationTesting_CorConnect.Constants;

namespace AutomationTesting_CorConnect.DataObjects
{
    internal class UserData
    {
        public string User { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }

        public UserData(string user, string passwd, UserType type)
        {
            User = user;
            Password = passwd;
            Type = type;
        }
    }
}
