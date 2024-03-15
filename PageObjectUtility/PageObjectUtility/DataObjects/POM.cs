using OpenQA.Selenium;

namespace PageObjectUtility.DataObjects
{
    internal class POM
    {
        internal string ID { get; set; }

        internal string Name { get; set; }

        internal By xpath { get; set; }
    }

    internal class CreateBy : By
    {
        internal CreateBy(Xpath xpath) : base(xpath.Mechanism, xpath.Criteria)
        {
        }
    }

    public class Xpath
    {
        internal string Mechanism { get; set; }
        internal string Criteria { get; set; }
    }
}
