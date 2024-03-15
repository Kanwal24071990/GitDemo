using AutomationTesting_CorConnect.Helper;
using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Utils;
using AutomationTesting_CorConnect.Utils.ManageTaxExemptions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutomationTesting_CorConnect.PageObjects.ManageTaxExemptions
{
    internal class ManageTaxExemptionsPage : PopUpPage
    {
        internal readonly string UploadTaxDocumentsEditType = "Upload Tax Documents";
        internal readonly string DeleteTaxDocumentsEditType = "Delete Tax Documents";
        internal ManageTaxExemptionsPage(IWebDriver driver) : base(driver, Pages.ManageTaxExemptions) { }

        internal void PopulateGrid(string editType, string fleetCode, string name = null)
        {
            SelectValueTableDropDown(FieldNames.EditType, editType);
            if (!string.IsNullOrEmpty(fleetCode))
            {
                SearchAndSelectValueAfterOpen(FieldNames.FleetCode, fleetCode);
            }
            else
            {
                SelectValueFirstRow(FieldNames.FleetCode);
            }
            if (!string.IsNullOrEmpty(name))
            {
                EnterTextAfterClear(FieldNames.Name, name);
            }
            ButtonClick(ButtonsAndMessages.Search);
            WaitForLoadingGrid();
        }

        internal List<string> ValidateSorting(string table, string tableHeader, List<string> currentList)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                currentList = currentList.Select(x => Regex.Replace(x, @"\s+", "")).ToList();
                LoggingHelper.LogMessage($"Verifing sorting for  Table [{table}] Header [{tableHeader}]");
                ScrollToHeader(table, tableHeader);
                ClickTableHeader(table, tableHeader);
                List<string> sortedList = GetElementsList(table, tableHeader).Select(x => Regex.Replace(x, @"\s+", "")).ToList(); ;
                if (!CommonUtils.VerifySortOrder(sortedList, currentList, Constants.SortOrder.Ascending))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ValuesNotSorted, Constants.SortOrder.Ascending.ToString()) + $" TableHeader [{tableHeader}]");
                }
                ClickTableHeader(table, tableHeader);
                sortedList = GetElementsList(table, tableHeader).Select(x => Regex.Replace(x, @"\s+", "")).ToList(); ;
                if (!CommonUtils.VerifySortOrder(sortedList, currentList, Constants.SortOrder.Descending))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ValuesNotSorted, Constants.SortOrder.Descending.ToString()) + $" TableHeader [{tableHeader}]");
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> ValidateSortingCustomWait(string table, string tableHeader, List<string> currentList)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                currentList = currentList.Select(x => Regex.Replace(x, @"\s+", "")).ToList();
                LoggingHelper.LogMessage($"Verifing sorting for  Table [{table}] Header [{tableHeader}]");
                ScrollToHeader(table, tableHeader);
                ClickTableHeader(table, tableHeader);
                WaitForLoadingMessageThisPage();
                List<string> sortedList = GetElementsList(table, tableHeader).Select(x => Regex.Replace(x, @"\s+", "")).ToList(); ;
                if (!CommonUtils.VerifySortOrder(sortedList, currentList, Constants.SortOrder.Ascending))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ValuesNotSorted, Constants.SortOrder.Ascending.ToString()) + $" TableHeader [{tableHeader}]");
                }
                ClickTableHeader(table, tableHeader);
                WaitForLoadingMessageThisPage();
                sortedList = GetElementsList(table, tableHeader).Select(x => Regex.Replace(x, @"\s+", "")).ToList(); ;
                if (!CommonUtils.VerifySortOrder(sortedList, currentList, Constants.SortOrder.Descending))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.ValuesNotSorted, Constants.SortOrder.Descending.ToString()) + $" TableHeader [{tableHeader}]");
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> ValidateFiltering(string table, string tableHeader, bool isMainTable = true)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                LoggingHelper.LogMessage($"Verifing filtering for Table [{table}] Header [{tableHeader}]");
                ScrollToHeader(table, tableHeader);
                string columnVal = GetElementByIndex(table, tableHeader, 1);
                if (!string.IsNullOrEmpty(columnVal.Trim()))
                {
                    Filter(table, tableHeader, columnVal);
                    if (GetRowCount(table, isMainTable) <= 0)
                    {
                        throw new Exception(ErrorMessages.NoRowAfterFilter);
                    }
                    string filteredVal = GetElementByIndex(table, tableHeader, 1);
                    if (!filteredVal.Contains(columnVal))
                    {
                        errorMsgs.Add(ErrorMessages.FilterNotWorking + $" Header [{tableHeader}]");
                    }
                    ClearFilter(table, tableHeader);
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> AddStatesToExemption(List<string> statesList)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                LoggingHelper.LogMessage("Adding states to exemptions list.");
                CheckCheckBox(FieldNames.DonotinheritsettingsfromParentLocation);
                for (int i = 0; i < statesList.Count; i++)
                {
                    CheckCheckBox(statesList[i]);
                }
                ButtonClick(ButtonsAndMessages.SaveChanges);
                WaitForLoadingMessageThisPage();
                if (ButtonsAndMessages.RecordSavedSuccessfully_caps != GetText(FieldNames.PerformedOperationMessage))
                {
                    errorMsgs.Add(ErrorMessages.SaveOperationFailed);
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> VerifyStatesCheckBoxAvailability(List<string> statesList)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                LoggingHelper.LogMessage("verifying states.");
                CheckCheckBox(FieldNames.DonotinheritsettingsfromParentLocation);
                for (int i = 0; i < statesList.Count; i++)
                {
                    if (!IsCheckBoxExistsWithLabel(statesList[i]))
                    {
                        errorMsgs.Add($"State {statesList[i]} not exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> VerifyStatesInExemptionsColumn(string table, List<string> statesList, bool stateShouldExist = true)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                LoggingHelper.LogMessage("Verifying states in exemptions column of Tax Exempted Fleet Locations table.");
                var exemptionsList = GetElementsList(table, TableHeaders.Exemptions);
                var fleetCodeList = GetElementsList(table, TableHeaders.FleetCode);
                for (int i = 0; i < exemptionsList.Count; i++)
                {
                    for (int j = 0; j < statesList.Count; j++)
                    {
                        if (stateShouldExist)
                        {
                            if (!exemptionsList[i].Contains(statesList[j]))
                            {
                                errorMsgs.Add(ErrorMessages.StateMisMatch + $". State [{statesList[j]}] not in Exemptions column for Fleet Code [{fleetCodeList[i]}]");
                            }
                        }
                        else
                        {
                            if (exemptionsList[i].Contains(statesList[j]))
                            {
                                errorMsgs.Add(ErrorMessages.StateExists + $". State [{statesList[j]}] exists in Exemptions column for Fleet Code [{fleetCodeList[i]}]");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> RemoveStatesFromExemption(List<string> statesList)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                LoggingHelper.LogMessage("Adding states to exemptions list.");
                CheckCheckBox(FieldNames.DonotinheritsettingsfromParentLocation);
                for (int i = 0; i < statesList.Count; i++)
                {
                    UncheckCheckBox(statesList[i]);
                }
                UncheckCheckBox(FieldNames.DonotinheritsettingsfromParentLocation);
                ButtonClick(ButtonsAndMessages.SaveChanges);
                WaitForLoadingMessageThisPage();
                if (ButtonsAndMessages.RecordSavedSuccessfully_caps != GetText(FieldNames.PerformedOperationMessage))
                {
                    errorMsgs.Add(ErrorMessages.SaveOperationFailed);
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal List<string> UploadDocument(string documentName, string corcentricCode, bool doUploadSuccessfully = true)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                Filter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, corcentricCode);
                CheckTableRowCheckBoxByIndex(FieldNames.ClientTaxDocumentsTable, 1);
                UploadFile(FieldNames.FileName, documentName);
                ButtonClick(ButtonsAndMessages.UploadTaxDocuments);
                AcceptAlert(out string alertMessage);
                if (doUploadSuccessfully)
                {
                    if (VerifyFileUpload() != ButtonsAndMessages.SavedSuccessfully)
                    {
                        throw new Exception(ErrorMessages.FileUploadingFailed);
                    }
                    AcceptAlert(ButtonsAndMessages.SavedSuccessfully);
                    WaitForLoadingGrid();
                }
                else
                {
                    WaitForOverlayToBeInvisible();
                    WaitForTextToBePresentInElementLocated(FieldNames.FileUploadError, ButtonsAndMessages.ThisFileExtensionIsNotAllowed);
                    string errorText = GetText(FieldNames.FileUploadError);
                    if (errorText != ButtonsAndMessages.ThisFileExtensionIsNotAllowed)
                    {
                        errorMsgs.Add(ErrorMessages.ErrorMessageMismatch +
                            string.Format(LoggerMesages.ExpectedMessage, ButtonsAndMessages.ThisFileExtensionIsNotAllowed, errorText));
                    }
                }
                Thread.Sleep(1000);
                ClearFilter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.ToString());
                errorMsgs.Add(ex.ToString());
            }
            return errorMsgs;
        }

        ///<summary>
        ///Verify file has been uploaded by validating success message.
        ///</summary>
        internal string VerifyFileUpload()
        {
            string uploadMessage = GetAlertMessage();
            return uploadMessage;
        }

        internal List<string> DeleteDocument(string corcentricCode, int editTypeId, string fileName)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                string[] splitted = fileName.Split("//");
                fileName = splitted[splitted.Length - 1];
                Filter(FieldNames.ClientTaxDocumentsTable, TableHeaders.TaxDocument, fileName);
                Filter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode, corcentricCode);
                CheckTableRowCheckBoxByIndex(FieldNames.ClientTaxDocumentsTable, 1);
                ButtonClick(ButtonsAndMessages.DeleteTaxDocuments);
                if (VerifyFileUpload() != ButtonsAndMessages.DeletedSuccessfully)
                {
                    throw new Exception(ErrorMessages.DeleteOperationFailed);
                }
                AcceptAlert(ButtonsAndMessages.SavedSuccessfully);
                WaitForLoadingGrid();
                ClearFilter(FieldNames.ClientTaxDocumentsTable, TableHeaders.TaxDocument);
                ClearFilter(FieldNames.ClientTaxDocumentsTable, TableHeaders.AccountCode);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal int GetRecordsCountFromDB(string fleetCode, int editTypeId)
        {
            return ManageTaxExemptionsUtils.GetRecordsCount(fleetCode, editTypeId);
        }

        internal List<string> DeleteRemainingDocuments(string fileName)
        {
            List<string> errorMsgs = new List<string>();
            try
            {
                string[] splitted = fileName.Split("//");
                fileName = splitted[splitted.Length - 1];
                Filter(FieldNames.ClientTaxDocumentsTable, TableHeaders.TaxDocument, fileName);
                ClickTableHeader(FieldNames.ClientTaxDocumentsTable, TableHeaders.SelectAllCheckBox);
                CheckCheckBox(FieldNames.DeleteTheDocumentFromLocationsThatAreUnderTheAboveSelectedLocations);
                ButtonClick(ButtonsAndMessages.DeleteTaxDocuments);
                if (VerifyFileUpload() != ButtonsAndMessages.DeletedSuccessfully)
                {
                    throw new Exception(ErrorMessages.DeleteOperationFailed);
                }
                AcceptAlert(ButtonsAndMessages.SavedSuccessfully);
                WaitForLoadingGrid();
            }
            catch (Exception ex)
            {
                LoggingHelper.LogException(ex.Message);
                errorMsgs.Add(ex.Message);
            }
            return errorMsgs;
        }

        internal void WaitForLoadingMessageThisPage()
        {
            WaitForElementToBeVisible(GetElement(FieldNames.LoadingMessage));
            WaitForElementToBeInvisibleCustomWait(GetElement(FieldNames.LoadingMessage));
        }

        internal List<string> ValidateHeadersByScroll(string table, params string[] headerNames)
        {
            List<string> errorMsgs = new List<string>();
            headerNames.ToList().ForEach(x =>
            {
                if (x.ToLower().Contains("dealer") || x.ToLower().Contains("fleet"))
                {
                    x = RenameMenuField(x);
                }
                try
                {
                    ScrollToHeader(table, x);
                }
                catch (NoSuchElementException)
                {
                    errorMsgs.Add(string.Format(ErrorMessages.TableHeaderMissing, x));
                }
            });
            return errorMsgs;
        }
    }
}
