using AutomationTesting_CorConnect.PageObjects.StaticPages;
using AutomationTesting_CorConnect.Resources;
using AutomationTesting_CorConnect.Constants;
using OpenQA.Selenium;
using System.Linq;
using AutomationTesting_CorConnect.Utils.AccountMaintenance;
using System.Collections.Generic;
using SeleniumExtras.WaitHelpers;
using AutomationTesting_CorConnect.Utils;
using NUnit.Framework;
using System;
using StackExchange.Redis;
using AutomationTesting_CorConnect.PageObjects.ASN;

namespace AutomationTesting_CorConnect.PageObjects.AccountMaintenance
{
    internal class AccountMaintenanceAspx : PopUpPage
    {
        public AccountMaintenanceAspx(IWebDriver webDriver) : base(webDriver, Pages.AccountMaintenanceAspx)
        {
        }

        internal void SelectRelationShipType(string entityCode, string RelationShipType)
        {
            SelectValueMultipleColumns(FieldNames.Entity, entityCode);
            WaitForElementToHaveFocus("RelationshipTypeInput");
            SelectValueTableDropDown(FieldNames.RelationshipType, RelationShipType);
            ButtonClick(ButtonsAndMessages.Display);
        }

        internal void SelectRelationShipType(string RelationShipType)
        {
            SelectValueFirstRow(FieldNames.Entity);
            WaitForElementToHaveFocus("RelationshipTypeInput");
            SelectValueByScroll(FieldNames.RelationshipType, RelationShipType);
            ButtonClick(ButtonsAndMessages.Display);
            WaitForElementToBeVisible(FieldNames.TableCreditPriceAudit);
        }

        internal void SelectRelationShipType(string relationShipType, string table, string entityCode, int index = 1)
        {
            if (string.IsNullOrEmpty(entityCode))
            {
                SelectValueFirstRow(FieldNames.Entity);
            }
            else
            {
                SelectValueMultipleColumns(FieldNames.Entity, entityCode, index);
            }
            WaitForElementToHaveFocus("RelationshipTypeInput");
            SelectValueByScroll(FieldNames.RelationshipType, relationShipType);
            ButtonClick(ButtonsAndMessages.Display);
            WaitForElementToBeVisible(table);
        }

        internal void SelectCreditPriceAudit()
        {
            SelectTransActionType("All");
            SelectPriceAuditCreditTransactions("Yes");
            ButtonClick(ButtonsAndMessages.Add_pascal);
            CheckForText("Record has been added successfully", true);
        }

        internal void SelectTransActionType(string transactionType)
        {
            SelectValueByScroll(FieldNames.TransactionType, transactionType);
        }

        internal void SelectPricingType(string pricingType, string section = null)
        {
            SelectValueTableDropDown(FieldNames.PricingType, pricingType, section);
        }

        internal void SelectMethod(string method, string section = null)
        {
            SelectValueTableDropDown(FieldNames.Method, method, section);
        }

        internal void SelectCurrency(string currency, string section = null)
        {
            SelectValueTableDropDown(FieldNames.Currency, currency, section);
        }

        internal void SelectPrecendenceOrder(string orderType, string section = null)
        {
            SelectValueByScroll(FieldNames.PrecendenceOrder, orderType, section);
        }

        internal void SelectAccountingDocType(string accountingType, string section = null)
        {
            SelectValueByScroll(FieldNames.AccountingDocumentType, accountingType, section);
        }

        internal void SelectTransActionTypeByTableDropDown(string value)
        {
            SelectValueTableDropDown(FieldNames.TransactionType, value);
        }

        internal void SelectTransActionType(string transactionType, string section)
        {
            SelectValueByScroll(FieldNames.TransactionType, transactionType, section);
        }

        internal void SelectPriceAuditCreditTransactions(string value)
        {
            SelectValueByScroll(FieldNames.PriceAuditCreditTransactions, value);
        }

        internal void SelectPriceAuditCreditTransactions(string value, string section)
        {
            SelectValueTableDropDown(FieldNames.PriceAuditCreditTransactions, value, section);
        }

        internal void SelectApplicableTo(string value)
        {
            SelectValueByScroll(FieldNames.ApplicableTo, value);
        }

        internal void SelectApplicableTo(string value, string section)
        {
            SelectValueTableDropDown(FieldNames.ApplicableTo, value, section);
        }


        internal bool VerifyTableData(string element, params string[] tableHeaders)
        {
            var elements = FindElements(element);

            foreach (var tableHeader in tableHeaders)
            {
                if (elements.Any(element => element.Text.Trim() == tableHeader))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        internal void ClickTab(string referenceName, string tabName)
        {
            var elements = FindElements(referenceName);
            foreach (var element in elements)
            {
                if (element.Text.Trim() == tabName)
                {
                    element.Click();
                    break;
                }
            }
        }

        internal void DeleteFirstRelationShip()
        {
            ClickHyperLinkOnGrid("RelationShipTable", "RelationShipTableHeader", "#");
            AcceptWindowAlert(out string msg);
            WaitforStalenessofRelationShipTable();
        }

        internal void AddResellerRelationship(string entityCode)
        {
            SelectRelationShipType(FieldNames.ResellerInvoiceRequired, FieldNames.ResellerInvoiceRequiredTable, entityCode, 2);
            SelectValueFirstRow(FieldNames.ResellerInvoiceRequired);
            ButtonClick(ButtonsAndMessages.Add_pascal);
            WaitForTextToBePresentInElementLocated(FieldNames.ResellerInvoiceRequiredError, ButtonsAndMessages.RecordAddedSuccessfully);
        }

        internal void DeleteFirstRelationShipIfExist(string table, string header, string headerName, string buttonCaption)
        {
            if (IsRelationExist(table, header, headerName, buttonCaption) == true)
            {
                ClickHyperLinkOnGrid("RelationShipTable", "RelationShipTableHeader", "#");
                AcceptAlert(out string msg);
                WaitforStalenessofRelationShipTable();
            }
        }

        internal void WaitforStalenessofRelationShipTable()
        {
            wait.Until(ExpectedConditions.StalenessOf(driver.FindElement(GetElement("RelationShipTable"))));
        }

        internal bool CheckFirstRelationShipIsDisabled()
        {
            return CheckHyperlinkIsDisabledOnGrid("RelationShipTable", "RelationShipTableHeader", "#");

        }

        internal bool IsCommandsButtonsNotVisible()
        {
            return IsAnchorButtonsNotVisible("Credit Audit History Table", "Credit Audit History Table Header", TableHeaders.Commands);

        }

        internal bool DeleteResellerRelationshipFromDB(string corcentricCode, string entityType)
        {
            corcentricCode = corcentricCode.Trim();
            entityType = entityType.Trim();
            if (entityType == EntityType.Dealer)
            {

                return AccountMaintenanceUtil.DeleteResellerRelationshipDealerSide(corcentricCode);

            }
            else
            {
                return AccountMaintenanceUtil.DeleteResellerRelationshipFleetSide(corcentricCode);
            }
        }

        internal bool DeletePricingTypeRelationshipFromDB(string corcentricCode, string entityType)
        {
            corcentricCode = corcentricCode.Trim();
            entityType = entityType.Trim();
            if (entityType == EntityType.Dealer)
            {

                return AccountMaintenanceUtil.DeletePricingTypeRelationshipDealerSide(corcentricCode);

            }
            else
            {
                return AccountMaintenanceUtil.DeletePricingTypeRelationshipFleetSide(corcentricCode);
            }
        }

        internal bool DeleteInvoiceDeliveryRelationshipFromDB(string corcentricCode, string entityType)
        {
            corcentricCode = corcentricCode.Trim();
            entityType = entityType.Trim();

            return AccountMaintenanceUtil.DeleteInvoiceDeliveryRelationship(corcentricCode);

        }

        internal bool DeleteCurrencyCodeRelationshipFromDB(string corcentricCode, string entityType)
        {
            corcentricCode = corcentricCode.Trim();
            entityType = entityType.Trim();
            if (entityType == EntityType.Dealer)
            {

                return AccountMaintenanceUtil.DeleteCurrencyCodeRelationshipDealerSide(corcentricCode);

            }
            else
            {
                return AccountMaintenanceUtil.DeleteCurrencyCodeRelationshipFleetSide(corcentricCode);
            }
        }

        internal bool DeleteRelationshipFromDB(string corcentricCode, string entityType, string relationship)
        {
            corcentricCode = corcentricCode.Trim();
            entityType = entityType.Trim();
            if (relationship == FieldNames.ResellerInvoiceRequired)
            {
                return DeleteResellerRelationshipFromDB(corcentricCode, entityType);
            }
            else if (relationship == FieldNames.CorePriceAdjustment)
            {
                return AccountMaintenanceUtil.DeleteCorePriceAdjustmentRel(corcentricCode, entityType);
            }
            else if (relationship == FieldNames.InvoiceValidityDays)
            {
            }
            else if (relationship == FieldNames.StatementDelivery)
            {
                return AccountMaintenanceUtil.DeleteStatementDeliveryRel(corcentricCode, entityType);
            }
            else if (relationship == FieldNames.PaymentTerms)
            {
                return AccountMaintenanceUtil.DeletePaymentTermsRel(corcentricCode, entityType);
            }
            else if (relationship == FieldNames.PaymentMethod)
            {
                return AccountMaintenanceUtil.DeletePaymentMethodRel(corcentricCode, entityType);
            }
            else if (relationship == FieldNames.FinancialHandling)
            {
                return AccountMaintenanceUtil.DeleteFinancialHandlingRel(corcentricCode, entityType);
            }
            return false;
        }

        internal List<string> VerifyPricingtypeFields()
        {
            List<string> errorMsgs = new List<string>();
            string requiredLabel = " Required Label";

            if (!IsElementDisplayed(FieldNames.PricingType))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PricingType));
            }
            if (!IsElementVisible(FieldNames.PricingType + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, FieldNames.PricingType));
            }
            if (!IsElementDisplayed(FieldNames.PrecendenceOrder))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.PrecendenceOrder));
            }
            if (!IsElementVisible(FieldNames.PrecendenceOrder + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, FieldNames.PrecendenceOrder));
            }
            if (!IsElementDisplayed(FieldNames.AccountingDocumentType))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.AccountingDocumentType));
            }
            if (!IsElementVisible(FieldNames.AccountingDocumentType + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, FieldNames.AccountingDocumentType));
            }

            return errorMsgs;
        }

        internal List<string> VerifyInvoiceDeliveryFields()
        {
            List<string> errorMsgs = new List<string>();
            string requiredLabel = " Required Label";

            if (!IsElementDisplayed(FieldNames.Method))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Method));
            }
            if (!IsElementVisible(FieldNames.Method + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, FieldNames.Method));
            }
            if (!IsElementDisplayed(FieldNames.Structure))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.Structure));
            }
            if (!IsElementVisible(FieldNames.Structure + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, FieldNames.Structure));
            }
            if (!IsElementDisplayed(FieldNames.DealerInvoiceCopy))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, FieldNames.DealerInvoiceCopy));
            }
            if (!IsElementVisible(FieldNames.DealerInvoiceCopy + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, FieldNames.DealerInvoiceCopy));
            }

            return errorMsgs;
        }

        internal List<string> VerifyAccountConfigFieldsDealer(LocationType locationType)
        {
            List<string> errorMsgs = new List<string>();
            string requiredLabel = " Required Label";
            string caption = "Requestor Name";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Requestor Phone";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Requestor Email";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Requestor Company";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Enrollment Status";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Display Name";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Legal Name";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Language";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Enrollment Type";
            if (!IsDropDownDisabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Location Type";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Parent Account Name";
            if (!IsDropDownDisabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            caption = "DDE Flag";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Account Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Accounting Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!locationType.Equals(LocationType.Shop) &&
                !locationType.Equals(LocationType.Master) &&
                !IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Vendor Payment Type";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Deactivated";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Terminated";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Terminated Date";
            if (!IsDropDownDisabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            caption = "Termination Notes";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Eligible for Transaction Submission";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Franchise Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Selected Franchise Codes";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Address1";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Address2";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "City";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Country";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "State";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Zip";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "County";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Phone Number";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Currency Dropdown";
            if (!IsDropDownDisabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            caption = "Dun Number";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Federal Tax ID Number";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Entity Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Community Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Program Start Date";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Pre Authorization";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }

            if (!locationType.Equals(LocationType.Master))
            {
                caption = "Sub Community";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
            }

            if (locationType == LocationType.Billing)
            {
                caption = "Master Location";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Invoice Forwarding";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Dealer Type";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Non Transactionable Locations";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Do Not Charge Fee";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
            }
            return errorMsgs;
        }

        internal List<string> VerifyAccountConfigFieldsFleet(LocationType locationType, string tabName = "Information")
        {
            List<string> errorMsgs = new List<string>();
            string requiredLabel = " Required Label";
            string caption = "Requestor Name";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Requestor Phone";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Requestor Email";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Requestor Company";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Enrollment Status";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.ElementEnabled, caption));
            }
            caption = "Display Name";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Legal Name";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Language";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Enrollment Type";
            if (!IsDropDownDisabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Location Type";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Parent Account Name";
            if (!IsDropDownDisabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            caption = "Account Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Accounting Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (locationType.Equals(LocationType.Billing) &&
                !IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Deactivated";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Terminated";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Termination Notes";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Eligible for Transaction Submission";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Address1";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Address2";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "City";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Country";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "State";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Zip";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "County";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Phone Number";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Currency Dropdown";
            if (!IsDropDownDisabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            caption = "Dun Number";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Entity Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Community Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Program Start Date";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }

            caption = "Pre Authorization";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Anticipated Monthly Spend";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Requested Credit Line";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Program Code";
            if (!IsElementVisible(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (!IsElementVisible(caption + requiredLabel))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
            }
            caption = "Credit Limit";
            if (IsElementEnabled(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
            }
            caption = "NA Manager";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (tabName == "Information") {
                caption = "Collector";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Customer Tier";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
            }
            caption = "Tax Info";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Direct Bill Code";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Invoice Approval";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Purchasing Level";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            caption = "Terminated Date";
            if (!IsElementDisplayed(caption))
            {
                errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
            }
            if (locationType.Equals(LocationType.Billing))
            {
                caption = "Enable Payment Portal";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
            }
            if (!locationType.Equals(LocationType.Master))
            {
                caption = "Sub Community";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Upload Tax Form";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Tax Form Name";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Tax ID Type";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Tax Classification";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
            }
            if (locationType.Equals(LocationType.MasterBilling)
                || locationType.Equals(LocationType.Master))
            {
                if (locationType.Equals(LocationType.MasterBilling))
                {
                    caption = "Community Invoices Forwarded to";
                    if (!IsDropDownDisabled(caption))
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisabled, caption));
                    }
                    caption = "Credit Manager Name";
                    if (!IsElementDisplayed(caption))
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                    }
                    caption = "Credit Manager Email";
                    if (!IsElementDisplayed(caption))
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                    }
                    caption = "Credit Manager Phone Number";
                    if (!IsElementDisplayed(caption))
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                    }
                    caption = "Invoice Forwarding";
                    if (!IsElementDisplayed(caption))
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                    }
                    caption = "Master Location";
                    if (!IsElementDisplayed(caption))
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                    }
                    caption = "Enable Payment Portal";
                    if (!IsElementDisplayed(caption))
                    {
                        errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                    }
                }
                caption = "Fleet Count";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                if (!IsElementVisible(caption + requiredLabel))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotMarkedMandatory, caption));
                }
                caption = "Tax ID Type";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
                caption = "Tax Classification";
                if (!IsElementDisplayed(caption))
                {
                    errorMsgs.Add(string.Format(ErrorMessages.FieldNotDisplayed, caption));
                }
            }
            return errorMsgs;
        }
        internal void AddTaxRelationshipDealerSide(string corcentricCode)
        {
            SelectRelationShipType(FieldNames.Tax, FieldNames.TaxTable, corcentricCode, 1);
            WaitForElementToHaveFocus("RelationshipTypeInput");
            ButtonClick(ButtonsAndMessages.Add_pascal);
            WaitForTextToBePresentInElementLocated(FieldNames.TaxError, ButtonsAndMessages.RecordAddedSuccessfully);
        }

        internal void CreateFinancialHandlingRelation(string dealer, string financialHandlingType)
        {
            Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.FinancialHandling);
            if (GetRowCount(FieldNames.RelationShipTable, true) > 0)
            {
                DeleteFirstRelationShip();
            }
            SelectRelationShipType(FieldNames.FinancialHandling, FieldNames.FinancialHandlingTable, RenameMenuField(dealer));
            WaitForElementToHaveFocus("RelationshipTypeInput");
            SelectValueByScroll(FieldNames.FinancialHandlingType, financialHandlingType);
            ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            ClearFilter(FieldNames.RelationShipTable, FieldNames.RelationshipType);
            Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.FinancialHandling);
            Assert.AreEqual(1, GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);
        }
        internal void CreatePaymentMethodRelation(string dealer, string paymentMethod)
        {
            Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentMethod);
            if (GetRowCount(FieldNames.RelationShipTable, true) > 0)
            {
                DeleteFirstRelationShip();
            }
            SelectRelationShipType(FieldNames.PaymentMethod, FieldNames.PaymentMethodTable, RenameMenuField(dealer));
            WaitForElementToHaveFocus("RelationshipTypeInput");
            SelectValueByScroll("Payment Method Type", paymentMethod);
            ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            ClearFilter(FieldNames.RelationShipTable, FieldNames.RelationshipType);
            Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentMethod);
            Assert.AreEqual(1, GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);
        }

        internal void UpdatePaymentMethodRelation(string paymentMethod)
        {
            Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentMethod);
            if (GetRowCount(FieldNames.RelationShipTable, true) > 0)
            {
                ClickButtonOnGrid("RelationShipTable", "RelationShipTableHeader", "Relationship Type", "Payment Method");
                ClickAnchorButton("Edit Payment Method Table", "Payment Method Table Header", TableHeaders.Commands, ButtonsAndMessages.Edit);
                SelectValueByScroll("Edit Payment Method Type", paymentMethod);
                UpdateEditGrid();
                Assert.AreEqual(ButtonsAndMessages.RecordUpdatedPleaseCloseToExitUpdateForm, GetEditMsg());
                ClearFilter(FieldNames.RelationShipTable, FieldNames.RelationshipType);
                Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.PaymentMethod);
                Assert.AreEqual(1, GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);
            }
            else 
            {
                Assert.Fail("No Record Found");
            }
        }


        internal void CreateDataRequirementRelation(string dealer, string dataRequirement)
        {
            Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.DataRequirements);
            if (GetRowCount(FieldNames.RelationShipTable, true) > 0)
            {
                DeleteFirstRelationShip();
            }
            SelectRelationShipType(FieldNames.DataRequirements, FieldNames.RelDataRequirementsTable, RenameMenuField(dealer));
            WaitForElementToHaveFocus("RelationshipTypeInput");
            SelectValueByScroll("Data Requirements Type", dataRequirement);
            WaitForStalenessOfElement(FieldNames.RelationshipType);
            SelectValueMultiSelectDropDown("Mandatory on Document Type",TableHeaders.Type, "All");
            SelectValueMultiSelectDropDown("Visible on Document Type",TableHeaders.Type, "All");
            ButtonClick(ButtonsAndMessages.Add_pascal);
            Assert.IsTrue(IsErrorLabelMessageVisibleWithMsg(ButtonsAndMessages.RecordAddedSuccessfully, out string actualMsg), string.Format(ErrorMessages.MessageMismatch, ButtonsAndMessages.RecordAddedSuccessfully, actualMsg));
            ClearFilter(FieldNames.RelationShipTable, FieldNames.RelationshipType);
            Filter(FieldNames.RelationShipTable, FieldNames.RelationShipTableHeader, FieldNames.RelationshipType, FieldNames.DataRequirements);
            Assert.AreEqual(1, GetRowCount(FieldNames.RelationShipTable, true), ErrorMessages.RecordNotUpdatedDB);
        }
    }
}