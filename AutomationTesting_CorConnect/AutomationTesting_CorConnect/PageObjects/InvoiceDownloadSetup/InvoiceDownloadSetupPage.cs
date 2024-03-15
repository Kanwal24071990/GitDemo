using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Threading.Tasks;

namespace AutomationTesting_CorConnect.PageObjects.InvoiceDownloadSetup
{
    internal class InvoiceDownloadSetupPage : PopUpPage
    {
        internal InvoiceDownloadSetupPage(IWebDriver driver) : base(driver, Pages.InvoiceDownloadSetup)
        {
        }

        internal void DefineExport(string exportName)
        {
            EnterTextDropDown(FieldNames.ExportName,exportName);
            WaitForLoadingGrid();
            SearchAndSelectValueWithoutLoadingMsg(FieldNames.ExportDefinitionfor, "Master");

            SearchAndSelectValueWithoutLoadingMsg(FieldNames.FileType, "Excel");
            SelectValueFirstRow(FieldNames.CompanyName);
        }

        internal void AddColumn(string column,bool includeInExport, bool PresentForSelection)
        {
            Task t = Task.Run(() => WaitForStalenessOfElement(FieldNames.MainTable));
            Click(ButtonsAndMessages.AddColumn);
            SelectValueByScroll(FieldNames.Column, column);
            Click(ButtonsAndMessages.Add_pascal);
            t.Wait();
            t.Dispose();
            if (includeInExport)
            {
                Click(FieldNames.IncludeinExport);
            }

            if (PresentForSelection)
            {
                Click(FieldNames.Presentforselectiononmanualexecution);
            }
        }
    }
}
