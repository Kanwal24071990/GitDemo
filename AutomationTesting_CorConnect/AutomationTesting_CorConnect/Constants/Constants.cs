using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Constants
{
    internal class TestCategory
    {
        internal const string Smoke = "SMOKE";
        internal const string FunctionalTest = "Functional";
        internal const string Premier = "Premier";
        internal const string EOPSmoke = "EOPSmoke";
    }

    internal class TestContextProperty
    {
        internal const string Type = "Type";
    }

    internal class DataFileConsts
    {
        internal const string client = "Client";
        internal const string userName = "UserName";
    }

    internal enum SortOrder
    {
        Ascending,
        Descending
    }

    internal class EntityType
    {
        internal const string Dealer = "Dealer";
        internal const string Fleet = "Fleet";
    }

    internal class FilterCriteria
    {
        internal const string BeginsWith = "Begins with";
        internal const string Contains = "Contains";
        internal const string DoesNotContain = "Doesn't contain";
        internal const string EndsWith = "Ends with";
        internal const string Equals = "Equals";
        internal const string DoesNotEqual = "Doesn't equal";
    }

    internal class LocationType
    {
        private LocationType(int value, string name, string displayName)
        {
            Value = value;
            Name = name;
            DisplayName = displayName;
        }
        public int Value { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public override string ToString()
        {
            return Name;
        }

        public static LocationType All = new LocationType(0, "All", "All");
        public static LocationType Master = new LocationType(24, "Master", "Master");
        public static LocationType MasterBilling = new LocationType(25, "MasterBilling", "Master Billing");
        public static LocationType Billing = new LocationType(25, "Billing", "Billing");
        public static LocationType Shop = new LocationType(27, "Shop", "Shop");
        public static LocationType ParentShop = new LocationType(26, "ParentShop", "Parent Shop");
    }

    internal class UserType
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string NameUpperCase { get; set; }

        private UserType(string name, int value)
        {
            Name = name;
            Value = value;
            NameUpperCase = name.ToUpper();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            return NameUpperCase == obj.ToString().ToUpper();
        }

        public static UserType GetObject(string type)
        {
            if (UserType.Fleet.Equals(type))
                return UserType.Fleet;
            if (UserType.Dealer.Equals(type))
                return UserType.Dealer;
            return UserType.Admin;

        }

        public static UserType Admin = new UserType("Admin", 0);
        public static UserType Dealer = new UserType("Dealer", 2);
        public static UserType Fleet = new UserType("Fleet", 3);
    }

    internal class MenuException
    {
        public string ClientNameLower { get; set; }
        public string OriginalMenuName { get; set; }
        public string ExceptionalMenuName { get; set; }

        public MenuException(string clientName, string originalMenuName, string exceptionalMenuName)
        {
            ClientNameLower = clientName;
            OriginalMenuName = originalMenuName;
            ExceptionalMenuName = exceptionalMenuName;
        }

        public static List<MenuException> MenuExceptions = new List<MenuException>
        {
            new MenuException("alliance", "Part Category Sales Summary - Location", "Part Type Sales Summary - Location"),
            new MenuException("alliance", "Part Category Sales Summary - Bill To", "Part Type Sales Summary - Bill To"),
            new MenuException("partsharecbp", "Part Category Sales Summary - Location", "Part Type Sales Summary - Location"),
            new MenuException("partsharecbp", "Part Category Sales Summary - Bill To", "Part Type Sales Summary - Bill To"),
            new MenuException("canxxus", "Part Category Sales Summary - Location", "Part Type Sales Summary - Location"),
            new MenuException("canxxus", "Part Category Sales Summary - Bill To", "Part Type Sales Summary - Bill To"),
            new MenuException("fleet2020", "Part Category Sales Summary - Location", "Part Type Sales Summary - Location"),
            new MenuException("fleet2020", "Part Category Sales Summary - Bill To", "Part Type Sales Summary - Bill To"),
            new MenuException("img", "Part Category Sales Summary - Location", "Part Type Sales Summary - Location"),
            new MenuException("img", "Part Category Sales Summary - Bill To", "Part Type Sales Summary - Bill To"),
            new MenuException("vipar", "Part Category Sales Summary - Location", "Part Type Sales Summary - Location"),
            new MenuException("vipar", "Part Category Sales Summary - Bill To", "Part Type Sales Summary - Bill To"),
            new MenuException("premierinc", "Part Category Sales Summary - Location", "Part Type Sales Summary - Location"),
            new MenuException("premierinc", "Part Category Sales Summary - Bill To", "Part Type Sales Summary - Bill To"),
            new MenuException("riteload", "Part Category Sales Summary - Location", "Part Type Sales Summary - Location"),
            new MenuException("riteload", "Part Category Sales Summary - Bill To", "Part Type Sales Summary - Bill To"),
            new MenuException("urg", "Part Category Sales Summary - Location", "Part Type Sales Summary - Location"),
            new MenuException("urg", "Part Category Sales Summary - Bill To", "Part Type Sales Summary - Bill To"),
            new MenuException("premierinc", "Parts", "Items")
        };
    }
}
