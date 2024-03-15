using Newtonsoft.Json;
using OpenQA.Selenium;

namespace AutomationTesting_CorConnect.DataObjects
{
    public class Xpath
    {
        public string Mechanism { get; set; }
        public string Criteria { get; set; }
    }

    public class PageObject
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Xpath xpath { get; set; }

        [JsonIgnore]
        public By by => new CreateBy(xpath);
    }

    public class CreateBy : By
    {
        public CreateBy(Xpath xpath) : base(xpath.Mechanism, xpath.Criteria)
        {
        }
    }

    internal class MenuObject
    {
        public string MenuName { get; set; }

        public string LongDescription { get; set; }

        public string Caption { get; set; }
    }
}
