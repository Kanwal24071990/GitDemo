namespace PageObjectUtility.DataObjects
{
    internal class Menu
    {
        public string MenuName { get; set; }
        public string LongDescription { get; set; }
        public string Caption { get; set; }
        public string OriginalSubMenuDescription { get; set; }
        public void ReplaceCaption(string oldStr, string newStr)
        {
            this.Caption.Replace(oldStr, newStr);
        }
    }
}
