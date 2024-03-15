using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjectUtility
{
    public class Constants
    {
        
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
            new MenuException("premierinc", "Part Category Sales Summary - Bill To", "Item Type Sales Summary - Bill Location"),
            new MenuException("premierinc", "Part Category Sales Summary - Bill To", "Item Type Sales Summary - Bill To"),
            new MenuException("premierinc", "Items and Prices", "Items and Prices"),
            new MenuException("premierinc", "Parts", "Items")
        };
    }
}
