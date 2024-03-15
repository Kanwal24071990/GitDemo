using AutomationTesting_CorConnect.Constants;
using AutomationTesting_CorConnect.DataBaseHelper.DataAccesLayer.AccountMaintenance;
using AutomationTesting_CorConnect.DataObjects;
using System.Collections.Generic;

namespace AutomationTesting_CorConnect.Utils.AccountMaintenance
{
    internal class AccountMaintenanceUtil
    {
        internal static RelCreditPriceAuditTb GetTransactionDetails()
        {
            return new AccountMaintenanceDAL().GetTransactionDetails();
        }

        internal static RelPricingTypeTb GetPricingTypeDetails()
        {
            return new AccountMaintenanceDAL().GetPricingTypeDetails();
        }

        internal static RelCurrencyCodeTb GetCurrencyCodeDetails()
        {
            return new AccountMaintenanceDAL().GetCurrencyCodeDetails();
        }

        internal static int GetLookupID(string value)
        {
            return new AccountMaintenanceDAL().GetLookupID(value);
        }

        internal static int GetApplicableToID(int parentLookupId, string value)
        {
            return new AccountMaintenanceDAL().GetApplicableToID(parentLookupId, value);
        }

        internal static List<string> GetTranscationNames()
        {
            return new AccountMaintenanceDAL().GetTransactionTypes();
        }
        internal static decimal GetFieldValues()
        {
            return new AccountMaintenanceDAL().GetFieldValues();
        }
        internal static RelationshipSenderReceiverTable GetRelSenderReceiverByFleetCode(string fleetCode, string relationshipName)
        {
            return new AccountMaintenanceDAL().GetRelSenderReceiverByFleetCode(fleetCode, relationshipName);
        }
       
        internal static RelationshipSenderReceiverTable GetRelSenderReceiverByDealerCode(string dealerCode, string relationshipName)
        {
            return new AccountMaintenanceDAL().GetRelSenderReceiverByDealerCode(dealerCode, relationshipName);
        }

        internal static bool GetIsActiveValue(int senderReceiverRelId)
        {
            return new AccountMaintenanceDAL().GetIsActiveValue(senderReceiverRelId);
        }

        internal static List<AuditTrailTable> GetAuditTrails()
        {
            return new AccountMaintenanceDAL().GetAuditTrails();
        }

        internal static void DeactivateTokenSeperateARAP()
        {
            new AccountMaintenanceDAL().DeactivateTokenSeperateARAP();

        }

        internal static void ActivateTokenSeperateARAP()
        {
            new AccountMaintenanceDAL().ActivateTokenSeperateARAP();

        }

        internal static int GetRowCountFromTable(string table)
        {
            return new AccountMaintenanceDAL().GetRowCountFromTable(table);
        }

        internal static int GetInvoiceDeliveryMethodId(string fleetCode)
        {
            return new AccountMaintenanceDAL().GetInvoiceDeliveryMethodId(fleetCode);
        }

        internal static bool DeleteResellerRelationshipDealerSide(string dealerCorcentricCode)
        {
            return new AccountMaintenanceDAL().DeleteResellerRelationshipDealerSide(dealerCorcentricCode);
        }

        internal static bool DeleteResellerRelationshipFleetSide(string fleetCorcentricCode)
        {
            return new AccountMaintenanceDAL().DeleteResellerRelationshipFleetSide(fleetCorcentricCode);
        }

        internal static bool DeletePricingTypeRelationshipDealerSide(string dealerCorcentricCode)
        {
            return new AccountMaintenanceDAL().DeletePricingTypeRelationshipDealerSide(dealerCorcentricCode);
        }

        internal static bool DeletePricingTypeRelationshipFleetSide(string fleetCorcentricCode)
        {
            return new AccountMaintenanceDAL().DeletePricingTypeRelationshipFleetSide(fleetCorcentricCode);
        }

        internal static bool DeleteCurrencyCodeRelationshipDealerSide(string dealerCorcentricCode)
        {
            return new AccountMaintenanceDAL().DeleteCurrencyCodeRelationshipDealerSide(dealerCorcentricCode);
        }

        internal static bool DeleteCurrencyCodeRelationshipFleetSide(string fleetCorcentricCode)
        {
            return new AccountMaintenanceDAL().DeleteCurrencyCodeRelationshipFleetSide(fleetCorcentricCode);
        }

        internal static bool DeleteInvoiceDeliveryRelationship(string fleetCorcentricCode)
        {
            return new AccountMaintenanceDAL().DeleteInvoiceDeliveryRelationship(fleetCorcentricCode);
        }

        internal static RelationshipDetails GetRelationshipDetailsForRelType(string relationshipType)
        {
            return new AccountMaintenanceDAL().GetRelationshipDetailsForRelType(relationshipType);
        }

        internal static string GetDealerCode(LocationType locationType)
        {
            return new AccountMaintenanceDAL().GetDealerCode(locationType);
        }

        internal static string GetDealerCodeCorcentric(LocationType locationType)
        {
            return new AccountMaintenanceDAL().GetDealerCodeCorcentric(locationType);
        }

        internal static string GetFleetCode(LocationType locationType)
        {
            return new AccountMaintenanceDAL().GetFleetCode(locationType);
        }

        internal static string GetFleetCodeEnabledForPaymentPortal(LocationType locationType)
        {
            return new AccountMaintenanceDAL().GetFleetCodeEnablePaymentPortal(locationType);
        }

        internal static bool DeleteCorePriceAdjustmentRel(string entityCode, string entityType)
        {
            if (entityType == Constants.EntityType.Dealer)
            {
                return new AccountMaintenanceDAL().DeleteCorePriceAdjustmentRelDealerSide(entityCode);
            }
            else
            {
                return new AccountMaintenanceDAL().DeleteCorePriceAdjustmentRelFleetSide(entityCode);
            }
        }

        internal static bool DeleteStatementDeliveryRel(string entityCode, string entityType)
        {
            if (entityType == Constants.EntityType.Dealer)
            {
                return new AccountMaintenanceDAL().DeleteStatementDeliveryRelDealerSide(entityCode);
            }
            else
            {
                return new AccountMaintenanceDAL().DeleteStatementDeliveryRelFleetSide(entityCode);
            }
        }

        internal static bool DeletePaymentTermsRel(string entityCode, string entityType)
        {
            if (entityType == Constants.EntityType.Dealer)
            {
                return new AccountMaintenanceDAL().DeletePaymentTermsRelDealerSide(entityCode);
            }
            else
            {
                return new AccountMaintenanceDAL().DeletePaymentTermsRelFleetSide(entityCode);
            }
        }

        internal static bool DeletePaymentMethodRel(string entityCode, string entityType)
        {
            return new AccountMaintenanceDAL().DeletePaymentMethodRelFleetSide(entityCode);
        }
        internal static bool DeleteFinancialHandlingRel(string entityCode, string entityType)
        {
            return new AccountMaintenanceDAL().DeleteFinancialHandlingRelFleetSide(entityCode);
        }
        internal static EntityDetails GetZipAndState(string firstname)
        {
            return new AccountMaintenanceDAL().GetZipAndState(firstname);
        }
        internal static void UpdateEntityToNonCorcentricLocation(string dealerCode)
        {
            new AccountMaintenanceDAL().UpdateEntityToNonCorcentricLocation(dealerCode);
        }
        internal static void UpdateEntityToCorcentricLocation(string dealerCode)
        {
            new AccountMaintenanceDAL().UpdateEntityToCorcentricLocation(dealerCode);
        }

        internal static string GetDealerCodePaymentTerms()
        {
            return new AccountMaintenanceDAL().GetDealerCodePaymentTerms();
        }

        internal static string GetTaxIDValue(string corcentricCode)
        {
            return new AccountMaintenanceDAL().GetTaxIDValue(corcentricCode);
        }

        internal static string GetTaxClassificationIDValue(string corcentricCode)
        {
            return new AccountMaintenanceDAL().GetTaxClassificationIDValue(corcentricCode);
        }

        internal static EntityDetails GetOverrideTermDescriptionDetails(string dealerCode, string fleetCode)
        {
            return new AccountMaintenanceDAL().GetOverrideTermDescriptionDetails(dealerCode, fleetCode);
        }

        internal static bool IsPaymentTermActive(string dealerCorcentricCode)
        {
            return new AccountMaintenanceDAL().IsPaymentTermActive(dealerCorcentricCode);
        }

        //internal static bool ToggleTaxRelationshipFleetSide(string fleetCode, bool doActivate)
        //{
        //    return new AccountMaintainceDAL().ToggleTaxRelationshipFleetSide(fleetCode, doActivate);
        //}

        //internal static bool ToggleTaxRelationshipDealerSide(string dealerCode, bool doActivate)
        //{
        //    return new AccountMaintainceDAL().ToggleTaxRelationshipDealerSide(dealerCode, doActivate);
        //}

        internal static bool DeleteTaxRelationship(string corcentricCode)
        {
            return new AccountMaintenanceDAL().DeleteTaxRelationship(corcentricCode);
        }

        internal static string GetNumberOfYears(string dealerCode)
        {
            return new AccountMaintenanceDAL().GetNumberOfYears(dealerCode);
        }

        internal static bool DeleteEnhancedDuplicateCheckRelationship(string corcentricCode)
        {
            return new AccountMaintenanceDAL().DeleteEnhancedDuplicateCheckRelationship(corcentricCode);
        }

        internal static bool DeleteCorePriceTypeRelationship(string corcentricCode)
        {
            return new AccountMaintenanceDAL().DeleteCorePriceTypeRelationship(corcentricCode);
        }
        internal static bool IsEntityOnlinePay(string corcentricCode)
        {
            return new AccountMaintenanceDAL().IsEntityOnlinePay(corcentricCode);
        }
    }
}

